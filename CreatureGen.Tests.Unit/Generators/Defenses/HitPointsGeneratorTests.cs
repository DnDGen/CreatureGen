﻿using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Generators.Defenses;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Defenses
{
    [TestFixture]
    public class HitPointsGeneratorTests
    {
        private Mock<Dice> mockDice;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private IHitPointsGenerator hitPointsGenerator;

        private Ability constitution;
        private List<Feat> feats;
        private Dictionary<int, Mock<PartialRoll>> mockPartialRolls;
        private TypeAndAmountSelection hitDieSelection;
        private TypeAndAmountSelection templateHitDieSelection;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            hitPointsGenerator = new HitPointsGenerator(mockDice.Object, mockTypeAndAmountSelector.Object);

            mockPartialRolls = new Dictionary<int, Mock<PartialRoll>>();
            feats = new List<Feat>();
            constitution = new Ability(AbilityConstants.Constitution);
            hitDieSelection = new TypeAndAmountSelection();
            templateHitDieSelection = new TypeAndAmountSelection();

            hitDieSelection.Type = "9266";
            hitDieSelection.Amount = 90210;

            SetUpRoll(9266, 90210, 42);
            SetUpAverageRoll("9266d90210", 600.5);

            mockTypeAndAmountSelector.Setup(s => s.SelectOne(TableNameConstants.Set.Adjustments.HitDice, "creature")).Returns(hitDieSelection);
            mockTypeAndAmountSelector.Setup(s => s.SelectOne(TableNameConstants.Set.Adjustments.HitDice, "template")).Returns(templateHitDieSelection);

            mockDice.Setup(d => d.Roll(It.IsAny<int>())).Returns((int q) => mockPartialRolls[q].Object);
        }

        private void SetUpRoll(int quantity, int die, params int[] rolls)
        {
            if (!mockPartialRolls.ContainsKey(quantity))
                mockPartialRolls[quantity] = new Mock<PartialRoll>();

            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsIndividualRolls()).Returns(rolls);

            mockPartialRolls[quantity].Setup(r => r.d(die)).Returns(endRoll.Object);
        }

        private void SetUpAverageRoll(string roll, double value)
        {
            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsPotentialAverage()).Returns(value);

            mockDice.Setup(d => d.Roll(roll)).Returns(endRoll.Object);
        }

        [Test]
        public void GetHitDie()
        {
            var hitPoints = hitPointsGenerator.GenerateFor("creature", constitution);
            Assert.That(hitPoints.Bonus, Is.EqualTo(0));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void ConstitutionBonusAppliedPerHitDie()
        {
            constitution.BaseValue = 20;
            SetUpRoll(9266, 90210, 42, 600);
            SetUpAverageRoll("9266d90210+46330", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", constitution);
            Assert.That(hitPoints.Bonus, Is.EqualTo(0));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210+46330"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(600 + 42 + 2 * 5));
        }

        [Test]
        public void ConstitutionPenaltyAppliedPerHitDie()
        {
            constitution.BaseValue = 2;
            SetUpRoll(9266, 90210, 42, 600);
            SetUpAverageRoll("9266d90210-37064", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", constitution);
            Assert.That(hitPoints.Bonus, Is.EqualTo(0));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210-37064"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(600 + 42 + 2 * -4));
        }

        [Test]
        public void NoConstitutionAppliedPerHitDie()
        {
            SetUpRoll(9266, 90210, 42, 600);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", null);
            Assert.That(hitPoints.Bonus, Is.EqualTo(0));
            Assert.That(hitPoints.Constitution, Is.Null);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(600 + 42));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerHitDie()
        {
            constitution.BaseValue = 1;
            SetUpRoll(9266, 90210, 1, 3, 5);
            SetUpAverageRoll("9266d90210-46330", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", constitution);
            Assert.That(hitPoints.Bonus, Is.EqualTo(0));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210-46330"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(3));
        }

        [Test]
        public void MinimumCheckAppliedPerHitDieOnRoll()
        {
            constitution.BaseValue = 6;
            SetUpRoll(9266, 90210, 1, 2, 4);
            SetUpAverageRoll("9266d90210-18532", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", constitution);
            Assert.That(hitPoints.Bonus, Is.EqualTo(0));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210-18532"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.Not.EqualTo(1)); //1 + 2 + 4 + 3 * -2 = 7 - 6
            Assert.That(hitPoints.Total, Is.EqualTo(4)); //[1-2,1]+[2-2,1]+[4-2,1] = 1 + 1 + 2
        }

        [Test]
        public void ToughnessIncreassHitPoints()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            SetUpAverageRoll("9266d90210+3", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", constitution);
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(3));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210+3"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(45));
        }

        [Test]
        public void NotToughnessDoesNotIncreassHitPoints()
        {
            feats.Add(new Feat { Name = "Not " + FeatConstants.Toughness, Power = 3 });

            SetUpAverageRoll("9266d90210+3", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", constitution);
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(0));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void ToughnessIncreassHitPointsMultipleTimes()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            SetUpAverageRoll("9266d90210+6", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", constitution);
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(6));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210+6"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(48));
        }
    }
}