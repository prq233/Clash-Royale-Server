namespace GL.Servers.CR.Packets.Messages.Client.Alliance
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.CR.Logic.Enums;

    internal class CreateAllianceMessage : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14301;
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

        public string Name;
        public string Description;

        public int AccessType;
        public int RequiredScore;

        public RegionData RegionData;
        public AllianceBadgeData BadgeData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAllianceMessage"/> class.
        /// </summary>
        internal CreateAllianceMessage()
        {
            // CreateAllianceMessage.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAllianceMessage"/> class.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        internal CreateAllianceMessage(ByteStream Stream) : base(Stream)
        {
            // CreateAllianceMessage.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Name           = this.Stream.ReadString();
            this.Description    = this.Stream.ReadString();
            this.BadgeData      = this.Stream.DecodeData<AllianceBadgeData>();
            this.AccessType     = this.Stream.ReadVInt();
            this.RequiredScore  = this.Stream.ReadVInt();
            this.RegionData     = this.Stream.DecodeData<RegionData>();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Stream.AddString(this.Name);
            this.Stream.AddString(this.Description);
            this.Stream.EncodeData(this.BadgeData);
            this.Stream.AddVInt(this.AccessType);
            this.Stream.AddVInt(this.RequiredScore);
            this.Stream.EncodeData(this.RegionData);
        }
    }
}