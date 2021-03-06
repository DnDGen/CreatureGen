﻿using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;

namespace DnDGen.CreatureGen.Templates.HalfDragons
{
    internal class HalfDragonBlackApplicator : HalfDragonApplicator
    {
        public HalfDragonBlackApplicator(
                ICollectionSelector collectionSelector,
                ISpeedsGenerator speedsGenerator,
                IAttacksGenerator attacksGenerator,
                IFeatsGenerator featsGenerator,
                ISkillsGenerator skillsGenerator,
                IAlignmentGenerator alignmentGenerator,
                Dice dice,
                IMagicGenerator magicGenerator)
            : base(collectionSelector, speedsGenerator, attacksGenerator, featsGenerator, skillsGenerator, alignmentGenerator, dice, magicGenerator)
        {
        }

        protected override string DragonSpecies => CreatureConstants.Templates.HalfDragon_Black;
    }
}
