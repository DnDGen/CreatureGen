﻿using CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal interface ITypeAndAmountSelector
    {
        Dictionary<string, IEnumerable<TypeAndAmountSelection>> SelectAll(string tableName);
        IEnumerable<TypeAndAmountSelection> Select(string tableName, string name);
        TypeAndAmountSelection SelectOne(string tableName, string name);
    }
}
