﻿using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class CreatureDataSelectorTests
    {
        private const string Creature = "creature";

        private ICreatureDataSelector creatureDataSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private string[] data;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            creatureDataSelector = new CreatureDataSelector(mockCollectionSelector.Object);

            data = DataIndexConstants.CreatureData.InitializeData();
            data[DataIndexConstants.CreatureData.ChallengeRating] = "challenge rating";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "9266";
            data[DataIndexConstants.CreatureData.Reach] = "90.210";
            data[DataIndexConstants.CreatureData.Size] = "size";
            data[DataIndexConstants.CreatureData.Space] = "4.2";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;
            data[DataIndexConstants.CreatureData.CasterLevel] = "600";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "1337";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "1336";

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureData, Creature))
                .Returns(data);
        }

        [Test]
        public void SelectChallengeRating()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public void SelectCreatureDataWithLevelAdjustment()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.LevelAdjustment, Is.EqualTo(9266));
        }

        [Test]
        public void SelectCreatureDataWithLevelAdjustmentOfZero()
        {
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "0";

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.LevelAdjustment, Is.EqualTo(0));
        }

        [Test]
        public void SelectCreatureDataWithoutLevelAdjustment()
        {
            data[DataIndexConstants.CreatureData.LevelAdjustment] = string.Empty;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.LevelAdjustment, Is.Null);
        }

        [Test]
        public void SelectReach()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.Reach, Is.EqualTo(90.210));
        }

        [Test]
        public void SelectSpace()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.Space, Is.EqualTo(4.2));
        }

        [Test]
        public void SelectCasterLevel()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.CasterLevel, Is.EqualTo(600));
        }

        [Test]
        public void SelectCreatureDataCanUseEquipment()
        {
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.CanUseEquipment, Is.True);
        }

        [Test]
        public void SelectCreatureDataCannotUseEquipment()
        {
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.FalseString;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.CanUseEquipment, Is.False);
        }

        [Test]
        public void SelectNaturalArmor()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.NaturalArmor, Is.EqualTo(1337));
        }

        [Test]
        public void SelectNumberOfHands()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.NumberOfHands, Is.EqualTo(1336));
        }
    }
}