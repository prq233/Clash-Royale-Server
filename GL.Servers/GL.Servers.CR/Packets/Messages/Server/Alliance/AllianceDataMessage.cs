namespace GL.Servers.CR.Packets.Messages.Server.Alliance
{
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;
    using GL.Servers.CR.Logic.Entries;

    internal class AllianceDataMessage : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 26550;
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

        public AllianceFullEntry AllianceFullEntry;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceDataMessage"/> class.
        /// </summary>
        public AllianceDataMessage()
        {
            // AllianceDataMessage.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceDataMessage"/> class.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        public AllianceDataMessage(ByteStream Stream) : base(Stream)
        {
            // AllianceDataMessage.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceDataMessage"/> class.
        /// </summary>
        /// <param name="AllianceFullEntry">The alliance full entry.</param>
        public AllianceDataMessage(AllianceFullEntry AllianceFullEntry)
        {
            this.AllianceFullEntry = AllianceFullEntry;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.AllianceFullEntry.Decode(this.Stream);
        }

        /// <summary>
        /// Encodes this instance;
        /// </summary>
        internal override void Encode()
        {
            this.AllianceFullEntry.Encode(this.Stream);
        }
    }
}