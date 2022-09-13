namespace GL.Servers.CR.Packets.Messages.Client.Alliance
{
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;

    internal class JoinAllianceMessage : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14305;
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
        /// Initializes a new instance of the <see cref="JoinAllianceMessage"/> class.
        /// </summary>
        public JoinAllianceMessage()
        {
            // JoinAllianceMessage.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinAllianceMessage"/> class.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        public JoinAllianceMessage(ByteStream Stream) : base(Stream)
        {
            // JoinAllianceMessage.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighId = this.Stream.ReadInt();
            this.LowId  = this.Stream.ReadInt();
            this.Stream.ReadInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Stream.AddInt(this.HighId);
            this.Stream.AddInt(this.LowId);
            this.Stream.AddInt(0);
        }
    }
}