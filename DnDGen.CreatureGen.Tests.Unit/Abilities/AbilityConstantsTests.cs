﻿using DnDGen.CreatureGen.Abilities;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Abilities
{
    [TestFixture]
    public class AbilityConstantsTests
    {
        [TestCase(AbilityConstants.Strength, "Strength")]
        [TestCase(AbilityConstants.Charisma, "Charisma")]
        [TestCase(AbilityConstants.Constitution, "Constitution")]
        [TestCase(AbilityConstants.Dexterity, "Dexterity")]
        [TestCase(AbilityConstants.Intelligence, "Intelligence")]
        [TestCase(AbilityConstants.Wisdom, "Wisdom")]
        public void AbilityConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}