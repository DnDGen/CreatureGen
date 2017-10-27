﻿using CreatureGen.Creatures;
using CreatureGen.Domain.Generators.Abilities;
using CreatureGen.Domain.Generators.Alignments;
using CreatureGen.Domain.Generators.Creatures;
using CreatureGen.Domain.Generators.Classes;
using CreatureGen.Domain.Generators.Combats;
using CreatureGen.Domain.Generators.Feats;
using CreatureGen.Domain.Generators.Items;
using CreatureGen.Domain.Generators.Languages;
using CreatureGen.Domain.Generators.Magics;
using CreatureGen.Domain.Generators.Races;
using CreatureGen.Domain.Generators.Randomizers.Abilities;
using CreatureGen.Domain.Generators.Randomizers.Alignments;
using CreatureGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames;
using CreatureGen.Domain.Generators.Randomizers.CharacterClasses.Levels;
using CreatureGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CreatureGen.Domain.Generators.Randomizers.Races.Metaraces;
using CreatureGen.Domain.Generators.Skills;
using CreatureGen.Leaders;
using CreatureGen.Randomizers.Abilities;
using CreatureGen.Randomizers.Alignments;
using CreatureGen.Randomizers.CharacterClasses;
using CreatureGen.Randomizers.Races;
using CreatureGen.Verifiers;
using NUnit.Framework;
using RollGen;
using TreasureGen.Generators;
using TreasureGen.Items;
using TreasureGen.Items.Magical;
using TreasureGen.Items.Mundane;

namespace CreatureGen.Tests.Integration.IoC.Modules
{
    [TestFixture]
    public class GeneratorsModuleTests : IoCTests
    {
        [Test]
        public void AlignmentGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAlignmentGenerator>();
        }

