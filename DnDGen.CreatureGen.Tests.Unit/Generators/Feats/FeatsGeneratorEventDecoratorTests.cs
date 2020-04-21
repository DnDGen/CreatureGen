﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.EventGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatsGeneratorEventDecoratorTests
    {
        private IFeatsGenerator decorator;
        private Mock<IFeatsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Dictionary<string, Ability> abilities;
        private Dictionary<string, Measurement> speeds;
        private List<Skill> skills;
        private List<Feat> preselectedFeats;
        private HitPoints hitPoints;
        private List<Feat> specialQualities;
        private List<Attack> attacks;
        private CreatureType creatureType;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IFeatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new FeatsGeneratorEventDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            abilities = new Dictionary<string, Ability>();
            speeds = new Dictionary<string, Measurement>();
            skills = new List<Skill>();
            preselectedFeats = new List<Feat>();
            hitPoints = new HitPoints();
            specialQualities = new List<Feat>();
            attacks = new List<Attack>();
            creatureType = new CreatureType();
            alignment = new Alignment("creature alignment");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ReturnInnerFeats(bool canUseEquipment)
        {
            var feats = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" },
            };

            mockInnerGenerator.Setup(g => g.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", canUseEquipment)).Returns(feats);

            var generatedFeats = decorator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", canUseEquipment);
            Assert.That(generatedFeats, Is.EqualTo(feats));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void LogEventsForFeatsGeneration(bool canUseEquipment)
        {
            var feats = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" },
            };

            mockInnerGenerator.Setup(g => g.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", canUseEquipment)).Returns(feats);

            var generatedFeats = decorator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", canUseEquipment);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating feats"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated 2 feats"), Times.Once);
        }

        [Test]
        public void ReturnInnerSpecialQualities()
        {
            var specialQualities = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" },
            };

            mockInnerGenerator.Setup(g => g.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment)).Returns(specialQualities);

            var generatedSpecialQualities = decorator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(generatedSpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public void ReturnInnerSpecialQualitiesAndCanUseEquipment()
        {
            var specialQualities = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" },
            };

            mockInnerGenerator.Setup(g => g.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, true, "size", alignment)).Returns(specialQualities);

            var generatedSpecialQualities = decorator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, true, "size", alignment);
            Assert.That(generatedSpecialQualities, Is.EqualTo(specialQualities));
        }

        [Test]
        public void LogEventsForSpecialQualitiesGeneration()
        {
            var specialQualities = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" },
            };

            mockInnerGenerator.Setup(g => g.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment)).Returns(specialQualities);

            var generatedSpecialQualities = decorator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(generatedSpecialQualities, Is.EqualTo(specialQualities));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating special qualities for creature"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated 2 special qualities"), Times.Once);
        }
    }
}