namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class SoundData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="SoundData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SoundData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // SoundData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string FileNames
        {
            get; set;
        }

        internal int MinVolume
        {
            get; set;
        }

        internal int MaxVolume
        {
            get; set;
        }

        internal int MinPitch
        {
            get; set;
        }

        internal int MaxPitch
        {
            get; set;
        }

        internal int Priority
        {
            get; set;
        }

        internal int MaximumByType
        {
            get; set;
        }

        internal int MaxRepeatMs
        {
            get; set;
        }

        internal bool Loop
        {
            get; set;
        }

        internal bool PlayVariationsInSequence
        {
            get; set;
        }

        internal bool PlayVariationsInSequenceManualReset
        {
            get; set;
        }

        internal int StartDelayMinMs
        {
            get; set;
        }

        internal int StartDelayMaxMs
        {
            get; set;
        }

        internal bool PlayOnlyWhenInView
        {
            get; set;
        }

        internal int MaxVolumeScaleLimit
        {
            get; set;
        }

        internal int NoSoundScaleLimit
        {
            get; set;
        }

        internal int PadEmpyToEndMs
        {
            get; set;
        }

    }
}