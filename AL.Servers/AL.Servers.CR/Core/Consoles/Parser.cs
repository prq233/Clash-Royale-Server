﻿namespace GL.Servers.CR.Core.Consoles
{
    using System;
    using System.Linq;
    using System.Threading;

    using GL.Servers.CR.Core.Events;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Commands;
    using GL.Servers.CR.Logic.Commands.Server;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Logic.Spells;
    using GL.Servers.CR.Packets.Messages.Server.Sector;
	
    using GL.Servers.Extensions;

    internal class Parser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class.
        /// </summary>
        internal static void Initialize()
        {
            new Thread(() =>
            {
                while (true)
                {
                    int CursorTop2 = Console.CursorTop = Console.WindowTop + Console.WindowHeight - 1;
                    Console.Write("root@localhost > ");

                    string Command = Console.ReadLine();

                    Console.SetCursorPosition(0, CursorTop2 - 1);
                    Console.WriteLine(new string(' ', Console.BufferWidth));
                    Console.SetCursorPosition(0, CursorTop2 - 2);

                    switch (Command)
                    {
                        case "/stats":
                        {
                            if (Resources.Started)
                            {
                                Console.WriteLine();
                                Console.WriteLine("# " + DateTime.Now.ToString("d") + " ---- STATS ---- " + DateTime.Now.ToString("T") + " #");
                                Console.WriteLine("# ----------------------------------- #");
                                Console.WriteLine("# In-Memory Players # " + ConsolePad.Padding(Resources.Players.Count.ToString(), 15) + " #");
                                Console.WriteLine("# In-Memory Clans   # " + ConsolePad.Padding(Resources.Clans.Count.ToString(), 15) + " #");
                                Console.WriteLine("# In-Memory Saea    # " + ConsolePad.Padding(Resources.TCPGateway.ReadPool.Pool.Count + " - " + Resources.TCPGateway.WritePool.Pool.Count, 15) + " #");
                                Console.WriteLine("# ----------------------------------- #");
                            }

                            break;
                        }

                        case "/test":
                        {
                            if (Resources.Started)
                            {
                                foreach (Player Player in Resources.Players.Values.ToList())
                                {
									new Sector_State_Message(Player.GameMode.Device, Player.GameMode).Send();
                                }
                            }

                            break;
                        }

                        case "/clear":
                        {
                            Console.Clear();
                            break;
                        }

                        case "/exit":
                        case "/shutdown":
                        case "/stop":
                        {
                            EventsHandler.Process();
                            break;
                        }

                        case "/debug":
                        {
                            string[] Names = LogicDebug.GetListOfCommands();

                            for (int i = 0; i < Names.Length; i++)
                            {
                                Console.WriteLine("[DEBUG] Logic : " + ConsolePad.Padding(Names[i]));
                            }

                            break;
                        }

                        default:
                        {
                            LogicDebug.Execute(Command, Resources.Players.Values.ToArray());
                            Console.WriteLine();
                            break;
                        }
                    }
                }
            }).Start();
        }
    }
}