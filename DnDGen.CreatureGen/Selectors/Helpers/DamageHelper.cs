﻿using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Helpers
{
    public class DamageHelper : DataHelper
    {
        public DamageHelper()
            : base(AttackSelection.DamageDivider)
        { }

        public string[] BuildData(string roll, string type)
        {
            var data = DataIndexConstants.AttackData.DamageData.InitializeData();

            data[DataIndexConstants.AttackData.DamageData.RollIndex] = roll;
            data[DataIndexConstants.AttackData.DamageData.TypeIndex] = type;

            return data;
        }

        public override string BuildKey(string creature, string[] data)
        {
            return BuildKeyFromSections(creature,
                data[DataIndexConstants.AttackData.DamageData.RollIndex],
                data[DataIndexConstants.AttackData.DamageData.TypeIndex]);
        }

        public override bool ValidateEntry(string entry)
        {
            var data = ParseEntry(entry);
            var init = DataIndexConstants.AttackData.DamageData.InitializeData();
            return data.Length == init.Length;
        }

        public string BuildEntries(params string[] data)
        {
            var entries = new List<string>();

            for (var i = 0; i < data.Length; i += 2)
            {
                var entry = BuildEntry(data[i], data[i + 1]);
                entries.Add(entry);
            }

            return string.Join(AttackSelection.DamageSplitDivider, entries);
        }

        public string[][] ParseEntries(string entry)
        {
            var entries = entry.Split(AttackSelection.DamageSplitDivider);
            var data = new string[entries.Length][];

            for (var i = 0; i < entries.Length; i++)
            {
                data[i] = ParseEntry(entries[i]);
            }

            return data;
        }
    }
}