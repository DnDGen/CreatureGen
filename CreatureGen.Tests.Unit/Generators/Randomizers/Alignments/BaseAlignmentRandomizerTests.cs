﻿using CreatureGen.Alignments;
using CreatureGen.Domain.Generators.Randomizers.Alignments;
using CreatureGen.Domain.Tables;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Alignments
{
    [TestFixture]
    public class BaseAlignmentRandomizerTests
    {
        private TestAlignmentRandomizer alignmentRandomizer;
        private Mock<IPercentileSelector> mockPercentileSelector;

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            var mockCollectionsSelector = new Mock<ICollectionSelector>();
            var generator = new ConfigurableIterationGenerator(2);

            alignmentRandomizer = new TestAlignmentRandomizer(mockPercentileSelector.Object, generator, mockCollectionsSelector.Object);

            mockPercentileSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness)).Returns(new[] { "other lawfulness", "lawfulness" });
            mockPercentileSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns(new[] { "other goodness", "goodness" });
            mockPercentileSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness)).Returns("lawfulness");
            mockPercentileSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns("goodness");

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<Alignment>>())).Returns((IEnumerable<Alignment> ss) => ss.ElementAt(index++ % ss.Count()));
        }

        [Test]
        public void ReturnLawfulnessFromSelector()
        {
            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo("lawfulness"));
        }

        [Test]
        public void ReturnsGoodnessFromSelector()
        {
            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo("goodness"));
        }

        [Test]
        public void RandomizeThrowsErrorIfNoLawfulnessPossibleResults()
        {
            mockPercentileSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness)).Returns(Enumerable.Empty<String>());
            Assert.That(() => alignmentRandomizer.Randomize(), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeThrowsErrorIfNoGoodnessPossibleResults()
        {
            mockPercentileSelector.Setup(p => p.SelectAllFrom(TableNameConstants.Set.Percentile.AlignmentGoodness)).Returns(Enumerable.Empty<String>());
            Assert.That(() => alignmentRandomizer.Randomize(), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeLoopsUntilAlignmentWithAllowedGoodnessIsRolled()
        {
            alignmentRandomizer.NotAllowedGoodness = "goodness";

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentGoodness))
                .Returns("goodness").Returns("other goodness");

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo("other goodness"));
        }

        [Test]
        public void RandomizeLoopsUntilAlignmentWithAllowedLawfulnessIsRolled()
        {
            alignmentRandomizer.NotAllowedLawfulness = "lawfulness";

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness))
                .Returns("lawfulness").Returns("other lawfulness");

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo("other lawfulness"));
        }

        [Test]
        public void RandomizeReturnsDefault()
        {
            alignmentRandomizer.NotAllowedGoodness = "goodness";
            alignmentRandomizer.NotAllowedLawfulness = "lawfulness";

            mockPercentileSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentLawfulness))
                .Returns("lawfulness");

            mockPercentileSelector.Setup(p => p.SelectFrom(TableNameConstants.Set.Percentile.AlignmentGoodness))
                .Returns("goodness");

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo("other lawfulness"));
            Assert.That(alignment.Goodness, Is.EqualTo("other goodness"));
        }

        [Test]
        public void GetAllPossibleResults()
        {
            var allAlignments = alignmentRandomizer.GetAllPossibleResults();
            var alignmentStrings = allAlignments.Select(a => a.ToString());

            Assert.That(alignmentStrings, Contains.Item("lawfulness goodness"));
            Assert.That(alignmentStrings, Contains.Item("lawfulness other goodness"));
            Assert.That(alignmentStrings, Contains.Item("other lawfulness goodness"));
            Assert.That(alignmentStrings, Contains.Item("other lawfulness other goodness"));
            Assert.That(alignmentStrings.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedCharacterClasses()
        {
            alignmentRandomizer.NotAllowedLawfulness = "lawfulness";
            alignmentRandomizer.NotAllowedGoodness = "goodness";

            var results = alignmentRandomizer.GetAllPossibleResults();
            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.First().ToString(), Is.EqualTo("other lawfulness other goodness"));
        }

        private class TestAlignmentRandomizer : BaseAlignmentRandomizer
        {
            public string NotAllowedLawfulness { get; set; }
            public string NotAllowedGoodness { get; set; }

            public TestAlignmentRandomizer(IPercentileSelector innerSelector, Generator generator, ICollectionSelector collectionsSelector)
                : base(innerSelector, generator, collectionsSelector)
            { }

            protected override bool AlignmentIsAllowed(Alignment alignment)
            {
                return alignment.Lawfulness != NotAllowedLawfulness && alignment.Goodness != NotAllowedGoodness;
            }
        }
    }
}