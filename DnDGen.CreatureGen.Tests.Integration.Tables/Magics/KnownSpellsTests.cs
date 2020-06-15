﻿using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Magics
{
    [TestFixture]
    public class KnownSpellsTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.KnownSpells;

        [Test]
        public void KnownSpellsContainsAllCastersAndLevels()
        {
            var names = KnownSpellsData.GetNames();

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(KnownSpellsData), "All")]
        public void SpellsPerDay(string name, Dictionary<string, int> spellsPerDay)
        {
            AssertTypesAndAmounts(name, spellsPerDay);
        }

        public class KnownSpellsData
        {
            public static IEnumerable All
            {
                get
                {
                    var names = GetNames();
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

                    foreach (var name in names)
                    {
                        testCases[name] = new Dictionary<string, int>();
                    }

                    var bard = new[]
                    {
                        new[] { 4 },
                        new[] { 5, 2 },
                        new[] { 6, 3 },
                        new[] { 6, 3, 2 },
                        new[] { 6, 4, 3 },
                        new[] { 6, 4, 3 },
                        new[] { 6, 4, 4, 2 },
                        new[] { 6, 4, 4, 3 },
                        new[] { 6, 4, 4, 3 },
                        new[] { 6, 4, 4, 4, 2 },
                        new[] { 6, 4, 4, 4, 3 },
                        new[] { 6, 4, 4, 4, 3 },
                        new[] { 6, 4, 4, 4, 4, 2 },
                        new[] { 6, 4, 4, 4, 4, 3 },
                        new[] { 6, 4, 4, 4, 4, 3 },
                        new[] { 6, 5, 4, 4, 4, 4, 2 },
                        new[] { 6, 5, 5, 4, 4, 4, 3 },
                        new[] { 6, 5, 5, 5, 4, 4, 3 },
                        new[] { 6, 5, 5, 5, 5, 4, 4 },
                        new[] { 6, 5, 5, 5, 5, 5, 4 },
                    };

                    for (var i = 0; i < bard.Length; i++)
                    {
                        var level = i + 1;

                        for (var j = 0; j < bard[i].Length; j++)
                        {
                            var spellLevel = j.ToString();
                            testCases[$"{SpellConstants.Casters.Bard}:{level}"][spellLevel] = bard[i][j];
                        }
                    }

                    var clericDruid = new[]
                    {
                        new[] { 3, 1 },
                        new[] { 4, 2 },
                        new[] { 4, 2, 1 },
                        new[] { 5, 3, 2 },
                        new[] { 5, 3, 2, 1 },
                        new[] { 5, 3, 3, 2 },
                        new[] { 6, 4, 3, 2, 1 },
                        new[] { 6, 4, 3, 3, 2 },
                        new[] { 6, 4, 4, 3, 2, 1 },
                        new[] { 6, 4, 4, 3, 3, 2 },
                        new[] { 6, 5, 4, 4, 3, 2, 1 },
                        new[] { 6, 5, 4, 4, 3, 3, 2 },
                        new[] { 6, 5, 5, 4, 4, 3, 2, 1 },
                        new[] { 6, 5, 5, 4, 4, 3, 3, 2 },
                        new[] { 6, 5, 5, 5, 4, 4, 3, 2, 1 },
                        new[] { 6, 5, 5, 5, 4, 4, 3, 3, 2 },
                        new[] { 6, 5, 5, 5, 5, 4, 4, 3, 2, 1 },
                        new[] { 6, 5, 5, 5, 5, 4, 4, 3, 3, 2 },
                        new[] { 6, 5, 5, 5, 5, 5, 4, 4, 3, 3 },
                        new[] { 6, 5, 5, 5, 5, 5, 4, 4, 4, 4 },
                    };

                    for (var i = 0; i < clericDruid.Length; i++)
                    {
                        var level = i + 1;

                        for (var j = 0; j < clericDruid[i].Length; j++)
                        {
                            var spellLevel = j.ToString();
                            testCases[$"{SpellConstants.Casters.Cleric}:{level}"][spellLevel] = clericDruid[i][j];
                            testCases[$"{SpellConstants.Casters.Druid}:{level}"][spellLevel] = clericDruid[i][j];
                        }
                    }

                    var sorcerer = new[]
                    {
                        new[] { 4, 2 },
                        new[] { 5, 2 },
                        new[] { 5, 3 },
                        new[] { 6, 3, 1 },
                        new[] { 6, 4, 2 },
                        new[] { 7, 4, 2, 1 },
                        new[] { 7, 5, 3, 2 },
                        new[] { 8, 5, 3, 2, 1 },
                        new[] { 8, 5, 4, 3, 2 },
                        new[] { 9, 5, 4, 3, 2, 1 },
                        new[] { 9, 5, 5, 4, 3, 2 },
                        new[] { 9, 5, 5, 4, 3, 2, 1 },
                        new[] { 9, 5, 5, 4, 4, 3, 2 },
                        new[] { 9, 5, 5, 4, 4, 3, 2, 1 },
                        new[] { 9, 5, 5, 4, 4, 4, 3, 2 },
                        new[] { 9, 5, 5, 4, 4, 4, 3, 2, 1 },
                        new[] { 9, 5, 5, 4, 4, 4, 3, 3, 2 },
                        new[] { 9, 5, 5, 4, 4, 4, 3, 3, 2, 1 },
                        new[] { 9, 5, 5, 4, 4, 4, 3, 3, 3, 2 },
                        new[] { 9, 5, 5, 4, 4, 4, 3, 3, 3, 3 },
                    };

                    for (var i = 0; i < sorcerer.Length; i++)
                    {
                        var level = i + 1;

                        for (var j = 0; j < sorcerer[i].Length; j++)
                        {
                            var spellLevel = j.ToString();
                            testCases[$"{SpellConstants.Casters.Sorcerer}:{level}"][spellLevel] = sorcerer[i][j];
                        }
                    }

                    return TestDataHelper.EnumerateTestCases(testCases);
                }
            }

            private static class TestDataHelper
            {
                public static IEnumerable EnumerateTestCases<T>(Dictionary<string, T> testCases)
                {
                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable<string> GetNames()
            {
                var casters = new[]
                {
                    SpellConstants.Casters.Bard,
                    SpellConstants.Casters.Cleric,
                    SpellConstants.Casters.Druid,
                    SpellConstants.Casters.Sorcerer,
                };

                var levels = Enumerable.Range(1, 20);
                var names = new List<string>();

                foreach (var level in levels)
                {
                    foreach (var caster in casters)
                    {
                        names.Add($"{caster}:{level}");
                    }
                }

                return names;
            }
        }
    }
}
