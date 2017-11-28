﻿using CreatureGen.Abilities;
using EventGen;
using System.Collections.Generic;

namespace CreatureGen.Generators.Abilities
{
    internal class AbilitiesGeneratorEventDecorator : IAbilitiesGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IAbilitiesGenerator innerGenerator;

        public AbilitiesGeneratorEventDecorator(IAbilitiesGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Dictionary<string, Ability> GenerateFor(string creatureName)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating abilities for {creatureName}");
            var abilities = innerGenerator.GenerateFor(creatureName);

            eventQueue.Enqueue("CreatureGen", $"Generated {abilities.Count} abilities");

            return abilities;
        }
    }
}