﻿using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests
    {
        private ICreatureVerifier verifier;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private Mock<ICollectionSelector> mockCollectionSelector;

        [SetUp]
        public void Setup()
        {
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            verifier = new CreatureVerifier(mockJustInTimeFactory.Object, mockCollectionSelector.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatibility_CreatureAndTemplate_Compatible_IfTemplateApplicatorSaysSo(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Where(c => compatible));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            var isCompatible = verifier.VerifyCompatibility(false, creature: "creature", template: "template");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BUG_VerifyCompatibility_CreatureAndTemplate_Compatible_IfTemplateApplicatorSaysSo_HonorNoneTemplate(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Where(c => compatible));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(mockApplicator.Object);

            var isCompatible = verifier.VerifyCompatibility(false, creature: "creature", template: CreatureConstants.Templates.None);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatiblity_CreatureAndTemplateAsCharacter_Compatible_IfCharacterAndTemplateApplicatorSaysSo(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Where(c => compatible));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, creature: "creature", template: "template");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BUG_VerifyCompatiblity_CreatureAndTemplateAsCharacter_Compatible_IfCharacterAndTemplateApplicatorSaysSo_HonorNoneTemplate(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Where(c => compatible));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, creature: "creature", template: CreatureConstants.Templates.None);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void VerifyCompatiblity_CreatureAndTemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, creature: "creature", template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void BUG_VerifyCompatiblity_CreatureAndTemplateAsCharacter_NotCompatible_IfNotCharacter_HonorNoneTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, creature: "creature", template: CreatureConstants.Templates.None);
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_Template_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void BUG_VerifyCompatiblity_Template_Compatible_HonorNoneTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: CreatureConstants.Templates.None);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatiblity_Template_Compatible_IfTemplate(bool asCharacter)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, null, null, null))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(asCharacter, template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BUG_VerifyCompatiblity_Template_NotCompatible_IfNotTemplate_HonorNoneTemplate(bool asCharacter)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, null, null, null))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(asCharacter, template: CreatureConstants.Templates.None);
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFilters_Compatible(bool asCharacter, string cr, string type, string alignment)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Intersect(new[] { "creature" }));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature", "wrong character" });
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong character" });

            var isCompatible = verifier.VerifyCompatibility(asCharacter, template: "template", challengeRating: cr, type: type, alignment: alignment);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_TemplateAndFilters_Compatible_HonorNoneTemplate(bool asCharacter, string cr, string type, string alignment)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Intersect(new[] { "creature" }));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature", "wrong character" });
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong character" });

            var isCompatible = verifier.VerifyCompatibility(asCharacter, template: CreatureConstants.Templates.None, challengeRating: cr, type: type);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFilters_NotCompatible_IfNotTemplate(bool asCharacter, string cr, string type, string alignment)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(asCharacter, template: "template", challengeRating: cr, type: type);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_TemplateAndFilters_NotCompatible_IfNotTemplate_HonorNoneTemplate(bool asCharacter, string cr, string type, string alignment)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(asCharacter, template: CreatureConstants.Templates.None, challengeRating: cr, type: type);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(null, null, null)]
        [TestCase(null, null, "my alignment")]
        [TestCase(null, "my type", null)]
        [TestCase(null, "my type", "my alignment")]
        [TestCase("my challenge rating", null, null)]
        [TestCase("my challenge rating", null, "my alignment")]
        [TestCase("my challenge rating", "my type", null)]
        [TestCase("my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFiltersAsCharacter_NotCompatible_IfNotCharacter(string cr, string type, string alignment)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Intersect(new[] { "creature" }));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: cr, type: type);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(null, null, null)]
        [TestCase(null, null, "my alignment")]
        [TestCase(null, "my type", null)]
        [TestCase(null, "my type", "my alignment")]
        [TestCase("my challenge rating", null, null)]
        [TestCase("my challenge rating", null, "my alignment")]
        [TestCase("my challenge rating", "my type", null)]
        [TestCase("my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_TemplateAndFiltersAsCharacter_NotCompatible_IfNotCharacter_HonorNoneTemplate(string cr, string type, string alignment)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Intersect(new[] { "creature" }));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: CreatureConstants.Templates.None, challengeRating: cr, type: type);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_WithFilters_Compatible_BaseCreature(bool asCharacter, string cr, string type, string alignment)
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature", "wrong character" });
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong character" });

            var noneApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(noneApplicator.Object);

            noneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr, string a) => cc.Take(1));

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(asCharacter, type: type, challengeRating: cr);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_WithFilters_Compatible_Template(bool asCharacter, string cr, string type, string alignment)
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature", "wrong character" });
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong character" });

            var noneApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(noneApplicator.Object);

            noneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                var isTemplate = template == "template";
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                    .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Intersect(new[] { "creature" }).Where(c => isTemplate));

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(asCharacter, type: type, challengeRating: cr);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_WithFilters_NotCompatible_HonorNoneTemplate(bool asCharacter, string cr, string type, string alignment)
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature", "wrong character" });
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong character" });

            var noneApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(noneApplicator.Object);

            noneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                var isTemplate = template == "template";
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr, alignment))
                    .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Intersect(new[] { "creature" }).Where(c => isTemplate));

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(asCharacter, template: CreatureConstants.Templates.None, type: type, challengeRating: cr);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(null, null, null)]
        [TestCase(null, null, "my alignment")]
        [TestCase(null, "my type", null)]
        [TestCase(null, "my type", "my alignment")]
        [TestCase("my challenge rating", null, null)]
        [TestCase("my challenge rating", null, "my alignment")]
        [TestCase("my challenge rating", "my type", null)]
        [TestCase("my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_WithFiltersAsCharacter_NotCompatible_IfNotCharacter(string cr, string type, string alignment)
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var noneApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None))
                .Returns(noneApplicator.Object);

            noneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, type, cr, alignment))
                .Returns(Enumerable.Empty<string>());

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                var isTemplate = template == "template";
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, type, cr, alignment))
                    .Returns((IEnumerable<string> cc, bool asc, string t, string cr, string a) => cc.Intersect(new[] { "creature" }).Where(c => isTemplate));

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: type, challengeRating: cr);
            Assert.That(isCompatible, Is.False);
        }
    }
}