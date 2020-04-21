﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Skills;
using DnDGen.EventGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Skills
{
    [TestFixture]
    public class SkillsGeneratorEventDecoratorTests
    {
        private ISkillsGenerator decorator;
        private Mock<ISkillsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Dictionary<string, Ability> abilities;
        private HitPoints hitPoints;
        private CreatureType creatureType;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ISkillsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new SkillsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            abilities = new Dictionary<string, Ability>();
            hitPoints = new HitPoints();
            creatureType = new CreatureType();

            hitPoints.HitDiceQuantity = 9266;
            abilities["ability"] = new Ability("ability");
        }

        [Test]
        public void ReturnInnerSkills()
        {
            var skills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            mockInnerGenerator.Setup(g => g.GenerateFor(hitPoints, "creature", creatureType, abilities, true, "size")).Returns(skills);

            var generatedSkills = decorator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, "size");
            Assert.That(generatedSkills, Is.EqualTo(skills));
        }

        [Test]
        public void LogEventsForSkillsGeneration()
        {
            var skills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            mockInnerGenerator.Setup(g => g.GenerateFor(hitPoints, "creature", creatureType, abilities, false, "size")).Returns(skills);

            var generatedSkills = decorator.GenerateFor(hitPoints, "creature", creatureType, abilities, false, "size");
            Assert.That(generatedSkills, Is.EqualTo(skills));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating skills for creature"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated 2 skills"), Times.Once);
        }

        [Test]
        public void ReturnUpdatedSkills()
        {
            var skills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            var updatedSkills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            mockInnerGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            var generatedSkills = decorator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(generatedSkills, Is.EqualTo(updatedSkills));
            Assert.That(generatedSkills, Is.Not.EqualTo(skills));
        }

        [Test]
        public void LogNoEventsForUpdatedSkills()
        {
            var skills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            var updatedSkills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            mockInnerGenerator.Setup(g => g.ApplyBonusesFromFeats(skills, feats, abilities)).Returns(updatedSkills);

            var generatedSkills = decorator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(generatedSkills, Is.EqualTo(updatedSkills));
            Assert.That(generatedSkills, Is.Not.EqualTo(skills));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnInnerModifiedSkills()
        {
            var skills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            var updatedSkills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            var equipment = new Equipment();

            mockInnerGenerator.Setup(g => g.SetArmorCheckPenalties(skills, equipment)).Returns(updatedSkills);

            var modifiedSkills = decorator.SetArmorCheckPenalties(skills, equipment);
            Assert.That(modifiedSkills, Is.EqualTo(updatedSkills));
        }

        [Test]
        public void LogEventsForSettingArmorCheckPenalties()
        {
            var skills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            var updatedSkills = new[]
            {
                new Skill("skill 1", abilities["ability"], 9266),
                new Skill("skill 2", abilities["ability"], 90210),
            };

            var equipment = new Equipment();

            mockInnerGenerator.Setup(g => g.SetArmorCheckPenalties(skills, equipment)).Returns(updatedSkills);

            var modifiedSkills = decorator.SetArmorCheckPenalties(skills, equipment);
            Assert.That(modifiedSkills, Is.EqualTo(updatedSkills));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Setting armor check penalties"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Set armor check penalties"), Times.Once);
        }
    }
}
