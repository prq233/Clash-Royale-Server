namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class DecoData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DecoData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DecoData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // DecoData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string FileName
        {
            get; set;
        }

        internal string ExportName
        {
            get; set;
        }

        internal string Layer
        {
            get; set;
        }

        internal string LowendLayer
        {
            get; set;
        }

        internal int ShadowScaleX
        {
            get; set;
        }

        internal int ShadowScaleY
        {
            get; set;
        }

        internal int ShadowX
        {
            get; set;
        }

        internal int ShadowY
        {
            get; set;
        }

        internal int ShadowSkew
        {
            get; set;
        }

        internal int CollisionRadius
        {
            get; set;
        }

        internal string Effect
        {
            get; set;
        }

        internal string AssetMinTrophy
        {
            get; set;
        }

        internal int AssetMinTrophyScore
        {
            get; set;
        }

        internal string AssetMinTrophyFileName
        {
            get; set;
        }

        internal int SortValue
        {
            get; set;
        }

        internal bool Audience
        {
            get; set;
        }

        internal string CheerFileName
        {
            get; set;
        }

        internal string CheerExportName
        {
            get; set;
        }

        internal string ClassType
        {
            get; set;
        }

    }
}