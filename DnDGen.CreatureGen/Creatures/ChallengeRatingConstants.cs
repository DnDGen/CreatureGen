﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Creatures
{
    public static class ChallengeRatingConstants
    {
        public const string Zero = "0";
        public const string OneTenth = "1/10";
        public const string OneEighth = "1/8";
        public const string OneSixth = "1/6";
        public const string OneFourth = "1/4";
        public const string OneThird = "1/3";
        public const string OneHalf = "1/2";
        public const string One = "1";
        public const string Two = "2";
        public const string Three = "3";
        public const string Four = "4";
        public const string Five = "5";
        public const string Six = "6";
        public const string Seven = "7";
        public const string Eight = "8";
        public const string Nine = "9";
        public const string Ten = "10";
        public const string Eleven = "11";
        public const string Twelve = "12";
        public const string Thirteen = "13";
        public const string Fourteen = "14";
        public const string Fifteen = "15";
        public const string Sixteen = "16";
        public const string Seventeen = "17";
        public const string Eighteen = "18";
        public const string Nineteen = "19";
        public const string Twenty = "20";
        public const string TwentyOne = "21";
        public const string TwentyTwo = "22";
        public const string TwentyThree = "23";
        public const string TwentyFour = "24";
        public const string TwentyFive = "25";
        public const string TwentySix = "26";
        public const string TwentySeven = "27";

        internal static string[] GetOrdered()
        {
            return new[]
            {
                Zero,
                OneTenth,
                OneEighth,
                OneSixth,
                OneFourth,
                OneThird,
                OneHalf,
                One,
                Two,
                Three,
                Four,
                Five,
                Six,
                Seven,
                Eight,
                Nine,
                Ten,
                Eleven,
                Twelve,
                Thirteen,
                Fourteen,
                Fifteen,
                Sixteen,
                Seventeen,
                Eighteen,
                Nineteen,
                Twenty,
                TwentyOne,
                TwentyTwo,
                TwentyThree,
                TwentyFour,
                TwentyFive,
                TwentySix,
                TwentySeven,
            };
        }

        public static IEnumerable<string> Fractional => GetOrdered().Skip(1).Take(6);

        public static string IncreaseChallengeRating(string challengeRating, int increaseAmount)
        {
            if (Fractional.Contains(challengeRating))
            {
                var ordered = GetOrdered();
                var index = Array.IndexOf(ordered, challengeRating);

                if (index + increaseAmount < ordered.Length)
                    return ordered[index + increaseAmount];

                challengeRating = ordered.Last();
                increaseAmount += index - ordered.Length + 1;
            }

            var cr = Convert.ToInt32(challengeRating);
            cr += increaseAmount;

            return cr.ToString();
        }
    }
}
