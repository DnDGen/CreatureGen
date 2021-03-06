﻿using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface IAttackSelector
    {
        IEnumerable<AttackSelection> Select(string creatureName, string originalSize, string advancedSize);
    }
}
