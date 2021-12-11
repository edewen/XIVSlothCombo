﻿namespace XIVComboExpandedPlugin.Combos
{
    internal static class RPR
    {
        public const byte JobID = 39;

        public const uint
            // Single Target
            Slice = 24373,
            WaxingSlice = 24374,
            InfernalSlice = 24375,
            // AoE
            SpinningScythe = 24376,
            NightmareScythe = 24377,
            WhorlOfDeath = 24379,
            // Soul Reaver
            BloodStalk = 24389,
            Gibbet = 24382,
            Gallows = 24383,
            Guillotine = 24384,
            // Sacrifice
            ArcaneCircle = 24405,
            PlentifulHarvest = 24385,
            // Shroud
            Enshroud = 24394,
            Communio = 24398,
            LemuresSlice = 24399,
            LemuresScythe = 24400,
            // Misc
            ShadowOfDeath = 24378,
            HellsIngress = 24401,
            HellsEgress = 24402,
            Regress = 24403;

        public static class Buffs
        {
            public const short
                SoulReaver = 2587,
                ImmortalSacrifice = 2592,
                EnhancedGibbet = 2588,
                EnhancedGallows = 2589,
                EnhancedVoidReaping = 2590,
                EnhancedCrossReaping = 2591,
                Enshrouded = 2593,
                Threshold = 2595;
        }

        public static class Debuffs
        {
            public const short
                DeathsDesign = 2586;
        }

        public static class Levels
        {
            public const byte
                WaxingSlice = 5,
                HellsIngress = 20,
                HellsEgress = 20,
                SpinningScythe = 25,
                InfernalSlice = 30,
                NightmareScythe = 45,
                SoulReaver = 70,
                Regress = 74,
                Enshroud = 80,
                PlentifulHarvest = 88,
                Communio = 90;
        }
    }

    internal class ReaperSliceCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperSliceCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.Slice)
            {
                if (comboTime > 0)
                {
                    if (lastComboMove == RPR.Slice && level >= RPR.Levels.WaxingSlice)
                        return RPR.WaxingSlice;
                    if (lastComboMove == RPR.WaxingSlice && level >= RPR.Levels.InfernalSlice)
                        return RPR.InfernalSlice;
                }
                if (IsEnabled(CustomComboPreset.ReaperWhorlOfDeathFeature))
                {
                    var deathsDesign = TargetHasEffect(RPR.Debuffs.DeathsDesign);
                    var deathsDesignTimer = FindTargetEffect(RPR.Debuffs.DeathsDesign);
                    var soulReaverBuff = HasEffectAny(RPR.Buffs.SoulReaver);

                    if ((!deathsDesign && !soulReaverBuff) || deathsDesignTimer.RemainingTime < 15 && !soulReaverBuff)
                        return RPR.ShadowOfDeath;

                }
                return RPR.Slice;
            }

            return actionID;
        }
    }

    internal class ReaperScytheCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperScytheCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.SpinningScythe)
            {
                if (comboTime > 0)
                {
                    if (lastComboMove == RPR.SpinningScythe && level >= RPR.Levels.NightmareScythe)
                        return RPR.NightmareScythe;
                }
                if(IsEnabled(CustomComboPreset.ReaperWhorlOfDeathFeature))
                {
                    var deathsDesign = TargetHasEffect(RPR.Debuffs.DeathsDesign);
                    var deathsDesignTimer = FindTargetEffect(RPR.Debuffs.DeathsDesign);
                    var soulReaverBuff = HasEffectAny(RPR.Buffs.SoulReaver);

                    if ((!deathsDesign && !soulReaverBuff) || deathsDesignTimer.RemainingTime < 15 && !soulReaverBuff)
                        return RPR.WhorlOfDeath;

                }
                return RPR.SpinningScythe;
            }

            return actionID;
        }
    }

    internal class EnshroudCommunioFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperEnshroudCommunioFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.Enshroud)
            {
                var enshroudBuff = HasEffect((short)RPR.Buffs.Enshrouded);
                if (level >= RPR.Levels.Communio && enshroudBuff)
                    return RPR.Communio;

                return RPR.Enshroud;
            }

            return actionID;
        }
    }
    internal class ReaperSoulReaverGallowsFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperSoulReaverGallowsFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.ShadowOfDeath)
            {
                if (level >= RPR.Levels.SoulReaver && (HasEffect(RPR.Buffs.SoulReaver) || HasEffect(RPR.Buffs.Enshrouded)))
                {
                    if (IsEnabled(CustomComboPreset.ReaperSoulReaverGallowsOption))
                        // Void Reaping
                        return OriginalHook(RPR.Gibbet);

                    // Cross Reaping
                    return OriginalHook(RPR.Gallows);
                }
            }

            return actionID;
        }
    }
    internal class ReaperHarvestFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperHarvestFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.ArcaneCircle)
            {
                if (level >= RPR.Levels.PlentifulHarvest && HasEffect(RPR.Buffs.ImmortalSacrifice))
                    return RPR.PlentifulHarvest;
            }
            return actionID;
        }
    }
    internal class ReaperEnshroudCommunioFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperEnshroudCommunioFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.Enshroud)
            {
                if (level >= RPR.Levels.Communio && HasEffect(RPR.Buffs.Enshrouded))
                    return RPR.Communio;
            }

            return actionID;
        }
    }
    internal class ReaperRegressFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperRegressFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.HellsEgress || actionID == RPR.HellsIngress)
            {
                if (level >= RPR.Levels.Regress && HasEffect(RPR.Buffs.Threshold))
                    return RPR.Regress;
            }

            return actionID;
        }
    }

}