﻿using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Skills;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Selectors.Selections;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Skills
{
    [TestFixture]
    public class SkillsGeneratorTests
    {
        private ISkillsGenerator skillsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private CharacterClass characterClass;
        private Dictionary<string, Ability> abilities;
        private List<string> classSkills;
        private List<string> monsterClassSkills;
        private List<string> crossClassSkills;
        private Mock<ISkillSelector> mockSkillSelector;
        private int classSkillPoints;
        private int racialSkillPoints;
        private int monsterHitDice;
        private Race race;
        private List<string> specialistSkills;
        private List<string> allSkills;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockSkillSelector = new Mock<ISkillSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            skillsGenerator = new SkillsGenerator(mockSkillSelector.Object, mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockPercentileSelector.Object);
            characterClass = new CharacterClass();
            abilities = new Dictionary<string, Ability>();
            classSkills = new List<string>();
            crossClassSkills = new List<string>();
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            race = new Race();
            specialistSkills = new List<string>();
            allSkills = new List<string>();
            monsterClassSkills = new List<string>();

            characterClass.Name = "class name";
            characterClass.Level = 5;
            characterClass.SpecialistFields = new[] { "specialist field" };
            race.BaseRace = "base race";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "class name")).Returns(classSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "specialist field")).Returns(specialistSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Untrained)).Returns(crossClassSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.All)).Returns(allSkills);

            var emptyAdjustments = new Dictionary<string, int>();
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(It.IsAny<string>())).Returns(emptyAdjustments);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, characterClass.Name)).Returns(() => classSkillPoints);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, race.BaseRace)).Returns(() => racialSkillPoints);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice, race.BaseRace)).Returns(() => monsterHitDice);

            var count = 0;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(() => Convert.ToBoolean(count++ % 2));

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<SkillSelection>>())).Returns((IEnumerable<SkillSelection> ss) => ss.ElementAt(index++ % ss.Count()));

            mockSkillSelector.Setup(s => s.SelectFor(It.IsAny<string>())).Returns((string skill) => new SkillSelection { SkillName = skill, BaseStatName = AbilityConstants.Intelligence });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace)).Returns(monsterClassSkills);
        }

        [Test]
        public void GetSkills()
        {
            characterClass.Level = 1;
            classSkillPoints = 2;
            AddClassSkills(3);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills, Is.Not.Empty);

            foreach (var skill in skills)
            {
                Assert.That(skill, Is.Not.Null);
                Assert.That(skill.Name, Is.Not.Empty);
                Assert.That(skill.RankCap, Is.Positive);
                Assert.That(skill.BaseAbility, Is.Not.Null);
                Assert.That(abilities.Values, Contains.Item(skill.BaseAbility));
            }
        }

        [Test]
        public void SkillHasArmorCheckPenalty()
        {
            characterClass.Level = 1;
            classSkillPoints = 2;
            AddClassSkills(3);

            var armorCheckSkills = new[] { "other skill", classSkills[0] };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty)).Returns(armorCheckSkills);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skill = skills.First(s => s.Name == classSkills[0]);
            Assert.That(skill.HasArmorCheckPenalty, Is.True);
        }

        [Test]
        public void SkillDoesNotHaveArmorCheckPenalty()
        {
            characterClass.Level = 1;
            classSkillPoints = 2;
            AddClassSkills(3);

            var armorCheckSkills = new[] { "other skill", "different skill" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty)).Returns(armorCheckSkills);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skill = skills.First(s => s.Name == classSkills[0]);
            Assert.That(skill.HasArmorCheckPenalty, Is.False);
        }

        [TestCase(1, 4)]
        [TestCase(2, 5)]
        [TestCase(3, 6)]
        [TestCase(4, 7)]
        [TestCase(5, 8)]
        [TestCase(6, 9)]
        [TestCase(7, 10)]
        [TestCase(8, 11)]
        [TestCase(9, 12)]
        [TestCase(10, 13)]
        [TestCase(11, 14)]
        [TestCase(12, 15)]
        [TestCase(13, 16)]
        [TestCase(14, 17)]
        [TestCase(15, 18)]
        [TestCase(16, 19)]
        [TestCase(17, 20)]
        [TestCase(18, 21)]
        [TestCase(19, 22)]
        [TestCase(20, 23)]
        public void GetSkillPointsPerLevel(int level, int points)
        {
            characterClass.Level = level;
            classSkillPoints = 1;
            AddClassSkills(3);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        private void AddClassSkills(int quantity)
        {
            while (quantity > 0)
            {
                classSkills.Add($"class skill {quantity--}");
            }
        }

        [TestCase(1, 4)]
        [TestCase(2, 8)]
        [TestCase(3, 12)]
        [TestCase(4, 16)]
        [TestCase(5, 20)]
        [TestCase(6, 24)]
        [TestCase(7, 28)]
        [TestCase(8, 32)]
        [TestCase(9, 36)]
        [TestCase(10, 40)]
        public void GetSkillPointsForClass(int classPoints, int points)
        {
            characterClass.Level = 1;
            classSkillPoints = classPoints;
            AddClassSkills(classPoints + 1);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        [TestCase(1, 8)]
        [TestCase(2, 10)]
        [TestCase(3, 12)]
        [TestCase(4, 14)]
        [TestCase(5, 16)]
        [TestCase(6, 18)]
        [TestCase(7, 20)]
        [TestCase(8, 22)]
        [TestCase(9, 24)]
        [TestCase(10, 26)]
        [TestCase(11, 28)]
        [TestCase(12, 30)]
        [TestCase(13, 32)]
        [TestCase(14, 34)]
        [TestCase(15, 36)]
        [TestCase(16, 38)]
        [TestCase(17, 40)]
        [TestCase(18, 42)]
        [TestCase(19, 44)]
        [TestCase(20, 46)]
        public void AddIntelligenceBonusToSkillPointsPerLevel(int level, int points)
        {
            characterClass.Level = level;
            classSkillPoints = 1;
            AddClassSkills(3);
            abilities[AbilityConstants.Intelligence].BaseValue = 12;

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        [Test]
        public void GetClassSkills()
        {
            AddClassSkills(4);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Select(s => s.Name), Is.EquivalentTo(classSkills));
        }

        [Test]
        public void GetCrossClassSkills()
        {
            AddCrossClassSkills(4);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Select(s => s.Name), Is.EquivalentTo(crossClassSkills));
        }

        private void AddCrossClassSkills(int quantity)
        {
            while (quantity > 0)
            {
                crossClassSkills.Add($"cross-class skill {quantity--}");
            }
        }

        [Test]
        public void GetSkillsFromSpecialistFields()
        {
            AddSpecialistSkills(2);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Select(s => s.Name), Is.EquivalentTo(specialistSkills));
        }

        private void AddSpecialistSkills(int quantity)
        {
            while (quantity > 0)
            {
                specialistSkills.Add($"specialist skill {quantity--}");
            }
        }

        [Test]
        public void DoNotGetDuplicateSkillsFromSpecialistFields()
        {
            AddClassSkills(1);
            specialistSkills.Add(classSkills[0]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Single().Name, Is.EqualTo("class skill 1"));
        }

        [Test]
        public void GetAllSkills()
        {
            AddClassSkills(2);
            AddCrossClassSkills(2);
            AddSpecialistSkills(1);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skillNames = skills.Select(s => s.Name);
            Assert.That(classSkills, Is.SubsetOf(skillNames));
            Assert.That(crossClassSkills, Is.SubsetOf(skillNames));
            Assert.That(specialistSkills, Is.SubsetOf(skillNames));
            Assert.That(skills.Count, Is.EqualTo(5));
        }

        [Test]
        public void AssignAbilitiesToSkills()
        {
            AddClassSkills(1);
            AddCrossClassSkills(1);
            AddSpecialistSkills(1);

            var classSkillSelection = new SkillSelection();
            classSkillSelection.BaseStatName = "stat 1";
            classSkillSelection.SkillName = "class skill name";

            var crossClassSkillSelection = new SkillSelection();
            crossClassSkillSelection.BaseStatName = "stat 2";
            crossClassSkillSelection.SkillName = "cross-class skill name";

            var specialistSkillSelection = new SkillSelection();
            specialistSkillSelection.BaseStatName = "stat 3";
            specialistSkillSelection.SkillName = "specialist class skill name";

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(classSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(crossClassSkills[0])).Returns(crossClassSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(specialistSkills[0])).Returns(specialistSkillSelection);

            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("class skill name"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities["stat 1"]));

            Assert.That(skills[1].Name, Is.EqualTo("specialist class skill name"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["stat 3"]));

            Assert.That(skills[2].Name, Is.EqualTo("cross-class skill name"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["stat 2"]));
        }

        [Test]
        public void SetClassSkill()
        {
            AddClassSkills(2);
            AddCrossClassSkills(2);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);

            Assert.That(skills.Single(s => s.Name == "class skill 1").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "class skill 2").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "cross-class skill 1").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "cross-class skill 2").ClassSkill, Is.False);
        }

        [Test]
        public void SpecialistSkillsAreClassSkills()
        {
            AddSpecialistSkills(1);
            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Single(s => s.Name == "specialist skill 1").ClassSkill, Is.True);
        }

        [Test]
        public void SpecialistSkillOverwritesCrossClassSkill()
        {
            AddCrossClassSkills(1);
            specialistSkills.Add(crossClassSkills[0]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Single(s => s.Name == "cross-class skill 1").ClassSkill, Is.True);
        }

        [Test]
        public void GetClassSkillWithSetFocus()
        {
            AddClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.Focus = Guid.NewGuid().ToString();

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(skillSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo(skillSelection.Focus));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GetCrossClassSkillWithSetFocus()
        {
            AddCrossClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.Focus = Guid.NewGuid().ToString();

            mockSkillSelector.Setup(s => s.SelectFor(crossClassSkills[0])).Returns(skillSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo(skillSelection.Focus));
            Assert.That(skill.ClassSkill, Is.False);
        }

        [Test]
        public void GetSpecialistSkillWithSetFocus()
        {
            AddSpecialistSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.Focus = Guid.NewGuid().ToString();

            mockSkillSelector.Setup(s => s.SelectFor(specialistSkills[0])).Returns(skillSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo(skillSelection.Focus));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GetClassSkillWithRandomFocus()
        {
            AddClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo("random"));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GetCrossClassSkillWithRandomFocus()
        {
            AddCrossClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(crossClassSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo("random"));
            Assert.That(skill.ClassSkill, Is.False);
        }

        [Test]
        public void GetSpecialistSkillWithRandomFocus()
        {
            AddSpecialistSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(specialistSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo("random"));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GetClassSkillWithMultipleRandomFoci()
        {
            AddClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("third random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(2));
        }

        [Test]
        public void GetCrossClassSkillWithMultipleRandomFoci()
        {
            AddCrossClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(crossClassSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.False);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("third random"));
            Assert.That(skills[1].ClassSkill, Is.False);

            Assert.That(skills.Length, Is.EqualTo(2));
        }

        [Test]
        public void GetSpecialistSkillWithMultipleRandomFoci()
        {
            AddSpecialistSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(specialistSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("third random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(2));
        }

        [Test]
        public void DoNotRepeatFociForClassSkill()
        {
            AddClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt((index++ / 2) % ss.Count()));

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("other random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(2));
        }

        [Test]
        public void DoNotRepeatFociForCrossClassSkill()
        {
            AddCrossClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(crossClassSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt((index++ / 2) % ss.Count()));

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.False);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("other random"));
            Assert.That(skills[1].ClassSkill, Is.False);

            Assert.That(skills.Length, Is.EqualTo(2));
        }

        [Test]
        public void DoNotRepeatFociForSpecialistSkill()
        {
            AddSpecialistSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(specialistSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt((index++ / 2) % ss.Count()));

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("other random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(2));
        }

        [Test]
        public void GetClassSkillWithAllFoci()
        {
            AddClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = SkillConstants.Foci.QuantityOfAll;

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("other random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[2].Focus, Is.EqualTo("third random"));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(3));
        }

        [Test]
        public void GetCrossClassSkillWithAllFoci()
        {
            AddCrossClassSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = SkillConstants.Foci.QuantityOfAll;

            mockSkillSelector.Setup(s => s.SelectFor(crossClassSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.False);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("other random"));
            Assert.That(skills[1].ClassSkill, Is.False);

            Assert.That(skills[2].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[2].Focus, Is.EqualTo("third random"));
            Assert.That(skills[2].ClassSkill, Is.False);

            Assert.That(skills.Length, Is.EqualTo(3));
        }

        [Test]
        public void GetSpecialistSkillWithAllFoci()
        {
            AddSpecialistSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = SkillConstants.Foci.QuantityOfAll;

            mockSkillSelector.Setup(s => s.SelectFor(specialistSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("other random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[2].Focus, Is.EqualTo("third random"));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(3));
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongClassSkills()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;
            AddClassSkills(2);

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(false);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[1].Name, Is.EqualTo("class skill 1"));
            Assert.That(skills[0].Name, Is.EqualTo("class skill 2"));
            Assert.That(skills[1].Ranks, Is.EqualTo(2));
            Assert.That(skills[0].Ranks, Is.EqualTo(3));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(3));
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongClassSkillsWithFoci()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;
            classSkills.Add($"class skill/focus");
            classSkills.Add($"class skill/other focus");
            classSkills.Add($"other class skill");

            mockSkillSelector.Setup(s => s.SelectFor("class skill/focus")).Returns(new SkillSelection { SkillName = "class skill", BaseStatName = AbilityConstants.Intelligence, Focus = "focus" });
            mockSkillSelector.Setup(s => s.SelectFor("class skill/other focus")).Returns(new SkillSelection { SkillName = "class skill", BaseStatName = AbilityConstants.Intelligence, Focus = "other focus" });

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(false);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("class skill"));
            Assert.That(skills[0].Focus, Is.EqualTo("focus"));
            Assert.That(skills[0].Ranks, Is.EqualTo(2));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2));

            Assert.That(skills[1].Name, Is.EqualTo("class skill"));
            Assert.That(skills[1].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[1].Ranks, Is.EqualTo(2));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(2));

            Assert.That(skills[2].Name, Is.EqualTo("other class skill"));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].Ranks, Is.EqualTo(1));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(1));
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongCrossClassSkills()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;
            AddCrossClassSkills(2);

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(true);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("cross-class skill 2"));
            Assert.That(skills[0].Ranks, Is.EqualTo(3));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(1.5));

            Assert.That(skills[1].Name, Is.EqualTo("cross-class skill 1"));
            Assert.That(skills[1].Ranks, Is.EqualTo(2));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(1));
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongCrossClassSkillsWithFoci()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;
            crossClassSkills.Add($"cross-class skill/focus");
            crossClassSkills.Add($"cross-class skill/other focus");
            crossClassSkills.Add($"other cross-class skill");

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(true);

            mockSkillSelector.Setup(s => s.SelectFor("cross-class skill/focus")).Returns(new SkillSelection { SkillName = "cross-class skill", BaseStatName = AbilityConstants.Intelligence, Focus = "focus" });
            mockSkillSelector.Setup(s => s.SelectFor("cross-class skill/other focus")).Returns(new SkillSelection { SkillName = "cross-class skill", BaseStatName = AbilityConstants.Intelligence, Focus = "other focus" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("cross-class skill"));
            Assert.That(skills[0].Focus, Is.EqualTo("focus"));
            Assert.That(skills[0].Ranks, Is.EqualTo(2));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(1));

            Assert.That(skills[1].Name, Is.EqualTo("cross-class skill"));
            Assert.That(skills[1].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[1].Ranks, Is.EqualTo(2));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(1));

            Assert.That(skills[2].Name, Is.EqualTo("other cross-class skill"));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].Ranks, Is.EqualTo(1));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(.5));
        }

        [Test]
        public void AssignPointsToClassSkills()
        {
            classSkillPoints = 1;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(false);

            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("class skill"));
            Assert.That(skills[0].Ranks, Is.EqualTo(8));

            Assert.That(skills[1].Name, Is.EqualTo("cross-class skill"));
            Assert.That(skills[1].Ranks, Is.EqualTo(0));
        }

        [Test]
        public void AssignPointsToCrossClassSkills()
        {
            classSkillPoints = 1;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(true);

            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("class skill"));
            Assert.That(skills[0].Ranks, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("cross-class skill"));
            Assert.That(skills[1].Ranks, Is.EqualTo(8));
        }

        [Test]
        public void AssignPointsRandomlyBetweenClassAndCrossClassSkills()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;

            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true).Returns(false).Returns(false).Returns(true).Returns(false);

            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("class skill"));
            Assert.That(skills[0].Ranks, Is.EqualTo(3));

            Assert.That(skills[1].Name, Is.EqualTo("cross-class skill"));
            Assert.That(skills[1].Ranks, Is.EqualTo(2));
        }

        [Test]
        public void CannotAssignMoreThanRankCapPerClassSkill()
        {
            classSkillPoints = 2;
            AddClassSkills(3);

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(false);
            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<SkillSelection>>(ss => ss.Count() == 3))).Returns((IEnumerable<SkillSelection> ss) => ss.ElementAt(index++ % 4 % 3));

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(classSkills[0]));
            Assert.That(skills[1].Name, Is.EqualTo(classSkills[1]));
            Assert.That(skills[2].Name, Is.EqualTo(classSkills[2]));
            Assert.That(skills[0].Ranks, Is.EqualTo(8));
            Assert.That(skills[1].Ranks, Is.EqualTo(4));
            Assert.That(skills[2].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].RankCap, Is.EqualTo(8));
            Assert.That(skills[1].RankCap, Is.EqualTo(8));
            Assert.That(skills[2].RankCap, Is.EqualTo(8));
        }

        [Test]
        public void CannotAssignMoreThanLevelPlusThreePointsPerCrossClassSkill()
        {
            classSkillPoints = 2;
            AddCrossClassSkills(3);

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(true);
            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<SkillSelection>>(ss => ss.Count() == 3))).Returns((IEnumerable<SkillSelection> ss) => ss.ElementAt(index++ % 4 % 3));

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(crossClassSkills[0]));
            Assert.That(skills[1].Name, Is.EqualTo(crossClassSkills[1]));
            Assert.That(skills[2].Name, Is.EqualTo(crossClassSkills[2]));
            Assert.That(skills[0].Ranks, Is.EqualTo(8));
            Assert.That(skills[1].Ranks, Is.EqualTo(4));
            Assert.That(skills[2].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].RankCap, Is.EqualTo(8));
            Assert.That(skills[1].RankCap, Is.EqualTo(8));
            Assert.That(skills[2].RankCap, Is.EqualTo(8));
        }

        [Test]
        public void AllSkillsMaxedOut()
        {
            classSkillPoints = 3;
            crossClassSkills.Add("skill 1");
            classSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);

            Assert.That(skills.Single(s => s.Name == "skill 1").Ranks, Is.EqualTo(characterClass.Level + 3));
            Assert.That(skills.Single(s => s.Name == "skill 2").Ranks, Is.EqualTo(characterClass.Level + 3));
        }

        [Test]
        public void ApplySkillSynergyIfSufficientRanks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1", "synergy 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 3")).Returns(new[] { "synergy 4" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 4")).Returns(new[] { "synergy 5" });

            classSkillPoints = 2;
            characterClass.Level = 13;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");
            classSkills.Add("synergy 3");
            classSkills.Add("synergy 4");
            classSkills.Add("synergy 5");
            classSkills.Add("synergy 6");

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Ranks, Is.EqualTo(0));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[2].Ranks, Is.EqualTo(4));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[3].Ranks, Is.EqualTo(0));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 3"));
            Assert.That(skills[4].Ranks, Is.EqualTo(4));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[4].Bonus, Is.EqualTo(0));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 4"));
            Assert.That(skills[5].Ranks, Is.EqualTo(0));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[5].Bonus, Is.EqualTo(0));

            Assert.That(skills[6].Name, Is.EqualTo("synergy 5"));
            Assert.That(skills[6].Ranks, Is.EqualTo(4));
            Assert.That(skills[6].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[6].Bonus, Is.EqualTo(2));

            Assert.That(skills[7].Name, Is.EqualTo("synergy 6"));
            Assert.That(skills[7].Ranks, Is.EqualTo(0));
            Assert.That(skills[7].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[7].Bonus, Is.EqualTo(0));

            Assert.That(skills[8].Name, Is.EqualTo("skill 3"));
            Assert.That(skills[8].Ranks, Is.EqualTo(0));
            Assert.That(skills[8].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[8].Bonus, Is.EqualTo(0));

            Assert.That(skills[9].Name, Is.EqualTo("skill 4"));
            Assert.That(skills[9].Ranks, Is.EqualTo(16));
            Assert.That(skills[9].EffectiveRanks, Is.EqualTo(8));
            Assert.That(skills[9].Bonus, Is.EqualTo(0));
        }

        [Test]
        public void ApplySkillSynergyWithFociIfSufficientRanks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1", "synergy 2/focus" });

            classSkillPoints = 2;
            characterClass.Level = 13;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("skill 3");
            classSkills.Add("skill 4");
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 3");

            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(new SkillSelection { SkillName = "synergy 2", BaseStatName = AbilityConstants.Intelligence, Focus = "focus" });
            mockSkillSelector.Setup(s => s.SelectFor("skill 3")).Returns(new SkillSelection { SkillName = "synergy 2", BaseStatName = AbilityConstants.Intelligence, Focus = "other focus" });
            mockSkillSelector.Setup(s => s.SelectFor("synergy 2/focus")).Returns(new SkillSelection { SkillName = "synergy 2", BaseStatName = AbilityConstants.Intelligence, Focus = "focus" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].Ranks, Is.EqualTo(6));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].Ranks, Is.EqualTo(6));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[1].Bonus, Is.EqualTo(2));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("skill 4"));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[4].Focus, Is.Empty);
            Assert.That(skills[4].Ranks, Is.EqualTo(5));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[4].Bonus, Is.EqualTo(2));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 3"));
            Assert.That(skills[5].Focus, Is.Empty);
            Assert.That(skills[5].Ranks, Is.EqualTo(5));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[5].Bonus, Is.EqualTo(0));
        }

        [Test]
        public void ApplySkillSynergyIfSufficientRanksWithFoci()
        {
            classSkillPoints = 2;
            characterClass.Level = 13;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("skill 3");
            classSkills.Add("skill 4");
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");

            mockSkillSelector.Setup(s => s.SelectFor("skill 1")).Returns(new SkillSelection { SkillName = "skill 1", BaseStatName = AbilityConstants.Intelligence, Focus = "focus" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1/focus")).Returns(new[] { "synergy 1", "synergy 2" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.EqualTo("focus"));
            Assert.That(skills[0].Ranks, Is.EqualTo(6));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].Ranks, Is.EqualTo(6));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("skill 3"));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("skill 4"));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[4].Focus, Is.Empty);
            Assert.That(skills[4].Ranks, Is.EqualTo(5));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[4].Bonus, Is.EqualTo(2));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[5].Focus, Is.Empty);
            Assert.That(skills[5].Ranks, Is.EqualTo(5));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[5].Bonus, Is.EqualTo(2));
        }

        [Test]
        public void SkillSynergyStacks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1", "synergy 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 1" });

            classSkillPoints = 4;
            characterClass.Level = 2;
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Ranks, Is.EqualTo(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(4));

            Assert.That(skills[3].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(2));
        }

        [Test]
        public void DoNotApplySkillSynergyIfThereIsNoSynergisticSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(Enumerable.Empty<string>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 1" });

            classSkillPoints = 2;
            characterClass.Level = 2;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("synergy 2");

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(false);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Ranks, Is.EqualTo(3));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[2].Ranks, Is.EqualTo(3));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills.Length, Is.EqualTo(3));
        }

        [Test]
        public void DoNotApplySkillSynergyIfThereIsNoSynergisticSkillWithCorrectFocus()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(Enumerable.Empty<string>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 1/focus" });

            classSkillPoints = 2;
            characterClass.Level = 2;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("synergy 1/other focus");

            mockSkillSelector.Setup(s => s.SelectFor("synergy 1/focus")).Returns(new SkillSelection { SkillName = "synergy 1", BaseStatName = AbilityConstants.Intelligence, Focus = "focus" });
            mockSkillSelector.Setup(s => s.SelectFor("synergy 1/other focus")).Returns(new SkillSelection { SkillName = "synergy 1", BaseStatName = AbilityConstants.Intelligence, Focus = "other focus" });
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(false);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].Ranks, Is.EqualTo(3));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[2].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[2].Ranks, Is.EqualTo(3));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills.Length, Is.EqualTo(3));
        }

        [Test]
        public void DoNotApplySkillSynergyIfInsufficientRanks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 3")).Returns(new[] { "synergy 3" });

            classSkillPoints = 3;
            characterClass.Level = 6;

            classSkills.Add("skill 1");
            crossClassSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");
            classSkills.Add("synergy 3");

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(7));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(7));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[1].Ranks, Is.EqualTo(0));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[1].Bonus, Is.EqualTo(2));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[2].Ranks, Is.EqualTo(7));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(7));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("synergy 3"));
            Assert.That(skills[3].Ranks, Is.EqualTo(0));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[4].Ranks, Is.EqualTo(4));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[4].Bonus, Is.EqualTo(0));

            Assert.That(skills[5].Name, Is.EqualTo("skill 3"));
            Assert.That(skills[5].Ranks, Is.EqualTo(9));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(4.5));
            Assert.That(skills[5].Bonus, Is.EqualTo(0));
        }

        [TestCase(1, 8)]
        [TestCase(2, 10)]
        [TestCase(3, 12)]
        [TestCase(4, 14)]
        [TestCase(5, 16)]
        [TestCase(6, 18)]
        [TestCase(7, 20)]
        [TestCase(8, 22)]
        [TestCase(9, 24)]
        [TestCase(10, 26)]
        [TestCase(11, 28)]
        [TestCase(12, 30)]
        [TestCase(13, 32)]
        [TestCase(14, 34)]
        [TestCase(15, 36)]
        [TestCase(16, 38)]
        [TestCase(17, 40)]
        [TestCase(18, 42)]
        [TestCase(19, 44)]
        [TestCase(20, 46)]
        public void HumansGetExtraSkillPointsPerLevel(int level, int points)
        {
            characterClass.Level = level;
            classSkillPoints = 1;
            AddClassSkills(3);
            race.BaseRace = SizeConstants.BaseRaces.Human;

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        [TestCase(1, 16)]
        [TestCase(2, 20)]
        [TestCase(3, 24)]
        [TestCase(4, 28)]
        [TestCase(5, 32)]
        [TestCase(6, 36)]
        [TestCase(7, 40)]
        [TestCase(8, 44)]
        [TestCase(9, 48)]
        [TestCase(10, 52)]
        [TestCase(11, 56)]
        [TestCase(12, 60)]
        [TestCase(13, 64)]
        [TestCase(14, 68)]
        [TestCase(15, 72)]
        [TestCase(16, 76)]
        [TestCase(17, 80)]
        [TestCase(18, 84)]
        [TestCase(19, 88)]
        [TestCase(20, 92)]
        public void AllPerLevelBonusesStack(int level, int points)
        {
            characterClass.Level = level;
            classSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 12;
            race.BaseRace = SizeConstants.BaseRaces.Human;
            AddClassSkills(5);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        [TestCase(1, 1, 4)]
        [TestCase(2, 1, 5)]
        [TestCase(3, 1, 6)]
        [TestCase(4, 1, 7)]
        [TestCase(5, 1, 8)]
        [TestCase(6, 1, 9)]
        [TestCase(7, 1, 10)]
        [TestCase(8, 1, 11)]
        [TestCase(9, 1, 12)]
        [TestCase(10, 1, 13)]
        [TestCase(11, 1, 14)]
        [TestCase(12, 1, 15)]
        [TestCase(13, 1, 16)]
        [TestCase(14, 1, 17)]
        [TestCase(15, 1, 18)]
        [TestCase(16, 1, 19)]
        [TestCase(17, 1, 20)]
        [TestCase(18, 1, 21)]
        [TestCase(19, 1, 22)]
        [TestCase(20, 1, 23)]
        [TestCase(1, 2, 8)]
        [TestCase(2, 2, 10)]
        [TestCase(3, 2, 12)]
        [TestCase(4, 2, 14)]
        [TestCase(5, 2, 16)]
        [TestCase(6, 2, 18)]
        [TestCase(7, 2, 20)]
        [TestCase(8, 2, 22)]
        [TestCase(9, 2, 24)]
        [TestCase(10, 2, 26)]
        [TestCase(11, 2, 28)]
        [TestCase(12, 2, 30)]
        [TestCase(13, 2, 32)]
        [TestCase(14, 2, 34)]
        [TestCase(15, 2, 36)]
        [TestCase(16, 2, 38)]
        [TestCase(17, 2, 40)]
        [TestCase(18, 2, 42)]
        [TestCase(19, 2, 44)]
        [TestCase(20, 2, 46)]
        public void MonstersGetMoreSkillPointsByMonsterHitDice(int hitDie, int monsterSkillPoints, int monsterPoints)
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 1;
            classSkillPoints = 1;
            racialSkillPoints = monsterSkillPoints;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            monsterHitDice = hitDie;
            AddMonsterSkills(hitDie + 3);
            AddClassSkills(2);
            classSkills.Add(monsterClassSkills[0]);
            crossClassSkills.Add(monsterClassSkills[1]);

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();
            var totalRanks = skills.Sum(s => s.Ranks);

            //INFO: Adding 4 to account for class skill points
            Assert.That(totalRanks, Is.EqualTo(monsterPoints + 4));

            Assert.That(skills[0].Name, Is.EqualTo(classSkills[0]));
            Assert.That(skills[0].RankCap, Is.EqualTo(4));

            Assert.That(skills[1].Name, Is.EqualTo(classSkills[1]));
            Assert.That(skills[1].RankCap, Is.EqualTo(4));

            Assert.That(skills[2].Name, Is.EqualTo(monsterClassSkills[0]));
            Assert.That(skills[2].RankCap, Is.EqualTo(4 + hitDie));

            Assert.That(skills[3].Name, Is.EqualTo(monsterClassSkills[1]));
            Assert.That(skills[3].RankCap, Is.EqualTo(4 + hitDie));

            for (var i = 4; i < skills.Length; i++)
                Assert.That(skills[i].RankCap, Is.EqualTo(hitDie + 3));
        }

        private void AddMonsterSkills(int quantity)
        {
            while (quantity > 0)
            {
                monsterClassSkills.Add($"monster skill {quantity--}");
            }
        }

        [Test]
        public void OldMonsterSkillsHaveCapIncreased()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters)).Returns(new[] { race.BaseRace, "other base race" });

            racialSkillPoints = 2;
            monsterHitDice = 1;
            AddClassSkills(2);
            AddCrossClassSkills(2);
            monsterClassSkills.Add(classSkills[0]);
            monsterClassSkills.Add(crossClassSkills[0]);
            AddMonsterSkills(1);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(monsterClassSkills[0]));
            Assert.That(skills[0].RankCap, Is.EqualTo(9));

            Assert.That(skills[1].Name, Is.EqualTo(classSkills[1]));
            Assert.That(skills[1].RankCap, Is.EqualTo(8));

            Assert.That(skills[2].Name, Is.EqualTo(monsterClassSkills[1]));
            Assert.That(skills[2].RankCap, Is.EqualTo(9));

            Assert.That(skills[3].Name, Is.EqualTo(crossClassSkills[1]));
            Assert.That(skills[3].RankCap, Is.EqualTo(8));

            Assert.That(skills[4].Name, Is.EqualTo(monsterClassSkills[2]));
            Assert.That(skills[4].RankCap, Is.EqualTo(4));
        }

        [TestCase(1, 12)]
        [TestCase(2, 15)]
        [TestCase(3, 18)]
        [TestCase(4, 21)]
        [TestCase(5, 24)]
        [TestCase(6, 27)]
        [TestCase(7, 30)]
        [TestCase(8, 33)]
        [TestCase(9, 36)]
        [TestCase(10, 39)]
        [TestCase(11, 42)]
        [TestCase(12, 45)]
        [TestCase(13, 48)]
        [TestCase(14, 51)]
        [TestCase(15, 54)]
        [TestCase(16, 57)]
        [TestCase(17, 60)]
        [TestCase(18, 63)]
        [TestCase(19, 66)]
        [TestCase(20, 69)]
        public void MonstersApplyIntelligenceBonusToMonsterSkillPoints(int hitDie, int monsterPoints)
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 1;
            classSkillPoints = 1;
            racialSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 12;
            monsterHitDice = hitDie;
            AddMonsterSkills(hitDie + 3);
            AddClassSkills(2);
            classSkills.Add(monsterClassSkills[0]);
            crossClassSkills.Add(monsterClassSkills[1]);

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();
            var totalRanks = skills.Sum(s => s.Ranks);

            //INFO: Adding 8 to account for class skill points
            Assert.That(totalRanks, Is.EqualTo(monsterPoints + 8));

            Assert.That(skills[0].Name, Is.EqualTo(classSkills[0]));
            Assert.That(skills[0].RankCap, Is.EqualTo(4));

            Assert.That(skills[1].Name, Is.EqualTo(classSkills[1]));
            Assert.That(skills[1].RankCap, Is.EqualTo(4));

            Assert.That(skills[2].Name, Is.EqualTo(monsterClassSkills[0]));
            Assert.That(skills[2].RankCap, Is.EqualTo(4 + hitDie));

            Assert.That(skills[3].Name, Is.EqualTo(monsterClassSkills[1]));
            Assert.That(skills[3].RankCap, Is.EqualTo(4 + hitDie));

            for (var i = 4; i < skills.Length; i++)
                Assert.That(skills[i].RankCap, Is.EqualTo(hitDie + 3));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void MonstersCannotHaveFewerThan1SkillPointPerMonsterHitDie(int hitDie)
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 1;
            classSkillPoints = 1;
            racialSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = -600;
            AddMonsterSkills(hitDie + 2);
            monsterHitDice = hitDie;

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var totalRanks = skills.Sum(s => s.Ranks);

            //INFO: Don't need to worry about class skill points, because we do not specify any class skills
            Assert.That(totalRanks, Is.EqualTo(hitDie));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void CannotHaveFewerThan1SkillPointPerLevel(int level)
        {
            abilities[AbilityConstants.Intelligence].BaseValue = -9266;
            characterClass.Level = level;
            classSkillPoints = 1;
            AddClassSkills(2);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(level));
        }

        [Test]
        public void ExpertGets10RandomSkillsAsClassSkills()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            classSkillPoints = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, CharacterClassConstants.Expert)).Returns(classSkillPoints);

            for (var i = 11; i > 0; i--)
                allSkills.Add("skill " + i.ToString());

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(allSkills)).Returns(allSkills[0]).Returns(allSkills[0]).Returns(allSkills[10])
                .Returns(allSkills[9]).Returns(allSkills[8]).Returns(allSkills[7]).Returns(allSkills[6]).Returns(allSkills[5]).Returns(allSkills[4])
                .Returns(allSkills[3]).Returns(allSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Single(s => s.Name == "skill 1").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 2").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 3").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 4").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 5").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 6").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 7").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 8").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 10").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 11").ClassSkill, Is.True);
            Assert.That(skills.Count, Is.EqualTo(10));
        }

        [Test]
        public void ExpertGetsRandomSkillWithSetFocusAsClassSkill()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            classSkillPoints = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, CharacterClassConstants.Expert)).Returns(classSkillPoints);

            for (var i = 11; i > 0; i--)
                allSkills.Add("skill " + i.ToString());

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(allSkills)).Returns(allSkills[0]).Returns(allSkills[0]).Returns(allSkills[10])
                .Returns(allSkills[9]).Returns(allSkills[8]).Returns(allSkills[7]).Returns(allSkills[6]).Returns(allSkills[5]).Returns(allSkills[4])
                .Returns(allSkills[3]).Returns(allSkills[1]);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.Focus = Guid.NewGuid().ToString();

            mockSkillSelector.Setup(s => s.SelectFor(allSkills[6])).Returns(skillSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Count, Is.EqualTo(10));

            var skillsWithFocus = skills.Where(s => s.Focus != string.Empty).ToArray();

            Assert.That(skillsWithFocus[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skillsWithFocus[0].Focus, Is.EqualTo(skillSelection.Focus));
            Assert.That(skillsWithFocus[0].ClassSkill, Is.True);

            Assert.That(skillsWithFocus.Length, Is.EqualTo(1));
        }

        [Test]
        public void ExpertGetsRandomSkillWithRandomFocusAsClassSkill()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            classSkillPoints = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, CharacterClassConstants.Expert)).Returns(classSkillPoints);

            for (var i = 11; i > 0; i--)
                allSkills.Add("skill " + i.ToString());

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(allSkills)).Returns(allSkills[0]).Returns(allSkills[0]).Returns(allSkills[10])
                .Returns(allSkills[9]).Returns(allSkills[8]).Returns(allSkills[7]).Returns(allSkills[6]).Returns(allSkills[5]).Returns(allSkills[4])
                .Returns(allSkills[3]).Returns(allSkills[1]);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(allSkills[6])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Count, Is.EqualTo(10));

            var skillsWithFocus = skills.Where(s => s.Focus != string.Empty).ToArray();

            Assert.That(skillsWithFocus[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skillsWithFocus[0].Focus, Is.EqualTo("random"));
            Assert.That(skillsWithFocus[0].ClassSkill, Is.True);

            Assert.That(skillsWithFocus.Length, Is.EqualTo(1));
        }

        [Test]
        public void ExpertCanTakeMultipleFociForSkills()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            classSkillPoints = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, CharacterClassConstants.Expert)).Returns(classSkillPoints);

            for (var i = 11; i > 0; i--)
                allSkills.Add("skill " + i.ToString());

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(allSkills)).Returns(allSkills[0]).Returns(allSkills[0]).Returns(allSkills[10])
                .Returns(allSkills[9]).Returns(allSkills[8]).Returns(allSkills[7]).Returns(allSkills[6]).Returns(allSkills[5]).Returns(allSkills[4])
                .Returns(allSkills[3]).Returns(allSkills[1]);

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(allSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Count, Is.EqualTo(10));

            var skillsWithFocus = skills.Where(s => s.Focus != string.Empty).ToArray();

            Assert.That(skillsWithFocus[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skillsWithFocus[0].Focus, Is.EqualTo("random"));
            Assert.That(skillsWithFocus[0].ClassSkill, Is.True);

            Assert.That(skillsWithFocus[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skillsWithFocus[1].Focus, Is.EqualTo("other random"));
            Assert.That(skillsWithFocus[1].ClassSkill, Is.True);

            Assert.That(skillsWithFocus.Length, Is.EqualTo(2));
        }

        [Test]
        public void ExpertCannotTakeDuplicateFociForSkills()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            classSkillPoints = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, CharacterClassConstants.Expert)).Returns(classSkillPoints);

            for (var i = 11; i > 0; i--)
                allSkills.Add("skill " + i.ToString());

            var skillSelection = new SkillSelection();
            skillSelection.BaseStatName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(allSkills[0])).Returns(skillSelection);
            var foci = new[] { "random", "other random", "third random" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(foci);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(foci)).Returns(() => foci.ElementAt((index++ / 2) % foci.Count()));
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(allSkills))
                .Returns(allSkills[0]) //random
                .Returns(allSkills[0]) //duplicate random
                .Returns(allSkills[0]) //other random
                .Returns(allSkills[10])
                .Returns(allSkills[9])
                .Returns(allSkills[8])
                .Returns(allSkills[7])
                .Returns(allSkills[6])
                .Returns(allSkills[5])
                .Returns(allSkills[4])
                .Returns(allSkills[3])
                .Returns(allSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Count, Is.EqualTo(10));

            var skillsWithFocus = skills.Where(s => s.Focus != string.Empty).ToArray();

            Assert.That(skillsWithFocus[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skillsWithFocus[0].Focus, Is.EqualTo("random"));
            Assert.That(skillsWithFocus[0].ClassSkill, Is.True);

            Assert.That(skillsWithFocus[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skillsWithFocus[1].Focus, Is.EqualTo("other random"));
            Assert.That(skillsWithFocus[1].ClassSkill, Is.True);

            Assert.That(skillsWithFocus.Length, Is.EqualTo(2));
        }

        [Test]
        public void ExpertCrossClassSkillsDoNotIncludeRandomClassSkills()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            classSkillPoints = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, CharacterClassConstants.Expert)).Returns(classSkillPoints);

            for (var i = 11; i > 0; i--)
                allSkills.Add("skill " + i.ToString());

            crossClassSkills.Add("other skill");
            crossClassSkills.Add("different skill");
            crossClassSkills.Add("skill 3");

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(allSkills)).Returns(allSkills[0]).Returns(allSkills[0]).Returns(allSkills[10])
                .Returns(allSkills[9]).Returns(allSkills[8]).Returns(allSkills[7]).Returns(allSkills[6]).Returns(allSkills[5]).Returns(allSkills[4])
                .Returns(allSkills[3]).Returns(allSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            Assert.That(skills.Single(s => s.Name == "skill 1").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 2").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 3").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 4").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 5").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 6").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 7").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 8").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 10").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "skill 11").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "other skill").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "different skill").ClassSkill, Is.False);
            Assert.That(skills.Count, Is.EqualTo(12));
        }

        [Test]
        public void IfCharacterHasBaseStat_GetSkill()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");
            specialistSkills.Add("skill 5");
            specialistSkills.Add("skill 6");

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);

            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(() => new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 2" });
            mockSkillSelector.Setup(s => s.SelectFor("skill 4")).Returns(() => new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 4" });
            mockSkillSelector.Setup(s => s.SelectFor("skill 6")).Returns(() => new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 6" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item("skill 1"));
            Assert.That(skillNames, Contains.Item("skill 3"));
            Assert.That(skillNames, Contains.Item("skill 5"));
            Assert.That(skillNames, Contains.Item("skill 2"));
            Assert.That(skillNames, Contains.Item("skill 4"));
            Assert.That(skillNames, Contains.Item("skill 6"));
            Assert.That(skills.Count, Is.EqualTo(6));
        }

        [Test]
        public void IfCharacterDoesNotHaveBaseStat_CannotGetSkill()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");
            specialistSkills.Add("skill 5");
            specialistSkills.Add("skill 6");

            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(() => new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 2" });
            mockSkillSelector.Setup(s => s.SelectFor("skill 4")).Returns(() => new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 4" });
            mockSkillSelector.Setup(s => s.SelectFor("skill 6")).Returns(() => new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 6" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item("skill 1"));
            Assert.That(skillNames, Contains.Item("skill 3"));
            Assert.That(skillNames, Contains.Item("skill 5"));
            Assert.That(skillNames, Is.All.Not.EqualTo("skill 2"));
            Assert.That(skillNames, Is.All.Not.EqualTo("skill 4"));
            Assert.That(skillNames, Is.All.Not.EqualTo("skill 6"));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void DoNotAssignSkillPointsToClassSkillsThatTheCharacterDoesNotHaveDueToNotHavingTheBaseStat()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            specialistSkills.Add("skill 5");
            specialistSkills.Add("skill 6");

            var constitutionSelection = new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(constitutionSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 6")).Returns(constitutionSelection);

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(false);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(4));

            Assert.That(skills[1].Name, Is.EqualTo("skill 5"));
            Assert.That(skills[1].Ranks, Is.EqualTo(1));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(1));
        }

        [Test]
        public void DoNotAssignSkillPointsToCrossClassSkillsThatTheCharacterDoesNotHaveDueToNotHavingTheBaseStat()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;

            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");

            var constitutionSelection = new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 4" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 4")).Returns(constitutionSelection);

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill)).Returns(true);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 3"));
            Assert.That(skills[0].Ranks, Is.EqualTo(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2.5));
        }

        [Test]
        public void DoNotAssignMonsterSkillPointsToMonsterSkillsIfCharacterDoesNotHaveRequiredBaseStat()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters)).Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var constitutionSelection = new SkillSelection { BaseStatName = AbilityConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 1")).Returns(constitutionSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();
            var skill = skills.Single();

            Assert.That(skill.Name, Is.EqualTo("skill 2"));
            Assert.That(skill.Ranks, Is.EqualTo(5));
            Assert.That(skill.RanksMaxedOut, Is.True);
        }

        [Test]
        public void SelectPresetFocusForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters)).Returns(new[] { race.BaseRace, "other base race" });

            racialSkillPoints = 2;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var selection = new SkillSelection { BaseStatName = AbilityConstants.Charisma, Focus = "focus", SkillName = "skill with focus" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(selection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));

            Assert.That(skills[1].Name, Is.EqualTo("skill with focus"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));

            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void SelectRandomFocusForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters)).Returns(new[] { race.BaseRace, "other base race" });

            racialSkillPoints = 2;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var randomSelection = new SkillSelection { BaseStatName = AbilityConstants.Charisma, RandomFociQuantity = 1, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void SelectMultipleRandomFociForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var randomSelection = new SkillSelection { BaseStatName = AbilityConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void DoNotSelectRepeatedRandomFociForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var randomSelection = new SkillSelection { BaseStatName = AbilityConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ / 2 % ss.Count()));

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void SelectAllRandomFociForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var randomSelection = new SkillSelection { BaseStatName = AbilityConstants.Charisma, RandomFociQuantity = 3, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void ProfessionSkillGrantsAdditionalClassSkills()
        {
            AddClassSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseStatName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseStatName = "stat 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseStatName = "stat 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseStatName = "stat 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });

            var professionSkills = new[] { "professional skill 1", "professional skill 2", "professional skill 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["stat 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["stat 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["stat 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void ProfessionSkillConvertsCrossClassSkillToClassSkill()
        {
            AddClassSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            crossClassSkills.Add("professional skill 1");
            crossClassSkills.Add("professional skill 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseStatName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseStatName = "stat 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseStatName = "stat 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseStatName = "stat 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });

            var professionSkills = new[] { "professional skill 1", "professional skill 2", "professional skill 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["stat 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["stat 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("other random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["stat 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills[4].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[4].Focus, Is.EqualTo("random"));
            Assert.That(skills[4].BaseAbility, Is.EqualTo(abilities["stat 3"]));
            Assert.That(skills[4].ClassSkill, Is.False);

            Assert.That(skills.Length, Is.EqualTo(5));
        }

        [Test]
        public void RandomProfessionSkillGrantsAdditionalClassSkills()
        {
            AddClassSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseStatName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.RandomFociQuantity = 1;

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseStatName = "stat 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseStatName = "stat 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseStatName = "stat 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, SkillConstants.Profession)).Returns(new[] { "random job", "other random job" });

            var professionSkills = new[] { "professional skill 1", "professional skill 2", "professional skill 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "random job")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("random job"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["stat 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["stat 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("other random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["stat 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void ProfessionSkillGrantsNoDuplicateClassSkills()
        {
            AddClassSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            classSkills.Add("professional skill 1");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseStatName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseStatName = "stat 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseStatName = "stat 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseStatName = "stat 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });

            var professionSkills = new[] { "professional skill 1", "professional skill 2", "professional skill 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["stat 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["stat 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["stat 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void ProfessionSkillGrantsNoAdditionalClassSkills()
        {
            AddClassSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseStatName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(professionSkillSelection);

            var professionSkills = Enumerable.Empty<string>();
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(1));
        }

        [Test]
        public void NoProfessionSkillGrantsNoAdditionalClassSkills()
        {
            AddClassSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var otherSkillSelection = new SkillSelection();
            otherSkillSelection.BaseStatName = AbilityConstants.Intelligence;
            otherSkillSelection.SkillName = "other skill";
            otherSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(otherSkillSelection);

            var professionSkills = new[] { "profession skill 1", "profession skill 2", "professional skill 3" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("other skill"));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(1));
        }

        [Test]
        public void GetSynergyFromProfessionSkill()
        {
            classSkillPoints = 2;
            characterClass.Level = 13;

            AddClassSkills(4);
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseStatName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(professionSkillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, $"{SkillConstants.Profession}/software developer")).Returns(new[] { "synergy 1", "synergy 2" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].Ranks, Is.EqualTo(6));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("class skill 3"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].Ranks, Is.EqualTo(6));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("class skill 2"));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("class skill 1"));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[4].Focus, Is.Empty);
            Assert.That(skills[4].Ranks, Is.EqualTo(5));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[4].Bonus, Is.EqualTo(2));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[5].Focus, Is.Empty);
            Assert.That(skills[5].Ranks, Is.EqualTo(5));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[5].Bonus, Is.EqualTo(2));
        }

        [Test]
        public void ApplySynergyToProfessionSkill()
        {
            classSkillPoints = 2;
            characterClass.Level = 13;

            AddClassSkills(4);
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseStatName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(professionSkillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, classSkills[1])).Returns(new[] { "synergy 1", $"{SkillConstants.Profession}/software developer" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, abilities).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].Ranks, Is.EqualTo(6));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[0].Bonus, Is.EqualTo(2));

            Assert.That(skills[1].Name, Is.EqualTo("class skill 3"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].Ranks, Is.EqualTo(6));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("class skill 2"));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("class skill 1"));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[4].Focus, Is.Empty);
            Assert.That(skills[4].Ranks, Is.EqualTo(5));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[4].Bonus, Is.EqualTo(2));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[5].Focus, Is.Empty);
            Assert.That(skills[5].Ranks, Is.EqualTo(5));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[5].Bonus, Is.EqualTo(0));
        }
    }
}