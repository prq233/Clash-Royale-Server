namespace GL.Servers.CR.Packets.Messages.Client.Alliance
{
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;

    internal class AskForAllianceDataMessage : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14302;
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

        public int HighId;
        public int LowId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AskForAllianceDataMessage"/> class.
        /// </summary>
        public AskForAllianceDataMessage()
        {
            // AskForAllianceDataMessage.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AskForAllianceDataMessage"/> class.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        public AskForAllianceDataMessage(ByteStream Stream) : base(Stream)
        {
            // AskForAllianceDataMessage.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighId = this.Stream.ReadInt();
            this.LowId  = this.Stream.ReadInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Stream.AddInt(this.HighId);
            this.Stream.AddInt(this.LowId);
        }
    }
}