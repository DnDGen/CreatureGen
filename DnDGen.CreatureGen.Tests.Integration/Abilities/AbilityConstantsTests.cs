﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.RollGen;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Abilities
{
    [TestFixture]
    public class AbilityConstantsTests : IntegrationTests
    {
        private Dice dice;

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
        }

        [TestCase(AbilityConstants.RandomizerRolls.Heroic, 15, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.BestOfFour, 3, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Default, 10, 11)]
        [TestCase(AbilityConstants.RandomizerRolls.Average, 10, 13)]
        [TestCase(AbilityConstants.RandomizerRolls.Good, 13, 16)]
        [TestCase(AbilityConstants.RandomizerRolls.OnesAsSixes, 6, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Poor, 3, 9)]
        [TestCase(AbilityConstants.RandomizerRolls.Raw, 3, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Wild, 2, 20)]
        public void RandomizerRoll_FitsRange(string roll, int lower, int upper)
        {
            Assert.That(dice.Roll(roll).AsPotentialMinimum(), Is.EqualTo(lower));
            Assert.That(dice.Roll(roll).AsPotentialMaximum(), Is.EqualTo(upper));
        }
    }
}