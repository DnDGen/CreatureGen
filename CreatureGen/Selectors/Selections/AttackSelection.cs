﻿using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using System;

namespace CreatureGen.Selectors.Selections
{
    internal class AttackSelection
    {
        public const char Divider = '@';

        public string Damage { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsMelee { get; set; }
        public bool IsNatural { get; set; }
        public bool IsSpecial { get; set; }
        public int FrequencyQuantity { get; set; }
        public string FrequencyTimePeriod { get; set; }
        public string Save { get; set; }
        public string SaveAbility { get; set; }
        public int BaseSave { get; set; }
        public string AttackType { get; set; }

        public AttackSelection()
        {
            Damage = string.Empty;
            Name = string.Empty;
        }

        public static AttackSelection From(string rawData)
        {
            var data = AttackHelper.ParseData(rawData);
            var selection = new AttackSelection();
            selection.IsMelee = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsMeleeIndex]);
            selection.IsNatural = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsNaturalIndex]);
            selection.IsPrimary = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsPrimaryIndex]);
            selection.IsSpecial = Convert.ToBoolean(data[DataIndexConstants.AttackData.IsSpecialIndex]);
            selection.Name = data[DataIndexConstants.AttackData.NameIndex];
            selection.Damage = data[DataIndexConstants.AttackData.DamageIndex];
            selection.FrequencyQuantity = Convert.ToInt32(data[DataIndexConstants.AttackData.FrequencyQuantityIndex]);
            selection.FrequencyTimePeriod = data[DataIndexConstants.AttackData.FrequencyTimePeriodIndex];
            selection.Save = data[DataIndexConstants.AttackData.SaveIndex];
            selection.SaveAbility = data[DataIndexConstants.AttackData.SaveAbilityIndex];
            selection.BaseSave = Convert.ToInt32(data[DataIndexConstants.AttackData.BaseSaveIndex]);
            selection.AttackType = data[DataIndexConstants.AttackData.AttackTypeIndex];

            return selection;
        }
    }
}
