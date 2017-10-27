﻿using CreatureGen.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class ClassNameRandomizerTypeConstantsTests
    {
        [TestCase(ClassNameRandomizerTypeConstants.AnyPlayer, "Any Player")]
        [TestCase(ClassNameRandomizerTypeConstants.AnyNPC, "Any NPC")]
        [TestCase(ClassNameRandomizerTypeConstants.ArcaneSpellcaster, "Arcane Spellcaster")]
        [TestCase(ClassNameRandomizerTypeConstants.DivineSpellcaster, "Divine Spellcaster")]
        [TestCase(ClassNameRandomizerTypeConstants.NonSpellcaster, "Non-Spellcaster")]
        [TestCase(ClassNameRandomizerTypeConstants.PhysicalCombat, "Physical Combat")]
        [TestCase(ClassNameRandomizerTypeConstants.Spellcaster, "Spellcaster")]
        [TestCase(ClassNameRandomizerTypeConstants.Stealth, "Stealth")]
        public void ClassNameRandomizerType(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
