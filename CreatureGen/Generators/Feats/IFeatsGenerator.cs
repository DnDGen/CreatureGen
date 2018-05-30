﻿using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Generators.Feats
{
    internal interface IFeatsGenerator
    {
        IEnumerable<Feat> GenerateSpecialQualities(
            string creatureName,
            HitPoints hitPoints,
            string size,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills);
        IEnumerable<Feat> GenerateFeats(
            HitPoints hitPoints,
            int baseAttackBonus,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Attack> attacks,
            IEnumerable<Feat> specialQualities,
            int casterLevel,
            Dictionary<string, Measurement> speeds,
            int naturalArmor,
            int hands,
            string size);
    }
}