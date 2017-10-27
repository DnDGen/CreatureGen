﻿using CreatureGen.Alignments;
using CreatureGen.Domain.Generators.Randomizers.Alignments;
using CreatureGen.Domain.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Alignments
{
    [TestFixture]
    public class GoodAlignmentRandomizerTests
    {
        private IEnumerable<Alignment> alignments;

        [SetUp]
        public void Setup()
        {
            var mockPercentileSelector = new Mock<IPercentileSelector>();
            var mockCollecionsSelector = new Mock<ICollectionSelector>();
            var generator = new ConfigurableIterationGenerator();
            var randomizer = new GoodAlignmentRandomizer(mockPercentileSelector.Object, generator, mockCollecionsSelector.Object);

            mockPercentileSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentGoodness))
                .Returns(new[] { AlignmentConstants.Good, AlignmentConstants.Neutral, AlignmentConstants.Evil });
            mockPercentileSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness))
                .Returns(new[] { AlignmentConstants.Lawful, AlignmentConstants.Neutral, AlignmentConstants.Chaotic });

            alignments = randomizer.GetAllPossibleResults();
        }

        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good)]
        public void Allowed(string lawfulness, string goodness)
        {
            var expectedAlignment = new Alignment { Lawfulness = lawfulness, Goodness = goodness };
            Assert.That(alignments, Contains.Item(expectedAlignment));
        }

        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil)]
        public void NotAllowed(string lawfulness, string goodness)
        {
            var expectedAlignment = new Alignment { Lawfulness = lawfulness, Goodness = goodness };
            Assert.That(alignments, Is.All.Not.EqualTo(expectedAlignment));
        }
    }
}