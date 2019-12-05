﻿using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Magic;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Helpers
{
    [TestFixture]
    public class SpecialQualityHelperTests
    {
        private SpecialQualityHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new SpecialQualityHelper();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BuildDataIntoArray_WithoutSave(bool equipment)
        {
            var data = helper.BuildData("feat name", "focus", 9266, "time period", 600, 1337, equipment);
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo("feat name"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo("focus"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(9266.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(600.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(1337.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(equipment.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(10));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BuildDataIntoArray_WithSave(bool equipment)
        {
            var data = helper.BuildData("feat name", "focus", 9266, "time period", 600, 1337, equipment, "save ability", "my save", 1336);
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo("feat name"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo("focus"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(9266.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo("time period"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(600.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(1337.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(equipment.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.EqualTo("save ability"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveIndex], Is.EqualTo("my save"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo(1336.ToString()));
            Assert.That(data.Length, Is.EqualTo(10));
        }

        [Test]
        public void BuildDataIntoArrayWithDefaults()
        {
            var data = helper.BuildData("feat name");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo("feat name"));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(false.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(10));
        }

        [Test]
        public void BuildDataIntoString()
        {
            var data = new[] { "this", "is", "my", "data" };
            var dataString = helper.BuildEntry(data);
            Assert.That(dataString, Is.EqualTo("this#is#my#data"));
        }

        [Test]
        public void BuildDataIntoStringWithDefaults()
        {
            var data = new[] { "this", "is", "my", "data", "with", "0", "defaults", string.Empty, string.Empty };
            var dataString = helper.BuildEntry(data);
            Assert.That(dataString, Is.EqualTo("this#is#my#data#with#0#defaults##"));
        }

        [Test]
        public void ParseData()
        {
            var data = helper.ParseEntry("this#is#my#data");
            Assert.That(data, Is.EqualTo(new[] { "this", "is", "my", "data" }));
        }

        [Test]
        public void ParseDataWithDefaults()
        {
            var data = helper.ParseEntry("this#is#my#data#with#0#defaults##");
            Assert.That(data, Is.EqualTo(new[] { "this", "is", "my", "data", "with", "0", "defaults", string.Empty, string.Empty }));
        }

        [Test]
        public void BuildRealData()
        {
            var data = helper.BuildData(
                FeatConstants.SpecialQualities.SpellLikeAbility,
                focus: SpellConstants.ClairaudienceClairvoyance,
                frequencyQuantity: 3,
                frequencyTimePeriod: FeatConstants.Frequencies.Day);
            var entry = helper.BuildEntry(data);
            Assert.That(entry, Is.EqualTo("Spell-Like Ability#0#Clairaudience/Clairvoyance#3#Day#0#False###0"));
        }

        [Test]
        public void BuildRealData_RequiringEquipment()
        {
            var data = helper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: GroupConstants.All, requiresEquipment: true);
            var entry = helper.BuildEntry(data);
            Assert.That(entry, Is.EqualTo("Simple Weapon Proficiency#0#All#0##0#True###0"));
        }

        [Test]
        public void BuildRealData_WithDefaults()
        {
            var data = helper.BuildData(FeatConstants.SpecialQualities.AllAroundVision);
            var entry = helper.BuildEntry(data);
            Assert.That(entry, Is.EqualTo("All-Around Vision#0##0##0#False###0"));
        }

        [Test]
        public void BuildRealData_WithSave()
        {
            var data = helper.BuildData(
                FeatConstants.SpecialQualities.SpellLikeAbility,
                focus: SpellConstants.Fireball,
                frequencyQuantity: 3,
                frequencyTimePeriod: FeatConstants.Frequencies.Day,
                saveAbility: AbilityConstants.Charisma,
                save: SaveConstants.Reflex,
                saveBaseValue: 13);
            var entry = helper.BuildEntry(data);
            Assert.That(entry, Is.EqualTo("Spell-Like Ability#0#Fireball#3#Day#0#False#Charisma#Reflex#13"));
        }

        [Test]
        public void ParseRealData()
        {
            var data = helper.ParseEntry("Spell-Like Ability#0#Clairaudience/Clairvoyance#3#Day#0#False###0");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo(FeatConstants.SpecialQualities.SpellLikeAbility));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo(SpellConstants.ClairaudienceClairvoyance));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(3.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Day));
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(false.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(10));
        }

        [Test]
        public void ParseRealData_RequiringEquipment()
        {
            var data = helper.ParseEntry("Simple Weapon Proficiency#0#All#0##0#True###0");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo(FeatConstants.WeaponProficiency_Simple));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo(GroupConstants.All));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(true.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(10));
        }

        [Test]
        public void ParseRealData_WithDefaults()
        {
            var data = helper.ParseEntry("All-Around Vision#0##0##0#False###0");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo(FeatConstants.SpecialQualities.AllAroundVision));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(false.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveIndex], Is.Empty);
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo(0.ToString()));
            Assert.That(data.Length, Is.EqualTo(10));
        }

        [Test]
        public void ParseRealData_WithSave()
        {
            var data = helper.ParseEntry("Spell-Like Ability#0#Fireball#3#Day#0#False#Charisma#Reflex#13");
            Assert.That(data[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo(FeatConstants.SpecialQualities.SpellLikeAbility));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo(SpellConstants.Fireball));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(3.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Day));
            Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo(0.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(false.ToString()));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.EqualTo(AbilityConstants.Charisma));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveIndex], Is.EqualTo(SaveConstants.Reflex));
            Assert.That(data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo(13.ToString()));
            Assert.That(data.Length, Is.EqualTo(10));
        }

        [Test]
        public void BuildKey_FromData_WithFocus()
        {
            var data = helper.ParseEntry("Spell-Like Ability#0#Clairaudience/Clairvoyance#3#Day#0#False###0");
            var key = helper.BuildKey("creature", data);
            Assert.That(key, Is.EqualTo("creatureSpell-Like AbilityClairaudience/Clairvoyance"));
        }

        [Test]
        public void BuildKey_FromData_WithoutFocus()
        {
            var data = helper.ParseEntry("All-Around Vision#0##0##0#False###0");
            var key = helper.BuildKey("creature", data);
            Assert.That(key, Is.EqualTo("creatureAll-Around Vision"));
        }

        [Test]
        public void BuildKey_FromString_WithFocus()
        {
            var key = helper.BuildKey("creature", "Spell-Like Ability#0#Clairaudience/Clairvoyance#3#Day#0#False###0");
            Assert.That(key, Is.EqualTo("creatureSpell-Like AbilityClairaudience/Clairvoyance"));
        }

        [Test]
        public void BuildKey_FromString_WithoutFocus()
        {
            var key = helper.BuildKey("creature", "All-Around Vision#0##0##0#False###0");
            Assert.That(key, Is.EqualTo("creatureAll-Around Vision"));
        }

        [Test]
        public void BuildKeyFromSections_FromNameAndFocus_WithFocus()
        {
            var key = helper.BuildKeyFromSections("creature", "Spell-Like Ability", "Clairaudience/Clairvoyance");
            Assert.That(key, Is.EqualTo("creatureSpell-Like AbilityClairaudience/Clairvoyance"));
        }

        [Test]
        public void BuildKeyFromSections_FromNameAndFocus_WithoutFocus()
        {
            var key = helper.BuildKeyFromSections("creature", "All-Around Vision", string.Empty);
            Assert.That(key, Is.EqualTo("creatureAll-Around Vision"));
        }
    }
}
