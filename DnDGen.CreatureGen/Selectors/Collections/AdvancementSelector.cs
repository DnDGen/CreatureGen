﻿using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal class AdvancementSelector : IAdvancementSelector
    {
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;

        public AdvancementSelector(
            ITypeAndAmountSelector typeAndAmountSelector,
            IPercentileSelector percentileSelector,
            ICollectionSelector collectionSelector,
            IAdjustmentsSelector adjustmentsSelector)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.percentileSelector = percentileSelector;
            this.collectionSelector = collectionSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public bool IsAdvanced(string creature, string challengeRatingFilter)
        {
            if (challengeRatingFilter != null)
                return false;

            var advancements = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Advancements, creature);
            if (!advancements.Any())
                return false;

            var isAdvanced = percentileSelector.SelectFrom(.9);
            return isAdvanced;
        }

        public AdvancementSelection SelectRandomFor(string creature, IEnumerable<string> templates, CreatureType creatureType, string originalSize, string originalChallengeRating)
        {
            var advancements = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Advancements, creature);
            var creatureHitDice = adjustmentsSelector.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, creature);
            var maxHitDice = int.MaxValue;
            templates ??= Enumerable.Empty<string>();

            foreach (var template in templates)
            {
                var templateMaxHitDice = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, template);
                maxHitDice = Math.Min(templateMaxHitDice, maxHitDice);
            }

            var validAdvancements = advancements.Where(a => creatureHitDice + a.Amount <= maxHitDice);
            if (!validAdvancements.Any())
            {
                var minimumAdvancement = advancements.OrderBy(a => a.Amount).First();
                var newAdvancementAmount = maxHitDice - (int)Math.Max(creatureHitDice, 1);
                minimumAdvancement.Amount = Math.Min(newAdvancementAmount, minimumAdvancement.Amount);

                validAdvancements = new[] { minimumAdvancement };
            }

            var randomAdvancement = collectionSelector.SelectRandomFrom(validAdvancements);
            var selection = GetAdvancementSelection(creature, creatureType, originalSize, originalChallengeRating, randomAdvancement);

            return selection;
        }

        private AdvancementSelection GetAdvancementSelection(string creatureName, CreatureType creatureType, string originalSize, string originalChallengeRating, TypeAndAmountSelection typeAndAmount)
        {
            var selection = new AdvancementSelection();
            selection.AdditionalHitDice = typeAndAmount.Amount;

            var sections = typeAndAmount.Type.Split(',');
            selection.Size = sections[DataIndexConstants.AdvancementSelectionData.Size];
            selection.Space = Convert.ToDouble(sections[DataIndexConstants.AdvancementSelectionData.Space]);
            selection.Reach = Convert.ToDouble(sections[DataIndexConstants.AdvancementSelectionData.Reach]);

            selection.StrengthAdjustment = GetStrengthAdjustment(originalSize, selection.Size);
            selection.ConstitutionAdjustment = GetConstitutionAdjustment(originalSize, selection.Size);
            selection.DexterityAdjustment = GetDexterityAdjustment(originalSize, selection.Size);
            selection.NaturalArmorAdjustment = GetNaturalArmorAdjustment(originalSize, selection.Size);

            if (IsBarghest(creatureName))
            {
                selection.StrengthAdjustment = selection.AdditionalHitDice;
                selection.ConstitutionAdjustment = selection.AdditionalHitDice;
                selection.NaturalArmorAdjustment = selection.AdditionalHitDice;
                selection.CasterLevelAdjustment = selection.AdditionalHitDice;
            }

            selection.AdjustedChallengeRating = AdjustChallengeRating(originalSize, selection.Size, originalChallengeRating, selection.AdditionalHitDice, creatureType.Name);

            return selection;
        }

        private string AdjustChallengeRating(string size, string advancedSize, string originalChallengeRating, int additionalHitDice, string creatureType)
        {
            var sizeAdjustedChallengeRating = AdjustChallengeRating(size, advancedSize, originalChallengeRating);
            var hitDieAdjustedChallengeRating = AdjustChallengeRating(sizeAdjustedChallengeRating, additionalHitDice, creatureType);

            return hitDieAdjustedChallengeRating;
        }

        private bool IsBarghest(string creatureName)
        {
            return creatureName == CreatureConstants.Barghest || creatureName == CreatureConstants.Barghest_Greater;
        }

        private string AdjustChallengeRating(string originalSize, string advancedSize, string originalChallengeRating)
        {
            var sizes = SizeConstants.GetOrdered();
            var originalSizeIndex = Array.IndexOf(sizes, originalSize);
            var advancedIndex = Array.IndexOf(sizes, advancedSize);
            var largeIndex = Array.IndexOf(sizes, SizeConstants.Large);

            if (advancedIndex < largeIndex || originalSize == advancedSize)
            {
                return originalChallengeRating;
            }

            var increase = advancedIndex - Math.Max(largeIndex - 1, originalSizeIndex);
            var adjustedChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(originalChallengeRating, increase);

            return adjustedChallengeRating;
        }

        private string AdjustChallengeRating(string originalChallengeRating, int additionalHitDice, string creatureType)
        {
            var creatureTypeDivisor = typeAndAmountSelector.SelectOne(TableNameConstants.TypeAndAmount.Advancements, creatureType);
            var divisor = creatureTypeDivisor.Amount;
            var advancementAmount = additionalHitDice / divisor;

            return ChallengeRatingConstants.IncreaseChallengeRating(originalChallengeRating, advancementAmount);
        }

        private int GetConstitutionAdjustment(string originalSize, string advancedSize)
        {
            var constitutionAdjustment = 0;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; constitutionAdjustment += 2; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; constitutionAdjustment += 4; break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; constitutionAdjustment += 4; break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; constitutionAdjustment += 4; break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; constitutionAdjustment += 4; break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return constitutionAdjustment;
        }

        private int GetDexterityAdjustment(string originalSize, string advancedSize)
        {
            var dexterityAdjustment = 0;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; dexterityAdjustment -= 2; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; dexterityAdjustment -= 2; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; dexterityAdjustment -= 2; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; dexterityAdjustment -= 2; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; dexterityAdjustment -= 2; break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; dexterityAdjustment -= 2; break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return dexterityAdjustment;
        }

        private int GetStrengthAdjustment(string originalSize, string advancedSize)
        {
            var strengthAdjustment = 0;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; strengthAdjustment += 2; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; strengthAdjustment += 4; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; strengthAdjustment += 4; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; strengthAdjustment += 8; break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; strengthAdjustment += 8; break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; strengthAdjustment += 8; break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; strengthAdjustment += 8; break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return strengthAdjustment;
        }

        private int GetNaturalArmorAdjustment(string originalSize, string advancedSize)
        {
            var naturalArmorAdjustment = 0;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; naturalArmorAdjustment += 2; break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; naturalArmorAdjustment += 3; break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; naturalArmorAdjustment += 4; break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; naturalArmorAdjustment += 5; break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return naturalArmorAdjustment;
        }
    }
}
