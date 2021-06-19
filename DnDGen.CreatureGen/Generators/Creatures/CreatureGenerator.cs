﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Items;
using DnDGen.CreatureGen.Generators.Languages;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal class CreatureGenerator : ICreatureGenerator
    {
        private readonly IAlignmentGenerator alignmentGenerator;
        private readonly ICreatureVerifier creatureVerifier;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAbilitiesGenerator abilitiesGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly IFeatsGenerator featsGenerator;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly IHitPointsGenerator hitPointsGenerator;
        private readonly IArmorClassGenerator armorClassGenerator;
        private readonly ISavesGenerator savesGenerator;
        private readonly JustInTimeFactory justInTimeFactory;
        private readonly IAdvancementSelector advancementSelector;
        private readonly IAttacksGenerator attacksGenerator;
        private readonly ISpeedsGenerator speedsGenerator;
        private readonly IEquipmentGenerator equipmentGenerator;
        private readonly IMagicGenerator magicGenerator;
        private readonly ILanguageGenerator languageGenerator;

        public CreatureGenerator(IAlignmentGenerator alignmentGenerator,
            ICreatureVerifier creatureVerifier,
            ICollectionSelector collectionsSelector,
            IAbilitiesGenerator abilitiesGenerator,
            ISkillsGenerator skillsGenerator,
            IFeatsGenerator featsGenerator,
            ICreatureDataSelector creatureDataSelector,
            IHitPointsGenerator hitPointsGenerator,
            IArmorClassGenerator armorClassGenerator,
            ISavesGenerator savesGenerator,
            JustInTimeFactory justInTimeFactory,
            IAdvancementSelector advancementSelector,
            IAttacksGenerator attacksGenerator,
            ISpeedsGenerator speedsGenerator,
            IEquipmentGenerator equipmentGenerator,
            IMagicGenerator magicGenerator,
            ILanguageGenerator languageGenerator)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.skillsGenerator = skillsGenerator;
            this.featsGenerator = featsGenerator;
            this.creatureVerifier = creatureVerifier;
            this.collectionsSelector = collectionsSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.hitPointsGenerator = hitPointsGenerator;
            this.armorClassGenerator = armorClassGenerator;
            this.savesGenerator = savesGenerator;
            this.justInTimeFactory = justInTimeFactory;
            this.advancementSelector = advancementSelector;
            this.attacksGenerator = attacksGenerator;
            this.speedsGenerator = speedsGenerator;
            this.equipmentGenerator = equipmentGenerator;
            this.magicGenerator = magicGenerator;
            this.languageGenerator = languageGenerator;
        }

        public Creature Generate(string creatureName, string template) => Generate(creatureName, template, false);
        public Creature GenerateAsCharacter(string creatureName, string template) => Generate(creatureName, template, true);

        public string GenerateRandomNameOfTemplate(string template, string challengeRating = null)
        {
            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            var templateCreatures = GetCreaturesOfTemplate(template, creatures, challengeRating: challengeRating);

            var randomCreature = collectionsSelector.SelectRandomFrom(templateCreatures);
            return randomCreature;
        }

        private IEnumerable<string> GetCreaturesOfTemplate(string template, IEnumerable<string> creatureGroup, string creatureType = null, string challengeRating = null)
        {
            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            var creatures = creatureGroup.Where(templateApplicator.IsCompatible);

            if (creatureType != null)
            {
                creatures = creatures.Where(c => templateApplicator.GetPotentialTypes(c).Contains(creatureType));
            }

            if (challengeRating != null)
            {
                creatures = creatures.Where(c => templateApplicator.GetPotentialChallengeRating(c) == challengeRating);
            }

            return creatures;
        }

        public Creature GenerateRandomOfTemplate(string template, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTemplate(template, challengeRating);
            return Generate(randomCreature, template);
        }

        public string GenerateRandomNameOfTemplateAsCharacter(string template, string challengeRating = null)
        {
            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
            var templateCreatures = GetCreaturesOfTemplate(template, creatures, challengeRating: challengeRating);

            if (!templateCreatures.Any())
                throw new IncompatibleCreatureAsCharacterException(template);

            var randomCreature = collectionsSelector.SelectRandomFrom(templateCreatures);
            return randomCreature;
        }

        public Creature GenerateRandomOfTemplateAsCharacter(string template, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTemplateAsCharacter(template, challengeRating);
            return Generate(randomCreature, template, true);
        }

        public (string CreatureName, string Template) GenerateRandomNameOfType(string creatureType, string challengeRating = null)
        {
            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            var pairings = GetCreaturesOfType(creatureType, creatures, challengeRating);
            var randomCreature = collectionsSelector.SelectRandomFrom(pairings);

            return randomCreature;
        }

        private IEnumerable<(string CreatureName, string Template)> GetCreaturesOfType(string creatureType, IEnumerable<string> creatureGroup, string challengeRating = null)
        {
            var pairings = new List<(string CreatureName, string Template)>();
            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

            var ofType = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, creatureType);

            if (challengeRating != null)
            {
                var ofChallengeRating = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, challengeRating);
                ofType = ofType.Intersect(ofChallengeRating);
            }

            var creaturesOfTypePairings = ofType.Intersect(creatureGroup).Select(c => (c, CreatureConstants.Templates.None));
            pairings.AddRange(creaturesOfTypePairings);

            foreach (var template in templates)
            {
                var creaturesOfTypeAndTemplate = GetCreaturesOfTemplate(template, creatureGroup, creatureType, challengeRating);
                creaturesOfTypePairings = creaturesOfTypeAndTemplate.Select(c => (c, template));

                pairings.AddRange(creaturesOfTypePairings);
            }

            return pairings;
        }

        public (string CreatureName, string Template) GenerateRandomNameOfChallengeRating(string challengeRating)
        {
            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            var pairings = GetCreaturesOfChallengeRating(challengeRating, creatures);
            var randomCreature = collectionsSelector.SelectRandomFrom(pairings);

            return randomCreature;
        }

        private IEnumerable<(string CreatureName, string Template)> GetCreaturesOfChallengeRating(string challengeRating, IEnumerable<string> creatureGroup)
        {
            var pairings = new List<(string CreatureName, string Template)>();
            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

            var ofChallengeRating = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, challengeRating);
            var creaturesOfChallengeRatingPairings = ofChallengeRating.Intersect(creatureGroup).Select(c => (c, CreatureConstants.Templates.None));
            pairings.AddRange(creaturesOfChallengeRatingPairings);

            foreach (var template in templates)
            {
                var creaturesOfChallengeRatingAndTemplate = GetCreaturesOfTemplate(template, creatureGroup, challengeRating: challengeRating);
                creaturesOfChallengeRatingPairings = creaturesOfChallengeRatingAndTemplate.Select(c => (c, template));

                pairings.AddRange(creaturesOfChallengeRatingPairings);
            }

            return pairings;
        }

        public Creature GenerateRandomOfType(string creatureType, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfType(creatureType, challengeRating);
            return Generate(randomCreature.CreatureName, randomCreature.Template);
        }

        public Creature GenerateRandomOfChallengeRating(string challengeRating)
        {
            var randomCreature = GenerateRandomNameOfChallengeRating(challengeRating);
            return Generate(randomCreature.CreatureName, randomCreature.Template);
        }

        public (string CreatureName, string Template) GenerateRandomNameOfTypeAsCharacter(string creatureType, string challengeRating = null)
        {
            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
            var pairings = GetCreaturesOfType(creatureType, creatures, challengeRating);

            if (!pairings.Any())
                throw new IncompatibleCreatureAsCharacterException(creatureType);

            var randomCreature = collectionsSelector.SelectRandomFrom(pairings);
            return randomCreature;
        }

        public (string CreatureName, string Template) GenerateRandomNameOfChallengeRatingAsCharacter(string challengeRating)
        {
            var creatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
            var pairings = GetCreaturesOfChallengeRating(challengeRating, creatures);

            if (!pairings.Any())
                throw new IncompatibleCreatureAsCharacterException($"CR {challengeRating}");

            var randomCreature = collectionsSelector.SelectRandomFrom(pairings);
            return randomCreature;
        }

        public Creature GenerateRandomOfTypeAsCharacter(string creatureType, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTypeAsCharacter(creatureType, challengeRating);
            return GenerateAsCharacter(randomCreature.CreatureName, randomCreature.Template);
        }
        public Creature GenerateRandomOfChallengeRatingAsCharacter(string challengeRating)
        {
            var randomCreature = GenerateRandomNameOfChallengeRatingAsCharacter(challengeRating);
            return GenerateAsCharacter(randomCreature.CreatureName, randomCreature.Template);
        }

        private Creature Generate(string creatureName, string template, bool asCharacter)
        {
            var compatible = creatureVerifier.CanBeCharacter(creatureName);
            if (!compatible && asCharacter)
                throw new IncompatibleCreatureAsCharacterException(creatureName);

            compatible = creatureVerifier.VerifyCompatibility(creatureName, template);
            if (!compatible)
                throw new IncompatibleCreatureAndTemplateException(creatureName, template);

            var creature = GeneratePrototype(creatureName, asCharacter);

            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = templateApplicator.ApplyTo(creature);

            return creature;
        }

        private Creature GeneratePrototype(string creatureName, bool asCharacter)
        {
            var creature = new Creature();
            creature.Name = creatureName;

            var creatureData = creatureDataSelector.SelectFor(creatureName);
            creature.Size = creatureData.Size;
            creature.Space.Value = creatureData.Space;
            creature.Reach.Value = creatureData.Reach;
            creature.CanUseEquipment = creatureData.CanUseEquipment;
            creature.ChallengeRating = creatureData.ChallengeRating;
            creature.LevelAdjustment = creatureData.LevelAdjustment;
            creature.CasterLevel = creatureData.CasterLevel;
            creature.NumberOfHands = creatureData.NumberOfHands;

            creature.Type = GetCreatureType(creatureName);
            creature.Abilities = abilitiesGenerator.GenerateFor(creatureName);

            if (advancementSelector.IsAdvanced(creatureName))
            {
                var advancement = advancementSelector.SelectRandomFor(creatureName, creature.Type, creature.Size, creature.ChallengeRating);

                creature.IsAdvanced = true;
                creature.Size = advancement.Size;
                creature.Space.Value = advancement.Space;
                creature.Reach.Value = advancement.Reach;
                creature.CasterLevel += advancement.CasterLevelAdjustment;
                creature.ChallengeRating = advancement.AdjustedChallengeRating;
                creatureData.NaturalArmor += advancement.NaturalArmorAdjustment;

                creature.Abilities[AbilityConstants.Strength].AdvancementAdjustment += advancement.StrengthAdjustment;
                creature.Abilities[AbilityConstants.Dexterity].AdvancementAdjustment += advancement.DexterityAdjustment;
                creature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment += advancement.ConstitutionAdjustment;

                creature.HitPoints = hitPointsGenerator.GenerateFor(
                    creatureName,
                    creature.Type,
                    creature.Abilities[AbilityConstants.Constitution],
                    creature.Size,
                    advancement.AdditionalHitDice, asCharacter);
            }
            else
            {
                creature.HitPoints = hitPointsGenerator.GenerateFor(
                    creatureName,
                    creature.Type,
                    creature.Abilities[AbilityConstants.Constitution],
                    creature.Size,
                    asCharacter: asCharacter);
            }

            if (creature.HitPoints.HitDiceQuantity == 0)
            {
                creature.ChallengeRating = ChallengeRatingConstants.CR0;
            }

            creature.Alignment = alignmentGenerator.Generate(creatureName);
            creature.Skills = skillsGenerator.GenerateFor(creature.HitPoints, creatureName, creature.Type, creature.Abilities, creature.CanUseEquipment, creature.Size);
            creature.Languages = languageGenerator.GenerateWith(creatureName, creature.Abilities, creature.Skills);

            creature.SpecialQualities = featsGenerator.GenerateSpecialQualities(
                creatureName,
                creature.Type,
                creature.HitPoints,
                creature.Abilities,
                creature.Skills,
                creature.CanUseEquipment,
                creature.Size,
                creature.Alignment);

            creature.BaseAttackBonus = attacksGenerator.GenerateBaseAttackBonus(creature.Type, creature.HitPoints);
            creature.Attacks = attacksGenerator.GenerateAttacks(
                creatureName,
                creatureData.Size,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.HitPoints.RoundedHitDiceQuantity);

            creature.Feats = featsGenerator.GenerateFeats(
                creature.HitPoints,
                creature.BaseAttackBonus,
                creature.Abilities,
                creature.Skills,
                creature.Attacks,
                creature.SpecialQualities,
                creature.CasterLevel,
                creature.Speeds,
                creatureData.NaturalArmor,
                creature.NumberOfHands,
                creature.Size,
                creature.CanUseEquipment);

            creature.Skills = skillsGenerator.ApplyBonusesFromFeats(creature.Skills, creature.Feats, creature.Abilities);
            creature.HitPoints = hitPointsGenerator.RegenerateWith(creature.HitPoints, creature.Feats);

            creature.GrappleBonus = attacksGenerator.GenerateGrappleBonus(
                creatureName,
                creature.Size,
                creature.BaseAttackBonus,
                creature.Abilities[AbilityConstants.Strength]);

            var allFeats = creature.Feats.Union(creature.SpecialQualities);
            creature.Attacks = attacksGenerator.ApplyAttackBonuses(creature.Attacks, allFeats, creature.Abilities);
            creature.Attacks = equipmentGenerator.AddAttacks(allFeats, creature.Attacks, creature.NumberOfHands);
            creature.Equipment = equipmentGenerator.Generate(
                creature.Name,
                creature.CanUseEquipment,
                allFeats,
                creature.HitPoints.RoundedHitDiceQuantity,
                creature.Attacks,
                creature.Abilities,
                creature.Size);

            creature.Abilities = abilitiesGenerator.SetMaxBonuses(creature.Abilities, creature.Equipment);
            creature.Skills = skillsGenerator.SetArmorCheckPenalties(creature.Name, creature.Skills, creature.Equipment);

            creature.InitiativeBonus = ComputeInitiativeBonus(creature.Feats);
            creature.Speeds = speedsGenerator.Generate(creature.Name);
            creature.ArmorClass = armorClassGenerator.GenerateWith(
                creature.Abilities,
                creature.Size,
                creatureName,
                creature.Type,
                allFeats,
                creatureData.NaturalArmor,
                creature.Equipment);
            creature.Saves = savesGenerator.GenerateWith(creature.Name, creature.Type, creature.HitPoints, allFeats, creature.Abilities);

            creature.Magic = magicGenerator.GenerateWith(creature.Name, creature.Alignment, creature.Abilities, creature.Equipment);

            return creature;
        }

        private int ComputeInitiativeBonus(IEnumerable<Feat> feats)
        {
            var initiativeBonus = 0;

            var improvedInitiative = feats.FirstOrDefault(f => f.Name == FeatConstants.Initiative_Improved);
            if (improvedInitiative != null)
                initiativeBonus += improvedInitiative.Power;

            return initiativeBonus;
        }

        private CreatureType GetCreatureType(string creatureName)
        {
            var creatureType = new CreatureType();
            var types = collectionsSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creatureName);

            creatureType.Name = types.First();
            creatureType.SubTypes = types.Skip(1);

            return creatureType;
        }

        public async Task<Creature> GenerateAsync(string creatureName, string template) => await GenerateAsync(creatureName, template, false);

        public async Task<Creature> GenerateAsCharacterAsync(string creatureName, string template) => await GenerateAsync(creatureName, template, true);

        public async Task<Creature> GenerateRandomOfTemplateAsync(string template, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTemplate(template, challengeRating);
            return await GenerateAsync(randomCreature, template);
        }

        public async Task<Creature> GenerateRandomOfTemplateAsCharacterAsync(string template, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTemplateAsCharacter(template, challengeRating);
            return await GenerateAsync(randomCreature, template, true);
        }

        public async Task<Creature> GenerateRandomOfTypeAsync(string creatureType, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfType(creatureType, challengeRating);
            return await GenerateAsync(randomCreature.CreatureName, randomCreature.Template);
        }

        public async Task<Creature> GenerateRandomOfTypeAsCharacterAsync(string creatureType, string challengeRating = null)
        {
            var randomCreature = GenerateRandomNameOfTypeAsCharacter(creatureType, challengeRating);
            return await GenerateAsCharacterAsync(randomCreature.CreatureName, randomCreature.Template);
        }

        public async Task<Creature> GenerateRandomOfChallengeRatingAsync(string challengeRating)
        {
            var randomCreature = GenerateRandomNameOfChallengeRating(challengeRating);
            return await GenerateAsync(randomCreature.CreatureName, randomCreature.Template);
        }

        public async Task<Creature> GenerateRandomOfChallengeRatingAsCharacterAsync(string challengeRating)
        {
            var randomCreature = GenerateRandomNameOfChallengeRatingAsCharacter(challengeRating);
            return await GenerateAsCharacterAsync(randomCreature.CreatureName, randomCreature.Template);
        }

        private async Task<Creature> GenerateAsync(string creatureName, string template, bool asCharacter)
        {
            var compatible = creatureVerifier.CanBeCharacter(creatureName);
            if (!compatible && asCharacter)
                throw new IncompatibleCreatureAsCharacterException(creatureName);

            compatible = creatureVerifier.VerifyCompatibility(creatureName, template);
            if (!compatible)
                throw new IncompatibleCreatureAndTemplateException(creatureName, template);

            var creature = GeneratePrototype(creatureName, asCharacter);

            var templateApplicator = justInTimeFactory.Build<TemplateApplicator>(template);
            creature = await templateApplicator.ApplyToAsync(creature);

            return creature;
        }
    }
}