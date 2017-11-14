﻿using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Skills
{
    internal class SkillsGenerator : ISkillsGenerator
    {
        private readonly ISkillSelector skillSelector;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public SkillsGenerator(ISkillSelector skillSelector, ICollectionSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector, IPercentileSelector percentileSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.skillSelector = skillSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.percentileSelector = percentileSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public IEnumerable<Skill> GenerateFor(HitPoints hitPoints, string creatureName, Dictionary<string, Ability> abilities)
        {
            if (hitPoints.HitDiceQuantity == 0)
                return Enumerable.Empty<Skill>();

            var skillNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, creatureName);
            var skillSelections = GetSkillSelections(skillNames);
            var skills = InitializeSkills(abilities, skillSelections, hitPoints);

            var points = GetTotalSkillPoints(creatureName, hitPoints.HitDiceQuantity, abilities[AbilityConstants.Intelligence]);
            var validSkills = GetValidSkills(skillSelections, skills);

            while (points-- > 0 && validSkills.Any())
            {
                var skillSelection = collectionsSelector.SelectRandomFrom(validSkills);
                var skill = skills.First(s => skillSelection.IsEqualTo(s));
                skill.Ranks++;

                validSkills = GetValidSkills(skillSelections, skills);
            }

            skills = ApplySkillSynergies(skills);

            return skills;
        }

        private IEnumerable<SkillSelection> GetSkillSelections(IEnumerable<string> skillNames)
        {
            var selections = skillNames.Select(s => skillSelector.SelectFor(s));
            var explodedSelections = selections.SelectMany(s => ExplodeSelectedSkill(s));

            //INFO: Calling immediate execution, since exploding includes potentially random results, and after this method is complete, we want consistent results.
            return explodedSelections.ToList();
        }

        private IEnumerable<SkillSelection> ExplodeSelectedSkill(SkillSelection skillSelection)
        {
            if (skillSelection.RandomFociQuantity == 0)
                return new[] { skillSelection };

            var skillFoci = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName).ToList();

            if (skillSelection.RandomFociQuantity >= skillFoci.Count)
            {
                return skillFoci.Select(f => new SkillSelection { BaseAbilityName = skillSelection.BaseAbilityName, SkillName = skillSelection.SkillName, Focus = f });
            }

            var selections = new List<SkillSelection>();

            while (skillSelection.RandomFociQuantity > selections.Count)
            {
                var focus = collectionsSelector.SelectRandomFrom(skillFoci);
                var selection = new SkillSelection();

                selection.BaseAbilityName = skillSelection.BaseAbilityName;
                selection.SkillName = skillSelection.SkillName;
                selection.Focus = focus;

                selections.Add(selection);
                skillFoci.Remove(focus);
            }

            return selections;
        }

        private IEnumerable<Skill> InitializeSkills(Dictionary<string, Ability> abilities, IEnumerable<SkillSelection> skillSelections, HitPoints hitPoints)
        {
            var skills = new List<Skill>();

            foreach (var skillSelection in skillSelections)
            {
                if (!abilities.ContainsKey(skillSelection.BaseAbilityName))
                    continue;

                var skill = skills.FirstOrDefault(s => skillSelection.IsEqualTo(s));

                if (skill == null)
                {
                    skill = new Skill(skillSelection.SkillName, abilities[skillSelection.BaseAbilityName], 3, skillSelection.Focus);

                    var skillsWithArmorCheckPenalties = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty);
                    skill.HasArmorCheckPenalty = skillsWithArmorCheckPenalties.Contains(skill.Name);
                    skill.RankCap += hitPoints.HitDiceQuantity;

                    skills.Add(skill);
                }

                //INFO: all creature skills are class skills
                skill.ClassSkill = true;
            }

            return skills;
        }

        private int GetTotalSkillPoints(string creatureName, int hitDieQuantity, Ability intelligence)
        {
            if (hitDieQuantity == 0)
                return 0;

            var points = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, creatureName);
            var perHitDie = Math.Max(1, points + intelligence.Bonus);
            var multiplier = hitDieQuantity + 3;
            var total = perHitDie * multiplier;

            return total;
        }

        private IEnumerable<SkillSelection> GetValidSkills(IEnumerable<SkillSelection> skillSelections, IEnumerable<Skill> skills)
        {
            return skillSelections.Where(ss => skills.Any(s => ss.IsEqualTo(s) && !s.RanksMaxedOut));
        }

        private IEnumerable<Skill> ApplySkillSynergies(IEnumerable<Skill> skills)
        {
            var skillsWarrantingSynergy = skills.Where(s => s.QualifiesForSkillSynergy);

            foreach (var skill in skillsWarrantingSynergy)
            {
                var name = skill.Focus.Any() ? $"{skill.Name}/{skill.Focus}" : skill.Name;
                var synergySkillNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, name);

                foreach (var synergySkillName in synergySkillNames)
                {
                    var synergySkill = skills.FirstOrDefault(s => s.IsEqualTo(synergySkillName));

                    if (synergySkill != null)
                        synergySkill.Bonus += 2;
                }
            }

            return skills;
        }

        public IEnumerable<Skill> ApplyBonusesFromFeats(IEnumerable<Skill> skills, IEnumerable<Feat> feats)
        {
            var allFeatGrantingSkillBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, FeatConstants.SkillBonus);
            var featGrantingSkillBonuses = feats.Where(f => allFeatGrantingSkillBonuses.Contains(f.Name));
            var allSkillFocusNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, GroupConstants.Skills);

            foreach (var feat in featGrantingSkillBonuses)
            {
                if (feat.Foci.Any())
                {
                    foreach (var focus in feat.Foci)
                    {
                        if (!allSkillFocusNames.Any(s => focus.StartsWith(s)))
                            continue;

                        var skillName = allSkillFocusNames.First(s => focus.StartsWith(s));
                        var skill = skills.FirstOrDefault(s => s.IsEqualTo(skillName));

                        if (skill == null)
                            continue;

                        var circumstantial = !allSkillFocusNames.Contains(focus);
                        skill.CircumstantialBonus |= circumstantial;

                        if (!circumstantial)
                            skill.Bonus += feat.Power;
                    }
                }
                else
                {
                    var skillsToReceiveBonus = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, feat.Name);

                    foreach (var skillName in skillsToReceiveBonus)
                    {
                        var skill = skills.FirstOrDefault(s => s.IsEqualTo(skillName));

                        if (skill != null)
                            skill.Bonus += feat.Power;
                    }
                }
            }

            return skills;
        }
    }
}