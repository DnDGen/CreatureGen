﻿namespace CreatureGen.Verifiers
{
    public interface ICreatureVerifier
    {
        bool VerifyCompatibility(string creatureName, string templateName);
    }
}