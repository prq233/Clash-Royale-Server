﻿namespace GL.Servers.CR.Core.Network
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Packets;

    internal class TCPServer
    {
        internal SocketAsyncEventArgsPool ReadPool;
        internal SocketAsyncEventArgsPool WritePool;

        internal Socket Listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="TCPServer"/> class.
        /// </summary>
        internal TCPServer()
        {
            this.ReadPool               = new SocketAsyncEventArgsPool(Constants.MaxPlayers);
            this.WritePool              = new SocketAsyncEventArgsPool(Constants.MaxSends);

            this.Initialize();
            
            this.Listener               = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.Listener.ReceiveBufferSize = Constants.ReceiveBuffer;
            this.Listener.SendBufferSize    = Constants.SendBuffer;

            this.Listener.Blocking      = false;
            this.Listener.NoDelay       = true;

            this.Listener.Bind(new IPEndPoint(IPAddress.Any, 9339));
            this.Listener.Listen(300);

            Console.WriteLine("TCP Server is listening on " + this.Listener.LocalEndPoint + ".");

            SocketAsyncEventArgs AcceptEvent = new SocketAsyncEventArgs();
            AcceptEvent.Completed           += this.OnAcceptCompleted;

            this.StartAccept(AcceptEvent);
        }

        /// <summary>
        /// Initializes the read and write pools.
        /// </summary>
        internal void Initialize()
        {
            for (int Index = 0; Index < Constants.MaxPlayers; Index++)
            {
                SocketAsyncEventArgs ReadEvent      = new SocketAsyncEventArgs();

                ReadEvent.SetBuffer(new byte[Constants.ReceiveBuffer], 0, Constants.ReceiveBuffer);

                ReadEvent.Completed                += this.OnReceiveCompleted;
                ReadEvent.DisconnectReuseSocket     = true;

                this.ReadPool.Enqueue(ReadEvent);
            }

            Logging.Info(this.GetType(), "Initialized ReadPool with " + Constants.MaxPlayers + " AsyncEvents.");

            for (int Index = 0; Index < Constants.MaxSends; Index++)
            {
                SocketAsyncEventArgs WriteEvent     = new SocketAsyncEventArgs();

                WriteEvent.Completed               += this.OnSendCompleted;
                WriteEvent.DisconnectReuseSocket    = true;

                this.WritePool.Enqueue(WriteEvent);
            }

            Logging.Info(this.GetType(), "Initialized WritePool with " + Constants.MaxSends + " AsyncEvents.");
        }

        /// <summary>
        /// Accepts a TCP Request.
        /// </summary>
        /// <param name="AcceptEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void StartAccept(SocketAsyncEventArgs AcceptEvent)
        {
            AcceptEvent.AcceptSocket    = null;
            AcceptEvent.RemoteEndPoint  = null;

            if (!this.Listener.AcceptAsync(AcceptEvent))
            {
                this.OnAcceptCompleted(null, AcceptEvent);
            }
        }

        /// <summary>
        /// Called when the client has been accepted.
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="AsyncEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void OnAcceptCompleted(object Sender, SocketAsyncEventArgs AsyncEvent)
        {
            if (AsyncEvent.SocketError == SocketError.Success)
            {
                this.ProcessAccept(AsyncEvent);
            }
            else
            {
                AsyncEvent.AcceptSocket.Close();

                Logging.Error(this.GetType(), AsyncEvent.SocketError + " exception happened when accepting a new connection, aborting.");

                this.StartAccept(AsyncEvent);
            }
        }

        /// <summary>
        /// Accept the new client and store it in memory.
        /// </summary>
        /// <param name="AsyncEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void ProcessAccept(SocketAsyncEventArgs AsyncEvent)
        {
            if (AsyncEvent != null)
            {
                Socket Socket = AsyncEvent.AcceptSocket;

                if (Socket.Connected)
                {
                    SocketAsyncEventArgs ReadEvent = this.ReadPool.Dequeue();

                    // Logging.Info(this.GetType(), "Connection from " + Socket.RemoteEndPoint + ".");

                    if (ReadEvent == null)
                    {
                        ReadEvent = new SocketAsyncEventArgs();

                        ReadEvent.SetBuffer(new byte[Constants.ReceiveBuffer], 0, Constants.ReceiveBuffer);
                        ReadEvent.Completed += this.OnReceiveCompleted;

                        ReadEvent.DisconnectReuseSocket = false;
                    }

                    Device Device   = new Device(Socket);
                    Token Token     = new Token(ReadEvent, Device);

                    if (!Socket.ReceiveAsync(ReadEvent))
                    {
                        this.ProcessReceive(ReadEvent);
                    }
                }
                else
                {
                    Socket.Close();
                }
            }
            else
            {
                Logging.Error(this.GetType(), "AsyncEvent was null at ProcessAccept(Saea).");
            }

            this.StartAccept(AsyncEvent);
        }

        /// <summary>
        /// Receives data from the specified client.
        /// </summary>
        /// <param name="AsyncEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void ProcessReceive(SocketAsyncEventArgs AsyncEvent)
        {
            if (AsyncEvent.BytesTransferred > 0)
            {
                if (AsyncEvent.SocketError == SocketError.Success)
                {
                    Token Token = AsyncEvent.UserToken as Token;

                    if (Token != null)
                    {
                        if (!Token.Aborting)
                        {
                            Token.SetData();

                            try
                            {
                                if (Token.Device.Socket.Available == 0)
                                {
                                    Token.Process();

                                    if (!Token.Aborting)
                                    {
                                        if (!Token.Device.Socket.ReceiveAsync(AsyncEvent))
                                        {
                                            this.ProcessReceive(AsyncEvent);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!Token.Device.Socket.ReceiveAsync(AsyncEvent))
                                    {
                                        this.ProcessReceive(AsyncEvent);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                this.Disconnect(AsyncEvent);
                            }
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Token was null at ProcessReceive(Saea).");
                    }
                }
                else
                {
                    this.Disconnect(AsyncEvent);
                }
            }
            else
            {
                this.Disconnect(AsyncEvent);
            }
        }

        /// <summary>
        /// Called when [receive completed].
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="AsyncEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void OnReceiveCompleted(object Sender, SocketAsyncEventArgs AsyncEvent)
        {
            if (AsyncEvent.SocketError == SocketError.Success)
            {
                this.ProcessReceive(AsyncEvent);
            }
            else
            {
                this.Disconnect(AsyncEvent);
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        internal void Send(Message Message)
        {
            if (Message != null)
            {
                if (Message.Device.Connected)
                {
                    SocketAsyncEventArgs WriteEvent = this.WritePool.Dequeue();

                    if (WriteEvent == null)
                    {
                        WriteEvent = new SocketAsyncEventArgs
                        {
                            DisconnectReuseSocket = false
                        };
                    }

                    WriteEvent.SetBuffer(Message.ToBytes, Message.Offset, Message.Length + 7 - Message.Offset);

                    WriteEvent.AcceptSocket     = Message.Device.Socket;
                    WriteEvent.RemoteEndPoint   = Message.Device.Socket.RemoteEndPoint;
                    WriteEvent.UserToken        = Message.Device.Token;

                    if (!Message.Device.Socket.SendAsync(WriteEvent))
                    {
                        this.ProcessSend(Message, WriteEvent);
                    }
                }
                else
                {
                    this.Disconnect(Message.Device.Token.Args);
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Message was null at Send(Message).");
            }
        }

        /// <summary>
        /// Processes to send the specified message using the specified SocketAsyncEventArgs.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Args">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void ProcessSend(Message Message, SocketAsyncEventArgs Args)
        {
            if (Message != null)
            {
                if (Args.SocketError == SocketError.Success)
                {
                    Message.Offset += Args.BytesTransferred;

                    if (Message.Length + 7 > Message.Offset)
                    {
                        if (Message.Device.Connected)
                        {
                            Args.SetBuffer(Message.Offset, Message.Length + 7 - Message.Offset);

                            if (!Message.Device.Socket.SendAsync(Args))
                            {
                                this.ProcessSend(Message, Args);
                            }
                        }
                        else
                        {
                            this.OnSendCompleted(null, Args);
                            this.Disconnect(Message.Device.Token.Args);
                        }
                    }
                    else
                    {
                        this.OnSendCompleted(null, Args);
                    }
                }
                else
                {
                    this.OnSendCompleted(null, Args);
                    this.Disconnect(Message.Device.Token.Args);
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Message was null at ProcessSend(Message, Saea).");
            }
        }

        /// <summary>
        /// Called when [send completed].
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="AsyncEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void OnSendCompleted(object Sender, SocketAsyncEventArgs AsyncEvent)
        {
            if (AsyncEvent.DisconnectReuseSocket)
            {
                this.WritePool.Enqueue(AsyncEvent);
            }
            else
            {
                AsyncEvent.Dispose();
                AsyncEvent = null;
            }
        }

        /// <summary>
        /// Closes the specified client's socket.
        /// </summary>
        /// <param name="AsyncEvent">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        internal void Disconnect(SocketAsyncEventArgs AsyncEvent)
        {
            if (AsyncEvent != null)
            {
                Token Token = AsyncEvent.UserToken as Token;
                
                if (!Token.Aborting)
                {
                    Token.Aborting = true;

                    if (Token.Device != null)
                    {
                        if (Token.Device.GameMode != null)
                        {
                            if (Token.Device.GameMode.Player != null)
                            {
                                Resources.Players.Remove(Token.Device.GameMode.Player);
                            }
                        }

                        Token.Device.State = Servers.Logic.Enums.State.DISCONNECTED;

                        try
                        {
                            Token.Device.Socket.Shutdown(SocketShutdown.Both);
                        }
                        catch (Exception)
                        {
                            // Already Closed.
                        }

                        Token.Device.Socket.Close();
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Token.Device was null at Disconnect(Saea).");
                    }

                    if (AsyncEvent.DisconnectReuseSocket)
                    {
                        this.ReadPool.Enqueue(AsyncEvent);
                    }
                    else
                    {
                        AsyncEvent.Dispose();
                        AsyncEvent = null;
                    }
                }
                else
                {
                    // Logging.Error(this.GetType(), "Token.Aborting was already set to true at Disconnect(Saea).");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "AsyncEvent was null at Disconnect(Saea);");
            }
        }
    }
}
