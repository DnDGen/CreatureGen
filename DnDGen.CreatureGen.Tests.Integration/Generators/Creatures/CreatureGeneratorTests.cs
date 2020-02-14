﻿using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.EventGen;
using DnDGen.TreasureGen.Items;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Generators.Creatures
{
    [TestFixture]
    public class CreatureGeneratorTests : IntegrationTests
    {
        [Inject]
        public ICreatureGenerator CreatureGenerator { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        private CreatureAsserter creatureAsserter;

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);

            creatureAsserter = new CreatureAsserter();
        }

        [TestCase(CreatureConstants.Rakshasa)]
        public void CanGenerateSpellsForThoseWhoCastAsSpellcaster(string creatureName)
        {
            Assert.Fail("TODO");
        }

        [TestCase(CreatureConstants.Human)]
        [TestCase(CreatureConstants.Dwarf_Hill)]
        [TestCase(CreatureConstants.Elf_Half)]
        [TestCase(CreatureConstants.Elf_High)]
        [TestCase(CreatureConstants.Gnome_Rock)]
        [TestCase(CreatureConstants.Halfling_Lightfoot)]
        [TestCase(CreatureConstants.Orc)]
        [TestCase(CreatureConstants.Orc_Half)]
        [TestCase(CreatureConstants.Goblin)]
        [TestCase(CreatureConstants.Ogre)]
        [TestCase(CreatureConstants.Balor)]
        public void CanGenerateEquipment(string creatureName)
        {
            var creature = CreatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
            Assert.That(creature.Equipment, Is.Not.Null);
            Assert.That(creature.Equipment.Weapons, Is.Not.Empty);

            var unnaturalAttacks = creature.Attacks.Where(a => !a.IsNatural);

            foreach (var attack in unnaturalAttacks)
            {
                var weapon = creature.Equipment.Weapons.FirstOrDefault(w => w.Name == attack.Name) as Weapon;
                Assert.That(weapon, Is.Not.Null);
                Assert.That(attack.DamageRoll, Is.EqualTo(weapon.Damage));
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CanGenerateCreature(string creatureName)
        {
            var creature = CreatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), "Templates")]
        public void CanGenerateTemplate(string template)
        {
            var creature = CreatureGenerator.Generate(CreatureConstants.Human, template);
            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Destrachan)]
        [TestCase(CreatureConstants.Grimlock)]
        [TestCase(CreatureConstants.Yrthak)]
        public void BUG_DoesNotHaveSight(string creatureName)
        {
            var creature = CreatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);

            Assert.That(creature.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = creature.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.SpecialQualities.Blindsight));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.AllAroundVision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.Darkvision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LowLightVision));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LowLightVision_Superior));
        }

        [TestCase(CreatureConstants.Elf_Aquatic)]
        [TestCase(CreatureConstants.Elf_Drow)]
        [TestCase(CreatureConstants.Elf_Gray)]
        [TestCase(CreatureConstants.Elf_Half)]
        [TestCase(CreatureConstants.Elf_High)]
        [TestCase(CreatureConstants.Elf_Wild)]
        [TestCase(CreatureConstants.Elf_Wood)]
        public void BUG_ElfCanUseShield(string elfName)
        {
            var elf = CreatureGenerator.Generate(elfName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(elf);

            Assert.That(elf.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = elf.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.ShieldProficiency));
        }

        [Test]
        public void BUG_GiantOwlDoesNotDoubleUpOnLowLightVision()
        {
            var owl = CreatureGenerator.Generate(CreatureConstants.Owl_Giant, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(owl);

            Assert.That(owl.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = owl.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Contains.Item(FeatConstants.SpecialQualities.LowLightVision_Superior));
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LowLightVision));
        }

        [Test]
        public void BUG_HalfOrcIsNotSensitiveToLight()
        {
            var halfOrc = CreatureGenerator.Generate(CreatureConstants.Orc_Half, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(halfOrc);

            Assert.That(halfOrc.SpecialQualities, Is.Not.Empty);

            var specialQualityNames = halfOrc.SpecialQualities.Select(q => q.Name);
            Assert.That(specialQualityNames, Does.Not.Contain(FeatConstants.SpecialQualities.LightSensitivity));
        }

        [Test]
        public void BUG_NightcrawlerHasConcentration()
        {
            var nightcrawler = CreatureGenerator.Generate(CreatureConstants.Nightcrawler, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(nightcrawler);

            Assert.That(nightcrawler.Skills, Is.Not.Empty);

            var skillNames = nightcrawler.Skills.Select(q => q.Name);
            Assert.That(skillNames, Contains.Item(SkillConstants.Concentration));
        }
    }
}
