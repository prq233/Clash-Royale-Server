namespace GL.Servers.CR.Logic.Entries
{
    using Newtonsoft.Json;
    using GL.Servers.DataStream;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Client;
    using GL.Servers.CR.Files.Logic;
    public class AllianceHeaderEntry
    {
        [JsonProperty("highId")] public int HighId;
        [JsonProperty("lowId")] public int LowId;

        [JsonProperty("name")] public string Name;

        [JsonProperty("type")] public int Type;
        [JsonProperty("score")] public int Score;
        [JsonProperty("requiredScore")] public int RequiredScore;
        [JsonProperty("members")] public int MembersCount;
        [JsonProperty("donations")] public int Donations;

        [JsonProperty("region")] internal RegionData Region;
        [JsonProperty("locale")] internal LocaleData Locale;
        [JsonProperty("badge")] internal AllianceBadgeData Badge;

        public long ClanId
        {
            get
            {
                return (long)(uint)this.HighId << 32 | (uint)this.LowId;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceHeaderEntry"/> class.
        /// </summary>
        public AllianceHeaderEntry()
        {
            // AllianceHeaderEntry.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceHeaderEntry"/> class.
        /// </summary>
        public AllianceHeaderEntry(int HighId, int LowId)
        {
            this.HighId = HighId;
            this.LowId = LowId;
        }

        /// <summary>
        /// Sets the alliance.
        /// </summary>
        /// <param name="HighId">The high identifier.</param>
        /// <param name="LowId">The low identifier.</param>
        /// <param name="MembersCount">The members count.</param>
        public void SetAlliance(int HighId, int LowId, int MembersCount)
        {
            this.HighId = HighId;
            this.LowId = LowId;
            this.MembersCount = MembersCount;
        }

        /// <summary>
        /// Decodes from the specified stream.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        public void Decode(ByteStream Stream)
        {
            this.HighId = Stream.ReadInt();
            this.LowId = Stream.ReadInt();

            this.Name = Stream.ReadString();
            this.Badge = Stream.DecodeData<AllianceBadgeData>();

            this.Type = Stream.ReadVInt();
            this.MembersCount = Stream.ReadVInt();
            this.Score = Stream.ReadVInt();
            this.RequiredScore = Stream.ReadVInt();

            Stream.ReadVInt();
            Stream.ReadVInt();
            Stream.ReadVInt();
            Stream.ReadVInt();

            this.Donations = Stream.ReadVInt();

            Stream.ReadVInt();

            this.Locale = Stream.DecodeData<LocaleData>();
            this.Region = Stream.DecodeData<RegionData>();

            bool Event = Stream.ReadBoolean();

            if (Event)
            {
                // TODO : Decode the clan event.
            }
        }

        /// <summary>
        /// Encodes in the specified stream.
        /// </summary>
        /// <param name="Stream">The stream.</param>
        public void Encode(ChecksumEncoder Stream)
        {
            Stream.AddInt(this.HighId);
            Stream.AddInt(this.LowId);
            Stream.AddString(this.Name);
            Stream.EncodeData(this.Badge);

            Stream.AddVInt(this.Type);
            Stream.AddVInt(this.MembersCount);
            Stream.AddVInt(this.Score);
            Stream.AddVInt(this.RequiredScore);

            Stream.AddVInt(0);
            Stream.AddVInt(0);
            Stream.AddVInt(0);
            Stream.AddVInt(50);
            Stream.AddVInt(this.Donations);
            Stream.AddVInt(2);

            Stream.EncodeData(this.Locale);
            Stream.EncodeData(this.Region);

            Stream.AddBoolean(false);

            if (false)
            {
                // TODO : Encode the clan event.
            }
        }
    }
}