        [Test]
        public void AlignmentGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IAlignmentGenerator, AlignmentGeneratorEventGenDecorator>();
        }

        [Test]
        public void CharacterGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICharacterGenerator>();
        }

        [Test]
        public void CharacterGeneratorIsDecorated()
        {
            AssertIsInstanceOf<ICharacterGenerator, CharacterGeneratorEventGenDecorator>();
        }

        [Test]
        public void CharacterClassGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICharacterClassGenerator>();
        }

        [Test]
        public void CharacterClassGeneratorIsDecorated()
        {
            AssertIsInstanceOf<ICharacterClassGenerator, CharacterClassGeneratorEventGenDecorator>();
        }

        [Test]
        public void HitPointsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IHitPointsGenerator>();
        }

        [Test]
        public void LanguageGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ILanguageGenerator>();
        }

        [Test]
        public void LanguageGeneratorIsDecorated()
        {
            AssertIsInstanceOf<ILanguageGenerator, LanguageGeneratorEventGenDecorator>();
        }

        [Test]
        public void RaceGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IRaceGenerator>();
        }

        [Test]
        public void RaceGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IRaceGenerator, RaceGeneratorEventGenDecorator>();
        }

        [Test]
        public void RandomizerVerifierIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IRandomizerVerifier>();
        }

        [Test]
        public void AbilitiesGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAbilitiesGenerator>();
        }

        [Test]
        public void AbilitiesGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IAbilitiesGenerator, AbilitiesGeneratorEventGenDecorator>();
        }

        [Test]
        public void AlignmentRandomizerNamedAnyIsAnyAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, AnyAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
        }

        [Test]
        public void AlignmentRandomizerNamedChaoticIsChaoticAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, ChaoticAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Chaotic);
        }

        [Test]
        public void AlignmentRandomizerNamedEvilIsEvilAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, EvilAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Evil);
        }

        [Test]
        public void AlignmentRandomizerNamedGoodIsGoodAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, GoodAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Good);
        }

        [Test]
        public void AlignmentRandomizerNamedLawfulIsLawfulAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, LawfulAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Lawful);
        }

        [Test]
        public void AlignmentRandomizerNamedNeutralIsNeutralAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NeutralAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Neutral);
        }

        [Test]
        public void AlignmentRandomizerNamedNonChaoticIsNonChaoticAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonChaoticAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonChaotic);
        }

        [Test]
        public void AlignmentRandomizerNamedNonEvilIsNonEvilAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonEvilAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonEvil);
        }

        [Test]
        public void AlignmentRandomizerNamedNonGoodIsNonGoodAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonGoodAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonGood);
        }

        [Test]
        public void AlignmentRandomizerNamedNonLawfulIsNonLawfulAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonLawfulAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonLawful);
        }

        [Test]
        public void AlignmentRandomizerNamedNonNeutralIsNonNeutralAlignmentRandomizer()
        {
            AssertNamedIsInstanceOf<IAlignmentRandomizer, NonNeutralAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonNeutral);
        }

        [TestCase(AlignmentRandomizerTypeConstants.Any)]
        [TestCase(AlignmentRandomizerTypeConstants.Chaotic)]
        [TestCase(AlignmentRandomizerTypeConstants.Evil)]
        [TestCase(AlignmentRandomizerTypeConstants.Good)]
        [TestCase(AlignmentRandomizerTypeConstants.Lawful)]
        [TestCase(AlignmentRandomizerTypeConstants.Neutral)]
        [TestCase(AlignmentRandomizerTypeConstants.NonChaotic)]
        [TestCase(AlignmentRandomizerTypeConstants.NonEvil)]
        [TestCase(AlignmentRandomizerTypeConstants.NonGood)]
        [TestCase(AlignmentRandomizerTypeConstants.NonLawful)]
        [TestCase(AlignmentRandomizerTypeConstants.NonNeutral)]
        public void AlignmentRandomizerIsNotBuiltAsSingleton(string name)
        {
            AssertNotSingleton<IAlignmentRandomizer>(name);
        }

        [Test]
        public void SetAlignmentRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetAlignmentRandomizer>();
        }

        [Test]
        public void ClassNameRandomizerNamedAnyPlayerIsAnyPlayerClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, AnyPlayerClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
        }

        [Test]
        public void ClassNameRandomizerNamedAnyNPCIsAnyNPCClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, AnyNPCClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
        }

        [Test]
        public void ClassNameRandomizerNamedHealerIsHealerClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, DivineSpellcasterClassNameRandomizer>(ClassNameRandomizerTypeConstants.DivineSpellcaster);
        }

        [Test]
        public void ClassNameRandomizerNamedMageIsMageClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, ArcaneSpellcasterClassNameRandomizer>(ClassNameRandomizerTypeConstants.ArcaneSpellcaster);
        }

        [Test]
        public void ClassNameRandomizerNamedNonSpellcasterIsNonSpellcasterClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, NonSpellcasterClassNameRandomizer>(ClassNameRandomizerTypeConstants.NonSpellcaster);
        }

        [Test]
        public void ClassNameRandomizerNamedSpellcasterIsSpellcasterClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, SpellcasterClassNameRandomizer>(ClassNameRandomizerTypeConstants.Spellcaster);
        }

        [Test]
        public void ClassNameRandomizerNamedStealthIsStealthClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, StealthClassNameRandomizer>(ClassNameRandomizerTypeConstants.Stealth);
        }

        [Test]
        public void ClassNameRandomizerNameWarriorIsWarriorClassNameRandomizer()
        {
            AssertNamedIsInstanceOf<IClassNameRandomizer, PhysicalCombatClassNameRandomizer>(ClassNameRandomizerTypeConstants.PhysicalCombat);
        }

        [TestCase(ClassNameRandomizerTypeConstants.AnyPlayer)]
        [TestCase(ClassNameRandomizerTypeConstants.AnyNPC)]
        [TestCase(ClassNameRandomizerTypeConstants.DivineSpellcaster)]
        [TestCase(ClassNameRandomizerTypeConstants.ArcaneSpellcaster)]
        [TestCase(ClassNameRandomizerTypeConstants.NonSpellcaster)]
        [TestCase(ClassNameRandomizerTypeConstants.Spellcaster)]
        [TestCase(ClassNameRandomizerTypeConstants.Stealth)]
        [TestCase(ClassNameRandomizerTypeConstants.PhysicalCombat)]
        public void ClassNameRandomizerIsNotBuiltAsSingleton(string name)
        {
            AssertNotSingleton<IClassNameRandomizer>(name);
        }

        [Test]
        public void SetClassNameRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetClassNameRandomizer>();
        }

        [Test]
        public void LevelRandomizerNamedAnyIsAnyLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, AnyLevelRandomizer>(LevelRandomizerTypeConstants.Any);
        }

        [Test]
        public void LevelRandomizerNamedHighIsHighLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, HighLevelRandomizer>(LevelRandomizerTypeConstants.High);
        }

        [Test]
        public void LevelRandomizerNamedLowIsLowLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, LowLevelRandomizer>(LevelRandomizerTypeConstants.Low);
        }

        [Test]
        public void LevelRandomizerNamedMediumIsMediumLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, MediumLevelRandomizer>(LevelRandomizerTypeConstants.Medium);
        }

        [Test]
        public void LevelRandomizerNamedVeryHighIsVeryHighLevelRandomizer()
        {
            AssertNamedIsInstanceOf<ILevelRandomizer, VeryHighLevelRandomizer>(LevelRandomizerTypeConstants.VeryHigh);
        }

        [TestCase(LevelRandomizerTypeConstants.Any)]
        [TestCase(LevelRandomizerTypeConstants.High)]
        [TestCase(LevelRandomizerTypeConstants.Low)]
        [TestCase(LevelRandomizerTypeConstants.Medium)]
        [TestCase(LevelRandomizerTypeConstants.VeryHigh)]
        public void LevelRandomizerIsNotBuiltAsSingleton(string name)
        {
            AssertNotSingleton<ILevelRandomizer>(name);
        }

        [Test]
        public void SetLevelRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetLevelRandomizer>();
        }

        [Test]
        public void BaseRaceRandomizerNamedAnyIsAnyBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, AnyBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedAquaticIsAquaticBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, AquaticBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AquaticBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonStandardIsNonStandardBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NonStandardBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonStandardBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedMonsterIsMonsterBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, MonsterBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.MonsterBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedNonMonsterIsNonMonsterBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NonMonsterBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonMonsterBase);
        }

        [Test]
        public void BaseRaceRandomizerNamedStandardIsStandardBaseRaceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, StandardBaseRaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.StandardBase);
        }

        [TestCase(RaceRandomizerTypeConstants.BaseRace.AnyBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.AquaticBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.MonsterBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonMonsterBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.NonStandardBase)]
        [TestCase(RaceRandomizerTypeConstants.BaseRace.StandardBase)]
        public void BaseRaceRandomizerIsNotBuiltAsSingleton(string name)
        {
            AssertNotSingleton<RaceRandomizer>(name);
        }

        [Test]
        public void SetBaseRaceRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetBaseRaceRandomizer>();
        }

        [Test]
        public void MetaraceRandomizerNamedAnyIsAnyMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, AnyMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, AnyMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedGeneticIsGeneticMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, GeneticMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, GeneticMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedLycanthropeIsLycanthropeMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, LycanthropeMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, LycanthropeMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedNoneIsNoMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<RaceRandomizer, NoMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
        }

        [Test]
        public void MetaraceRandomizerNamedUndeadIsUndeadMetaraceRandomizer()
        {
            AssertNamedIsInstanceOf<IForcableMetaraceRandomizer, UndeadMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
            AssertNamedIsInstanceOf<RaceRandomizer, UndeadMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
        }

        [TestCase(RaceRandomizerTypeConstants.Metarace.AnyMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.GeneticMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta)]
        [TestCase(RaceRandomizerTypeConstants.Metarace.UndeadMeta)]
        public void MetaraceRandomizerIsNotBuiltAsSingleton(string name)
        {
            AssertNotSingleton<IForcableMetaraceRandomizer>(name);
            AssertNotSingleton<RaceRandomizer>(name);
        }

        [Test]
        public void NoMetaraceRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
        }

        [Test]
        public void SetMetaraceRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetMetaraceRandomizer>();
        }

        [Test]
        public void AbilitiesRandomizerNamedAverageIsAverageAbilitiesRandomizer()
        {
            AssertNamedIsInstanceOf<IAbilitiesRandomizer, AverageAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Average);
        }

        [Test]
        public void AbilitiesRandomizerNamedBestOfFourIsBestOfFourAbilitiesRandomizer()
        {
            AssertNamedIsInstanceOf<IAbilitiesRandomizer, BestOfFourAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.BestOfFour);
        }

        [Test]
        public void AbilitiesRandomizerNamedGoodIsGoodAbilitiesRandomizer()
        {
            AssertNamedIsInstanceOf<IAbilitiesRandomizer, GoodAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Good);
        }

        [Test]
        public void AbilitiesRandomizerNamedHeroicIsHeroicAbilitiesRandomizer()
        {
            AssertNamedIsInstanceOf<IAbilitiesRandomizer, HeroicAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Heroic);
        }

        [Test]
        public void AbilitiesRandomizerNamedOnesAsSixesIsOnesAsSixesAbilitiesRandomizer()
        {
            AssertNamedIsInstanceOf<IAbilitiesRandomizer, OnesAsSixesAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.OnesAsSixes);
        }

        [Test]
        public void AbilitiesRandomizerNamedPoorIsPoorAbilitiesRandomizer()
        {
            AssertNamedIsInstanceOf<IAbilitiesRandomizer, PoorAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Poor);
        }

        [Test]
        public void AbilitiesRandomizerNamedRawIsRawAbilitiesRandomizer()
        {
            AssertNamedIsInstanceOf<IAbilitiesRandomizer, RawAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw);
        }

        [Test]
        public void AbilitiesRandomizerNamedTwoTenSidedDiceIsTwoTenSidedDiceAbilitiesRandomizer()
        {
            AssertNamedIsInstanceOf<IAbilitiesRandomizer, TwoTenSidedDiceAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.TwoTenSidedDice);
        }

        [TestCase(AbilitiesRandomizerTypeConstants.Average)]
        [TestCase(AbilitiesRandomizerTypeConstants.BestOfFour)]
        [TestCase(AbilitiesRandomizerTypeConstants.Good)]
        [TestCase(AbilitiesRandomizerTypeConstants.Heroic)]
        [TestCase(AbilitiesRandomizerTypeConstants.OnesAsSixes)]
        [TestCase(AbilitiesRandomizerTypeConstants.Poor)]
        [TestCase(AbilitiesRandomizerTypeConstants.Raw)]
        [TestCase(AbilitiesRandomizerTypeConstants.TwoTenSidedDice)]
        public void StatRandomizerIsNotBuiltAsSingleton(string name)
        {
            AssertNotSingleton<IAbilitiesRandomizer>(name);
        }

        [Test]
        public void SetAbilitiesRandomizerIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISetAbilitiesRandomizer>();
        }

        [Test]
        public void CombatGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ICombatGenerator>();
        }

        [Test]
        public void CombatGeneratorIsDecorated()
        {
            AssertIsInstanceOf<ICombatGenerator, CombatGeneratorEventGenDecorator>();
        }

        [Test]
        public void EquipmentGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IEquipmentGenerator>();
        }

        [Test]
        public void EquipmentGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IEquipmentGenerator, EquipmentGeneratorEventGenDecorator>();
        }

        [Test]
        public void SkillsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<ISkillsGenerator>();
        }

        [Test]
        public void SkillsGeneratorIsDecorated()
        {
            AssertIsInstanceOf<ISkillsGenerator, SkillsGeneratorEventGenDecorator>();
        }

        [Test]
        public void FeatsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IFeatsGenerator>();
        }

        [Test]
        public void FeatsGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IFeatsGenerator, FeatsGeneratorEventGenDecorator>();
        }

        [Test]
        public void AdditionalFeatsGeneratorIsNotBuiltAsSingleton()
        {
            AssertNotSingleton<IAdditionalFeatsGenerator>();
        }

        [Test]
        public void AdditionalFeatsGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IAdditionalFeatsGenerator, AdditionalFeatsGeneratorEventGenDecorator>();
        }

        [Test]
        public void ClassFeatsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IClassFeatsGenerator>();
        }

        [Test]
        public void ClassFeatsGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IClassFeatsGenerator, ClassFeatsGeneratorEventGenDecorator>();
        }

        [Test]
        public void RacialFeatsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IRacialFeatsGenerator>();
        }

        [Test]
        public void RacialFeatsGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IRacialFeatsGenerator, RacialFeatsGeneratorEventGenDecorator>();
        }

        [Test]
        public void SavingThrowsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISavingThrowsGenerator>();
        }

        [Test]
        public void FeatFocusGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IFeatFocusGenerator>();
        }

        [Test]
        public void FeatFocusGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IFeatFocusGenerator, FeatFocusGeneratorEventGenDecorator>();
        }

        [Test]
        public void ArmorGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IArmorGenerator>();
        }

        [Test]
        public void WeaponGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IWeaponGenerator>();
        }

        [Test]
        public void MagicGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IMagicGenerator>();
        }

        [Test]
        public void MagicGeneratorIsDecorated()
        {
            AssertIsInstanceOf<IMagicGenerator, MagicGeneratorEventGenDecorator>();
        }

        [Test]
        public void SpellsGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ISpellsGenerator>();
        }

        [Test]
        public void AnimalGeneratorIsNotASingleton()
        {
            AssertNotSingleton<IAnimalGenerator>();
        }

        [Test]
        public void LeadershipGeneratorIsNotASingleton()
        {
            AssertNotSingleton<ILeadershipGenerator>();
        }

        [Test]
        public void EXTERNAL_DiceIsInjected()
        {
            AssertNotSingleton<Dice>();
        }

        [Test]
        public void EXTERNAL_TreasureGeneratorIsInjected()
        {
            AssertNotSingleton<ITreasureGenerator>();
        }

        [TestCase(ItemTypeConstants.AlchemicalItem)]
        [TestCase(ItemTypeConstants.Armor)]
        [TestCase(ItemTypeConstants.Tool)]
        [TestCase(ItemTypeConstants.Weapon)]
        public void EXTERNAL_MundaneItemGeneratorIsInjected(string name)
        {
            AssertNotSingleton<MundaneItemGenerator>(name);
        }

        [TestCase(ItemTypeConstants.Armor)]
        [TestCase(ItemTypeConstants.Potion)]
        [TestCase(ItemTypeConstants.Ring)]
        [TestCase(ItemTypeConstants.Rod)]
        [TestCase(ItemTypeConstants.Scroll)]
        [TestCase(ItemTypeConstants.Staff)]
        [TestCase(ItemTypeConstants.Wand)]
        [TestCase(ItemTypeConstants.Weapon)]
        [TestCase(ItemTypeConstants.WondrousItem)]
        public void EXTERNAL_MagicalItemGeneratorIsInjected(string name)
        {
            AssertNotSingleton<MagicalItemGenerator>(name);
        }
    }
}