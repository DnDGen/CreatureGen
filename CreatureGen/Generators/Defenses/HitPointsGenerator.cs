﻿using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Defenses
{
    internal class HitPointsGenerator : IHitPointsGenerator
    {
        private readonly Dice dice;
        private readonly IAdjustmentsSelector adjustmentSelector;

        public HitPointsGenerator(Dice dice, IAdjustmentsSelector adjustmentSelector)
        {
            this.dice = dice;
            this.adjustmentSelector = adjustmentSelector;
        }

        public HitPoints GenerateFor(string creatureName, CreatureType creatureType, Ability constitution)
        {
            var hitPoints = new HitPoints();

            hitPoints.HitDiceQuantity = adjustmentSelector.SelectFrom<double>(TableNameConstants.Set.Adjustments.HitDice, creatureName);
            hitPoints.HitDie = adjustmentSelector.SelectFrom<int>(TableNameConstants.Set.Adjustments.HitDice, creatureType.Name);
            hitPoints.Constitution = constitution;

            hitPoints.RollDefault(dice);
            hitPoints.Roll(dice);

            return hitPoints;
        }

        public HitPoints RegenerateWith(HitPoints hitPoints, IEnumerable<Feat> feats)
        {
            hitPoints.Bonus = feats.Where(f => f.Name == FeatConstants.Toughness).Sum(f => f.Power);

            hitPoints.RollDefault(dice);
            hitPoints.Roll(dice);

            return hitPoints;
        }
    }
}