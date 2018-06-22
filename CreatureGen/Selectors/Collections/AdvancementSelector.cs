﻿using CreatureGen.Creatures;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System;
using System.Linq;

namespace CreatureGen.Selectors.Collections
{
    internal class AdvancementSelector : IAdvancementSelector
    {
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionSelector;

        public AdvancementSelector(ITypeAndAmountSelector typeAndAmountSelector, IPercentileSelector percentileSelector, ICollectionSelector collectionSelector)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.percentileSelector = percentileSelector;
            this.collectionSelector = collectionSelector;
        }

        public bool IsAdvanced(string creature)
        {
            var typesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Advancements, creature);

            return percentileSelector.SelectFrom(.1) && typesAndAmounts.Any();
        }

        public AdvancementSelection SelectRandomFor(string creature, CreatureType creatureType, string originalSize, string originalChallengeRating)
        {
            var typesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Advancements, creature);
            var randomTypeAndAmount = collectionSelector.SelectRandomFrom(typesAndAmounts);
            var selection = GetAdvancementSelection(creature, creatureType, originalSize, originalChallengeRating, randomTypeAndAmount);

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
            var adjustedChallengeRating = originalChallengeRating;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; adjustedChallengeRating = GetNextChallengeRating(adjustedChallengeRating); break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; adjustedChallengeRating = GetNextChallengeRating(adjustedChallengeRating); break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; adjustedChallengeRating = GetNextChallengeRating(adjustedChallengeRating); break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; adjustedChallengeRating = GetNextChallengeRating(adjustedChallengeRating); break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return adjustedChallengeRating;
        }

        private string AdjustChallengeRating(string originalChallengeRating, int additionalHitDice, string creatureType)
        {
            var creatureTypeDivisor = typeAndAmountSelector.SelectOne(TableNameConstants.TypeAndAmount.Advancements, creatureType);
            var divisor = creatureTypeDivisor.Amount;
            var advancementAmount = additionalHitDice / divisor;

            return GetNextChallengeRating(originalChallengeRating, advancementAmount);
        }

        private string GetNextChallengeRating(string challengeRating, int amount = 1)
        {
            var orderedChallengeRatings = ChallengeRatingConstants.GetOrdered();
            var index = Array.IndexOf(orderedChallengeRatings, challengeRating);

            if (index < 0)
                return IncrementChallengeRating(challengeRating, amount);

            if (index + amount < orderedChallengeRatings.Length)
                return orderedChallengeRatings[index + amount];

            var lastChallengeRating = orderedChallengeRatings.Last();
            var additionalIncrement = index + amount - orderedChallengeRatings.Length + 1;

            return IncrementChallengeRating(lastChallengeRating, additionalIncrement);
        }

        private string IncrementChallengeRating(string challengeRating, int amount)
        {
            var numericCR = Convert.ToInt32(challengeRating);
            return Convert.ToString(numericCR + amount);
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
