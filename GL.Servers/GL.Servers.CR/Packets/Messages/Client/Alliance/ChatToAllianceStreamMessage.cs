namespace GL.Servers.CR.Packets.Messages.Client.Alliance
{
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;

    internal class ChatToAllianceStreamMessage : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14308;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Alliance;
            }
        }

        public string Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatToAllianceStreamMessage"/> class.
        /// </summary>
        public ChatToAllianceStreamMessage()
        {
            // ChatToAllianceStreamMessage.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatToAllianceStreamMessage"/> class.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        public ChatToAllianceStreamMessage(ByteStream Stream) : base(Stream)
        {
            // ChatToAllianceStreamMessage.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Message = this.Stream.ReadString();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Stream.AddString(this.Message);
        }
    }
}