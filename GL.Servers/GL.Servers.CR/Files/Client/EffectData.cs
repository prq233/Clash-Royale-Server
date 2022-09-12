namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class EffectData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EffectData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EffectData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // EffectData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal bool Loop
        {
            get; set;
        }

        internal bool FollowParent
        {
            get; set;
        }

        internal int ShakeScreen
        {
            get; set;
        }

        internal int Time
        {
            get; set;
        }

        internal int RenderableScale
        {
            get; set;
        }

        internal string Sound
        {
            get; set;
        }

        internal string Type
        {
            get; set;
        }

        internal string FileName
        {
            get; set;
        }

        internal string ExportName
        {
            get; set;
        }

        internal string ParticleEmitterName
        {
            get; set;
        }

        internal string Effect
        {
            get; set;
        }

        internal string Layer
        {
            get; set;
        }

        internal int Scale
        {
            get; set;
        }

        internal string TextInstanceName
        {
            get; set;
        }

        internal string TextParentInstanceName
        {
            get; set;
        }

        internal string EnemyVersion
        {
            get; set;
        }

        internal int FlashWidth
        {
            get; set;
        }

        internal bool KillLoopingSoundsOnEnd
        {
            get; set;
        }

        internal string OutputEvent
        {
            get; set;
        }

        internal int ParentLookAtOffsetRadius
        {
            get; set;
        }

        internal bool Shadow
        {
            get; set;
        }

    }
}