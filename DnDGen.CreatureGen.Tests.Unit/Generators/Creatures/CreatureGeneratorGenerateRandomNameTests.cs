﻿using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class CreatureGeneratorGenerateRandomNameTests : CreatureGeneratorTests
    {
        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateCreatureName_NoTemplate(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            var name = creatureGenerator.GenerateRandomName(asCharacter, null, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateCreatureName_WithPresetNoneTemplate(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = CreatureConstants.Templates.None;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var name = creatureGenerator.GenerateRandomName(asCharacter, template, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateCreatureName_WithPresetTemplate(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var name = creatureGenerator.GenerateRandomName(asCharacter, template, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateCreatureName_WithRandomTemplate_Only1TemplateCompatible(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator2.Object);

            var name = creatureGenerator.GenerateRandomName(asCharacter, null, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateCreatureName_WithRandomTemplate_OnlyTemplatesCompatible(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature name", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator2.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { template, "other template" }))))
                .Returns(template);

            var name = creatureGenerator.GenerateRandomName(asCharacter, null, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateRandomCreatureName_NoTemplate(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Except(new[] { "wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(new[] { template, "other template" })))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, null, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateRandomCreatureName_WithPresetNoneTemplate(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = CreatureConstants.Templates.None;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, template, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(CreatureConstants.Templates.None));
        }

        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateRandomCreatureName_WithPresetTemplate(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, template, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null)]
        [TestCase(true, null, "my challenge rating")]
        [TestCase(true, "my type", null)]
        [TestCase(true, "my type", "my challenge rating")]
        [TestCase(false, null, null)]
        [TestCase(false, null, "my challenge rating")]
        [TestCase(false, "my type", null)]
        [TestCase(false, "my type", "my challenge rating")]
        public void GenerateRandomName_GenerateRandomCreatureName_WithRandomTemplate(bool asCharacter, string type, string cr)
        {
            var creatureName = "my creature";
            var template = "my template";

            var creatures = new[] { "wrong creature", "other wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, type, cr)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Except(new[] { "wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc.Except(new[] { "other wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns((IEnumerable<string> cc, bool asC, string t, string cr) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, type, cr))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[]
                {
                    "other creature",
                    creatureName,
                    "other wrong creature",
                    template,
                    "other template"
                }))))
                .Returns(template);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[]
                {
                    "other creature",
                    creatureName,
                    "wrong creature"
                }))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, null, type, cr);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Template, Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "challenge rating")]
        [TestCase(true, null, "type", null)]
        [TestCase(true, null, "type", "challenge rating")]
        [TestCase(true, CreatureConstants.Templates.None, null, null)]
        [TestCase(true, CreatureConstants.Templates.None, null, "challenge rating")]
        [TestCase(true, CreatureConstants.Templates.None, "type", null)]
        [TestCase(true, CreatureConstants.Templates.None, "type", "challenge rating")]
        [TestCase(true, "template", null, null)]
        [TestCase(true, "template", null, "challenge rating")]
        [TestCase(true, "template", "type", null)]
        [TestCase(true, "template", "type", "challenge rating")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "challenge rating")]
        [TestCase(false, null, "type", null)]
        [TestCase(false, null, "type", "challenge rating")]
        [TestCase(false, CreatureConstants.Templates.None, null, null)]
        [TestCase(false, CreatureConstants.Templates.None, null, "challenge rating")]
        [TestCase(false, CreatureConstants.Templates.None, "type", null)]
        [TestCase(false, CreatureConstants.Templates.None, "type", "challenge rating")]
        [TestCase(false, "template", null, null)]
        [TestCase(false, "template", null, "challenge rating")]
        [TestCase(false, "template", "type", null)]
        [TestCase(false, "template", "type", "challenge rating")]
        public void GenerateRandomName_ThrowException_WhenNotCompatible(bool asCharacter, string template, string type, string challengeRating)
        {
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, template, type, challengeRating)).Returns(false);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tAs Character: {asCharacter}");

            if (!string.IsNullOrEmpty(template))
                message.AppendLine($"\tTemplate: {template}");

            if (type != null)
                message.AppendLine($"\tType: {type}");

            if (challengeRating != null)
                message.AppendLine($"\tCR: {challengeRating}");

            Assert.That(() => creatureGenerator.GenerateRandomName(asCharacter, template, type, challengeRating),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }
    }
}