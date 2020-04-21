﻿using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal class CreatureDataSelector : ICreatureDataSelector
    {
        private readonly ICollectionSelector collectionSelector;

        public CreatureDataSelector(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
        }

        public CreatureDataSelection SelectFor(string creatureName)
        {
            var selection = new CreatureDataSelection();
            var data = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureData, creatureName).ToArray();

            selection.ChallengeRating = data[DataIndexConstants.CreatureData.ChallengeRating];

            if (!string.IsNullOrEmpty(data[DataIndexConstants.CreatureData.LevelAdjustment]))
                selection.LevelAdjustment = Convert.ToInt32(data[DataIndexConstants.CreatureData.LevelAdjustment]);

            selection.Reach = Convert.ToDouble(data[DataIndexConstants.CreatureData.Reach]);
            selection.Size = data[DataIndexConstants.CreatureData.Size];
            selection.Space = Convert.ToDouble(data[DataIndexConstants.CreatureData.Space]);
            selection.CanUseEquipment = Convert.ToBoolean(data[DataIndexConstants.CreatureData.CanUseEquipment]);
            selection.CasterLevel = Convert.ToInt32(data[DataIndexConstants.CreatureData.CasterLevel]);
            selection.NaturalArmor = Convert.ToInt32(data[DataIndexConstants.CreatureData.NaturalArmor]);
            selection.NumberOfHands = Convert.ToInt32(data[DataIndexConstants.CreatureData.NumberOfHands]);

            return selection;
        }
    }
}