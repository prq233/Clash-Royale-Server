namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class HeroData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HeroData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HeroData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // HeroData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string Rarity
        {
            get; set;
        }

        internal int SightRange
        {
            get; set;
        }

        internal int DeployTime
        {
            get; set;
        }

        internal int ChargeRange
        {
            get; set;
        }

        internal int Speed
        {
            get; set;
        }

        internal int Hitpoints
        {
            get; set;
        }

        internal int HitSpeed
        {
            get; set;
        }

        internal int LoadTime
        {
            get; set;
        }

        internal int Damage
        {
            get; set;
        }

        internal int DamageSpecial
        {
            get; set;
        }

        internal int CrownTowerDamagePercent
        {
            get; set;
        }

        internal bool LoadFirstHit
        {
            get; set;
        }

        internal int StopTimeAfterAttack
        {
            get; set;
        }

        internal int StopTimeAfterSpecialAttack
        {
            get; set;
        }

        internal string Projectile
        {
            get; set;
        }

        internal string CustomFirstProjectile
        {
            get; set;
        }

        internal int MultipleProjectiles
        {
            get; set;
        }

        internal int MultipleTargets
        {
            get; set;
        }

        internal bool AllTargetsHit
        {
            get; set;
        }

        internal int Range
        {
            get; set;
        }

        internal int MinimumRange
        {
            get; set;
        }

        internal bool AttacksGround
        {
            get; set;
        }

        internal bool AttacksAir
        {
            get; set;
        }

        internal int DeathDamageRadius
        {
            get; set;
        }

        internal int DeathDamage
        {
            get; set;
        }

        internal int DeathPushBack
        {
            get; set;
        }

        internal int AttackPushBack
        {
            get; set;
        }

        internal bool PushbackStaticDir
        {
            get; set;
        }

        internal int ReloadAfterHits
        {
            get; set;
        }

        internal int ReloadTime
        {
            get; set;
        }

        internal int LifeTime
        {
            get; set;
        }

        internal string ProjectileSpecial
        {
            get; set;
        }

        internal string ProjectileEffect
        {
            get; set;
        }

        internal string ProjectileEffectSpecial
        {
            get; set;
        }

        internal int AreaDamageRadius
        {
            get; set;
        }

        internal bool TargetOnlyBuildings
        {
            get; set;
        }

        internal int SpecialAttackInterval
        {
            get; set;
        }

        internal int OpponentCardHealthReduction
        {
            get; set;
        }

        internal int OwnCardHealthReduction
        {
            get; set;
        }

        internal string BuffOnDamage
        {
            get; set;
        }

        internal int BuffOnDamageTime
        {
            get; set;
        }

        internal bool IgnoreTargetIfImmuneToBuff
        {
            get; set;
        }

        internal string StartingBuff
        {
            get; set;
        }

        internal int StartingBuffTime
        {
            get; set;
        }

        internal string FileName
        {
            get; set;
        }

        internal string BlueExportName
        {
            get; set;
        }

        internal string BlueTopExportName
        {
            get; set;
        }

        internal string RedExportName
        {
            get; set;
        }

        internal string RedTopExportName
        {
            get; set;
        }

        internal bool UseAnimator
        {
            get; set;
        }

        internal string AttachedCharacter
        {
            get; set;
        }

        internal int AttachedCharacterHeight
        {
            get; set;
        }

        internal string DamageEffect
        {
            get; set;
        }

        internal string DamageEffectSpecial
        {
            get; set;
        }

        internal string DeathEffect
        {
            get; set;
        }

        internal string MoveEffect
        {
            get; set;
        }

        internal string SpawnEffect
        {
            get; set;
        }

        internal bool CrowdEffects
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

        internal int Pushback
        {
            get; set;
        }

        internal bool IgnorePushback
        {
            get; set;
        }

        internal int Scale
        {
            get; set;
        }

        internal int CollisionRadius
        {
            get; set;
        }

        internal int Mass
        {
            get; set;
        }

        internal int TileSizeOverride
        {
            get; set;
        }

        internal string AreaBuff
        {
            get; set;
        }

        internal int AreaBuffTime
        {
            get; set;
        }

        internal int AreaBuffRadius
        {
            get; set;
        }

        internal bool AreaBuffOwnTroops
        {
            get; set;
        }

        internal bool AreaBuffEnemies
        {
            get; set;
        }

        internal int Gold
        {
            get; set;
        }

        internal int ManaOnDeath
        {
            get; set;
        }

        internal string HealthBar
        {
            get; set;
        }

        internal int HealthBarOffsetY
        {
            get; set;
        }

        internal bool ShowHealthNumber
        {
            get; set;
        }

        internal int FlyingHeight
        {
            get; set;
        }

        internal bool FlyFromGround
        {
            get; set;
        }

        internal string DamageExportName
        {
            get; set;
        }

        internal int GrowTime
        {
            get; set;
        }

        internal int GrowSize
        {
            get; set;
        }

        internal string MorphCharacter
        {
            get; set;
        }

        internal string MorphEffect
        {
            get; set;
        }

        internal bool HealOnMorph
        {
            get; set;
        }

        internal string AreaEffectOnMorph
        {
            get; set;
        }

        internal string AttackStartEffect
        {
            get; set;
        }

        internal string AttackStartEffectSpecial
        {
            get; set;
        }

        internal string DashStartEffect
        {
            get; set;
        }

        internal string DashEffect
        {
            get; set;
        }

        internal int DashCooldown
        {
            get; set;
        }

        internal int JumpHeight
        {
            get; set;
        }

        internal int DashPushBack
        {
            get; set;
        }

        internal int DashRadius
        {
            get; set;
        }

        internal int DashDamage
        {
            get; set;
        }

        internal string LandingEffect
        {
            get; set;
        }

        internal int DashMinRange
        {
            get; set;
        }

        internal int DashMaxRange
        {
            get; set;
        }

        internal int JumpSpeed
        {
            get; set;
        }

        internal string ContinuousEffect
        {
            get; set;
        }

        internal int OpponentCardSpawn
        {
            get; set;
        }

        internal int OwnCardSpawn
        {
            get; set;
        }

        internal int SpawnStartTime
        {
            get; set;
        }

        internal int SpawnInterval
        {
            get; set;
        }

        internal int SpawnNumber
        {
            get; set;
        }

        internal int SpawnLimit
        {
            get; set;
        }

        internal int SpawnPauseTime
        {
            get; set;
        }

        internal int SpawnCharacterLevelIndex
        {
            get; set;
        }

        internal string SpawnCharacter
        {
            get; set;
        }

        internal string SpawnCharacterEffect
        {
            get; set;
        }

        internal string SpawnDeployBaseAnim
        {
            get; set;
        }

        internal int SpawnRadius
        {
            get; set;
        }

        internal int DeathSpawnCount
        {
            get; set;
        }

        internal string DeathSpawnCharacter
        {
            get; set;
        }

        internal int DeathSpawnRadius
        {
            get; set;
        }

        internal int DeathSpawnAngleShift
        {
            get; set;
        }

        internal int DeathSpawnDeployTime
        {
            get; set;
        }

        internal bool DeathSpawnPushback
        {
            get; set;
        }

        internal string DeathAreaEffect
        {
            get; set;
        }

        internal bool Kamikaze
        {
            get; set;
        }

        internal string KamikazeEffect
        {
            get; set;
        }

        internal int SpawnPathfindSpeed
        {
            get; set;
        }

        internal string SpawnPathfindEffect
        {
            get; set;
        }

        internal string SpawnPathfindMorph
        {
            get; set;
        }

        internal int SpawnPushback
        {
            get; set;
        }

        internal int SpawnPushbackRadius
        {
            get; set;
        }

        internal string SpawnAreaObject
        {
            get; set;
        }

        internal int SpawnAreaObjectLevelIndex
        {
            get; set;
        }

        internal string ChargeEffect
        {
            get; set;
        }

        internal string TakeDamageEffect
        {
            get; set;
        }

        internal int ProjectileStartRadius
        {
            get; set;
        }

        internal int ProjectileStartZ
        {
            get; set;
        }

        internal int StopMovementAfterMS
        {
            get; set;
        }

        internal int WaitMS
        {
            get; set;
        }

        internal bool DontStopMoveAnim
        {
            get; set;
        }

        internal bool IsSummonerTower
        {
            get; set;
        }

        internal int NoDeploySizeW
        {
            get; set;
        }

        internal int NoDeploySizeH
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal int VariableDamage2
        {
            get; set;
        }

        internal int VariableDamageTime1
        {
            get; set;
        }

        internal int VariableDamage3
        {
            get; set;
        }

        internal int VariableDamageTime2
        {
            get; set;
        }

        internal string TargettedDamageEffect1
        {
            get; set;
        }

        internal string TargettedDamageEffect2
        {
            get; set;
        }

        internal string TargettedDamageEffect3
        {
            get; set;
        }

        internal string DamageLevelTransitionEffect12
        {
            get; set;
        }

        internal string DamageLevelTransitionEffect23
        {
            get; set;
        }

        internal string FlameEffect1
        {
            get; set;
        }

        internal string FlameEffect2
        {
            get; set;
        }

        internal string FlameEffect3
        {
            get; set;
        }

        internal int TargetEffectY
        {
            get; set;
        }

        internal bool SelfAsAoeCenter
        {
            get; set;
        }

        internal bool HidesWhenNotAttacking
        {
            get; set;
        }

        internal int HideTimeMs
        {
            get; set;
        }

        internal bool HideBeforeFirstHit
        {
            get; set;
        }

        internal bool SpecialAttackWhenHidden
        {
            get; set;
        }

        internal string TargetedHitEffect
        {
            get; set;
        }

        internal string TargetedHitEffectSpecial
        {
            get; set;
        }

        internal int UpTimeMs
        {
            get; set;
        }

        internal string HideEffect
        {
            get; set;
        }

        internal string AppearEffect
        {
            get; set;
        }

        internal int AppearPushbackRadius
        {
            get; set;
        }

        internal int AppearPushback
        {
            get; set;
        }

        internal string AppearAreaObject
        {
            get; set;
        }

        internal int ManaCollectAmount
        {
            get; set;
        }

        internal int ManaGenerateTimeMs
        {
            get; set;
        }

        internal int ManaGenerateLimit
        {
            get; set;
        }

        internal bool HasRotationOnTimeline
        {
            get; set;
        }

        internal int TurretMovement
        {
            get; set;
        }

        internal int ProjectileYOffset
        {
            get; set;
        }

        internal int ChargeSpeedMultiplier
        {
            get; set;
        }

        internal int DeployDelay
        {
            get; set;
        }

        internal string DeployBaseAnimExportName
        {
            get; set;
        }

        internal bool JumpEnabled
        {
            get; set;
        }

        internal int SightClip
        {
            get; set;
        }

        internal string AreaEffectOnDash
        {
            get; set;
        }

        internal int SightClipSide
        {
            get; set;
        }

        internal int WalkingSpeedTweakPercentage
        {
            get; set;
        }

        internal int ShieldHitpoints
        {
            get; set;
        }

        internal int ShieldDiePushback
        {
            get; set;
        }

        internal string ShieldLostEffect
        {
            get; set;
        }

        internal string BlueShieldExportName
        {
            get; set;
        }

        internal string RedShieldExportName
        {
            get; set;
        }

        internal string LoadAttackEffect1
        {
            get; set;
        }

        internal string LoadAttackEffect2
        {
            get; set;
        }

        internal string LoadAttackEffect3
        {
            get; set;
        }

        internal string LoadAttackEffectReady
        {
            get; set;
        }

        internal int RotateAngleSpeed
        {
            get; set;
        }

        internal bool EnableAttackOnDamage
        {
            get; set;
        }

        internal int SecondaryHitDelay
        {
            get; set;
        }

        internal int DeployTimerDelay
        {
            get; set;
        }

        internal bool RetargetAfterAttack
        {
            get; set;
        }

        internal int AttackShakeTime
        {
            get; set;
        }

        internal int VisualHitSpeed
        {
            get; set;
        }

        internal string Ability
        {
            get; set;
        }

        internal int Burst
        {
            get; set;
        }

        internal int BurstDelay
        {
            get; set;
        }

        internal bool BurstKeepTarget
        {
            get; set;
        }

        internal int ActivationTime
        {
            get; set;
        }

    }
}