﻿using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public class TraitsTests : PercentileTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Percentile.Traits; }
        }

        [TestCase(1, 1, "Distinctive scar")]
        [TestCase(2, 2, "Missing tooth")]
        [TestCase(3, 3, "Missing finger")]
        [TestCase(4, 4, "Bad breath")]
        [TestCase(5, 5, "Strong body odor")]
        [TestCase(6, 6, "Pleasant smelling (perfumed)")]
        [TestCase(7, 7, "Sweaty")]
        [TestCase(8, 8, "Hands shake")]
        [TestCase(9, 9, "Unusual eye color")]
        [TestCase(10, 10, "Hacking cough")]
        [TestCase(11, 11, "Sneezes and sniffles")]
        [TestCase(12, 12, "Particularly low voice")]
        [TestCase(13, 13, "Particularly high voice")]
        [TestCase(14, 14, "Slurs words")]
        [TestCase(15, 15, "Lisps")]
        [TestCase(16, 16, "Stutters")]
        [TestCase(17, 17, "Enunciates very clearly")]
        [TestCase(18, 18, "Speaks loudly")]
        [TestCase(19, 19, "Whispers")]
        [TestCase(20, 20, "Hard of hearing")]
        [TestCase(21, 21, "Tattoo")]
        [TestCase(22, 22, "Birthmark")]
        [TestCase(23, 23, "Unusual skin color")]
        [TestCase(24, 24, "Bald")]
        [TestCase(25, 25, "Particularly long hair")]
        [TestCase(26, 26, "Completely normal and uninteresting")]
        [TestCase(27, 27, "Unusual hair color")]
        [TestCase(28, 28, "Walks with a limp")]
        [TestCase(29, 29, "Distinctive jewelry")]
        [TestCase(30, 30, "Wears flamboyant or outlandish clothes")]
        [TestCase(31, 31, "Underdressed")]
        [TestCase(32, 32, "Overdressed")]
        [TestCase(33, 33, "Nervous eye twitch")]
        [TestCase(34, 34, "Fiddles and fidgets nervously")]
        [TestCase(35, 35, "Whistles a lot")]
        [TestCase(36, 36, "Signs a lot")]
        [TestCase(37, 37, "Flips a coin")]
        [TestCase(38, 38, "Good posture")]
        [TestCase(39, 39, "Stooped back")]
        [TestCase(40, 40, "Talks a lot")]
        [TestCase(41, 41, "Talks very little")]
        [TestCase(42, 42, "Constantly quotes others")]
        [TestCase(43, 43, "Has a thick accent")]
        [TestCase(44, 44, "Visible wounds or sores")]
        [TestCase(45, 45, "Squints")]
        [TestCase(46, 46, "Stares off into distance")]
        [TestCase(47, 47, "Frequently chewing something")]
        [TestCase(48, 48, "Dirty and unkempt")]
        [TestCase(49, 49, "Clean")]
        [TestCase(50, 50, "Distinctive nose")]
        [TestCase(51, 51, "Selfish")]
        [TestCase(52, 52, "Obsequious")]
        [TestCase(53, 53, "Drowsy")]
        [TestCase(54, 54, "Bookish")]
        [TestCase(55, 55, "Observant")]
        [TestCase(56, 56, "Not very observant")]
        [TestCase(57, 57, "Overly critical")]
        [TestCase(58, 58, "Passionate artist or art lover")]
        [TestCase(59, 59, "Passionate hobbyist (fishing, hunting, gaming, animals, etc.)")]
        [TestCase(60, 60, "Collector (books, trophies, coins, weapons, etc.)")]
        [TestCase(61, 61, "Skinflint")]
        [TestCase(62, 62, "Spendthrift")]
        [TestCase(63, 63, "Pessimist")]
        [TestCase(64, 64, "Optimist")]
        [TestCase(65, 65, "Drunkard")]
        [TestCase(66, 66, "Teetotaler")]
        [TestCase(67, 67, "Well mannered")]
        [TestCase(68, 68, "Rude")]
        [TestCase(69, 69, "Jumpy")]
        [TestCase(70, 70, "Foppish")]
        [TestCase(71, 71, "Overbearing")]
        [TestCase(72, 72, "Aloof")]
        [TestCase(73, 73, "Proud")]
        [TestCase(74, 74, "Individualist")]
        [TestCase(75, 75, "Conformist")]
        [TestCase(76, 76, "Hot tempered")]
        [TestCase(77, 77, "Even tempered")]
        [TestCase(78, 78, "Neurotic")]
        [TestCase(79, 79, "Jealous")]
        [TestCase(80, 80, "Brave")]
        [TestCase(81, 81, "Cowardly")]
        [TestCase(82, 82, "Careless")]
        [TestCase(83, 83, "Curious")]
        [TestCase(84, 84, "Truthful")]
        [TestCase(85, 85, "Liar")]
        [TestCase(86, 86, "Lazy")]
        [TestCase(87, 87, "Energetic")]
        [TestCase(88, 88, "Reverent or pious")]
        [TestCase(89, 89, "Irreverent or irreligious")]
        [TestCase(90, 90, "Strong opinions on politics or morals")]
        [TestCase(91, 91, "Moody")]
        [TestCase(92, 92, "Cruel")]
        [TestCase(93, 93, "Uses flowery speech or long words")]
        [TestCase(94, 94, "Uses the same phrase over and over")]
        [TestCase(95, 95, "Sexist, racist, or otherwise prejudiced")]
        [TestCase(96, 96, "Fascinated by magic")]
        [TestCase(97, 97, "Distrustful of magic")]
        [TestCase(98, 98, "Prefers members of one class over all others")]
        [TestCase(99, 99, "Jokester")]
        [TestCase(100, 100, "No sense of humor")]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }
    }
}