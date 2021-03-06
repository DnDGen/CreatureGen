﻿using DnDGen.CreatureGen.Abilities;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Abilities
{
    [TestFixture]
    public class AbilityTests
    {
        private Ability ability;

        [SetUp]
        public void Setup()
        {
            ability = new Ability("ability name");
        }

        [Test]
        public void DefaultScoreIs10()
        {
            Assert.That(Ability.DefaultScore, Is.EqualTo(10));
        }

        [Test]
        public void AbilityInitialized()
        {
            Assert.That(ability.Name, Is.EqualTo("ability name"));
            Assert.That(ability.BaseScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(ability.RacialAdjustment, Is.Zero);
            Assert.That(ability.AdvancementAdjustment, Is.Zero);
            Assert.That(ability.TemplateAdjustment, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
            Assert.That(ability.FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(ability.MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(ability.TemplateScore, Is.EqualTo(-1));
            Assert.That(ability.Bonuses, Is.Empty);
            Assert.That(ability.Bonus, Is.Zero);
        }

        [TestCase(1, -5)]
        [TestCase(2, -4)]
        [TestCase(3, -4)]
        [TestCase(4, -3)]
        [TestCase(5, -3)]
        [TestCase(6, -2)]
        [TestCase(7, -2)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        [TestCase(21, 5)]
        [TestCase(22, 6)]
        [TestCase(23, 6)]
        [TestCase(24, 7)]
        [TestCase(25, 7)]
        [TestCase(26, 8)]
        [TestCase(27, 8)]
        [TestCase(28, 9)]
        [TestCase(29, 9)]
        [TestCase(30, 10)]
        [TestCase(31, 10)]
        [TestCase(32, 11)]
        [TestCase(33, 11)]
        [TestCase(34, 12)]
        [TestCase(35, 12)]
        [TestCase(36, 13)]
        [TestCase(37, 13)]
        [TestCase(38, 14)]
        [TestCase(39, 14)]
        [TestCase(40, 15)]
        [TestCase(41, 15)]
        [TestCase(42, 16)]
        [TestCase(43, 16)]
        [TestCase(44, 17)]
        [TestCase(45, 17)]
        public void AbilityModifier(int baseValue, int bonus)
        {
            ability.BaseScore = baseValue;
            Assert.That(ability.Modifier, Is.EqualTo(bonus));
        }

        [TestCase(1, -5)]
        [TestCase(2, -4)]
        [TestCase(3, -4)]
        [TestCase(4, -3)]
        [TestCase(5, -3)]
        [TestCase(6, -2)]
        [TestCase(7, -2)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        [TestCase(21, 5)]
        [TestCase(22, 6)]
        [TestCase(23, 6)]
        [TestCase(24, 7)]
        [TestCase(25, 7)]
        [TestCase(26, 8)]
        [TestCase(27, 8)]
        [TestCase(28, 9)]
        [TestCase(29, 9)]
        [TestCase(30, 10)]
        [TestCase(31, 10)]
        [TestCase(32, 11)]
        [TestCase(33, 11)]
        [TestCase(34, 12)]
        [TestCase(35, 12)]
        [TestCase(36, 13)]
        [TestCase(37, 13)]
        [TestCase(38, 14)]
        [TestCase(39, 14)]
        [TestCase(40, 15)]
        [TestCase(41, 15)]
        [TestCase(42, 16)]
        [TestCase(43, 16)]
        [TestCase(44, 17)]
        [TestCase(45, 17)]
        public void AbilityModifier_WithTemplateScore(int templateScore, int bonus)
        {
            ability.TemplateScore = templateScore;
            Assert.That(ability.Modifier, Is.EqualTo(bonus));
        }

        [TestCase(1, 0, -5)]
        [TestCase(1, 1, -5)]
        [TestCase(1, 2, -5)]
        [TestCase(1, 3, -5)]
        [TestCase(1, 4, -5)]
        [TestCase(1, 5, -5)]
        [TestCase(1, 6, -5)]
        [TestCase(1, 7, -5)]
        [TestCase(1, 8, -5)]
        [TestCase(1, 9, -5)]
        [TestCase(1, 10, -5)]
        [TestCase(2, 0, -4)]
        [TestCase(2, 1, -4)]
        [TestCase(2, 2, -4)]
        [TestCase(2, 3, -4)]
        [TestCase(2, 4, -4)]
        [TestCase(2, 5, -4)]
        [TestCase(2, 6, -4)]
        [TestCase(2, 7, -4)]
        [TestCase(2, 8, -4)]
        [TestCase(2, 9, -4)]
        [TestCase(2, 10, -4)]
        [TestCase(3, 0, -4)]
        [TestCase(3, 1, -4)]
        [TestCase(3, 2, -4)]
        [TestCase(3, 3, -4)]
        [TestCase(3, 4, -4)]
        [TestCase(3, 5, -4)]
        [TestCase(3, 6, -4)]
        [TestCase(3, 7, -4)]
        [TestCase(3, 8, -4)]
        [TestCase(3, 9, -4)]
        [TestCase(3, 10, -4)]
        [TestCase(4, 0, -3)]
        [TestCase(4, 1, -3)]
        [TestCase(4, 2, -3)]
        [TestCase(4, 3, -3)]
        [TestCase(4, 4, -3)]
        [TestCase(4, 5, -3)]
        [TestCase(4, 6, -3)]
        [TestCase(4, 7, -3)]
        [TestCase(4, 8, -3)]
        [TestCase(4, 9, -3)]
        [TestCase(4, 10, -3)]
        [TestCase(5, 0, -3)]
        [TestCase(5, 1, -3)]
        [TestCase(5, 2, -3)]
        [TestCase(5, 3, -3)]
        [TestCase(5, 4, -3)]
        [TestCase(5, 5, -3)]
        [TestCase(5, 6, -3)]
        [TestCase(5, 7, -3)]
        [TestCase(5, 8, -3)]
        [TestCase(5, 9, -3)]
        [TestCase(5, 10, -3)]
        [TestCase(6, 0, -2)]
        [TestCase(6, 1, -2)]
        [TestCase(6, 2, -2)]
        [TestCase(6, 3, -2)]
        [TestCase(6, 4, -2)]
        [TestCase(6, 5, -2)]
        [TestCase(6, 6, -2)]
        [TestCase(6, 7, -2)]
        [TestCase(6, 8, -2)]
        [TestCase(6, 9, -2)]
        [TestCase(6, 10, -2)]
        [TestCase(7, 0, -2)]
        [TestCase(7, 1, -2)]
        [TestCase(7, 2, -2)]
        [TestCase(7, 3, -2)]
        [TestCase(7, 4, -2)]
        [TestCase(7, 5, -2)]
        [TestCase(7, 6, -2)]
        [TestCase(7, 7, -2)]
        [TestCase(7, 8, -2)]
        [TestCase(7, 9, -2)]
        [TestCase(7, 10, -2)]
        [TestCase(8, 0, -1)]
        [TestCase(8, 1, -1)]
        [TestCase(8, 2, -1)]
        [TestCase(8, 3, -1)]
        [TestCase(8, 4, -1)]
        [TestCase(8, 5, -1)]
        [TestCase(8, 6, -1)]
        [TestCase(8, 7, -1)]
        [TestCase(8, 8, -1)]
        [TestCase(8, 9, -1)]
        [TestCase(8, 10, -1)]
        [TestCase(9, 0, -1)]
        [TestCase(9, 1, -1)]
        [TestCase(9, 2, -1)]
        [TestCase(9, 3, -1)]
        [TestCase(9, 4, -1)]
        [TestCase(9, 5, -1)]
        [TestCase(9, 6, -1)]
        [TestCase(9, 7, -1)]
        [TestCase(9, 8, -1)]
        [TestCase(9, 9, -1)]
        [TestCase(9, 10, -1)]
        [TestCase(10, 0, 0)]
        [TestCase(10, 1, 0)]
        [TestCase(10, 2, 0)]
        [TestCase(10, 3, 0)]
        [TestCase(10, 4, 0)]
        [TestCase(10, 5, 0)]
        [TestCase(10, 6, 0)]
        [TestCase(10, 7, 0)]
        [TestCase(10, 8, 0)]
        [TestCase(10, 9, 0)]
        [TestCase(10, 10, 0)]
        [TestCase(11, 0, 0)]
        [TestCase(11, 1, 0)]
        [TestCase(11, 2, 0)]
        [TestCase(11, 3, 0)]
        [TestCase(11, 4, 0)]
        [TestCase(11, 5, 0)]
        [TestCase(11, 6, 0)]
        [TestCase(11, 7, 0)]
        [TestCase(11, 8, 0)]
        [TestCase(11, 9, 0)]
        [TestCase(11, 10, 0)]
        [TestCase(12, 0, 0)]
        [TestCase(12, 1, 1)]
        [TestCase(12, 2, 1)]
        [TestCase(12, 3, 1)]
        [TestCase(12, 4, 1)]
        [TestCase(12, 5, 1)]
        [TestCase(12, 6, 1)]
        [TestCase(12, 7, 1)]
        [TestCase(12, 8, 1)]
        [TestCase(12, 9, 1)]
        [TestCase(12, 10, 1)]
        [TestCase(13, 0, 0)]
        [TestCase(13, 1, 1)]
        [TestCase(13, 2, 1)]
        [TestCase(13, 3, 1)]
        [TestCase(13, 4, 1)]
        [TestCase(13, 5, 1)]
        [TestCase(13, 6, 1)]
        [TestCase(13, 7, 1)]
        [TestCase(13, 8, 1)]
        [TestCase(13, 9, 1)]
        [TestCase(13, 10, 1)]
        [TestCase(14, 0, 0)]
        [TestCase(14, 1, 1)]
        [TestCase(14, 2, 2)]
        [TestCase(14, 3, 2)]
        [TestCase(14, 4, 2)]
        [TestCase(14, 5, 2)]
        [TestCase(14, 6, 2)]
        [TestCase(14, 7, 2)]
        [TestCase(14, 8, 2)]
        [TestCase(14, 9, 2)]
        [TestCase(14, 10, 2)]
        [TestCase(15, 0, 0)]
        [TestCase(15, 1, 1)]
        [TestCase(15, 2, 2)]
        [TestCase(15, 3, 2)]
        [TestCase(15, 4, 2)]
        [TestCase(15, 5, 2)]
        [TestCase(15, 6, 2)]
        [TestCase(15, 7, 2)]
        [TestCase(15, 8, 2)]
        [TestCase(15, 9, 2)]
        [TestCase(15, 10, 2)]
        [TestCase(16, 0, 0)]
        [TestCase(16, 1, 1)]
        [TestCase(16, 2, 2)]
        [TestCase(16, 3, 3)]
        [TestCase(16, 4, 3)]
        [TestCase(16, 5, 3)]
        [TestCase(16, 6, 3)]
        [TestCase(16, 7, 3)]
        [TestCase(16, 8, 3)]
        [TestCase(16, 9, 3)]
        [TestCase(16, 10, 3)]
        [TestCase(17, 0, 0)]
        [TestCase(17, 1, 1)]
        [TestCase(17, 2, 2)]
        [TestCase(17, 3, 3)]
        [TestCase(17, 4, 3)]
        [TestCase(17, 5, 3)]
        [TestCase(17, 6, 3)]
        [TestCase(17, 7, 3)]
        [TestCase(17, 8, 3)]
        [TestCase(17, 9, 3)]
        [TestCase(17, 10, 3)]
        [TestCase(18, 0, 0)]
        [TestCase(18, 1, 1)]
        [TestCase(18, 2, 2)]
        [TestCase(18, 3, 3)]
        [TestCase(18, 4, 4)]
        [TestCase(18, 5, 4)]
        [TestCase(18, 6, 4)]
        [TestCase(18, 7, 4)]
        [TestCase(18, 8, 4)]
        [TestCase(18, 9, 4)]
        [TestCase(18, 10, 4)]
        [TestCase(19, 0, 0)]
        [TestCase(19, 1, 1)]
        [TestCase(19, 2, 2)]
        [TestCase(19, 3, 3)]
        [TestCase(19, 4, 4)]
        [TestCase(19, 5, 4)]
        [TestCase(19, 6, 4)]
        [TestCase(19, 7, 4)]
        [TestCase(19, 8, 4)]
        [TestCase(19, 9, 4)]
        [TestCase(19, 10, 4)]
        [TestCase(20, 0, 0)]
        [TestCase(20, 1, 1)]
        [TestCase(20, 2, 2)]
        [TestCase(20, 3, 3)]
        [TestCase(20, 4, 4)]
        [TestCase(20, 5, 5)]
        [TestCase(20, 6, 5)]
        [TestCase(20, 7, 5)]
        [TestCase(20, 8, 5)]
        [TestCase(20, 9, 5)]
        [TestCase(20, 10, 5)]
        [TestCase(21, 0, 0)]
        [TestCase(21, 1, 1)]
        [TestCase(21, 2, 2)]
        [TestCase(21, 3, 3)]
        [TestCase(21, 4, 4)]
        [TestCase(21, 5, 5)]
        [TestCase(21, 6, 5)]
        [TestCase(21, 7, 5)]
        [TestCase(21, 8, 5)]
        [TestCase(21, 9, 5)]
        [TestCase(21, 10, 5)]
        [TestCase(22, 0, 0)]
        [TestCase(22, 1, 1)]
        [TestCase(22, 2, 2)]
        [TestCase(22, 3, 3)]
        [TestCase(22, 4, 4)]
        [TestCase(22, 5, 5)]
        [TestCase(22, 6, 6)]
        [TestCase(22, 7, 6)]
        [TestCase(22, 8, 6)]
        [TestCase(22, 9, 6)]
        [TestCase(22, 10, 6)]
        [TestCase(23, 0, 0)]
        [TestCase(23, 1, 1)]
        [TestCase(23, 2, 2)]
        [TestCase(23, 3, 3)]
        [TestCase(23, 4, 4)]
        [TestCase(23, 5, 5)]
        [TestCase(23, 6, 6)]
        [TestCase(23, 7, 6)]
        [TestCase(23, 8, 6)]
        [TestCase(23, 9, 6)]
        [TestCase(23, 10, 6)]
        [TestCase(24, 0, 0)]
        [TestCase(24, 1, 1)]
        [TestCase(24, 2, 2)]
        [TestCase(24, 3, 3)]
        [TestCase(24, 4, 4)]
        [TestCase(24, 5, 5)]
        [TestCase(24, 6, 6)]
        [TestCase(24, 7, 7)]
        [TestCase(24, 8, 7)]
        [TestCase(24, 9, 7)]
        [TestCase(24, 10, 7)]
        [TestCase(25, 0, 0)]
        [TestCase(25, 1, 1)]
        [TestCase(25, 2, 2)]
        [TestCase(25, 3, 3)]
        [TestCase(25, 4, 4)]
        [TestCase(25, 5, 5)]
        [TestCase(25, 6, 6)]
        [TestCase(25, 7, 7)]
        [TestCase(25, 8, 7)]
        [TestCase(25, 9, 7)]
        [TestCase(25, 10, 7)]
        [TestCase(26, 0, 0)]
        [TestCase(26, 1, 1)]
        [TestCase(26, 2, 2)]
        [TestCase(26, 3, 3)]
        [TestCase(26, 4, 4)]
        [TestCase(26, 5, 5)]
        [TestCase(26, 6, 6)]
        [TestCase(26, 7, 7)]
        [TestCase(26, 8, 8)]
        [TestCase(26, 9, 8)]
        [TestCase(26, 10, 8)]
        [TestCase(27, 0, 0)]
        [TestCase(27, 1, 1)]
        [TestCase(27, 2, 2)]
        [TestCase(27, 3, 3)]
        [TestCase(27, 4, 4)]
        [TestCase(27, 5, 5)]
        [TestCase(27, 6, 6)]
        [TestCase(27, 7, 7)]
        [TestCase(27, 8, 8)]
        [TestCase(27, 9, 8)]
        [TestCase(27, 10, 8)]
        [TestCase(28, 0, 0)]
        [TestCase(28, 1, 1)]
        [TestCase(28, 2, 2)]
        [TestCase(28, 3, 3)]
        [TestCase(28, 4, 4)]
        [TestCase(28, 5, 5)]
        [TestCase(28, 6, 6)]
        [TestCase(28, 7, 7)]
        [TestCase(28, 8, 8)]
        [TestCase(28, 9, 9)]
        [TestCase(28, 10, 9)]
        [TestCase(29, 0, 0)]
        [TestCase(29, 1, 1)]
        [TestCase(29, 2, 2)]
        [TestCase(29, 3, 3)]
        [TestCase(29, 4, 4)]
        [TestCase(29, 5, 5)]
        [TestCase(29, 6, 6)]
        [TestCase(29, 7, 7)]
        [TestCase(29, 8, 8)]
        [TestCase(29, 9, 9)]
        [TestCase(29, 10, 9)]
        [TestCase(30, 0, 0)]
        [TestCase(30, 1, 1)]
        [TestCase(30, 2, 2)]
        [TestCase(30, 3, 3)]
        [TestCase(30, 4, 4)]
        [TestCase(30, 5, 5)]
        [TestCase(30, 6, 6)]
        [TestCase(30, 7, 7)]
        [TestCase(30, 8, 8)]
        [TestCase(30, 9, 9)]
        [TestCase(30, 10, 10)]
        [TestCase(31, 0, 0)]
        [TestCase(31, 1, 1)]
        [TestCase(31, 2, 2)]
        [TestCase(31, 3, 3)]
        [TestCase(31, 4, 4)]
        [TestCase(31, 5, 5)]
        [TestCase(31, 6, 6)]
        [TestCase(31, 7, 7)]
        [TestCase(31, 8, 8)]
        [TestCase(31, 9, 9)]
        [TestCase(31, 10, 10)]
        [TestCase(32, 0, 0)]
        [TestCase(32, 1, 1)]
        [TestCase(32, 2, 2)]
        [TestCase(32, 3, 3)]
        [TestCase(32, 4, 4)]
        [TestCase(32, 5, 5)]
        [TestCase(32, 6, 6)]
        [TestCase(32, 7, 7)]
        [TestCase(32, 8, 8)]
        [TestCase(32, 9, 9)]
        [TestCase(32, 10, 10)]
        [TestCase(33, 0, 0)]
        [TestCase(33, 1, 1)]
        [TestCase(33, 2, 2)]
        [TestCase(33, 3, 3)]
        [TestCase(33, 4, 4)]
        [TestCase(33, 5, 5)]
        [TestCase(33, 6, 6)]
        [TestCase(33, 7, 7)]
        [TestCase(33, 8, 8)]
        [TestCase(33, 9, 9)]
        [TestCase(33, 10, 10)]
        [TestCase(34, 0, 0)]
        [TestCase(34, 1, 1)]
        [TestCase(34, 2, 2)]
        [TestCase(34, 3, 3)]
        [TestCase(34, 4, 4)]
        [TestCase(34, 5, 5)]
        [TestCase(34, 6, 6)]
        [TestCase(34, 7, 7)]
        [TestCase(34, 8, 8)]
        [TestCase(34, 9, 9)]
        [TestCase(34, 10, 10)]
        [TestCase(35, 0, 0)]
        [TestCase(35, 1, 1)]
        [TestCase(35, 2, 2)]
        [TestCase(35, 3, 3)]
        [TestCase(35, 4, 4)]
        [TestCase(35, 5, 5)]
        [TestCase(35, 6, 6)]
        [TestCase(35, 7, 7)]
        [TestCase(35, 8, 8)]
        [TestCase(35, 9, 9)]
        [TestCase(35, 10, 10)]
        [TestCase(36, 0, 0)]
        [TestCase(36, 1, 1)]
        [TestCase(36, 2, 2)]
        [TestCase(36, 3, 3)]
        [TestCase(36, 4, 4)]
        [TestCase(36, 5, 5)]
        [TestCase(36, 6, 6)]
        [TestCase(36, 7, 7)]
        [TestCase(36, 8, 8)]
        [TestCase(36, 9, 9)]
        [TestCase(36, 10, 10)]
        [TestCase(37, 0, 0)]
        [TestCase(37, 1, 1)]
        [TestCase(37, 2, 2)]
        [TestCase(37, 3, 3)]
        [TestCase(37, 4, 4)]
        [TestCase(37, 5, 5)]
        [TestCase(37, 6, 6)]
        [TestCase(37, 7, 7)]
        [TestCase(37, 8, 8)]
        [TestCase(37, 9, 9)]
        [TestCase(37, 10, 10)]
        [TestCase(38, 0, 0)]
        [TestCase(38, 1, 1)]
        [TestCase(38, 2, 2)]
        [TestCase(38, 3, 3)]
        [TestCase(38, 4, 4)]
        [TestCase(38, 5, 5)]
        [TestCase(38, 6, 6)]
        [TestCase(38, 7, 7)]
        [TestCase(38, 8, 8)]
        [TestCase(38, 9, 9)]
        [TestCase(38, 10, 10)]
        [TestCase(39, 0, 0)]
        [TestCase(39, 1, 1)]
        [TestCase(39, 2, 2)]
        [TestCase(39, 3, 3)]
        [TestCase(39, 4, 4)]
        [TestCase(39, 5, 5)]
        [TestCase(39, 6, 6)]
        [TestCase(39, 7, 7)]
        [TestCase(39, 8, 8)]
        [TestCase(39, 9, 9)]
        [TestCase(39, 10, 10)]
        [TestCase(40, 0, 0)]
        [TestCase(40, 1, 1)]
        [TestCase(40, 2, 2)]
        [TestCase(40, 3, 3)]
        [TestCase(40, 4, 4)]
        [TestCase(40, 5, 5)]
        [TestCase(40, 6, 6)]
        [TestCase(40, 7, 7)]
        [TestCase(40, 8, 8)]
        [TestCase(40, 9, 9)]
        [TestCase(40, 10, 10)]
        [TestCase(41, 0, 0)]
        [TestCase(41, 1, 1)]
        [TestCase(41, 2, 2)]
        [TestCase(41, 3, 3)]
        [TestCase(41, 4, 4)]
        [TestCase(41, 5, 5)]
        [TestCase(41, 6, 6)]
        [TestCase(41, 7, 7)]
        [TestCase(41, 8, 8)]
        [TestCase(41, 9, 9)]
        [TestCase(41, 10, 10)]
        [TestCase(42, 0, 0)]
        [TestCase(42, 1, 1)]
        [TestCase(42, 2, 2)]
        [TestCase(42, 3, 3)]
        [TestCase(42, 4, 4)]
        [TestCase(42, 5, 5)]
        [TestCase(42, 6, 6)]
        [TestCase(42, 7, 7)]
        [TestCase(42, 8, 8)]
        [TestCase(42, 9, 9)]
        [TestCase(42, 10, 10)]
        [TestCase(43, 0, 0)]
        [TestCase(43, 1, 1)]
        [TestCase(43, 2, 2)]
        [TestCase(43, 3, 3)]
        [TestCase(43, 4, 4)]
        [TestCase(43, 5, 5)]
        [TestCase(43, 6, 6)]
        [TestCase(43, 7, 7)]
        [TestCase(43, 8, 8)]
        [TestCase(43, 9, 9)]
        [TestCase(43, 10, 10)]
        [TestCase(44, 0, 0)]
        [TestCase(44, 1, 1)]
        [TestCase(44, 2, 2)]
        [TestCase(44, 3, 3)]
        [TestCase(44, 4, 4)]
        [TestCase(44, 5, 5)]
        [TestCase(44, 6, 6)]
        [TestCase(44, 7, 7)]
        [TestCase(44, 8, 8)]
        [TestCase(44, 9, 9)]
        [TestCase(44, 10, 10)]
        [TestCase(45, 0, 0)]
        [TestCase(45, 1, 1)]
        [TestCase(45, 2, 2)]
        [TestCase(45, 3, 3)]
        [TestCase(45, 4, 4)]
        [TestCase(45, 5, 5)]
        [TestCase(45, 6, 6)]
        [TestCase(45, 7, 7)]
        [TestCase(45, 8, 8)]
        [TestCase(45, 9, 9)]
        [TestCase(45, 10, 10)]
        public void AbilityModifier_WithMaxModifier(int baseValue, int max, int bonus)
        {
            ability.BaseScore = baseValue;
            ability.MaxModifier = max;
            Assert.That(ability.Modifier, Is.EqualTo(bonus));
        }

        [TestCase(1, 0, -5)]
        [TestCase(1, 1, -5)]
        [TestCase(1, 2, -5)]
        [TestCase(1, 3, -5)]
        [TestCase(1, 4, -5)]
        [TestCase(1, 5, -5)]
        [TestCase(1, 6, -5)]
        [TestCase(1, 7, -5)]
        [TestCase(1, 8, -5)]
        [TestCase(1, 9, -5)]
        [TestCase(1, 10, -5)]
        [TestCase(2, 0, -4)]
        [TestCase(2, 1, -4)]
        [TestCase(2, 2, -4)]
        [TestCase(2, 3, -4)]
        [TestCase(2, 4, -4)]
        [TestCase(2, 5, -4)]
        [TestCase(2, 6, -4)]
        [TestCase(2, 7, -4)]
        [TestCase(2, 8, -4)]
        [TestCase(2, 9, -4)]
        [TestCase(2, 10, -4)]
        [TestCase(3, 0, -4)]
        [TestCase(3, 1, -4)]
        [TestCase(3, 2, -4)]
        [TestCase(3, 3, -4)]
        [TestCase(3, 4, -4)]
        [TestCase(3, 5, -4)]
        [TestCase(3, 6, -4)]
        [TestCase(3, 7, -4)]
        [TestCase(3, 8, -4)]
        [TestCase(3, 9, -4)]
        [TestCase(3, 10, -4)]
        [TestCase(4, 0, -3)]
        [TestCase(4, 1, -3)]
        [TestCase(4, 2, -3)]
        [TestCase(4, 3, -3)]
        [TestCase(4, 4, -3)]
        [TestCase(4, 5, -3)]
        [TestCase(4, 6, -3)]
        [TestCase(4, 7, -3)]
        [TestCase(4, 8, -3)]
        [TestCase(4, 9, -3)]
        [TestCase(4, 10, -3)]
        [TestCase(5, 0, -3)]
        [TestCase(5, 1, -3)]
        [TestCase(5, 2, -3)]
        [TestCase(5, 3, -3)]
        [TestCase(5, 4, -3)]
        [TestCase(5, 5, -3)]
        [TestCase(5, 6, -3)]
        [TestCase(5, 7, -3)]
        [TestCase(5, 8, -3)]
        [TestCase(5, 9, -3)]
        [TestCase(5, 10, -3)]
        [TestCase(6, 0, -2)]
        [TestCase(6, 1, -2)]
        [TestCase(6, 2, -2)]
        [TestCase(6, 3, -2)]
        [TestCase(6, 4, -2)]
        [TestCase(6, 5, -2)]
        [TestCase(6, 6, -2)]
        [TestCase(6, 7, -2)]
        [TestCase(6, 8, -2)]
        [TestCase(6, 9, -2)]
        [TestCase(6, 10, -2)]
        [TestCase(7, 0, -2)]
        [TestCase(7, 1, -2)]
        [TestCase(7, 2, -2)]
        [TestCase(7, 3, -2)]
        [TestCase(7, 4, -2)]
        [TestCase(7, 5, -2)]
        [TestCase(7, 6, -2)]
        [TestCase(7, 7, -2)]
        [TestCase(7, 8, -2)]
        [TestCase(7, 9, -2)]
        [TestCase(7, 10, -2)]
        [TestCase(8, 0, -1)]
        [TestCase(8, 1, -1)]
        [TestCase(8, 2, -1)]
        [TestCase(8, 3, -1)]
        [TestCase(8, 4, -1)]
        [TestCase(8, 5, -1)]
        [TestCase(8, 6, -1)]
        [TestCase(8, 7, -1)]
        [TestCase(8, 8, -1)]
        [TestCase(8, 9, -1)]
        [TestCase(8, 10, -1)]
        [TestCase(9, 0, -1)]
        [TestCase(9, 1, -1)]
        [TestCase(9, 2, -1)]
        [TestCase(9, 3, -1)]
        [TestCase(9, 4, -1)]
        [TestCase(9, 5, -1)]
        [TestCase(9, 6, -1)]
        [TestCase(9, 7, -1)]
        [TestCase(9, 8, -1)]
        [TestCase(9, 9, -1)]
        [TestCase(9, 10, -1)]
        [TestCase(10, 0, 0)]
        [TestCase(10, 1, 0)]
        [TestCase(10, 2, 0)]
        [TestCase(10, 3, 0)]
        [TestCase(10, 4, 0)]
        [TestCase(10, 5, 0)]
        [TestCase(10, 6, 0)]
        [TestCase(10, 7, 0)]
        [TestCase(10, 8, 0)]
        [TestCase(10, 9, 0)]
        [TestCase(10, 10, 0)]
        [TestCase(11, 0, 0)]
        [TestCase(11, 1, 0)]
        [TestCase(11, 2, 0)]
        [TestCase(11, 3, 0)]
        [TestCase(11, 4, 0)]
        [TestCase(11, 5, 0)]
        [TestCase(11, 6, 0)]
        [TestCase(11, 7, 0)]
        [TestCase(11, 8, 0)]
        [TestCase(11, 9, 0)]
        [TestCase(11, 10, 0)]
        [TestCase(12, 0, 0)]
        [TestCase(12, 1, 1)]
        [TestCase(12, 2, 1)]
        [TestCase(12, 3, 1)]
        [TestCase(12, 4, 1)]
        [TestCase(12, 5, 1)]
        [TestCase(12, 6, 1)]
        [TestCase(12, 7, 1)]
        [TestCase(12, 8, 1)]
        [TestCase(12, 9, 1)]
        [TestCase(12, 10, 1)]
        [TestCase(13, 0, 0)]
        [TestCase(13, 1, 1)]
        [TestCase(13, 2, 1)]
        [TestCase(13, 3, 1)]
        [TestCase(13, 4, 1)]
        [TestCase(13, 5, 1)]
        [TestCase(13, 6, 1)]
        [TestCase(13, 7, 1)]
        [TestCase(13, 8, 1)]
        [TestCase(13, 9, 1)]
        [TestCase(13, 10, 1)]
        [TestCase(14, 0, 0)]
        [TestCase(14, 1, 1)]
        [TestCase(14, 2, 2)]
        [TestCase(14, 3, 2)]
        [TestCase(14, 4, 2)]
        [TestCase(14, 5, 2)]
        [TestCase(14, 6, 2)]
        [TestCase(14, 7, 2)]
        [TestCase(14, 8, 2)]
        [TestCase(14, 9, 2)]
        [TestCase(14, 10, 2)]
        [TestCase(15, 0, 0)]
        [TestCase(15, 1, 1)]
        [TestCase(15, 2, 2)]
        [TestCase(15, 3, 2)]
        [TestCase(15, 4, 2)]
        [TestCase(15, 5, 2)]
        [TestCase(15, 6, 2)]
        [TestCase(15, 7, 2)]
        [TestCase(15, 8, 2)]
        [TestCase(15, 9, 2)]
        [TestCase(15, 10, 2)]
        [TestCase(16, 0, 0)]
        [TestCase(16, 1, 1)]
        [TestCase(16, 2, 2)]
        [TestCase(16, 3, 3)]
        [TestCase(16, 4, 3)]
        [TestCase(16, 5, 3)]
        [TestCase(16, 6, 3)]
        [TestCase(16, 7, 3)]
        [TestCase(16, 8, 3)]
        [TestCase(16, 9, 3)]
        [TestCase(16, 10, 3)]
        [TestCase(17, 0, 0)]
        [TestCase(17, 1, 1)]
        [TestCase(17, 2, 2)]
        [TestCase(17, 3, 3)]
        [TestCase(17, 4, 3)]
        [TestCase(17, 5, 3)]
        [TestCase(17, 6, 3)]
        [TestCase(17, 7, 3)]
        [TestCase(17, 8, 3)]
        [TestCase(17, 9, 3)]
        [TestCase(17, 10, 3)]
        [TestCase(18, 0, 0)]
        [TestCase(18, 1, 1)]
        [TestCase(18, 2, 2)]
        [TestCase(18, 3, 3)]
        [TestCase(18, 4, 4)]
        [TestCase(18, 5, 4)]
        [TestCase(18, 6, 4)]
        [TestCase(18, 7, 4)]
        [TestCase(18, 8, 4)]
        [TestCase(18, 9, 4)]
        [TestCase(18, 10, 4)]
        [TestCase(19, 0, 0)]
        [TestCase(19, 1, 1)]
        [TestCase(19, 2, 2)]
        [TestCase(19, 3, 3)]
        [TestCase(19, 4, 4)]
        [TestCase(19, 5, 4)]
        [TestCase(19, 6, 4)]
        [TestCase(19, 7, 4)]
        [TestCase(19, 8, 4)]
        [TestCase(19, 9, 4)]
        [TestCase(19, 10, 4)]
        [TestCase(20, 0, 0)]
        [TestCase(20, 1, 1)]
        [TestCase(20, 2, 2)]
        [TestCase(20, 3, 3)]
        [TestCase(20, 4, 4)]
        [TestCase(20, 5, 5)]
        [TestCase(20, 6, 5)]
        [TestCase(20, 7, 5)]
        [TestCase(20, 8, 5)]
        [TestCase(20, 9, 5)]
        [TestCase(20, 10, 5)]
        [TestCase(21, 0, 0)]
        [TestCase(21, 1, 1)]
        [TestCase(21, 2, 2)]
        [TestCase(21, 3, 3)]
        [TestCase(21, 4, 4)]
        [TestCase(21, 5, 5)]
        [TestCase(21, 6, 5)]
        [TestCase(21, 7, 5)]
        [TestCase(21, 8, 5)]
        [TestCase(21, 9, 5)]
        [TestCase(21, 10, 5)]
        [TestCase(22, 0, 0)]
        [TestCase(22, 1, 1)]
        [TestCase(22, 2, 2)]
        [TestCase(22, 3, 3)]
        [TestCase(22, 4, 4)]
        [TestCase(22, 5, 5)]
        [TestCase(22, 6, 6)]
        [TestCase(22, 7, 6)]
        [TestCase(22, 8, 6)]
        [TestCase(22, 9, 6)]
        [TestCase(22, 10, 6)]
        [TestCase(23, 0, 0)]
        [TestCase(23, 1, 1)]
        [TestCase(23, 2, 2)]
        [TestCase(23, 3, 3)]
        [TestCase(23, 4, 4)]
        [TestCase(23, 5, 5)]
        [TestCase(23, 6, 6)]
        [TestCase(23, 7, 6)]
        [TestCase(23, 8, 6)]
        [TestCase(23, 9, 6)]
        [TestCase(23, 10, 6)]
        [TestCase(24, 0, 0)]
        [TestCase(24, 1, 1)]
        [TestCase(24, 2, 2)]
        [TestCase(24, 3, 3)]
        [TestCase(24, 4, 4)]
        [TestCase(24, 5, 5)]
        [TestCase(24, 6, 6)]
        [TestCase(24, 7, 7)]
        [TestCase(24, 8, 7)]
        [TestCase(24, 9, 7)]
        [TestCase(24, 10, 7)]
        [TestCase(25, 0, 0)]
        [TestCase(25, 1, 1)]
        [TestCase(25, 2, 2)]
        [TestCase(25, 3, 3)]
        [TestCase(25, 4, 4)]
        [TestCase(25, 5, 5)]
        [TestCase(25, 6, 6)]
        [TestCase(25, 7, 7)]
        [TestCase(25, 8, 7)]
        [TestCase(25, 9, 7)]
        [TestCase(25, 10, 7)]
        [TestCase(26, 0, 0)]
        [TestCase(26, 1, 1)]
        [TestCase(26, 2, 2)]
        [TestCase(26, 3, 3)]
        [TestCase(26, 4, 4)]
        [TestCase(26, 5, 5)]
        [TestCase(26, 6, 6)]
        [TestCase(26, 7, 7)]
        [TestCase(26, 8, 8)]
        [TestCase(26, 9, 8)]
        [TestCase(26, 10, 8)]
        [TestCase(27, 0, 0)]
        [TestCase(27, 1, 1)]
        [TestCase(27, 2, 2)]
        [TestCase(27, 3, 3)]
        [TestCase(27, 4, 4)]
        [TestCase(27, 5, 5)]
        [TestCase(27, 6, 6)]
        [TestCase(27, 7, 7)]
        [TestCase(27, 8, 8)]
        [TestCase(27, 9, 8)]
        [TestCase(27, 10, 8)]
        [TestCase(28, 0, 0)]
        [TestCase(28, 1, 1)]
        [TestCase(28, 2, 2)]
        [TestCase(28, 3, 3)]
        [TestCase(28, 4, 4)]
        [TestCase(28, 5, 5)]
        [TestCase(28, 6, 6)]
        [TestCase(28, 7, 7)]
        [TestCase(28, 8, 8)]
        [TestCase(28, 9, 9)]
        [TestCase(28, 10, 9)]
        [TestCase(29, 0, 0)]
        [TestCase(29, 1, 1)]
        [TestCase(29, 2, 2)]
        [TestCase(29, 3, 3)]
        [TestCase(29, 4, 4)]
        [TestCase(29, 5, 5)]
        [TestCase(29, 6, 6)]
        [TestCase(29, 7, 7)]
        [TestCase(29, 8, 8)]
        [TestCase(29, 9, 9)]
        [TestCase(29, 10, 9)]
        [TestCase(30, 0, 0)]
        [TestCase(30, 1, 1)]
        [TestCase(30, 2, 2)]
        [TestCase(30, 3, 3)]
        [TestCase(30, 4, 4)]
        [TestCase(30, 5, 5)]
        [TestCase(30, 6, 6)]
        [TestCase(30, 7, 7)]
        [TestCase(30, 8, 8)]
        [TestCase(30, 9, 9)]
        [TestCase(30, 10, 10)]
        [TestCase(31, 0, 0)]
        [TestCase(31, 1, 1)]
        [TestCase(31, 2, 2)]
        [TestCase(31, 3, 3)]
        [TestCase(31, 4, 4)]
        [TestCase(31, 5, 5)]
        [TestCase(31, 6, 6)]
        [TestCase(31, 7, 7)]
        [TestCase(31, 8, 8)]
        [TestCase(31, 9, 9)]
        [TestCase(31, 10, 10)]
        [TestCase(32, 0, 0)]
        [TestCase(32, 1, 1)]
        [TestCase(32, 2, 2)]
        [TestCase(32, 3, 3)]
        [TestCase(32, 4, 4)]
        [TestCase(32, 5, 5)]
        [TestCase(32, 6, 6)]
        [TestCase(32, 7, 7)]
        [TestCase(32, 8, 8)]
        [TestCase(32, 9, 9)]
        [TestCase(32, 10, 10)]
        [TestCase(33, 0, 0)]
        [TestCase(33, 1, 1)]
        [TestCase(33, 2, 2)]
        [TestCase(33, 3, 3)]
        [TestCase(33, 4, 4)]
        [TestCase(33, 5, 5)]
        [TestCase(33, 6, 6)]
        [TestCase(33, 7, 7)]
        [TestCase(33, 8, 8)]
        [TestCase(33, 9, 9)]
        [TestCase(33, 10, 10)]
        [TestCase(34, 0, 0)]
        [TestCase(34, 1, 1)]
        [TestCase(34, 2, 2)]
        [TestCase(34, 3, 3)]
        [TestCase(34, 4, 4)]
        [TestCase(34, 5, 5)]
        [TestCase(34, 6, 6)]
        [TestCase(34, 7, 7)]
        [TestCase(34, 8, 8)]
        [TestCase(34, 9, 9)]
        [TestCase(34, 10, 10)]
        [TestCase(35, 0, 0)]
        [TestCase(35, 1, 1)]
        [TestCase(35, 2, 2)]
        [TestCase(35, 3, 3)]
        [TestCase(35, 4, 4)]
        [TestCase(35, 5, 5)]
        [TestCase(35, 6, 6)]
        [TestCase(35, 7, 7)]
        [TestCase(35, 8, 8)]
        [TestCase(35, 9, 9)]
        [TestCase(35, 10, 10)]
        [TestCase(36, 0, 0)]
        [TestCase(36, 1, 1)]
        [TestCase(36, 2, 2)]
        [TestCase(36, 3, 3)]
        [TestCase(36, 4, 4)]
        [TestCase(36, 5, 5)]
        [TestCase(36, 6, 6)]
        [TestCase(36, 7, 7)]
        [TestCase(36, 8, 8)]
        [TestCase(36, 9, 9)]
        [TestCase(36, 10, 10)]
        [TestCase(37, 0, 0)]
        [TestCase(37, 1, 1)]
        [TestCase(37, 2, 2)]
        [TestCase(37, 3, 3)]
        [TestCase(37, 4, 4)]
        [TestCase(37, 5, 5)]
        [TestCase(37, 6, 6)]
        [TestCase(37, 7, 7)]
        [TestCase(37, 8, 8)]
        [TestCase(37, 9, 9)]
        [TestCase(37, 10, 10)]
        [TestCase(38, 0, 0)]
        [TestCase(38, 1, 1)]
        [TestCase(38, 2, 2)]
        [TestCase(38, 3, 3)]
        [TestCase(38, 4, 4)]
        [TestCase(38, 5, 5)]
        [TestCase(38, 6, 6)]
        [TestCase(38, 7, 7)]
        [TestCase(38, 8, 8)]
        [TestCase(38, 9, 9)]
        [TestCase(38, 10, 10)]
        [TestCase(39, 0, 0)]
        [TestCase(39, 1, 1)]
        [TestCase(39, 2, 2)]
        [TestCase(39, 3, 3)]
        [TestCase(39, 4, 4)]
        [TestCase(39, 5, 5)]
        [TestCase(39, 6, 6)]
        [TestCase(39, 7, 7)]
        [TestCase(39, 8, 8)]
        [TestCase(39, 9, 9)]
        [TestCase(39, 10, 10)]
        [TestCase(40, 0, 0)]
        [TestCase(40, 1, 1)]
        [TestCase(40, 2, 2)]
        [TestCase(40, 3, 3)]
        [TestCase(40, 4, 4)]
        [TestCase(40, 5, 5)]
        [TestCase(40, 6, 6)]
        [TestCase(40, 7, 7)]
        [TestCase(40, 8, 8)]
        [TestCase(40, 9, 9)]
        [TestCase(40, 10, 10)]
        [TestCase(41, 0, 0)]
        [TestCase(41, 1, 1)]
        [TestCase(41, 2, 2)]
        [TestCase(41, 3, 3)]
        [TestCase(41, 4, 4)]
        [TestCase(41, 5, 5)]
        [TestCase(41, 6, 6)]
        [TestCase(41, 7, 7)]
        [TestCase(41, 8, 8)]
        [TestCase(41, 9, 9)]
        [TestCase(41, 10, 10)]
        [TestCase(42, 0, 0)]
        [TestCase(42, 1, 1)]
        [TestCase(42, 2, 2)]
        [TestCase(42, 3, 3)]
        [TestCase(42, 4, 4)]
        [TestCase(42, 5, 5)]
        [TestCase(42, 6, 6)]
        [TestCase(42, 7, 7)]
        [TestCase(42, 8, 8)]
        [TestCase(42, 9, 9)]
        [TestCase(42, 10, 10)]
        [TestCase(43, 0, 0)]
        [TestCase(43, 1, 1)]
        [TestCase(43, 2, 2)]
        [TestCase(43, 3, 3)]
        [TestCase(43, 4, 4)]
        [TestCase(43, 5, 5)]
        [TestCase(43, 6, 6)]
        [TestCase(43, 7, 7)]
        [TestCase(43, 8, 8)]
        [TestCase(43, 9, 9)]
        [TestCase(43, 10, 10)]
        [TestCase(44, 0, 0)]
        [TestCase(44, 1, 1)]
        [TestCase(44, 2, 2)]
        [TestCase(44, 3, 3)]
        [TestCase(44, 4, 4)]
        [TestCase(44, 5, 5)]
        [TestCase(44, 6, 6)]
        [TestCase(44, 7, 7)]
        [TestCase(44, 8, 8)]
        [TestCase(44, 9, 9)]
        [TestCase(44, 10, 10)]
        [TestCase(45, 0, 0)]
        [TestCase(45, 1, 1)]
        [TestCase(45, 2, 2)]
        [TestCase(45, 3, 3)]
        [TestCase(45, 4, 4)]
        [TestCase(45, 5, 5)]
        [TestCase(45, 6, 6)]
        [TestCase(45, 7, 7)]
        [TestCase(45, 8, 8)]
        [TestCase(45, 9, 9)]
        [TestCase(45, 10, 10)]
        public void AbilityModifier_WithTemplateScore_WithMaxModifier(int templateScore, int max, int bonus)
        {
            ability.TemplateScore = templateScore;
            ability.MaxModifier = max;
            Assert.That(ability.Modifier, Is.EqualTo(bonus));
        }

        [Test]
        public void FullScore_UseTemplateScore()
        {
            ability.TemplateScore = 9266;
            Assert.That(ability.FullScore, Is.EqualTo(9266));
            Assert.That(ability.Modifier, Is.EqualTo(4628));
        }

        [Test]
        public void FullScore_UseTemplateScore_Zero()
        {
            ability.TemplateScore = 0;
            ability.TemplateAdjustment = 0;

            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void FullScore_UseTemplateScore_NoAdditives()
        {
            ability.TemplateScore = 9266;
            ability.BaseScore = 666;
            ability.AdvancementAdjustment = 666;
            ability.RacialAdjustment = 666;

            Assert.That(ability.FullScore, Is.EqualTo(9266));
            Assert.That(ability.Modifier, Is.EqualTo(4628));
        }

        [Test]
        public void FullScore_UseTemplateScore_AddTemplateAdjustment()
        {
            ability.TemplateScore = 9266;
            ability.TemplateAdjustment = 90210;

            Assert.That(ability.FullScore, Is.EqualTo(9266 + 90210));
            Assert.That(ability.Modifier, Is.EqualTo(49733));
        }

        [Test]
        public void FullScore_UseTemplateScore_AddNegativeTemplateAdjustment()
        {
            ability.TemplateScore = 9266;
            ability.TemplateAdjustment = -6;

            Assert.That(ability.FullScore, Is.EqualTo(9260));
            Assert.That(ability.Modifier, Is.EqualTo(4625));
        }

        [Test]
        public void FullScore_AddRacialAdjustment()
        {
            ability.RacialAdjustment = 9266;
            Assert.That(ability.FullScore, Is.EqualTo(9276));
            Assert.That(ability.Modifier, Is.EqualTo(4633));
        }

        [Test]
        public void FullScore_AddNegativeRacialAdjustment()
        {
            ability.RacialAdjustment = -6;
            Assert.That(ability.FullScore, Is.EqualTo(4));
            Assert.That(ability.Modifier, Is.EqualTo(-3));
        }

        [Test]
        public void FullScore_AddAdvancementAdjustment()
        {
            ability.AdvancementAdjustment = 9266;
            Assert.That(ability.FullScore, Is.EqualTo(9276));
            Assert.That(ability.Modifier, Is.EqualTo(4633));
        }

        [Test]
        public void FullScore_AddTemplateAdjustment()
        {
            ability.TemplateAdjustment = 9266;
            Assert.That(ability.FullScore, Is.EqualTo(9276));
            Assert.That(ability.Modifier, Is.EqualTo(4633));
        }

        [Test]
        public void FullScore_AddAllAdjustments()
        {
            ability.AdvancementAdjustment = 9266;
            ability.RacialAdjustment = 90210;
            ability.TemplateAdjustment = 42;

            Assert.That(ability.FullScore, Is.EqualTo(Ability.DefaultScore + 9266 + 90210 + 42));
            Assert.That(ability.Modifier, Is.EqualTo(49759));
        }

        [Test]
        public void AbilityCannotHaveFullScoreLessThan1()
        {
            ability.RacialAdjustment = -9266;
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityCanHaveScoreOfZero()
        {
            ability.BaseScore = 0;
            Assert.That(ability.BaseScore, Is.Zero);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void AbilityHasTemplateScore(int score)
        {
            ability.TemplateScore = score;

            Assert.That(ability.HasTemplateScore, Is.True);
            Assert.That(ability.FullScore, Is.EqualTo(score));
        }

        [Test]
        public void AbilityDoesNotHaveTemplateScore()
        {
            ability.TemplateScore = -1;

            Assert.That(ability.HasTemplateScore, Is.False);
            Assert.That(ability.FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityHasScore_HasTemplateScore()
        {
            ability.TemplateScore = 1;
            ability.BaseScore = 0;

            Assert.That(ability.HasScore, Is.True);
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityHasScore()
        {
            ability.BaseScore = 1;

            Assert.That(ability.HasScore, Is.True);
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityDoesNotHaveScore_HasTemplateScore()
        {
            ability.TemplateScore = 0;
            ability.BaseScore = 1;

            Assert.That(ability.HasScore, Is.False);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityDoesNotHaveScore_HasTemplateScoreAndAdjustment()
        {
            ability.TemplateScore = 0;
            ability.TemplateAdjustment = 1;
            ability.BaseScore = 1;

            Assert.That(ability.HasScore, Is.False);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityDoesNotHaveScore()
        {
            ability.BaseScore = 0;

            Assert.That(ability.HasScore, Is.False);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityHasScore_WithTemplateAdjustment()
        {
            ability.BaseScore = 0;
            ability.TemplateAdjustment = 1;

            Assert.That(ability.HasScore, Is.True);
            Assert.That(ability.FullScore, Is.EqualTo(1));
            Assert.That(ability.Modifier, Is.EqualTo(-5));
        }

        [Test]
        public void AbilityHasNoScore_WithTemplateAdjustment()
        {
            ability.BaseScore = 1;
            ability.TemplateAdjustment = -1;

            Assert.That(ability.HasScore, Is.False);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void AbilityHasFullScoreOfZero()
        {
            ability.BaseScore = 0;
            ability.RacialAdjustment = 9266;
            ability.AdvancementAdjustment = 90210;

            Assert.That(ability.BaseScore, Is.Zero);
            Assert.That(ability.FullScore, Is.Zero);
            Assert.That(ability.Modifier, Is.Zero);
        }

        [Test]
        public void Bonus_TotalsBonuses()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void Bonus_TotalsBonuses_WithNegative()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.Bonuses.Add(new Bonus { Value = -42 });

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210 - 42));
        }

        [Test]
        public void Bonus_TotalsBonuses_WithAllNegative()
        {
            ability.Bonuses.Add(new Bonus { Value = -9266 });
            ability.Bonuses.Add(new Bonus { Value = -42 });

            Assert.That(ability.Bonus, Is.EqualTo(-9266 - 42));
        }

        [Test]
        public void Bonus_TotalsBonuses_DoNotIncludeConditional()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 666, Condition = "only sometimes" });
            ability.Bonuses.Add(new Bonus { Value = 90210 });

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void Bonus_TotalsBonuses_AllConditional()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266, Condition = "when I want" });
            ability.Bonuses.Add(new Bonus { Value = 666, Condition = "only sometimes" });

            Assert.That(ability.Bonus, Is.Zero);
        }

        [Test]
        public void FullScore_IncludeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 + 90210));
        }

        [Test]
        public void FullScore_IncludeBonus_NoConditional()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.Bonuses.Add(new Bonus { Value = 666, Condition = "only sometimes" });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 + 90210));
        }

        [Test]
        public void FullScore_IncludeNegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = -600 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 - 600));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 - 600));
        }

        [Test]
        public void FullScore_IncludeAllNegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = -4 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(-4));
            Assert.That(ability.FullScore, Is.EqualTo(42 - 4));
        }

        [Test]
        public void FullScore_IncludeNegativeBonus_StillMin1()
        {
            ability.Bonuses.Add(new Bonus { Value = -600 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(-600));
            Assert.That(ability.FullScore, Is.EqualTo(1));
        }

        [Test]
        public void Modifier_IncludeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 + 90210));
            Assert.That(ability.Modifier, Is.EqualTo(49754));
        }

        [Test]
        public void Modifier_IncludeBonus_NoConditional()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = 90210 });
            ability.Bonuses.Add(new Bonus { Value = 666, Condition = "only sometimes" });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 + 90210));
            Assert.That(ability.Modifier, Is.EqualTo(49754));
        }

        [Test]
        public void Modifier_IncludeNegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = 9266 });
            ability.Bonuses.Add(new Bonus { Value = -600 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(9266 - 600));
            Assert.That(ability.FullScore, Is.EqualTo(42 + 9266 - 600));
            Assert.That(ability.Modifier, Is.EqualTo(4349));
        }

        [Test]
        public void Modifier_IncludeAllNegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = -4 });
            ability.BaseScore = 42;

            Assert.That(ability.Bonus, Is.EqualTo(-4));
            Assert.That(ability.FullScore, Is.EqualTo(42 - 4));
            Assert.That(ability.Modifier, Is.EqualTo(14));
        }

        [Test]
        public void Modifier_IncludeAllNegativeBonus_NegativeBonus()
        {
            ability.Bonuses.Add(new Bonus { Value = -4 });
            ability.BaseScore = 12;

            Assert.That(ability.Bonus, Is.EqualTo(-4));
            Assert.That(ability.FullScore, Is.EqualTo(12 - 4));
            Assert.That(ability.Modifier, Is.EqualTo(-1));
        }
    }
}