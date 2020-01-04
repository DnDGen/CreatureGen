﻿using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration
{
    public class CreatureAsserter
    {
        private readonly IEnumerable<string> skillsWithFoci;

        public CreatureAsserter()
        {
            skillsWithFoci = new[]
            {
                SkillConstants.Craft,
                SkillConstants.Knowledge,
                SkillConstants.Perform,
                SkillConstants.Profession,
            };
        }

        public void AssertCreature(Creature creature)
        {
            VerifySummary(creature);
            VerifyAlignment(creature);
            VerifyStatistics(creature);
            VerifyAbilities(creature);
            VerifySkills(creature);
            VerifyFeats(creature);
            VerifyCombat(creature);

            Assert.That(creature.ChallengeRating, Is.Not.Empty, creature.Summary);
            Assert.That(creature.CasterLevel, Is.Not.Negative, creature.Summary);
            Assert.That(creature.NumberOfHands, Is.Not.Negative, creature.Summary);
        }

        private void VerifySummary(Creature creature)
        {
            Assert.That(creature.Name, Is.Not.Empty);
            Assert.That(creature.Template, Is.Not.Null);
            Assert.That(creature.Summary, Is.Not.Empty);
            Assert.That(creature.Summary, Contains.Substring(creature.Name));
            Assert.That(creature.Summary, Contains.Substring(creature.Template));
        }

        private void VerifyAlignment(Creature creature)
        {
            Assert.That(creature.Alignment, Is.Not.Null);

            if (!string.IsNullOrEmpty(creature.Alignment.Full))
            {
                Assert.That(creature.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                    .Or.EqualTo(AlignmentConstants.Neutral)
                    .Or.EqualTo(AlignmentConstants.Evil), creature.Summary);
                Assert.That(creature.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                    .Or.EqualTo(AlignmentConstants.Neutral)
                    .Or.EqualTo(AlignmentConstants.Chaotic), creature.Summary);
            }
        }

        private void VerifyStatistics(Creature creature)
        {
            Assert.That(ChallengeRatingConstants.GetOrdered(), Contains.Item(creature.ChallengeRating));
            Assert.That(creature.Size, Is.EqualTo(SizeConstants.Large)
                .Or.EqualTo(SizeConstants.Colossal)
                .Or.EqualTo(SizeConstants.Gargantuan)
                .Or.EqualTo(SizeConstants.Huge)
                .Or.EqualTo(SizeConstants.Tiny)
                .Or.EqualTo(SizeConstants.Diminutive)
                .Or.EqualTo(SizeConstants.Medium)
                .Or.EqualTo(SizeConstants.Small), creature.Summary);

            VerifySpeeds(creature);
        }

        private void VerifySpeeds(Creature creature)
        {
            foreach (var speedKVP in creature.Speeds)
            {
                VerifySpeed(speedKVP.Value, creature.Summary, speedKVP.Key);
            }
        }

        private void VerifySpeed(Measurement speed, string creatureSummary, string name)
        {
            var message = $"{creatureSummary} {name}";
            Assert.That(speed.Value, Is.Not.Negative, message);
            Assert.That(speed.Value % 5, Is.EqualTo(0), message);
            Assert.That(speed.Unit, Is.EqualTo("feet per round"), message);

            if (name == SpeedConstants.Fly)
                Assert.That(speed.Description, Is.Not.Empty, message);
            else
                Assert.That(speed.Description, Is.Empty, message);
        }

        private void VerifyAbilities(Creature creature)
        {
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Charisma), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Constitution), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Dexterity), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Intelligence), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Strength), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Wisdom), creature.Summary);
            Assert.That(creature.Abilities.Count, Is.EqualTo(6), creature.Summary);

            foreach (var statKVP in creature.Abilities)
            {
                var stat = statKVP.Value;
                Assert.That(stat.Name, Is.EqualTo(statKVP.Key), creature.Summary);
                Assert.That(stat.FullScore, Is.Not.Negative, creature.Summary);
            }
        }

        private void VerifySkills(Creature creature)
        {
            Assert.That(creature.Skills, Is.Not.Empty, creature.Summary);

            foreach (var skill in creature.Skills)
            {
                var message = creature.Summary + skill.Name;

                Assert.That(skill.ArmorCheckPenalty, Is.Not.Positive, message);
                Assert.That(skill.Ranks, Is.AtMost(skill.RankCap), message);
                Assert.That(skill.RankCap, Is.Positive, message);
                Assert.That(skill.BaseAbility, Is.Not.Null, message);
                Assert.That(creature.Abilities.Values, Contains.Item(skill.BaseAbility), message);
                Assert.That(skill.Focus, Is.Not.Null, message);

                if (skillsWithFoci.Contains(skill.Name))
                    Assert.That(skill.Focus, Is.Not.Empty, message);
                else
                    Assert.That(skill.Focus, Is.Empty, message);
            }

            var skillNamesAndFoci = creature.Skills.Select(s => s.Name + s.Focus);
            Assert.That(skillNamesAndFoci, Is.Unique);
        }

        private void VerifyFeats(Creature creature)
        {
            Assert.That(creature.Feats, Is.Not.Null, creature.Summary);
            Assert.That(creature.SpecialQualities, Is.Not.Null, creature.Summary);

            var weapons = WeaponConstants.GetAllWeapons();
            var allFeats = creature.Feats.Union(creature.SpecialQualities);

            foreach (var feat in allFeats)
            {
                var message = $"Creature: {creature.Summary}\nFeat: {feat.Name}";

                Assert.That(feat.Name, Is.Not.Empty, message);
                Assert.That(feat.Foci, Is.Not.Null, message);
                Assert.That(feat.Foci, Is.All.Not.Null, message);
                Assert.That(feat.Foci, Is.All.Not.EqualTo(FeatConstants.Foci.NoValidFociAvailable), message);
                Assert.That(feat.Power, Is.Not.Negative, message);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, message);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Hit)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.EqualTo(FeatConstants.Frequencies.Minute)
                    .Or.EqualTo(FeatConstants.Frequencies.Hour)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.EqualTo(FeatConstants.Frequencies.Month)
                    .Or.EqualTo(FeatConstants.Frequencies.Year)
                    .Or.EqualTo(FeatConstants.Frequencies.Life)
                    .Or.Empty, message);

                if (!creature.CanUseEquipment)
                {
                    var weaponFoci = feat.Foci.Intersect(weapons);
                    Assert.That(weaponFoci, Is.Empty, message);

                    //TODO: Also should assert that equipment is empty, but equipment does not exist on creatures yet
                    //add it once we have added that
                }
            }
        }

        private void VerifyCombat(Creature creature)
        {
            Assert.That(creature.BaseAttackBonus, Is.Not.Negative, creature.Summary);

            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Positive, creature.Summary);
            Assert.That(creature.HitPoints.HitDie, Is.Positive, creature.Summary);
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.AtLeast(1), creature.Summary);
            Assert.That(creature.HitPoints.DefaultRoll, Contains.Substring($"{creature.HitPoints.RoundedHitDiceQuantity}d{creature.HitPoints.HitDie}"), creature.Summary);
            Assert.That(creature.HitPoints.Total, Is.Positive
                .And.AtLeast(creature.HitPoints.HitDiceQuantity)
                .And.AtLeast(creature.HitPoints.RoundedHitDiceQuantity), creature.Summary);
            Assert.That(creature.HitPoints.DefaultTotal, Is.Positive
                .And.AtLeast(creature.HitPoints.HitDiceQuantity)
                .And.AtLeast(creature.HitPoints.RoundedHitDiceQuantity), creature.Summary);

            Assert.That(creature.FullMeleeAttack, Is.Not.Null, creature.Summary);
            Assert.That(creature.FullRangedAttack, Is.Not.Null, creature.Summary);

            if (creature.MeleeAttack != null)
            {
                Assert.That(creature.MeleeAttack.IsMelee, Is.True, creature.Summary);
                Assert.That(creature.MeleeAttack.IsSpecial, Is.False, creature.Summary);
                Assert.That(creature.FullMeleeAttack, Is.Not.Empty, creature.Summary);
                Assert.That(creature.FullMeleeAttack.All(a => a.IsMelee && !a.IsSpecial), Is.True, creature.Summary);
            }

            if (creature.RangedAttack != null)
            {
                Assert.That(creature.RangedAttack.IsMelee, Is.False, creature.Summary);
                Assert.That(creature.RangedAttack.IsSpecial, Is.False, creature.Summary);
                Assert.That(creature.FullRangedAttack, Is.Not.Empty, creature.Summary);
                Assert.That(creature.FullRangedAttack.All(a => !a.IsMelee && !a.IsSpecial), Is.True, creature.Summary);
            }

            foreach (var attack in creature.Attacks)
                AssertAttack(attack, creature);

            Assert.That(creature.ArmorClass.TotalBonus, Is.Positive, creature.Summary);
            Assert.That(creature.ArmorClass.FlatFootedBonus, Is.Positive, creature.Summary);
            Assert.That(creature.ArmorClass.TouchBonus, Is.Positive, creature.Summary);

            Assert.That(creature.InitiativeBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Dexterity].Modifier), creature.Summary);

            Assert.That(creature.Saves[SaveConstants.Reflex].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Dexterity].Modifier), creature.Summary);
            Assert.That(creature.Saves[SaveConstants.Will].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Wisdom].Modifier), creature.Summary);
            Assert.That(creature.Saves[SaveConstants.Fortitude].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Constitution].Modifier), creature.Summary);
        }

        private void AssertAttack(Attack attack, Creature creature)
        {
            var message = $"Creature: {creature.Summary}\nAttack: {attack.Name}";
            Assert.That(attack.Name, Is.Not.Empty, message);
            Assert.That(attack.AttackType, Is.Not.Empty, message);
            Assert.That(attack.BaseAttackBonus, Is.Not.Negative, message);
            Assert.That(attack.Frequency, Is.Not.Null, message);
            Assert.That(attack.Frequency.Quantity, Is.Positive, message);
            Assert.That(attack.Frequency.TimePeriod, Contains.Substring(FeatConstants.Frequencies.Round)
                .Or.Contains(FeatConstants.Frequencies.Hit)
                .Or.Contains(FeatConstants.Frequencies.Minute)
                .Or.Contains(FeatConstants.Frequencies.Hour)
                .Or.Contains(FeatConstants.Frequencies.Day)
                .Or.Contains(FeatConstants.Frequencies.Week)
                .Or.Contains(FeatConstants.Frequencies.Month)
                .Or.Contains(FeatConstants.Frequencies.Year)
                .Or.Contains(FeatConstants.Frequencies.Life), message);

            if (!attack.IsNatural)
            {
                Assert.That(creature.CanUseEquipment, Is.True, message);
            }

            if (attack.IsPrimary)
            {
                Assert.That(attack.SecondaryAttackPenalty, Is.Zero);
            }

            if (!attack.IsSpecial)
            {
                Assert.That(attack.BaseAbility, Is.Not.Null, message);
                Assert.That(creature.Abilities.Values, Contains.Item(attack.BaseAbility), message);

                if (attack.IsNatural)
                {
                    Assert.That(attack.Damage, Is.Not.Empty, message);
                }
            }

            if (attack.Save != null)
            {
                Assert.That(creature.Abilities.Values, Contains.Item(attack.Save.BaseAbility), message);
                Assert.That(attack.Save.BaseValue, Is.Positive, message);
                Assert.That(attack.Save.DC, Is.Positive, message);
                Assert.That(attack.Save.Save, Is.EqualTo(SaveConstants.Fortitude)
                    .Or.EqualTo(SaveConstants.Reflex)
                    .Or.EqualTo(SaveConstants.Will)
                    .Or.Empty, message);
            }
        }
    }
}
