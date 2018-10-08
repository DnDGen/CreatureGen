﻿namespace CreatureGen.Selectors.Selections
{
    internal class BonusSelection
    {
        public string Target { get; set; }
        public int Bonus { get; set; }
        public string Condition { get; set; }

        public BonusSelection()
        {
            Target = string.Empty;
            Condition = string.Empty;
        }
    }
}