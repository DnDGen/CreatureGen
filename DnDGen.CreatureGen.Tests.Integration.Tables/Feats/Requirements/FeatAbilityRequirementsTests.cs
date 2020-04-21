﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magic;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class FeatAbilityRequirementsTests : TypesAndAmountsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.TypeAndAmount.FeatAbilityRequirements;
            }
        }

        private SpecialQualityHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new SpecialQualityHelper();
        }

        [Test]
        public void FeatAbilityRequirementsNames()
        {
            var names = GetNames();

            AssertCollectionNames(names);
        }

        private IEnumerable<string> GetNames()
        {
            var feats = FeatConstants.All();
            var metamagic = FeatConstants.Metamagic.All();
            var monster = FeatConstants.Monster.All();
            var craft = FeatConstants.MagicItemCreation.All();

            var specialQualityData = CollectionMapper.Map(TableNameConstants.Collection.SpecialQualityData);
            var specialQualities = specialQualityData
                .Where(kvp => kvp.Value.Any())
                .SelectMany(kvp => kvp.Value.Select(v => helper.BuildKey(kvp.Key, v)));

            var featsWithFoci = AbilityRequirementsTestData.GetFeatsWithFoci();

            var names = feats
                .Union(metamagic)
                .Union(monster)
                .Union(craft)
                .Union(specialQualities)
                .Union(featsWithFoci);

            return names;
        }

        [TestCaseSource(typeof(AbilityRequirementsTestData), "Feats")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Metamagic")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Monster")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Craft")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "CreatureSpecialQualities")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "CreatureTypeSpecialQualities")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "CreatureSubtypeSpecialQualities")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "FeatsWithFoci")]
        public void AbilityRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        public class AbilityRequirementsTestData
        {
            public static IEnumerable Feats
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    testCases[FeatConstants.BullRush_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Cleave][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Cleave_Great][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.CombatExpertise][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.DeflectArrows][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Disarm_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.Dodge][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Feint_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.Grapple_Improved][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Manyshot][AbilityConstants.Dexterity] = 17;
                    testCases[FeatConstants.Mobility][AbilityConstants.Dexterity] = 13;
                    //INFO: Natural Spell is only available to Druids
                    //testCases[FeatConstants.NaturalSpell][AbilityConstants.Wisdom] = 13;
                    testCases[FeatConstants.Overrun_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.PowerAttack][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.PreciseShot_Improved][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.RapidShot][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.ShotOnTheRun][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.SnatchArrows][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.SpringAttack][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.StunningFist][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.StunningFist][AbilityConstants.Wisdom] = 13;
                    testCases[FeatConstants.Sunder_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Trip_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.TwoWeaponDefense][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.TwoWeaponFighting][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.TwoWeaponFighting_Greater][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.TwoWeaponFighting_Improved][AbilityConstants.Dexterity] = 17;
                    testCases[FeatConstants.WhirlwindAttack][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.WhirlwindAttack][AbilityConstants.Intelligence] = 13;

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Metamagic
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.Metamagic.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Monster
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.Monster.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    testCases[FeatConstants.Monster.AwesomeBlow][AbilityConstants.Strength] = 25;
                    testCases[FeatConstants.Monster.MultiweaponFighting][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Monster.MultiweaponFighting_Greater][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.Monster.MultiweaponFighting_Improved][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.Monster.NaturalArmor_Improved][AbilityConstants.Constitution] = 13;

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Craft
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.MagicItemCreation.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable CreatureSpecialQualities
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var helper = new SpecialQualityHelper();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aasimar, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aasimar, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daylight)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.MucusCloud, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, SpellConstants.HypnoticPattern)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, SpellConstants.IllusoryWall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, SpellConstants.MirageArcana)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, SpellConstants.PersistentImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, SpellConstants.ProgrammedImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.Psionic, SpellConstants.ProjectImage)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Achaierai, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Allip, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyMace)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium Humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BladeBarrier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLies)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + " (self only)")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveDisease)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium Humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate evil damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BladeBarrier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster_Mass)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectSnaresAndPits)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLies)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Earthquake)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FlameStrike)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + " (self only)")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RaiseDead)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveDisease)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Lesser)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithDead)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WavesOfExhaustion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WavesOfFatigue)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium Humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to epic evil")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate evil damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimateObjects)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BladeBarrier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster_Mass)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Commune)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectSnaresAndPits)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionalAnchor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLies)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Earthquake)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HealHarm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Imprisonment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + " (self only)")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Permanency)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordBlind)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordKill)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PrismaticSpray)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveDisease)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ResistEnergy)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Lesser)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Resurrection)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithDead)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonMonsterVII)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WavesOfExhaustion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WavesOfFatigue)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ankheg, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Annis, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ant_Giant_Queen, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ant_Giant_Queen, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ant_Giant_Soldier, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ant_Giant_Soldier, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ant_Giant_Worker, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ant_Giant_Worker, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ape, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ape_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Aranea, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium humanoid; or Medium spider-human hybrid (like a Lycanthrope)")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Elder, FeatConstants.WeaponFocus, "Bite")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Camouflage, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil or silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.LayOnHands, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Command)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Light)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LightningBolt)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicMissile)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Azer, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.ProtectiveSlime, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Babau, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Baboon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Badger, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Badger, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Badger, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Badger_Dire, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Badger_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.Whip)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.FlamingBody, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blasphemy)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominateMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FireStorm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Implosion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Insanity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Telekinesis)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Balor, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.BarbedDefense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.OrdersWrath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ScorchingRay + ": 2 rays only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.ChangeShape, "Goblin or wolf")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blink)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CrushingDespair)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Misdirection)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace + ": in wolf form")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Rage)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.ChangeShape, "Goblin or wolf")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blink)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BullsStrength_Mass)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CrushingDespair)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EnlargePerson_Mass)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InvisibilitySphere)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Misdirection)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace + ": in wolf form")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Rage)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bat, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bat_Dire, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bat_Swarm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Basilisk_AbyssalGreater, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Basilisk_AbyssalGreater, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Basilisk_AbyssalGreater, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Basilisk_AbyssalGreater, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bear_Black, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bear_Brown, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bear_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bear_Polar, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Glaive)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Behir, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Behir, FeatConstants.SpecialQualities.Immunity, "Tripping")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Behir, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Beholder, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Beholder, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Beholder, FeatConstants.SpecialQualities.AntimagicCone, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Beholder, FeatConstants.SpecialQualities.Flight, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Beholder_Gauth, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Beholder_Gauth, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Beholder_Gauth, FeatConstants.SpecialQualities.Flight, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Belker, FeatConstants.SpecialQualities.SmokeForm, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bison, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.BlackPudding, FeatConstants.SpecialQualities.Split, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.BlackPudding_Elder, FeatConstants.SpecialQualities.Split, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.BlinkDog, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blink)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Boar, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Boar_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bodak, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bodak, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bodak, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bodak, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bodak, FeatConstants.SpecialQualities.Vulnerability, "Sunlight")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionalAnchor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fly)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.AlternateForm, "Humanoid or whirlwind form")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron or evil weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictSeriousWounds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LightningBolt)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WindWall)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bugbear, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bugbear, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bugbear, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Morningstar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bugbear, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bugbear, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bulette, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Bulette, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Camel_Bactrian, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Camel_Dromedary, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.CarrionCrawler, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cat, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centaur, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centaur, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centaur, FeatConstants.MountedCombat, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centipede_Monstrous_Large, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centipede_Monstrous_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centipede_Monstrous_Small, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centipede_Monstrous_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centipede_Swarm, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Centipede_Swarm, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.ChainDevil_Kyton, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.SpikedChain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate damage from silver weapons or good-aligned damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.Immunity, "Transformation")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cheetah, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cheetah, FeatConstants.SpecialQualities.Sprint, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Chimera_Black, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Chimera_Blue, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Chimera_Green, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Chimera_Red, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Chimera_White, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Choker, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Choker, FeatConstants.SpecialQualities.Quickness, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Chuul, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Chuul, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cloaker, FeatConstants.SpecialQualities.ShadowShift, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cockatrice, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Couatl, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Couatl, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Psionic, SpellConstants.PlaneShift)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Couatl, FeatConstants.EschewMaterials, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Crocodile, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Crocodile_Giant, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_5Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_5Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_5Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_6Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_6Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_6Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_7Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_7Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_7Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_8Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_8Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_8Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_9Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_9Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_9Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_10Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_10Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_10Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_11Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_11Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_11Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_12Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_12Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Cryohydra_12Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Darkmantle, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Darkmantle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Deinonychus, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Delver, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Delver, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Delver, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.LightRepeatingCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.SpecialQualities.Madness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daze)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SoundBurst)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro, FeatConstants.SpecialQualities.Vulnerability, "Sunlight")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.LightRepeatingCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daze)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SoundBurst)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.Vulnerability, "Sunlight")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, "Gaze attacks")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, "Visual effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, "Illusions")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, "Attacks that rely on sight")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellDeflection, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlUndead)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhoulTouch)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlanarAlly_Lesser)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RayOfEnfeeblement)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpectralHand)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Digester, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Digester, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.DisplacerBeast, FeatConstants.SpecialQualities.Displacement, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.DisplacerBeast_PackLord, FeatConstants.SpecialQualities.Displacement, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateWater + ": creates wine instead of water")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorCreation + ": created vegetable matter is permanent")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WindWalk)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateWater + ": creates wine instead of water")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorCreation + ": created vegetable matter is permanent")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WindWalk)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish + ": 3 wishes to any non-genie who captures it")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dog, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dog, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dog_Riding, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dog_Riding, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Donkey, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium Humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.Immunity, "Charm effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InsectPlague)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.CharmReptiles, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InsectPlague)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InsectPlague)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Veil)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirageArcana)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Veil)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Veil)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 60 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 100 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 120 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonMonsterVII + ": one Djinni")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 40 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 70 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 80 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 90 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 110 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 50 ft.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfStone)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MoveEarth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfStone)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfStone)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sunburst)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Foresight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sunburst)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sunburst)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominatePerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CommandPlants)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominatePerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominatePerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FindThePath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLocation)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FindThePath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FindThePath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ReverseGravity)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>(); ;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.DragonTurtle, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.DragonTurtle, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dragonne, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Scare)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StinkingCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FaerieFire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeepSlumber)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithPlants)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TreeShape)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TreeStride)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.TreeDependent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dryad, FeatConstants.SpecialQualities.WildEmpathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Deep, FeatConstants.WeaponProficiency_Martial, WeaponConstants.DwarvenWaraxe)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Deep, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenUrgrosh)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenWaraxe)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Warhammer)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, "Phantasms")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EnlargePerson + ": only self + carried items")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": only self + carried items")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Hill, FeatConstants.WeaponProficiency_Martial, WeaponConstants.DwarvenWaraxe)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Hill, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenUrgrosh)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenWaraxe)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Mountain, FeatConstants.WeaponProficiency_Martial, WeaponConstants.DwarvenWaraxe)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Mountain, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenUrgrosh)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenWaraxe)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Eagle, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Eagle_Giant, FeatConstants.SpecialQualities.Evasion, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.ChangeShape, "Any Small, Medium, or Large Humanoid or Giant")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PermanentImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ProduceFlame)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Pyrotechnics)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ScorchingRay + ": 1 ray only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfFire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish + ": Grant up to 3 wishes to nongenies")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elasmosaurus, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Elder, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Elder, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Greater, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Greater, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Huge, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Huge, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Large, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Large, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Medium, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Small, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Air_Small, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Elder, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Greater, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Huge, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Large, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Medium, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Earth_Small, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Elder, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Elder, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Greater, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Greater, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Huge, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Huge, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Large, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Large, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Medium, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Small, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Fire_Small, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Water_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Water_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Water_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elemental_Water_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elephant, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.Net)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Shortspear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Aquatic, FeatConstants.SpecialQualities.Gills, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Aquatic, FeatConstants.SpecialQualities.LowLightVision_Superior, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.HandCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.LightBlindness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FaerieFire)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeShortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Gray, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Half, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Half, FeatConstants.SpecialQualities.ElvenBlood, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Half, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeShortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_High, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeShortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wild, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeShortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Elf_Wood, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MinorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.EtherealFilcher, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.EtherealFilcher, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.EtherealMarauder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ettercap, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ettin, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ettin, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ettin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ettin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Morningstar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ettin, FeatConstants.SpecialQualities.TwoWeaponFighting_Superior, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Dictum)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.OrdersWrath)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.EschewMaterials, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CalmEmotions)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Dictum)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Divination)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.OrdersWrath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ShieldOfLaw)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Telepathy, "Any intelligent creature within 50 miles whose presence she is aware of")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominateMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictSeriousWounds + ": 8 workers work together to cast the spell")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MakeWhole + ": 3 workers work together to cast the spell")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.FrostWorm, FeatConstants.SpecialQualities.DeathThroes, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gargoyle, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gargoyle, FeatConstants.SpecialQualities.Freeze, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gargoyle_Kapoacinth, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gargoyle_Kapoacinth, FeatConstants.SpecialQualities.Freeze, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.GelatinousCube, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GelatinousCube, FeatConstants.SpecialQualities.Transparent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.AlternateForm, "Humanoid and globe forms")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil, cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.ProtectiveAura, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChainLightning)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ColorSpray)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ComprehendLanguages)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictLightWounds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PrismaticSpray)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfForce)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghoul, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghoul_Ghast, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ghoul_Lacedon, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Morningstar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.OversizedWeapon, SizeConstants.Gargantuan)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate + ": self plus 2,000 pounds")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ObscuringMist)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Fire, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Fire, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Frost, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Frost, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greataxe)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Frost, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Hill, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Hill, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Hill, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatclub)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Hill, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatclub)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatclub)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneTell)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CallLightning)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChainLightning)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FreedomOfMovement)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.GibberingMouther, FeatConstants.SpecialQualities.Amorphous, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GibberingMouther, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Girallon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githyanki, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githyanki, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githyanki, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githyanki, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.Psionic, SpellConstants.Daze)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.Psionic, SpellConstants.MageHand)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githzerai, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githzerai, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.InertialArmor, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Psionic, SpellConstants.Daze)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Psionic, SpellConstants.FeatherFall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Psionic, SpellConstants.Shatter)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChaosHammer)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ReverseGravity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish + ": for a mortal humanoid. The demon can use this ability to offer a mortal whatever he or she desires - but unless the wish is used to create pain and suffering in the world, the glabrezu demands either terrible evil acts or great sacrifice as compensation.")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnoll, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnoll, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Battleaxe)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnoll, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnoll, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Orc)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Reptilian)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Prestidigitation)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Prestidigitation)][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals + ": on a very basic level with forest animals")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Prestidigitation)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Prestidigitation)][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals + ": burrowing mammals only, duration 1 minute")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.WeaponProficiency_Martial, WeaponConstants.HeavyPick)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BlindnessDeafness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.Stonecunning, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Goblin, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Goblin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Goblin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Morningstar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Goblin, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine, bludgeoning weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Haste + ": after at least 1 round of combat, lasts 3 rounds")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Flesh, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Flesh, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Iron, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Iron, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Stone, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Stone, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Stone_Greater, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Golem_Stone_Greater, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gorgon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Transparent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.GrayRender, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WaterBreathing)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grick, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grick, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grick, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Griffon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.Dodge, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Pyrotechnics)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.Dodge, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Pyrotechnics)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grimlock, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, "Attack forms that rely on sight")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, "Gaze attacks")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, "Illusions")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, "Visual effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ComprehendLanguages)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LegendLore)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ReadMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfDeath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfInsanity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfPain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfPersuasion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfSleep)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfStunning)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Halfling_Deep, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Halfling_Deep, FeatConstants.SpecialQualities.Stonecunning, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Harpy, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Club)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hawk, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.HellHound, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HellHound, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.HellHound_NessianWarhound, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HellHound_NessianWarhound, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.InvisibleInLight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hellwasp_Swarm, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hellwasp_Swarm, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blasphemy)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChaosHammer)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hippogriff, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hobgoblin, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hobgoblin, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hobgoblin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hobgoblin, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.SpikedChain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate damage from good-aligned, silvered weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LightningBolt)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Horse_Heavy, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Horse_Heavy_War, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Horse_Light, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Horse_Light_War, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.AuraOfMenace, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.ChangeShape, "Any canine form of Small to Large size")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Message)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_5Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_5Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_5Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_6Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_6Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_6Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_7Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_7Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_7Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_8Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_8Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_8Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_9Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_9Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_9Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_10Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_10Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_10Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_11Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_11Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_11Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_12Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_12Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hydra_12Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Hyena, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Shortspear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate good damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fly)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.IceStorm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.AlternateForm, "Imp Alternate Form")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Commune + ": ask 6 questions")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.InvisibleStalker, FeatConstants.SpecialQualities.Tracking_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.InvisibleStalker, FeatConstants.SpecialQualities.NaturalInvisibility, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.SpecialQualities.ElementalEndurance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Janni, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kobold, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Sling)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kobold, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kobold, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kobold, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to chaotic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLies)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateCreature)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MarkOfJustice)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kraken, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kraken, FeatConstants.SpecialQualities.Jet, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominateAnimal)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ResistEnergy)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Krenshar, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Krenshar, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Shortspear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.PincerStaff)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Adhesive, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.KeenSight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.LightBlindness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Slippery, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lamia, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeepSlumber)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.AuraOfMenace, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic, evil weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lemure, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lemure, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lemure, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lemure, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil, silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.LayOnHands, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.ProtectiveAura, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictCriticalWounds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HealHarm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveDisease)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals + ": does not require sound")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfForce)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Leopard, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, "Bardic music ability as a 6th-level Bard")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Knock)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Light)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithPlants)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lion, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lion_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lizard, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lizardfolk, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lizardfolk, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lizardfolk, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Club)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Lizardfolk, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Locathah, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Locathah, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Magmin, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Magmin, FeatConstants.SpecialQualities.MeltWeapons, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Manticore, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Manticore, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.Monster.MultiweaponFighting, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AlignWeapon)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BladeBarrier)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicWeapon)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ProjectImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Telekinesis)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to chaotic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AirWalk)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChainLightning)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CircleOfDeath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Command_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictLightWounds_Mass)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Earthquake)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateCreature)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MarkOfJustice)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfForce)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Medusa, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Medusa, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Megaraptor, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.FastHealing, "Exposed to moving air")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.FastHealing, "In arid, dusty environment")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WindWall)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.FastHealing, "Underground or buried up to its waist in earth")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EnlargePerson + ": self only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SoftenEarthAndStone)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.FastHealing, "Touching a flame at least as large as a torch")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HeatMetal)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ScorchingRay)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.FastHealing, "Touching a piece of ice at least Tiny in size, or ambient temperature is freezing or lower")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChillMetal)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicMissile)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.ChangeShape, "A pool of lava 3 feet in diameter and 6 inches deep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.FastHealing, "Touching magma, lava, or a flame at least as large as a torch")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Pyrotechnics)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.FastHealing, "In a wet or muddy environment")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AcidArrow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StinkingCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.FastHealing, "In an arid environment")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Glitterdust)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.SpellLikeAbility, "Draw moisture from the air")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.FastHealing, "Touching boiling water or in a hot, humid area")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.SpellLikeAbility, "Rainstorm of boiling water")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.FastHealing, "Exposed to rain or submerged up to its waist in water")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AcidArrow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StinkingCloud)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Merfolk, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Merfolk, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Merfolk, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Merfolk, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Merfolk, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mimic, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mimic, FeatConstants.SpecialQualities.MimicShape, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, SpellConstants.Levitate)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, SpellConstants.PlaneShift)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Psionic, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Minotaur, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greataxe)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Minotaur, FeatConstants.SpecialQualities.NaturalCunning, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Minotaur, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Monkey, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mule, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mummy, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Mummy, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.Immunity, "Any form of mind reading")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Naga_Dark, FeatConstants.EschewMaterials, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Naga_Guardian, FeatConstants.EschewMaterials, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Naga_Spirit, FeatConstants.EschewMaterials, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Naga_Water, FeatConstants.EschewMaterials, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CallLightning)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Feeblemind)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Slow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium Humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron, magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, "Charm")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, "Fear")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicMissile)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RayOfEnfeeblement)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sleep)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Etherealness)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.AversionToDaylight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver, magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.DesecratingAura, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Contagion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FingerOfDeath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Haste)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster_Mass)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightmare, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AstralProjection)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightmare, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Etherealness)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightmare_Cauchemar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AstralProjection)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightmare_Cauchemar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Etherealness)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.AversionToDaylight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver, magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.DesecratingAura, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Contagion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FingerOfDeath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Haste)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.AversionToDaylight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver, magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.DesecratingAura, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Contagion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FingerOfDeath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Haste)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.Dodge, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WaterBreathing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nixie, FeatConstants.SpecialQualities.WildEmpathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nymph, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nymph, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nymph, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nymph, FeatConstants.SpecialQualities.UnearthlyGrace, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Nymph, FeatConstants.SpecialQualities.WildEmpathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.OchreJelly, FeatConstants.SpecialQualities.Split, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Octopus, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Octopus, FeatConstants.SpecialQualities.Jet, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Octopus_Giant, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Octopus_Giant, FeatConstants.SpecialQualities.Jet, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatclub)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre_Merrow, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre_Merrow, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre_Merrow, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre_Merrow, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ogre_Merrow, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.ChangeShape, "Any Small, Medium, or Large Humanoid or Giant")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Flight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Regeneration, "Fire and acid deal normal damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sleep)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Orc, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Falchion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greataxe)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Orc, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Orc, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Orc_Half, FeatConstants.SpecialQualities.OrcBlood, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Otyugh, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Owl, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Owl_Giant, FeatConstants.SpecialQualities.LowLightVision_Superior, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Owlbear, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pegasus, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pegasus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment + ": within 60-foot radius")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.PhantomFungus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.PhaseSpider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Phasm, FeatConstants.SpecialQualities.AlternateForm, "Any form Large size or smaller, including Humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Amorphous, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate damage from good spells or effects, or from good-aligned silvered weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blasphemy)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateUndead)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster_Mass)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MeteorSwarm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.Dodge, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion_Lesser)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PermanentImage + ": visual and auditory elements only")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.Dodge, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion_Lesser)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.IrresistibleDance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PermanentImage + ": visual and auditory elements only")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pony, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pony_War, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Porpoise, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Porpoise, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pseudodragon, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.PurpleWorm, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_5Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_5Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_5Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_6Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_6Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_6Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_7Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_7Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_7Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_8Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_8Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_8Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_9Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_9Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_9Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_10Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_10Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_10Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_11Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_11Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_11Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_12Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_12Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Pyrohydra_12Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.AlternateForm, "Bat, Small or Medium monstrous centipede, toad, and wolf")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear + ": 30-foot radius area from the quasit")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Commune + ": can ask 6 questions")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.ChangeShape, "Any Humanoid form")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, piercing weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rast, FeatConstants.SpecialQualities.Flight, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rat, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rat, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rat_Dire, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rat_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rat_Swarm, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Rat_Swarm, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Raven, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ravid, FeatConstants.SpecialQualities.Flight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ravid, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Ravid, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimateObjects)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Remorhaz, FeatConstants.SpecialQualities.Heat, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Remorhaz, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Retriever, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Roper, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Roper, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Roper, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Roper, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.RustMonster, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.FreshwaterSensitivity, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.LightBlindness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.SpeakWithSharks, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.WaterDependent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Malenti, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Malenti, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Malenti, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.FreshwaterSensitivity, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.SpeakWithSharks, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.WaterDependent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Mutant, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Mutant, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Mutant, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.FreshwaterSensitivity, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.LightBlindness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.SpeakWithSharks, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.WaterDependent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Average, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Average, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Average, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Flamebrother, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Flamebrother, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BurningHands)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FlamingSphere)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonMonsterVII + ": Huge fire elemental")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfFire)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Satyr, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Satyr, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Satyr, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Satyr, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Satyr_WithPipes, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Satyr_WithPipes, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Satyr_WithPipes, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Satyr_WithPipes, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Colossal, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Huge, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Large, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Medium, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Small, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Small, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpion_Monstrous_Tiny, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Lance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.SeaCat, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.SeaCat, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.SeaHag, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.SeaHag, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shadow, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shadow_Greater, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShadowMastiff, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShadowMastiff, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShadowMastiff, FeatConstants.SpecialQualities.ShadowBlend, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shark_Dire, FeatConstants.SpecialQualities.KeenScent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shark_Huge, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shark_Huge, FeatConstants.SpecialQualities.KeenScent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shark_Large, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shark_Large, FeatConstants.SpecialQualities.KeenScent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shark_Medium, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Shark_Medium, FeatConstants.SpecialQualities.KeenScent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.FindMaster, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.Guard, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.SpellStoring, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ShieldOther + ": within 100 feet of the amulet.  Does not provide spell's AC or save bonuses")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShockerLizard, FeatConstants.SpecialQualities.ElectricitySense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.ShockerLizard, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Skum, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChaosHammer)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Passwall)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Telekinesis)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.ChangeShape, "Any humanoid form")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to lawful weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimateObjects)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChaosHammer)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FingerOfDeath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fly)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Identify)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Shatter)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CircleOfDeath)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CloakOfChaos)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WordOfChaos)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Implosion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordBlind)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.ChangeShape, "Any humanoid form")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to lawful weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChaosHammer)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Identify)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LightningBolt)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Shatter)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimateObjects)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fly)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.ChangeShape, "Medium or Large humanoid form")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChaosHammer)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ProtectionFromAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Shatter)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Constrictor, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Constrictor_Giant, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Viper_Huge, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Viper_Large, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Viper_Medium, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Viper_Small, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Viper_Small, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Viper_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Snake_Viper_Tiny, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spectre, FeatConstants.SpecialQualities.SunlightPowerlessness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spectre, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spectre, FeatConstants.SpecialQualities.UnnaturalAura, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.SpiderEater, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.SpiderEater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FreedomOfMovement)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Colossal, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Huge, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Large, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Medium, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Small, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Small, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_Hunter_Tiny, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Large, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Small, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Small, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Swarm, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Spider_Swarm, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Squid, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Squid, FeatConstants.SpecialQualities.Jet, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Squid_Giant, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Squid_Giant, FeatConstants.SpecialQualities.Jet, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium Humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or cold iron weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Stirge, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Carapace, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to epic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, "Ability Damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, "Disease")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, "Energy Drain")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Regeneration, "No form of attack deals lethal damage to the tarrasque")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tendriculos, FeatConstants.SpecialQualities.Regeneration, "Bludgeoning weapons and acid deal normal damage")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Thoqqua, FeatConstants.SpecialQualities.Heat, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Thoqqua, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tiefling, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tiger, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tiger_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Warhammer)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium Humanoid")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to lawful weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.OversizedWeapon, SizeConstants.Gargantuan)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BestowCurse)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChainLightning)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CrushingHand)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictCriticalWounds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daylight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Etherealness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FireStorm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Gate)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InvisibilityPurge)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Maze)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MeteorSwarm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Greater)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonNaturesAllyIX)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WordOfChaos)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Toad, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Treant, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to slashing weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Treant, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Triceratops, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Triton, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonNaturesAllyIV)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troglodyte, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Club)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troglodyte, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troglodyte, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troglodyte, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troll, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troll, FeatConstants.SpecialQualities.Regeneration, "Fire and acid deal normal damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troll, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Regeneration, "Fire and acid deal normal damage; only regenerates when immersed in water")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.AuraOfMenace, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.TrumpetArchon, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Message)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Tyrannosaurus, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.UmberHulk, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.UmberHulk_TrulyHorrid, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, "Charm")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, "Compulsion")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictLightWounds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictModerateWounds)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": within its forest home")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.WildEmpathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.LightningReflexes, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vargouille, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Heroism)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Telekinesis)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Weasel, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Weasel, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Weasel_Dire, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Weasel_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Whale_Baleen, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Whale_Baleen, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Whale_Cachalot, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Whale_Cachalot, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Whale_Orca, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Whale_Orca, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.WillOWisp, FeatConstants.WeaponFinesse, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.WillOWisp, FeatConstants.SpecialQualities.Immunity, "Spells and spell-like effects that allow spell resistance, except Magic Missile and Maze")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.WillOWisp, FeatConstants.SpecialQualities.NaturalInvisibility, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.WinterWolf, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wolf, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wolf, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wolf_Dire, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wolf_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wolverine, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wolverine, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wolverine_Dire, FeatConstants.Track, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wolverine_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Worg, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith, FeatConstants.SpecialQualities.DaylightPowerlessness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith, FeatConstants.SpecialQualities.UnnaturalAura, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith_Dread, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith_Dread, FeatConstants.Initiative_Improved, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.DaylightPowerlessness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.Lifesense, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.UnnaturalAura, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wyvern, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Wyvern, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xill, FeatConstants.Monster.Multiattack, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xill, FeatConstants.SpecialQualities.Planewalk, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xill, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Average, FeatConstants.Cleave, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Elder, FeatConstants.Cleave, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.YethHound, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YethHound, FeatConstants.SpecialQualities.Flight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YethHound, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, "Attacks that rely on sight")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, "Gaze attacks")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, "Illusions")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, "Visual effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Sonic)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.BlindFight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.ChameleonPower, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BalefulPolymorph + ": into snake form only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.BlindFight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.ChameleonPower, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeArms, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.BlindFight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.ChameleonPower, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeHead, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.BlindFight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.ChameleonPower, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTail, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.BlindFight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.ChameleonPower, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.Scent, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.Alertness, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.BlindFight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.MountedCombat, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.SpikedChain)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to chaotic weapons")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionalAnchor)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Earthquake)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Geas_Lesser)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateCreature)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MarkOfJustice)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new Dictionary<string, int>();

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable CreatureTypeSpecialQualities
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var helper = new SpecialQualityHelper();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Aberration, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Aberration, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Aberration, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Animal, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Ability Damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Ability Drain")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Being raised or resurrected")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Death")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Death from massive damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Disease")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Effect that requires a Fortitude save")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Energy Drain")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Exhaustion")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Fatigue")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Necromancy")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Nonlethal damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Dragon, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Flanking")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Fey, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Fey, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Fey, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Giant, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Giant, FeatConstants.WeaponProficiency_Martial, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Giant, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Giant, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Humanoid, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Humanoid, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.MagicalBeast, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.MagicalBeast, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Flanking")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Gaze attacks, visual effects, illusions, and other attack forms that rely on sight")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Polymorph")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Outsider, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Outsider, FeatConstants.WeaponProficiency_Martial, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Outsider, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Outsider, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Polymorph")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Ability Damage to Strength, Dexterity, or Constitution")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Ability Drain")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Death")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Death from massive damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Disease")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Effect that requires a Fortitude save")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Energy Drain")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Exhaustion")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Fatigue")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Nonlethal damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Poison")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Vermin, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Vermin, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new Dictionary<string, int>();

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable CreatureSubtypeSpecialQualities
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var helper = new SpecialQualityHelper();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.ProtectiveAura, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Cold, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Cold, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Goblinoid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Orc)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.Stability, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.Stonecunning, string.Empty)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Elf, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Elf, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Fire, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Fire, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Cold)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Kobold)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Goblinoid)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.GnomeHookedHammer)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.SpecialQualities.AttackBonus, "thrown weapons and slings")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.ArmorProficiency_Light, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "50% chance to ignore any damage from a corporeal source (except for positive energy, negative energy, force effects such as magic missiles, or attacks made with ghost touch weapons)")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, FeatConstants.SpecialQualities.Blindsense)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, FeatConstants.SpecialQualities.Blindsight)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "Falling or falling damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "Grapple")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "Nonmagical attacks")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, FeatConstants.SpecialQualities.Scent)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, FeatConstants.SpecialQualities.Tremorsense)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "Trip")] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Shapechanger, FeatConstants.ShieldProficiency, string.Empty)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Shapechanger, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new Dictionary<string, int>();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.HalfDamage, AttributeConstants.DamageTypes.Piercing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.HalfDamage, AttributeConstants.DamageTypes.Slashing)] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Any spell that targets a specific number of creatures, including single-target spells")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Bull Rush")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Dying state")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Flanking")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Grapple")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Staggering")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Trip")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Weapon damage")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, "Area-of-effect spells")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, "High winds")] = new Dictionary<string, int>();
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, "Splash damage")] = new Dictionary<string, int>();

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable<string> GetFeatsWithFoci()
            {
                var featsWithFoci = new List<string>();

                var simpleWeapons = WeaponConstants.GetAllSimple(false, true);
                foreach (var weapon in simpleWeapons)
                {
                    featsWithFoci.Add($"{FeatConstants.WeaponProficiency_Simple}/{weapon}");
                }

                var martialWeapons = WeaponConstants.GetAllMartial(false, true);
                foreach (var weapon in martialWeapons)
                {
                    featsWithFoci.Add($"{FeatConstants.WeaponProficiency_Martial}/{weapon}");
                }

                var exoticWeapons = WeaponConstants.GetAllExotic(false, true);
                foreach (var weapon in exoticWeapons)
                {
                    featsWithFoci.Add($"{FeatConstants.WeaponProficiency_Exotic}/{weapon}");
                }

                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Appraise}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Balance}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Bluff}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Climb}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Concentration}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Craft}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Glass)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Leather)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.DecipherScript}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Diplomacy}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.DisableDevice}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Disguise}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.EscapeArtist}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Forgery}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.GatherInformation}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.HandleAnimal}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Heal}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Hide}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Intimidate}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Jump}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Knowledge}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Listen}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.MoveSilently}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.OpenLock}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Perform}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Act)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Profession}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Clerk)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coiffeur)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cook)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Laborer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Launderer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Nursemaid)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Polisher)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Porter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Scribe)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Ride}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Search}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.SenseMotive}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.SleightOfHand}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Spellcraft}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Spot}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Survival}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Swim}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Tumble}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.UseMagicDevice}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.UseRope}");

                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Boar}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Rat}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Raven}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Spider_Monstrous_Hunter_Small}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Spider_Monstrous_Hunter_Medium}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Spider_Monstrous_WebSpinner_Small}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Spider_Monstrous_WebSpinner_Medium}");

                return featsWithFoci;
            }

            public static IEnumerable FeatsWithFoci
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

                    //Prep feats with foci
                    var featsWithFoci = GetFeatsWithFoci();
                    foreach (var featWithFoci in featsWithFoci)
                    {
                        testCases[featWithFoci] = new Dictionary<string, int>();
                    }

                    //Set requirements
                    testCases[$"{FeatConstants.WeaponProficiency_Exotic}/{WeaponConstants.BastardSword}"][AbilityConstants.Strength] = 13;
                    testCases[$"{FeatConstants.WeaponProficiency_Exotic}/{WeaponConstants.DwarvenWaraxe}"][AbilityConstants.Strength] = 13;

                    //Return test cases
                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}