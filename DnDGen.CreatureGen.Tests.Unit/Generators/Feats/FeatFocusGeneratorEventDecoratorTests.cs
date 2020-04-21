﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.EventGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatFocusGeneratorEventDecoratorTests
    {
        private IFeatFocusGenerator decorator;
        private Mock<IFeatFocusGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private List<Skill> skills;
        private List<RequiredFeatSelection> requiredFeats;
        private List<Feat> otherFeats;
        private Dictionary<string, Ability> abilities;
        private List<Attack> attacks;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IFeatFocusGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new FeatFocusGeneratorEventDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            skills = new List<Skill>();
            requiredFeats = new List<RequiredFeatSelection>();
            otherFeats = new List<Feat>();
            abilities = new Dictionary<string, Ability>();
            attacks = new List<Attack>();
        }

        [Test]
        public void ReturnInnerFocusAllowingAll()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks)).Returns("focus");

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void LogEventsForFocusGenerationAllowingAll()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks)).Returns("focus");

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feat: focus"), Times.Once);
        }

        [Test]
        public void LogEventsForEmptyFocusGenerationAllowingAll()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks)).Returns(string.Empty);

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks);
            Assert.That(focus, Is.Empty);
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no focus for feat"), Times.Once);
        }

        [Test]
        public void ReturnInnerFocus()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks)).Returns("focus");

            var focus = decorator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void LogEventsForFocusGeneration()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks)).Returns("focus");

            var focus = decorator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feat: focus"), Times.Once);
        }

        [Test]
        public void LogEventsForEmptyFocusGeneration()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks)).Returns(string.Empty);

            var focus = decorator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 9266, abilities, attacks);
            Assert.That(focus, Is.Empty);
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no focus for feat"), Times.Once);
        }

        [Test]
        public void ReturnInnerFocusAllowingAllEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, abilities)).Returns("focus");

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void LogEventsForFocusGenerationAllowingAllEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, abilities)).Returns("focus");

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feat: focus"), Times.Once);
        }

        [Test]
        public void LogEventsForEmptyFocusGenerationAllowingAllEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, abilities)).Returns(string.Empty);

            var focus = decorator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.Empty);
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no focus for feat"), Times.Once);
        }

        [Test]
        public void ReturnInnerFocusEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, abilities)).Returns("focus");

            var focus = decorator.GenerateFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void LogEventsForFocusGenerationEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, abilities)).Returns("focus");

            var focus = decorator.GenerateFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feat: focus"), Times.Once);
        }

        [Test]
        public void LogEventsForEmptyFocusGenerationEarly()
        {
            mockInnerGenerator.Setup(g => g.GenerateFrom("feat", "focus type", skills, abilities)).Returns(string.Empty);

            var focus = decorator.GenerateFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.Empty);
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating focus for feat"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no focus for feat"), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ReturnIfFocusIsPreset(bool preset)
        {
            mockInnerGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(preset);

            var isPreset = decorator.FocusTypeIsPreset("focus type");
            Assert.That(isPreset, Is.EqualTo(preset));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DoNotLogEventsForIfFocusIsPreset(bool preset)
        {
            mockInnerGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(preset);

            var focus = decorator.FocusTypeIsPreset("focus type");
            Assert.That(focus, Is.EqualTo(preset));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}