﻿using CreatureGen.Combats;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class DeathSlaadFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Slaad_Death); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.NaturalArmor,
                FeatConstants.Darkvision,
                FeatConstants.Stun,
                FeatConstants.SpellLikeAbility + SpellConstants.AnimateObjects,
                FeatConstants.SpellLikeAbility + SpellConstants.ChaosHammer,
                FeatConstants.SpellLikeAbility + SpellConstants.DeeperDarkness,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectMagic,
                FeatConstants.SpellLikeAbility + SpellConstants.DispelAlignment,
                FeatConstants.SpellLikeAbility + SpellConstants.Fear,
                FeatConstants.SpellLikeAbility + SpellConstants.FingerOfDeath,
                FeatConstants.SpellLikeAbility + SpellConstants.Fireball,
                FeatConstants.SpellLikeAbility + SpellConstants.Fly,
                FeatConstants.SpellLikeAbility + SpellConstants.Identify,
                FeatConstants.SpellLikeAbility + SpellConstants.Invisibility,
                FeatConstants.SpellLikeAbility + SpellConstants.MagicCircleAgainstAlignment,
                FeatConstants.SpellLikeAbility + SpellConstants.SeeInvisibility,
                FeatConstants.SpellLikeAbility + SpellConstants.Shatter,
                FeatConstants.SpellLikeAbility + SpellConstants.CircleOfDeath,
                FeatConstants.SpellLikeAbility + SpellConstants.CloakOfChaos,
                FeatConstants.SpellLikeAbility + SpellConstants.WordOfChaos,
                FeatConstants.SpellLikeAbility + SpellConstants.Implosion,
                FeatConstants.SpellLikeAbility + SpellConstants.PowerWordBlind,
                FeatConstants.ChangeShape,
                FeatConstants.DamageReduction,
                FeatConstants.SummonSlaad,
                FeatConstants.FastHealing,
                FeatConstants.ImmuneToEffect,
                FeatConstants.Telepathy,
                FeatConstants.Resistance + FeatConstants.Foci.Acid,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.Resistance + FeatConstants.Foci.Fire,
                FeatConstants.SkillBonus + SkillConstants.Survival,
                FeatConstants.SkillBonus + SkillConstants.UseRope,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            9,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            9,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            9,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            12,
            0, 0)]
        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        [TestCase(FeatConstants.Telepathy,
            FeatConstants.Telepathy,
            "",
            0,
            "",
            0,
            "",
            100,
            0, 0)]
        [TestCase(FeatConstants.Stun,
            FeatConstants.Stun,
            "",
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.AnimateObjects,
            FeatConstants.SpellLikeAbility,
            SpellConstants.AnimateObjects,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.ChaosHammer,
            FeatConstants.SpellLikeAbility,
            SpellConstants.ChaosHammer,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DeeperDarkness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DeeperDarkness,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectMagic,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectMagic,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DispelAlignment,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DispelAlignment,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Fear,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Fear,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.FingerOfDeath,
            FeatConstants.SpellLikeAbility,
            SpellConstants.FingerOfDeath,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Fireball,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Fireball,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Fly,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Fly,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Identify,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Identify,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Invisibility,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Invisibility,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.MagicCircleAgainstAlignment,
            FeatConstants.SpellLikeAbility,
            SpellConstants.MagicCircleAgainstAlignment,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SeeInvisibility,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SeeInvisibility,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Shatter,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Shatter,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.CircleOfDeath,
            FeatConstants.SpellLikeAbility,
            SpellConstants.CircleOfDeath,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.CloakOfChaos,
            FeatConstants.SpellLikeAbility,
            SpellConstants.CloakOfChaos,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.WordOfChaos,
            FeatConstants.SpellLikeAbility,
            SpellConstants.WordOfChaos,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Implosion,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Implosion,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.PowerWordBlind,
            FeatConstants.SpellLikeAbility,
            SpellConstants.PowerWordBlind,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ChangeShape,
            FeatConstants.ChangeShape,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.DamageReduction,
            FeatConstants.DamageReduction,
            "Must be lawful to overcome",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.SummonSlaad,
            FeatConstants.SummonSlaad,
            "",
            2,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.FastHealing,
            FeatConstants.FastHealing,
            "",
            1,
            FeatConstants.Frequencies.Turn,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Sonic,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Acid,
            FeatConstants.Resistance,
            FeatConstants.Foci.Acid,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Cold,
            FeatConstants.Resistance,
            FeatConstants.Foci.Cold,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Electricity,
            FeatConstants.Resistance,
            FeatConstants.Foci.Electricity,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Fire,
            FeatConstants.Resistance,
            FeatConstants.Foci.Fire,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Survival,
            FeatConstants.SkillBonus,
            SkillConstants.Survival + " (when tracking)",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.UseRope,
            FeatConstants.SkillBonus,
            SkillConstants.UseRope + " (when using to bind someone or something)",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
