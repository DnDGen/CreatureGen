﻿using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Stress;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace DnDGen.CreatureGen.Tests.Integration.Stress
{
    [TestFixture]
    public abstract class StressTests : IntegrationTests
    {
        protected ICreatureVerifier creatureVerifier;
        protected IEnumerable<string> allCreatures;
        protected IEnumerable<string> allTemplates;
        protected Stressor stressor;

        [OneTimeSetUp]
        public void OneTimeStressSetup()
        {
            var options = new StressorOptions();
            options.RunningAssembly = Assembly.GetExecutingAssembly();

            //INFO: Non-stress operations take up to 10 minutes, or ~17% of the total runtime.
            //Adding in another 3% of buffer
            options.TimeLimitPercentage = .80;

            //INFO: When the tests run in parallel, and with parallel batches, the overhead of multithread switching severely impacts execution time
            //Better to let it only one 1 at a time.
            options.MaxAsyncBatch = 1;

#if STRESS
            options.IsFullStress = true;
#else
            options.IsFullStress = false;
#endif

            stressor = new Stressor(options);
        }

        [SetUp]
        public void StressSetup()
        {
            allCreatures = CreatureConstants.GetAll();
            allTemplates = CreatureConstants.Templates.GetAll();
            creatureVerifier = GetNewInstanceOf<ICreatureVerifier>();
        }
    }
}