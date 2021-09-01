﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class NoneApplicatorTests
    {
        private TemplateApplicator templateApplicator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();

            templateApplicator = new NoneApplicator(mockCollectionSelector.Object, mockCreatureDataSelector.Object, mockAdjustmentSelector.Object);
        }

        [Test]
        public void DoNotAlterCreature()
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var templatedCreature = templateApplicator.ApplyTo(clone);
            Assert.That(templatedCreature, Is.EqualTo(clone));
            Assert.That(templatedCreature.Abilities, Has.Count.EqualTo(creature.Abilities.Count));
            Assert.That(templatedCreature.Abilities.Keys, Is.EquivalentTo(creature.Abilities.Keys));

            foreach (var kvp in creature.Abilities)
            {
                Assert.That(templatedCreature.Abilities[kvp.Key].AdvancementAdjustment, Is.EqualTo(kvp.Value.AdvancementAdjustment));
                Assert.That(templatedCreature.Abilities[kvp.Key].BaseScore, Is.EqualTo(kvp.Value.BaseScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].FullScore, Is.EqualTo(kvp.Value.FullScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].HasScore, Is.EqualTo(kvp.Value.HasScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].Modifier, Is.EqualTo(kvp.Value.Modifier));
                Assert.That(templatedCreature.Abilities[kvp.Key].Name, Is.EqualTo(kvp.Value.Name).And.EqualTo(kvp.Key));
                Assert.That(templatedCreature.Abilities[kvp.Key].RacialAdjustment, Is.EqualTo(kvp.Value.RacialAdjustment));
            }

            Assert.That(templatedCreature.Alignment, Is.Not.Null);
            Assert.That(templatedCreature.Alignment.Full, Is.EqualTo(creature.Alignment.Full));
            Assert.That(templatedCreature.Alignment.Goodness, Is.EqualTo(creature.Alignment.Goodness));
            Assert.That(templatedCreature.Alignment.Lawfulness, Is.EqualTo(creature.Alignment.Lawfulness));

            Assert.That(templatedCreature.ArmorClass, Is.Not.Null);
            Assert.That(templatedCreature.ArmorClass.ArmorBonus, Is.EqualTo(creature.ArmorClass.ArmorBonus));
            Assert.That(templatedCreature.ArmorClass.ArmorBonuses, Is.EqualTo(creature.ArmorClass.ArmorBonuses));
            Assert.That(templatedCreature.ArmorClass.Bonuses, Is.EqualTo(creature.ArmorClass.Bonuses));
            Assert.That(templatedCreature.ArmorClass.DeflectionBonus, Is.EqualTo(creature.ArmorClass.DeflectionBonus));
            Assert.That(templatedCreature.ArmorClass.DeflectionBonuses, Is.EqualTo(creature.ArmorClass.DeflectionBonuses));
            Assert.That(templatedCreature.ArmorClass.Dexterity, Is.EqualTo(templatedCreature.Abilities[AbilityConstants.Dexterity]));
            Assert.That(templatedCreature.ArmorClass.DexterityBonus, Is.EqualTo(creature.ArmorClass.DexterityBonus));
            Assert.That(templatedCreature.ArmorClass.DodgeBonus, Is.EqualTo(creature.ArmorClass.DodgeBonus));
            Assert.That(templatedCreature.ArmorClass.DodgeBonuses, Is.EqualTo(creature.ArmorClass.DodgeBonuses));
            Assert.That(templatedCreature.ArmorClass.FlatFootedBonus, Is.EqualTo(creature.ArmorClass.FlatFootedBonus));
            Assert.That(templatedCreature.ArmorClass.IsConditional, Is.EqualTo(creature.ArmorClass.IsConditional));
            Assert.That(templatedCreature.ArmorClass.MaxDexterityBonus, Is.EqualTo(creature.ArmorClass.MaxDexterityBonus));
            Assert.That(templatedCreature.ArmorClass.NaturalArmorBonus, Is.EqualTo(creature.ArmorClass.NaturalArmorBonus));
            Assert.That(templatedCreature.ArmorClass.NaturalArmorBonuses, Is.EqualTo(creature.ArmorClass.NaturalArmorBonuses));
            Assert.That(templatedCreature.ArmorClass.ShieldBonus, Is.EqualTo(creature.ArmorClass.ShieldBonus));
            Assert.That(templatedCreature.ArmorClass.ShieldBonuses, Is.EqualTo(creature.ArmorClass.ShieldBonuses));
            Assert.That(templatedCreature.ArmorClass.SizeModifier, Is.EqualTo(creature.ArmorClass.SizeModifier));
            Assert.That(templatedCreature.ArmorClass.TotalBonus, Is.EqualTo(creature.ArmorClass.TotalBonus));
            Assert.That(templatedCreature.ArmorClass.TouchBonus, Is.EqualTo(creature.ArmorClass.TouchBonus));

            Assert.That(templatedCreature.Attacks.Count(), Is.EqualTo(creature.Attacks.Count()));
            foreach (var attack in creature.Attacks)
            {
                var templatedAttack = templatedCreature.Attacks.FirstOrDefault(a => a.Name == attack.Name);
                Assert.That(templatedAttack, Is.Not.Null);
                Assert.That(templatedAttack.FullAttackBonuses, Is.EqualTo(attack.FullAttackBonuses));
                Assert.That(templatedAttack.AttackType, Is.EqualTo(attack.AttackType));
                Assert.That(templatedAttack.BaseAbility, Is.EqualTo(templatedCreature.Abilities[attack.BaseAbility.Name]));
                Assert.That(templatedAttack.BaseAttackBonus, Is.EqualTo(attack.BaseAttackBonus));
                Assert.That(templatedAttack.DamageDescription, Is.EqualTo(attack.DamageDescription));
                Assert.That(templatedAttack.DamageBonus, Is.EqualTo(attack.DamageBonus));
                Assert.That(templatedAttack.DamageEffect, Is.EqualTo(attack.DamageEffect));
                Assert.That(templatedAttack.Frequency.Quantity, Is.EqualTo(attack.Frequency.Quantity));
                Assert.That(templatedAttack.Frequency.TimePeriod, Is.EqualTo(attack.Frequency.TimePeriod));
                Assert.That(templatedAttack.IsMelee, Is.EqualTo(attack.IsMelee));
                Assert.That(templatedAttack.IsNatural, Is.EqualTo(attack.IsNatural));
                Assert.That(templatedAttack.IsPrimary, Is.EqualTo(attack.IsPrimary));
                Assert.That(templatedAttack.IsSpecial, Is.EqualTo(attack.IsSpecial));
                Assert.That(templatedAttack.Name, Is.EqualTo(attack.Name));
                Assert.That(templatedAttack.Save, Is.EqualTo(attack.Save));
                Assert.That(templatedAttack.AttackBonuses, Is.EqualTo(attack.AttackBonuses));
                Assert.That(templatedAttack.SizeModifier, Is.EqualTo(attack.SizeModifier));
                Assert.That(templatedAttack.TotalAttackBonus, Is.EqualTo(attack.TotalAttackBonus));
            }

            Assert.That(templatedCreature.BaseAttackBonus, Is.EqualTo(creature.BaseAttackBonus));
            Assert.That(templatedCreature.CanUseEquipment, Is.EqualTo(creature.CanUseEquipment));
            Assert.That(templatedCreature.CasterLevel, Is.EqualTo(creature.CasterLevel));
            Assert.That(templatedCreature.ChallengeRating, Is.EqualTo(creature.ChallengeRating));

            Assert.That(templatedCreature.Feats.Count(), Is.EqualTo(creature.Feats.Count()));
            foreach (var feat in creature.Feats)
            {
                var templatedFeat = templatedCreature.Feats.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(templatedCreature.GrappleBonus, Is.EqualTo(creature.GrappleBonus));
            Assert.That(templatedCreature.HitPoints.Bonus, Is.EqualTo(creature.HitPoints.Bonus));
            Assert.That(templatedCreature.HitPoints.Constitution, Is.EqualTo(templatedCreature.Abilities[AbilityConstants.Constitution]));
            Assert.That(templatedCreature.HitPoints.DefaultRoll, Is.EqualTo(creature.HitPoints.DefaultRoll));
            Assert.That(templatedCreature.HitPoints.DefaultTotal, Is.EqualTo(creature.HitPoints.DefaultTotal));
            Assert.That(templatedCreature.HitPoints.HitDiceQuantity, Is.EqualTo(creature.HitPoints.HitDiceQuantity));
            Assert.That(templatedCreature.HitPoints.HitDice, Has.Count.EqualTo(creature.HitPoints.HitDice.Count));

            for (var i = 0; i < templatedCreature.HitPoints.HitDice.Count; i++)
            {
                Assert.That(templatedCreature.HitPoints.HitDice[i].Quantity, Is.EqualTo(creature.HitPoints.HitDice[i].Quantity));
                Assert.That(templatedCreature.HitPoints.HitDice[i].HitDie, Is.EqualTo(creature.HitPoints.HitDice[i].HitDie));
            }

            Assert.That(templatedCreature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(creature.HitPoints.RoundedHitDiceQuantity));
            Assert.That(templatedCreature.HitPoints.Total, Is.EqualTo(creature.HitPoints.Total));

            Assert.That(templatedCreature.TotalInitiativeBonus, Is.EqualTo(creature.TotalInitiativeBonus));
            Assert.That(templatedCreature.LevelAdjustment, Is.EqualTo(creature.LevelAdjustment));
            Assert.That(templatedCreature.Name, Is.EqualTo(creature.Name));
            Assert.That(templatedCreature.NumberOfHands, Is.EqualTo(creature.NumberOfHands));

            Assert.That(templatedCreature.Reach.Description, Is.EqualTo(creature.Reach.Description));
            Assert.That(templatedCreature.Reach.Unit, Is.EqualTo(creature.Reach.Unit));
            Assert.That(templatedCreature.Reach.Value, Is.EqualTo(creature.Reach.Value));

            Assert.That(templatedCreature.Saves, Has.Count.EqualTo(creature.Saves.Count));
            Assert.That(templatedCreature.Saves.Keys, Is.EquivalentTo(creature.Saves.Keys));

            foreach (var kvp in creature.Saves)
            {
                Assert.That(templatedCreature.Saves[kvp.Key].BaseAbility, Is.EqualTo(templatedCreature.Abilities[kvp.Value.BaseAbility.Name]));
                Assert.That(templatedCreature.Saves[kvp.Key].BaseValue, Is.EqualTo(kvp.Value.BaseValue));
                Assert.That(templatedCreature.Saves[kvp.Key].Bonus, Is.EqualTo(kvp.Value.Bonus));
                Assert.That(templatedCreature.Saves[kvp.Key].Bonuses, Is.EqualTo(kvp.Value.Bonuses));
                Assert.That(templatedCreature.Saves[kvp.Key].HasSave, Is.EqualTo(kvp.Value.HasSave));
                Assert.That(templatedCreature.Saves[kvp.Key].IsConditional, Is.EqualTo(kvp.Value.IsConditional));
                Assert.That(templatedCreature.Saves[kvp.Key].TotalBonus, Is.EqualTo(kvp.Value.TotalBonus));
            }

            Assert.That(templatedCreature.Size, Is.EqualTo(creature.Size));
            Assert.That(templatedCreature.Skills.Count(), Is.EqualTo(creature.Skills.Count()));
            foreach (var skill in creature.Skills)
            {
                var templatedSkill = templatedCreature.Skills.FirstOrDefault(a => a.Name == skill.Name);
                Assert.That(templatedSkill, Is.Not.Null);
                Assert.That(templatedSkill.ArmorCheckPenalty, Is.EqualTo(skill.ArmorCheckPenalty));
                Assert.That(templatedSkill.BaseAbility, Is.EqualTo(templatedCreature.Abilities[skill.BaseAbility.Name]));
                Assert.That(templatedSkill.Bonus, Is.EqualTo(skill.Bonus));
                Assert.That(templatedSkill.Bonuses, Is.EqualTo(skill.Bonuses));
                Assert.That(templatedSkill.CircumstantialBonus, Is.EqualTo(skill.CircumstantialBonus));
                Assert.That(templatedSkill.ClassSkill, Is.EqualTo(skill.ClassSkill));
                Assert.That(templatedSkill.EffectiveRanks, Is.EqualTo(skill.EffectiveRanks));
                Assert.That(templatedSkill.Focus, Is.EqualTo(skill.Focus));
                Assert.That(templatedSkill.HasArmorCheckPenalty, Is.EqualTo(skill.HasArmorCheckPenalty));
                Assert.That(templatedSkill.Key, Is.EqualTo(skill.Key));
                Assert.That(templatedSkill.Name, Is.EqualTo(skill.Name));
                Assert.That(templatedSkill.QualifiesForSkillSynergy, Is.EqualTo(skill.QualifiesForSkillSynergy));
                Assert.That(templatedSkill.RankCap, Is.EqualTo(skill.RankCap));
                Assert.That(templatedSkill.Ranks, Is.EqualTo(skill.Ranks));
                Assert.That(templatedSkill.RanksMaxedOut, Is.EqualTo(skill.RanksMaxedOut));
                Assert.That(templatedSkill.TotalBonus, Is.EqualTo(skill.TotalBonus));
            }

            Assert.That(templatedCreature.Space.Description, Is.EqualTo(creature.Space.Description));
            Assert.That(templatedCreature.Space.Unit, Is.EqualTo(creature.Space.Unit));
            Assert.That(templatedCreature.Space.Value, Is.EqualTo(creature.Space.Value));

            Assert.That(templatedCreature.SpecialQualities.Count(), Is.EqualTo(creature.SpecialQualities.Count()));
            foreach (var feat in creature.SpecialQualities)
            {
                var templatedFeat = templatedCreature.SpecialQualities.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(templatedCreature.Speeds, Has.Count.EqualTo(creature.Speeds.Count));
            Assert.That(templatedCreature.Speeds.Keys, Is.EquivalentTo(creature.Speeds.Keys));

            foreach (var kvp in creature.Speeds)
            {
                Assert.That(templatedCreature.Speeds[kvp.Key].Description, Is.EqualTo(kvp.Value.Description));
                Assert.That(templatedCreature.Speeds[kvp.Key].Unit, Is.EqualTo(kvp.Value.Unit));
                Assert.That(templatedCreature.Speeds[kvp.Key].Value, Is.EqualTo(kvp.Value.Value));
            }

            Assert.That(templatedCreature.Summary, Is.EqualTo(creature.Summary));
            Assert.That(templatedCreature.Template, Is.EqualTo(creature.Template).And.EqualTo(CreatureConstants.Templates.None));
            Assert.That(templatedCreature.Type.Name, Is.EqualTo(creature.Type.Name));
            Assert.That(templatedCreature.Type.SubTypes, Is.EquivalentTo(creature.Type.SubTypes));
        }

        [Test]
        public async Task ApplyToAsync_DoNotAlterCreature()
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var templatedCreature = await templateApplicator.ApplyToAsync(clone);
            Assert.That(templatedCreature, Is.EqualTo(clone));
            Assert.That(templatedCreature.Abilities, Has.Count.EqualTo(creature.Abilities.Count));
            Assert.That(templatedCreature.Abilities.Keys, Is.EquivalentTo(creature.Abilities.Keys));

            foreach (var kvp in creature.Abilities)
            {
                Assert.That(templatedCreature.Abilities[kvp.Key].AdvancementAdjustment, Is.EqualTo(kvp.Value.AdvancementAdjustment));
                Assert.That(templatedCreature.Abilities[kvp.Key].BaseScore, Is.EqualTo(kvp.Value.BaseScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].FullScore, Is.EqualTo(kvp.Value.FullScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].HasScore, Is.EqualTo(kvp.Value.HasScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].Modifier, Is.EqualTo(kvp.Value.Modifier));
                Assert.That(templatedCreature.Abilities[kvp.Key].Name, Is.EqualTo(kvp.Value.Name).And.EqualTo(kvp.Key));
                Assert.That(templatedCreature.Abilities[kvp.Key].RacialAdjustment, Is.EqualTo(kvp.Value.RacialAdjustment));
            }

            Assert.That(templatedCreature.Alignment, Is.Not.Null);
            Assert.That(templatedCreature.Alignment.Full, Is.EqualTo(creature.Alignment.Full));
            Assert.That(templatedCreature.Alignment.Goodness, Is.EqualTo(creature.Alignment.Goodness));
            Assert.That(templatedCreature.Alignment.Lawfulness, Is.EqualTo(creature.Alignment.Lawfulness));

            Assert.That(templatedCreature.ArmorClass, Is.Not.Null);
            Assert.That(templatedCreature.ArmorClass.ArmorBonus, Is.EqualTo(creature.ArmorClass.ArmorBonus));
            Assert.That(templatedCreature.ArmorClass.ArmorBonuses, Is.EqualTo(creature.ArmorClass.ArmorBonuses));
            Assert.That(templatedCreature.ArmorClass.Bonuses, Is.EqualTo(creature.ArmorClass.Bonuses));
            Assert.That(templatedCreature.ArmorClass.DeflectionBonus, Is.EqualTo(creature.ArmorClass.DeflectionBonus));
            Assert.That(templatedCreature.ArmorClass.DeflectionBonuses, Is.EqualTo(creature.ArmorClass.DeflectionBonuses));
            Assert.That(templatedCreature.ArmorClass.Dexterity, Is.EqualTo(templatedCreature.Abilities[AbilityConstants.Dexterity]));
            Assert.That(templatedCreature.ArmorClass.DexterityBonus, Is.EqualTo(creature.ArmorClass.DexterityBonus));
            Assert.That(templatedCreature.ArmorClass.DodgeBonus, Is.EqualTo(creature.ArmorClass.DodgeBonus));
            Assert.That(templatedCreature.ArmorClass.DodgeBonuses, Is.EqualTo(creature.ArmorClass.DodgeBonuses));
            Assert.That(templatedCreature.ArmorClass.FlatFootedBonus, Is.EqualTo(creature.ArmorClass.FlatFootedBonus));
            Assert.That(templatedCreature.ArmorClass.IsConditional, Is.EqualTo(creature.ArmorClass.IsConditional));
            Assert.That(templatedCreature.ArmorClass.MaxDexterityBonus, Is.EqualTo(creature.ArmorClass.MaxDexterityBonus));
            Assert.That(templatedCreature.ArmorClass.NaturalArmorBonus, Is.EqualTo(creature.ArmorClass.NaturalArmorBonus));
            Assert.That(templatedCreature.ArmorClass.NaturalArmorBonuses, Is.EqualTo(creature.ArmorClass.NaturalArmorBonuses));
            Assert.That(templatedCreature.ArmorClass.ShieldBonus, Is.EqualTo(creature.ArmorClass.ShieldBonus));
            Assert.That(templatedCreature.ArmorClass.ShieldBonuses, Is.EqualTo(creature.ArmorClass.ShieldBonuses));
            Assert.That(templatedCreature.ArmorClass.SizeModifier, Is.EqualTo(creature.ArmorClass.SizeModifier));
            Assert.That(templatedCreature.ArmorClass.TotalBonus, Is.EqualTo(creature.ArmorClass.TotalBonus));
            Assert.That(templatedCreature.ArmorClass.TouchBonus, Is.EqualTo(creature.ArmorClass.TouchBonus));

            Assert.That(templatedCreature.Attacks.Count(), Is.EqualTo(creature.Attacks.Count()));
            foreach (var attack in creature.Attacks)
            {
                var templatedAttack = templatedCreature.Attacks.FirstOrDefault(a => a.Name == attack.Name);
                Assert.That(templatedAttack, Is.Not.Null);
                Assert.That(templatedAttack.FullAttackBonuses, Is.EqualTo(attack.FullAttackBonuses));
                Assert.That(templatedAttack.AttackType, Is.EqualTo(attack.AttackType));
                Assert.That(templatedAttack.BaseAbility, Is.EqualTo(templatedCreature.Abilities[attack.BaseAbility.Name]));
                Assert.That(templatedAttack.BaseAttackBonus, Is.EqualTo(attack.BaseAttackBonus));
                Assert.That(templatedAttack.DamageDescription, Is.EqualTo(attack.DamageDescription));
                Assert.That(templatedAttack.DamageBonus, Is.EqualTo(attack.DamageBonus));
                Assert.That(templatedAttack.DamageEffect, Is.EqualTo(attack.DamageEffect));
                Assert.That(templatedAttack.Frequency.Quantity, Is.EqualTo(attack.Frequency.Quantity));
                Assert.That(templatedAttack.Frequency.TimePeriod, Is.EqualTo(attack.Frequency.TimePeriod));
                Assert.That(templatedAttack.IsMelee, Is.EqualTo(attack.IsMelee));
                Assert.That(templatedAttack.IsNatural, Is.EqualTo(attack.IsNatural));
                Assert.That(templatedAttack.IsPrimary, Is.EqualTo(attack.IsPrimary));
                Assert.That(templatedAttack.IsSpecial, Is.EqualTo(attack.IsSpecial));
                Assert.That(templatedAttack.Name, Is.EqualTo(attack.Name));
                Assert.That(templatedAttack.Save, Is.EqualTo(attack.Save));
                Assert.That(templatedAttack.AttackBonuses, Is.EqualTo(attack.AttackBonuses));
                Assert.That(templatedAttack.SizeModifier, Is.EqualTo(attack.SizeModifier));
                Assert.That(templatedAttack.TotalAttackBonus, Is.EqualTo(attack.TotalAttackBonus));
            }

            Assert.That(templatedCreature.BaseAttackBonus, Is.EqualTo(creature.BaseAttackBonus));
            Assert.That(templatedCreature.CanUseEquipment, Is.EqualTo(creature.CanUseEquipment));
            Assert.That(templatedCreature.CasterLevel, Is.EqualTo(creature.CasterLevel));
            Assert.That(templatedCreature.ChallengeRating, Is.EqualTo(creature.ChallengeRating));

            Assert.That(templatedCreature.Feats.Count(), Is.EqualTo(creature.Feats.Count()));
            foreach (var feat in creature.Feats)
            {
                var templatedFeat = templatedCreature.Feats.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(templatedCreature.GrappleBonus, Is.EqualTo(creature.GrappleBonus));
            Assert.That(templatedCreature.HitPoints.Bonus, Is.EqualTo(creature.HitPoints.Bonus));
            Assert.That(templatedCreature.HitPoints.Constitution, Is.EqualTo(templatedCreature.Abilities[AbilityConstants.Constitution]));
            Assert.That(templatedCreature.HitPoints.DefaultRoll, Is.EqualTo(creature.HitPoints.DefaultRoll));
            Assert.That(templatedCreature.HitPoints.DefaultTotal, Is.EqualTo(creature.HitPoints.DefaultTotal));
            Assert.That(templatedCreature.HitPoints.HitDiceQuantity, Is.EqualTo(creature.HitPoints.HitDiceQuantity));
            Assert.That(templatedCreature.HitPoints.HitDice, Has.Count.EqualTo(creature.HitPoints.HitDice.Count));

            for (var i = 0; i < templatedCreature.HitPoints.HitDice.Count; i++)
            {
                Assert.That(templatedCreature.HitPoints.HitDice[i].Quantity, Is.EqualTo(creature.HitPoints.HitDice[i].Quantity));
                Assert.That(templatedCreature.HitPoints.HitDice[i].HitDie, Is.EqualTo(creature.HitPoints.HitDice[i].HitDie));
            }

            Assert.That(templatedCreature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(creature.HitPoints.RoundedHitDiceQuantity));
            Assert.That(templatedCreature.HitPoints.Total, Is.EqualTo(creature.HitPoints.Total));

            Assert.That(templatedCreature.InitiativeBonus, Is.EqualTo(creature.InitiativeBonus));
            Assert.That(templatedCreature.TotalInitiativeBonus, Is.EqualTo(creature.TotalInitiativeBonus));
            Assert.That(templatedCreature.LevelAdjustment, Is.EqualTo(creature.LevelAdjustment));
            Assert.That(templatedCreature.Name, Is.EqualTo(creature.Name));
            Assert.That(templatedCreature.NumberOfHands, Is.EqualTo(creature.NumberOfHands));

            Assert.That(templatedCreature.Reach.Description, Is.EqualTo(creature.Reach.Description));
            Assert.That(templatedCreature.Reach.Unit, Is.EqualTo(creature.Reach.Unit));
            Assert.That(templatedCreature.Reach.Value, Is.EqualTo(creature.Reach.Value));

            Assert.That(templatedCreature.Saves, Has.Count.EqualTo(creature.Saves.Count));
            Assert.That(templatedCreature.Saves.Keys, Is.EquivalentTo(creature.Saves.Keys));

            foreach (var kvp in creature.Saves)
            {
                Assert.That(templatedCreature.Saves[kvp.Key].BaseAbility, Is.EqualTo(templatedCreature.Abilities[kvp.Value.BaseAbility.Name]));
                Assert.That(templatedCreature.Saves[kvp.Key].BaseValue, Is.EqualTo(kvp.Value.BaseValue));
                Assert.That(templatedCreature.Saves[kvp.Key].Bonus, Is.EqualTo(kvp.Value.Bonus));
                Assert.That(templatedCreature.Saves[kvp.Key].Bonuses, Is.EqualTo(kvp.Value.Bonuses));
                Assert.That(templatedCreature.Saves[kvp.Key].HasSave, Is.EqualTo(kvp.Value.HasSave));
                Assert.That(templatedCreature.Saves[kvp.Key].IsConditional, Is.EqualTo(kvp.Value.IsConditional));
                Assert.That(templatedCreature.Saves[kvp.Key].TotalBonus, Is.EqualTo(kvp.Value.TotalBonus));
            }

            Assert.That(templatedCreature.Size, Is.EqualTo(creature.Size));
            Assert.That(templatedCreature.Skills.Count(), Is.EqualTo(creature.Skills.Count()));
            foreach (var skill in creature.Skills)
            {
                var templatedSkill = templatedCreature.Skills.FirstOrDefault(a => a.Name == skill.Name);
                Assert.That(templatedSkill, Is.Not.Null);
                Assert.That(templatedSkill.ArmorCheckPenalty, Is.EqualTo(skill.ArmorCheckPenalty));
                Assert.That(templatedSkill.BaseAbility, Is.EqualTo(templatedCreature.Abilities[skill.BaseAbility.Name]));
                Assert.That(templatedSkill.Bonus, Is.EqualTo(skill.Bonus));
                Assert.That(templatedSkill.Bonuses, Is.EqualTo(skill.Bonuses));
                Assert.That(templatedSkill.CircumstantialBonus, Is.EqualTo(skill.CircumstantialBonus));
                Assert.That(templatedSkill.ClassSkill, Is.EqualTo(skill.ClassSkill));
                Assert.That(templatedSkill.EffectiveRanks, Is.EqualTo(skill.EffectiveRanks));
                Assert.That(templatedSkill.Focus, Is.EqualTo(skill.Focus));
                Assert.That(templatedSkill.HasArmorCheckPenalty, Is.EqualTo(skill.HasArmorCheckPenalty));
                Assert.That(templatedSkill.Key, Is.EqualTo(skill.Key));
                Assert.That(templatedSkill.Name, Is.EqualTo(skill.Name));
                Assert.That(templatedSkill.QualifiesForSkillSynergy, Is.EqualTo(skill.QualifiesForSkillSynergy));
                Assert.That(templatedSkill.RankCap, Is.EqualTo(skill.RankCap));
                Assert.That(templatedSkill.Ranks, Is.EqualTo(skill.Ranks));
                Assert.That(templatedSkill.RanksMaxedOut, Is.EqualTo(skill.RanksMaxedOut));
                Assert.That(templatedSkill.TotalBonus, Is.EqualTo(skill.TotalBonus));
            }

            Assert.That(templatedCreature.Space.Description, Is.EqualTo(creature.Space.Description));
            Assert.That(templatedCreature.Space.Unit, Is.EqualTo(creature.Space.Unit));
            Assert.That(templatedCreature.Space.Value, Is.EqualTo(creature.Space.Value));

            Assert.That(templatedCreature.SpecialQualities.Count(), Is.EqualTo(creature.SpecialQualities.Count()));
            foreach (var feat in creature.SpecialQualities)
            {
                var templatedFeat = templatedCreature.SpecialQualities.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(templatedCreature.Speeds, Has.Count.EqualTo(creature.Speeds.Count));
            Assert.That(templatedCreature.Speeds.Keys, Is.EquivalentTo(creature.Speeds.Keys));

            foreach (var kvp in creature.Speeds)
            {
                Assert.That(templatedCreature.Speeds[kvp.Key].Description, Is.EqualTo(kvp.Value.Description));
                Assert.That(templatedCreature.Speeds[kvp.Key].Unit, Is.EqualTo(kvp.Value.Unit));
                Assert.That(templatedCreature.Speeds[kvp.Key].Value, Is.EqualTo(kvp.Value.Value));
            }

            Assert.That(templatedCreature.Summary, Is.EqualTo(creature.Summary));
            Assert.That(templatedCreature.Template, Is.EqualTo(creature.Template).And.EqualTo(CreatureConstants.Templates.None));
            Assert.That(templatedCreature.Type.Name, Is.EqualTo(creature.Type.Name));
            Assert.That(templatedCreature.Type.SubTypes, Is.EquivalentTo(creature.Type.SubTypes));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsCompatible_ReturnsTrue(bool isCharacter)
        {
            var compatible = templateApplicator.IsCompatible("whatever", isCharacter);
            Assert.That(compatible, Is.True);
        }

        [TestCase(null, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Dragon, false)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase("subtype 1", true)]
        [TestCase("subtype 2", true)]
        [TestCase(CreatureConstants.Types.Subtypes.Native, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal, false)]
        [TestCase("wrong type", false)]
        public void IsCompatible_TypeMustMatch(string type, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            var isCompatible = templateApplicator.IsCompatible("my creature", false, type: type);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch(string original, string challengeRating, bool compatible)
        {
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { ChallengeRating = original });

            var isCompatible = templateApplicator.IsCompatible("my creature", false, challengeRating: challengeRating);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch_HumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(hitDiceQuantity);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { ChallengeRating = original });

            var isCompatible = templateApplicator.IsCompatible("my creature", true, challengeRating: challengeRating);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch_NonHumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(hitDiceQuantity);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { ChallengeRating = original });

            var isCompatible = templateApplicator.IsCompatible("my creature", false, challengeRating: challengeRating);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase("subtype 1", ChallengeRatingConstants.CR2, false)]
        [TestCase("subtype 1", ChallengeRatingConstants.CR1, true)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, false)]
        public void IsCompatible_TypeAndChallengeRatingMustMatch(string type, string challengeRating, bool compatible)
        {
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(2);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 });

            var isCompatible = templateApplicator.IsCompatible("my creature", false, challengeRating: challengeRating, type: type);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void GetPotentialTypes_ReturnOriginalCreatureTypes()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { "my type", "subtype 1", "subtype 2" });

            var types = templateApplicator.GetPotentialTypes("my creature");
            Assert.That(types, Is.EqualTo(new[] { "my type", "subtype 1", "subtype 2" }));
        }

        [Test]
        public void GetPotentialChallengeRating_ReturnOriginalCreatureTypes()
        {
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { ChallengeRating = "my challenge rating" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { "my type", "subtype 1", "subtype 2" });

            var challengeRating = templateApplicator.GetPotentialChallengeRating("my creature", false);
            Assert.That(challengeRating, Is.EqualTo("my challenge rating"));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_HumanoidCharacter))]
        public void GetPotentialChallengeRating_CreatureChallengeRating_ReturnOriginalCreatureTypes_HumanoidCharacter(double hitDiceQuantity, string original, string adjusted)
        {
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(hitDiceQuantity);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { ChallengeRating = original });

            var challengeRating = templateApplicator.GetPotentialChallengeRating("my creature", true);
            Assert.That(challengeRating, Is.EqualTo(adjusted));
        }

        private static IEnumerable ChallengeRatingAdjustments_HumanoidCharacter
        {
            get
            {
                var hitDice = new List<double>(Enumerable.Range(1, 20)
                    .Select(i => Convert.ToDouble(i)));

                hitDice.AddRange(new[]
                {
                    .1, .2, .3, .4, .5, .6, .7, .8, .9,
                });

                var challengeRatings = ChallengeRatingConstants.GetOrdered();

                foreach (var hitDie in hitDice)
                {
                    foreach (var cr in challengeRatings)
                    {
                        var newCr = cr;
                        if (hitDie <= 1)
                        {
                            newCr = ChallengeRatingConstants.CR0;
                        }

                        yield return new TestCaseData(hitDie, cr, newCr);
                    }
                }
            }
        }

        [Test]
        public void GetPotentialChallengeRating_CreatureChallengeRating_ReturnOriginalCreatureTypes_NonHumanoidCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" });

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { ChallengeRating = "my challenge rating" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            var challengeRating = templateApplicator.GetPotentialChallengeRating("my creature", true);
            Assert.That(challengeRating, Is.EqualTo("my challenge rating"));
        }

        [Test]
        public void GetChallengeRatings_ReturnsNull()
        {
            var challengeRatings = templateApplicator.GetChallengeRatings();
            Assert.That(challengeRatings, Is.Null);
        }

        [Test]
        public void GetChallengeRatings_FromChallengeRating_ReturnsAdjustedChallengeRating()
        {
            var challengeRatings = templateApplicator.GetChallengeRatings("my challenge rating");
            Assert.That(challengeRatings, Is.EqualTo(new[] { "my challenge rating" }));
        }

        [Test]
        public void GetHitDiceRange_ReturnsNull()
        {
            var hitDice = templateApplicator.GetHitDiceRange("my challenge rating");
            Assert.That(hitDice.Lower, Is.Null);
            Assert.That(hitDice.Upper, Is.Null);
        }

        [Test]
        public void GetCompatibleCreatures_Tests()
        {
            Assert.Fail("not yet written - need to come up with test cases");
        }
    }
}
