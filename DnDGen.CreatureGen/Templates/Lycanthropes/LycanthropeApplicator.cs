﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal abstract class LycanthropeApplicator : TemplateApplicator
    {
        protected abstract string LycanthropeSpecies { get; }
        protected abstract string AnimalSpecies { get; }

        private readonly ICollectionSelector collectionSelector;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly IHitPointsGenerator hitPointsGenerator;
        private readonly Dice dice;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IFeatsGenerator featsGenerator;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly ISavesGenerator savesGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IEnumerable<string> creatureTypes;

        public LycanthropeApplicator(
            ICollectionSelector collectionSelector,
            ICreatureDataSelector creatureDataSelector,
            IHitPointsGenerator hitPointsGenerator,
            Dice dice,
            ITypeAndAmountSelector typeAndAmountSelector,
            IFeatsGenerator featsGenerator,
            IAttacksGenerator attacksGenerator,
            ISavesGenerator savesGenerator,
            ISkillsGenerator skillsGenerator,
            ISpeedsGenerator speedsGenerator)
        {
            this.collectionSelector = collectionSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.hitPointsGenerator = hitPointsGenerator;
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.featsGenerator = featsGenerator;
            this.attacksGenerator = attacksGenerator;
            this.savesGenerator = savesGenerator;
            this.skillsGenerator = skillsGenerator;
            this.speedsGenerator = speedsGenerator;

            creatureTypes = new[]
            {
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
            };
        }

        public bool IsCompatible(string creature)
        {
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);
            if (!creatureTypes.Contains(types.First()))
                return false;

            var creatureData = creatureDataSelector.SelectFor(creature);
            var animalData = creatureDataSelector.SelectFor(AnimalSpecies);
            var sizes = SizeConstants.GetOrdered();
            var creatureIndex = Array.IndexOf(sizes, creatureData.Size);
            var animalIndex = Array.IndexOf(sizes, animalData.Size);

            if (Math.Abs(creatureIndex - animalIndex) > 1)
                return false;

            return true;
        }

        public Creature ApplyTo(Creature creature)
        {
            var animalCreatureType = new CreatureType { Name = CreatureConstants.Types.Animal };
            var animalData = creatureDataSelector.SelectFor(AnimalSpecies);

            // Creature type
            UpdateCreatureType(creature);

            // Abilities
            UpdateCreatureAbilities(creature);

            //INFO: This depends on abilities
            //Hit Points
            var animalHitPoints = UpdateCreatureHitPoints(creature, animalCreatureType, animalData);

            //INFO: This depends on hit points
            //Skills
            UpdateCreatureSkills(creature, animalCreatureType, animalHitPoints, animalData);

            //INFO: This depends on skills
            // Special Qualities
            UpdateCreatureSpecialQualities(creature, animalCreatureType, animalHitPoints, animalData);

            //INFO: This depends on special qualities
            // Attacks
            UpdateCreatureAttacks(creature, animalCreatureType, animalHitPoints, animalData);

            return creature;
        }

        private void UpdateCreatureType(Creature creature)
        {
            creature.Type.SubTypes = creature.Type.SubTypes.Union(new[]
            {
                CreatureConstants.Types.Subtypes.Shapechanger,
            });
        }

        private void UpdateCreatureAbilities(Creature creature)
        {
            if (creature.Abilities[AbilityConstants.Wisdom].HasScore)
                creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment = 2;

            var animalAbilityAdjustments = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, AnimalSpecies);
            var physicalAbilities = new[] { AbilityConstants.Strength, AbilityConstants.Constitution, AbilityConstants.Dexterity };

            foreach (var adjustment in animalAbilityAdjustments.Where(a => physicalAbilities.Contains(a.Type)))
            {
                creature.Abilities[adjustment.Type].Bonuses.Add(new Bonus
                {
                    Value = adjustment.Amount,
                    Condition = "In Animal or Hybrid form",
                });
            }
        }

        private HitPoints UpdateCreatureHitPoints(Creature creature, CreatureType animalCreatureType, CreatureDataSelection animalData)
        {
            var animalHitPoints = hitPointsGenerator.GenerateFor(AnimalSpecies, animalCreatureType, creature.Abilities[AbilityConstants.Constitution], animalData.Size);
            creature.HitPoints.HitDice.Add(animalHitPoints.HitDice[0]);

            creature.HitPoints.RollTotal(dice);
            creature.HitPoints.RollDefaultTotal(dice);

            return animalHitPoints;
        }

        private void UpdateCreatureSkills(Creature creature, CreatureType animalCreatureType, HitPoints animalHitPoints, CreatureDataSelection animalData)
        {
            var animalSkills = skillsGenerator.GenerateFor(
                animalHitPoints,
                AnimalSpecies,
                animalCreatureType,
                creature.Abilities,
                creature.CanUseEquipment,
                animalData.Size,
                false);

            if (LycanthropeSpecies.Contains("Afflicted"))
            {
                var controlShape = new Skill(
                    SkillConstants.Special.ControlShape,
                    creature.Abilities[AbilityConstants.Wisdom],
                    creature.HitPoints.RoundedHitDiceQuantity + 3);

                animalSkills = animalSkills.Union(new[] { controlShape });

                foreach (var animalSkill in animalSkills)
                {
                    animalSkill.Ranks = 0;
                }

                animalSkills = skillsGenerator.ApplySkillPointsAsRanks(
                    animalSkills,
                    animalHitPoints,
                    animalCreatureType,
                    creature.Abilities,
                    false);
            }

            foreach (var creatureSkill in creature.Skills)
            {
                creatureSkill.RankCap = creature.HitPoints.RoundedHitDiceQuantity + 3;
            }

            foreach (var animalSkill in animalSkills)
            {
                animalSkill.RankCap = creature.HitPoints.RoundedHitDiceQuantity + 3;

                if (creature.Skills.Any(s => s.Key == animalSkill.Key))
                {
                    var creatureSkill = creature.Skills.First(s => s.Key == animalSkill.Key);
                    creatureSkill.Ranks += animalSkill.Ranks;

                    foreach (var bonus in animalSkill.Bonuses)
                    {
                        creatureSkill.AddBonus(bonus.Value, bonus.Condition);
                    }
                }
                else
                {
                    creature.Skills = creature.Skills.Union(new[] { animalSkill });
                }
            }
        }

        private void UpdateCreatureSpecialQualities(Creature creature, CreatureType animalCreatureType, HitPoints animalHitPoints, CreatureDataSelection animalData)
        {
            var animalSpecialQualities = featsGenerator.GenerateSpecialQualities(
                AnimalSpecies,
                animalCreatureType,
                animalHitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                animalData.Size,
                creature.Alignment);

            foreach (var sq in animalSpecialQualities)
            {
                var matching = creature.SpecialQualities.FirstOrDefault(f =>
                    f.Name == sq.Name
                    && !f.Foci.Except(sq.Foci).Any()
                    && !sq.Foci.Except(f.Foci).Any());

                if (matching == null)
                {
                    creature.SpecialQualities = creature.SpecialQualities.Union(new[] { sq });
                }
                else if (matching.Power < sq.Power)
                {
                    matching.Power = sq.Power;
                }
            }

            var lycanthropeSpecialQualities = featsGenerator.GenerateSpecialQualities(
                LycanthropeSpecies,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            foreach (var sq in lycanthropeSpecialQualities)
            {
                var matching = creature.SpecialQualities.FirstOrDefault(f =>
                    f.Name == sq.Name
                    && !f.Foci.Except(sq.Foci).Any()
                    && !sq.Foci.Except(f.Foci).Any());

                if (matching == null)
                {
                    creature.SpecialQualities = creature.SpecialQualities.Union(new[] { sq });
                }
                else if (matching.Power < sq.Power)
                {
                    matching.Power = sq.Power;
                }
            }
        }

        private void UpdateCreatureAttacks(Creature creature, CreatureType animalCreatureType, HitPoints animalHitPoints, CreatureDataSelection animalData)
        {
            var baseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(animalCreatureType, animalHitPoints);
            creature.BaseAttackBonus += baseAttackBonus;

            foreach (var attack in creature.Attacks)
            {
                if (attack.IsSpecial)
                {
                    attack.Name += $" (in {creature.Type.Name} form)";
                }
                else
                {
                    attack.Name += $" (in {creature.Type.Name} or Hybrid form)";
                }
            }

            var animalAttacks = attacksGenerator.GenerateAttacks(
                AnimalSpecies,
                animalData.Size,
                animalData.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            animalAttacks = attacksGenerator.ApplyAttackBonuses(animalAttacks, allFeats, creature.Abilities);

            foreach (var animalAttack in animalAttacks)
            {
                animalAttack.Name += " (in Animal form)";
            }

            var biggerSize = GetBiggerSize(creature.Size, animalData.Size);
            var lycanthropeAttacks = attacksGenerator.GenerateAttacks(
                LycanthropeSpecies,
                SizeConstants.Medium,
                biggerSize,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            lycanthropeAttacks = attacksGenerator.ApplyAttackBonuses(lycanthropeAttacks, allFeats, creature.Abilities);

            foreach (var lycanthropeAttack in lycanthropeAttacks)
            {
                var searchName = lycanthropeAttack.Name.Replace(" (in Hybrid form)", string.Empty);
                var animalAttack = animalAttacks.FirstOrDefault(a => a.Name.StartsWith(searchName));
                if (animalAttack == null)
                    continue;

                if (!string.IsNullOrEmpty(lycanthropeAttack.DamageEffect))
                {
                    animalAttack.DamageEffect = lycanthropeAttack.DamageEffect;
                }
            }

            creature.Attacks = creature.Attacks.Union(animalAttacks).Union(lycanthropeAttacks);
        }

        private string GetBiggerSize(string size1, string size2)
        {
            var ordered = SizeConstants.GetOrdered();
            var index1 = Array.IndexOf(ordered, size1);
            var index2 = Array.IndexOf(ordered, size2);

            if (index1 >= index2)
                return size1;

            return size2;
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            var animalCreatureType = new CreatureType { Name = CreatureConstants.Types.Animal };
            var animalData = creatureDataSelector.SelectFor(AnimalSpecies);
            var tasks = new List<Task>();

            // Creature type
            var typeTask = Task.Run(() => UpdateCreatureType(creature));
            tasks.Add(typeTask);

            // Abilities
            var abilityTask = Task.Run(() => UpdateCreatureAbilities(creature));
            tasks.Add(abilityTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on abilities
            //Hit Points
            var hitPointTask = Task.Run(() => UpdateCreatureHitPoints(creature, animalCreatureType, animalData));
            tasks.Add(hitPointTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            var animalHitPoints = hitPointTask.Result;

            //INFO: This depends on hit points
            //Skills
            var skillTask = Task.Run(() => UpdateCreatureSkills(creature, animalCreatureType, animalHitPoints, animalData));
            tasks.Add(skillTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on skills
            // Special Qualities
            var qualityTask = Task.Run(() => UpdateCreatureSpecialQualities(creature, animalCreatureType, animalHitPoints, animalData));
            tasks.Add(qualityTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            //INFO: This depends on special qualities
            // Attacks
            var attackTask = Task.Run(() => UpdateCreatureAttacks(creature, animalCreatureType, animalHitPoints, animalData));
            tasks.Add(attackTask);

            await Task.WhenAll(tasks);
            tasks.Clear();

            return creature;
        }
    }
}
