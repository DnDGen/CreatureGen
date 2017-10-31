﻿using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces.Species
{
    [TestFixture]
    public class GreenFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Templates.Species.Green); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.NaturalWeapon,
                FeatConstants.ImmuneToEffect
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.NaturalWeapon,
            FeatConstants.NaturalWeapon,
            "Breath (30 ft cone of acid gas) (see Half-Dragon monster listing)",
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Acid,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
