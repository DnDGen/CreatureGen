﻿using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
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
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;

        [SetUp]
        public void Setup()
        {
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            verifier = new CreatureVerifier(mockJustInTimeFactory.Object, mockCreatureDataSelector.Object, mockCollectionSelector.Object, mockAdjustmentSelector.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatibility_CreatureAndTemplate_Compatible_IfTemplateApplicatorSaysSo(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc.Where(c => compatible));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            var isCompatible = verifier.VerifyCompatibility(false, creature: "creature", template: "template");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatiblity_CreatureAndTemplateAsCharacter_Compatible_IfCharacterAndTemplateApplicatorSaysSo(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc.Where(c => compatible));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, creature: "creature", template: "template");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void VerifyCompatiblity_CreatureAndTemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc);

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
        public void VerifyCompatiblity_Template_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc);

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
        public void VerifyCompatiblity_Template_NotCompatible_IfNotTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, null))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAsCharacter_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAsCharacter_NotCompatible_IfNotTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, null))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, null))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRating_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "challenge rating"))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc.Intersect(new[] { "creature" }));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRating_NotCompatible_IfNotTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRating_NotCompatible_IfNotChallengeRating()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, null, "challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRatingAsCharacter_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "challenge rating"))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc.Intersect(new[] { "creature" }));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRatingAsCharacter_NotCompatible_IfNotTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRatingAsCharacter_NotCompatible_IfNotChallengeRating()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "challenge rating"))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRatingAsCharacter_NotCompatible_IfNotCharacter()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, null, "challenge rating"))
                .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc.Intersect(new[] { "creature" }));

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRating_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "creature", "another wrong creature" });

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

            var isCompatible = verifier.VerifyCompatibility(false, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRating_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                var isTemplate = template == "template";
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, "type", "challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc.Intersect(new[] { "creature" }).Where(c => isTemplate));

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRating_NotCompatible_IfNotType()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, "type", "challenge rating"))
                    .Returns(Enumerable.Empty<string>());

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRating_NotCompatible_IfNotChallengeRating()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), false, "type", "challenge rating"))
                    .Returns(Enumerable.Empty<string>());

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "creature", "another wrong creature" });

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

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                var isTemplate = template == "template";
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, "type", "challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc.Intersect(new[] { "creature" }).Where(c => isTemplate));

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_NotCompatible_IfNotType()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, "type", "challenge rating"))
                    .Returns(Enumerable.Empty<string>());

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_NotCompatible_IfNotChallengeRating()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, "type", "challenge rating"))
                    .Returns(Enumerable.Empty<string>());

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_NotCompatible_IfNotCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                var isTemplate = template == "template";
                mockApplicator
                    .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), true, "type", "challenge rating"))
                    .Returns((IEnumerable<string> cc, bool asc, string t, string cr) => cc.Intersect(new[] { "creature" }).Where(c => isTemplate));

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRating_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "creature", "another wrong creature" });

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

            var isCompatible = verifier.VerifyCompatibility(false, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRating_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", false, null, "challenge rating")).Returns(template == "template");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(ChallengeRatingConstants.CR0, ChallengeRatingConstants.CR0)]
        [TestCase(ChallengeRatingConstants.CR0, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(ChallengeRatingConstants.CR0, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR0, ChallengeRatingConstants.CR27)]
        [TestCase(ChallengeRatingConstants.CR0, "9266")]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR27)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, "9266")]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR27)]
        [TestCase(ChallengeRatingConstants.CR1, "9266")]
        [TestCase(ChallengeRatingConstants.CR27, ChallengeRatingConstants.CR27)]
        [TestCase(ChallengeRatingConstants.CR27, "9266")]
        [TestCase("9266", "9266")]
        public void VerifyCompatiblity_ChallengeRating_Compatible_Template_NoMaxChallengeRating(string creatureCr, string crFilter)
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, crFilter))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var allCrs = new Dictionary<string, CreatureDataSelection>();
            allCrs["character"] = new CreatureDataSelection { ChallengeRating = creatureCr };
            allCrs["creature"] = new CreatureDataSelection { ChallengeRating = creatureCr };
            allCrs["wrong creature"] = new CreatureDataSelection { ChallengeRating = "wrong challenge rating" };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(allCrs);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", false, null, crFilter)).Returns(template == "template");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);

                IEnumerable<string> crs = null;
                mockApplicator.Setup(a => a.GetChallengeRatings()).Returns(crs);
            }

            var isCompatible = verifier.VerifyCompatibility(false, challengeRating: crFilter);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(ChallengeRatingConstants.CR0, ChallengeRatingConstants.CR0)]
        [TestCase(ChallengeRatingConstants.CR0, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(ChallengeRatingConstants.CR0, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR0, ChallengeRatingConstants.CR27)]
        [TestCase(ChallengeRatingConstants.CR0, "9266")]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR27)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, "9266")]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR27)]
        [TestCase(ChallengeRatingConstants.CR1, "9266")]
        [TestCase(ChallengeRatingConstants.CR27, ChallengeRatingConstants.CR0)]
        [TestCase(ChallengeRatingConstants.CR27, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(ChallengeRatingConstants.CR27, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR27, ChallengeRatingConstants.CR27)]
        [TestCase(ChallengeRatingConstants.CR27, "9266")]
        [TestCase("9266", ChallengeRatingConstants.CR0)]
        [TestCase("9266", ChallengeRatingConstants.CR1_2nd)]
        [TestCase("9266", ChallengeRatingConstants.CR1)]
        [TestCase("9266", ChallengeRatingConstants.CR27)]
        [TestCase("9266", "9266")]
        public void VerifyCompatiblity_ChallengeRating_Compatible_Template_HasMaxChallengeRating(string creatureCr, string crFilter)
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature", "other wrong creature", "another wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, crFilter))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var allCrs = new Dictionary<string, CreatureDataSelection>();
            allCrs["character"] = new CreatureDataSelection { ChallengeRating = creatureCr };
            allCrs["creature"] = new CreatureDataSelection { ChallengeRating = creatureCr };
            allCrs["wrong creature"] = new CreatureDataSelection { ChallengeRating = "wrong challenge rating" };
            allCrs["other wrong creature"] = new CreatureDataSelection { ChallengeRating = creatureCr };
            allCrs["another wrong creature"] = new CreatureDataSelection { ChallengeRating = creatureCr };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(allCrs);

            var allHitDice = new Dictionary<string, double>();
            allHitDice["character"] = 3;
            allHitDice["creature"] = 5;
            allHitDice["wrong creature"] = 7;
            allHitDice["other wrong creature"] = 666;
            allHitDice["another wrong creature"] = 1;

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(allCrs);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", false, null, crFilter)).Returns(template == "template");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);

                var crs = new[]
                {
                    ChallengeRatingConstants.CR1_2nd,
                    ChallengeRatingConstants.CR1,
                    ChallengeRatingConstants.CR2,
                    crFilter,
                };
                mockApplicator.Setup(a => a.GetChallengeRatings()).Returns(crs);

                mockApplicator.Setup(a => a.GetHitDiceRange(crFilter)).Returns((2, 7));
            }

            var isCompatible = verifier.VerifyCompatibility(false, challengeRating: crFilter);
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRating_Compatible_Template_FilterOutOfRange()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRating_NotCompatible_IfNotChallengeRating()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", false, null, "challenge rating")).Returns(false);

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "creature", "another wrong creature" });

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

            var isCompatible = verifier.VerifyCompatibility(true, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", true, null, "challenge rating")).Returns(template == "template");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_Compatible_Template_NoMaxChallengeRating()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_Compatible_Template_HasMaxChallengeRating()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_Compatible_Template_FilterOutOfRange()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_NotCompatible_IfNotChallengeRating()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", true, null, "challenge rating")).Returns(false);

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_NotCompatible_IfNotCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", true, null, "challenge rating")).Returns(template == "template");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_Type_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "creature", "other wrong creature" });

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

            var isCompatible = verifier.VerifyCompatibility(false, type: "type");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_Type_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", false, "type", null)).Returns(template == "template");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_Type_Compatible_Template_TypeFromTemplate()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void VerifyCompatiblity_Type_Compatible_Template_TypeFromCreature()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void VerifyCompatiblity_Type_NotCompatible_IfNotType()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", false, "type", null)).Returns(false);

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "creature", "other wrong creature" });

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

            var isCompatible = verifier.VerifyCompatibility(true, type: "type");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", true, "type", null)).Returns(template == "template");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_Compatible_Template_TypeFromTemplate()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_Compatible_Template_TypeFromCreature()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_NotCompatible_IfNotType()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", true, "type", null)).Returns(false);

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_NotCompatible_IfNotCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature", true, "type", null)).Returns(template == "template");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void CanBeCharacter_FalseIfNullLevelAdjustment()
        {
            var creatureData = new CreatureDataSelection();
            creatureData.LevelAdjustment = null;

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("creature"))
                .Returns(creatureData);

            var canBeCharacter = verifier.CanBeCharacter("creature");
            Assert.That(canBeCharacter, Is.False);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void CanBeCharacter_TrueIfLevelAdjustment(int levelAdjustment)
        {
            var creatureData = new CreatureDataSelection();
            creatureData.LevelAdjustment = levelAdjustment;

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("creature"))
                .Returns(creatureData);

            var canBeCharacter = verifier.CanBeCharacter("creature");
            Assert.That(canBeCharacter, Is.True);
        }
    }
}