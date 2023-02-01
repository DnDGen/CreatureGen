﻿using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class WeightsTests : TypesAndAmountsTests
    {
        private ICollectionSelector collectionSelector;
        private Dice dice;

        protected override string tableName => TableNameConstants.TypeAndAmount.Weights;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            dice = GetNewInstanceOf<Dice>();
        }

        [Test]
        public void WeightsNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(nameof(CreatureWeightsData))]
        public void CreatureWeights(string name, Dictionary<string, string> typesAndRolls)
        {
            var genders = collectionSelector.SelectFrom(TableNameConstants.Collection.Genders, name);

            Assert.That(typesAndRolls, Is.Not.Empty, name);
            Assert.That(typesAndRolls.Keys, Is.EqualTo(genders.Union(new[] { name })), name);

            AssertTypesAndAmounts(name, typesAndRolls);
        }

        public static Dictionary<string, Dictionary<string, string>> GetCreatureWeights()
        {
            var creatures = CreatureConstants.GetAll();
            var weights = new Dictionary<string, Dictionary<string, string>>();

            foreach (var creature in creatures)
            {
                weights[creature] = new Dictionary<string, string>();
            }

            //INFO: The weight modifier is multiplied by the height modifier
            weights[CreatureConstants.Aasimar][GenderConstants.Female] = "90";
            weights[CreatureConstants.Aasimar][GenderConstants.Male] = "110";
            weights[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "2d4"; //x5
            weights[CreatureConstants.Aboleth][GenderConstants.Hermaphrodite] = GetGenderFromAverage(6500);
            weights[CreatureConstants.Aboleth][CreatureConstants.Aboleth] = GetCreatureFromAverage(6500, 20 * 12);
            weights[CreatureConstants.Achaierai][GenderConstants.Female] = GetGenderFromAverage(750);
            weights[CreatureConstants.Achaierai][GenderConstants.Male] = GetGenderFromAverage(750);
            weights[CreatureConstants.Achaierai][CreatureConstants.Achaierai] = GetCreatureFromAverage(750, 15 * 12);
            weights[CreatureConstants.Allip][GenderConstants.Female] = "85";
            weights[CreatureConstants.Allip][GenderConstants.Male] = "120";
            weights[CreatureConstants.Allip][CreatureConstants.Human] = "2d4"; //x5
            weights[CreatureConstants.Androsphinx][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = GetCreatureFromAverage(800, 10 * 12);
            weights[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = GetGenderFromAverage(250);
            weights[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = GetGenderFromAverage(250);
            weights[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = GetCreatureFromAverage(250, 7 * 12 + 3);
            weights[CreatureConstants.Angel_Planetar][GenderConstants.Female] = GetGenderFromAverage(500);
            weights[CreatureConstants.Angel_Planetar][GenderConstants.Male] = GetGenderFromAverage(500);
            weights[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = GetCreatureFromAverage(500, 8 * 12 + 6);
            weights[CreatureConstants.Angel_Solar][GenderConstants.Female] = GetGenderFromAverage(500);
            weights[CreatureConstants.Angel_Solar][GenderConstants.Male] = GetGenderFromAverage(500);
            weights[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = GetCreatureFromAverage(500, 9 * 12 + 6);
            weights[CreatureConstants.AnimatedObject_Anvil_Colossal][GenderConstants.Agender] = GetGenderFromAverage(2400);
            weights[CreatureConstants.AnimatedObject_Anvil_Colossal][CreatureConstants.AnimatedObject_Anvil_Colossal] = GetCreatureFromAverage(2400, 36 * 12);
            weights[CreatureConstants.AnimatedObject_Anvil_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(1200);
            weights[CreatureConstants.AnimatedObject_Anvil_Gargantuan][CreatureConstants.AnimatedObject_Anvil_Gargantuan] = GetCreatureFromAverage(1200, 18 * 12);
            weights[CreatureConstants.AnimatedObject_Anvil_Huge][GenderConstants.Agender] = GetGenderFromAverage(600);
            weights[CreatureConstants.AnimatedObject_Anvil_Huge][CreatureConstants.AnimatedObject_Anvil_Huge] = GetCreatureFromAverage(600, 18 * 12 / 2);
            weights[CreatureConstants.AnimatedObject_Anvil_Large][GenderConstants.Agender] = GetGenderFromAverage(300);
            weights[CreatureConstants.AnimatedObject_Anvil_Large][CreatureConstants.AnimatedObject_Anvil_Large] = GetCreatureFromAverage(300, (9 * 12 + 6) / 2);
            weights[CreatureConstants.AnimatedObject_Anvil_Medium][GenderConstants.Agender] = GetGenderFromAverage(150);
            weights[CreatureConstants.AnimatedObject_Anvil_Medium][CreatureConstants.AnimatedObject_Anvil_Medium] = GetCreatureFromAverage(150, 3 * 12);
            weights[CreatureConstants.AnimatedObject_Anvil_Small][GenderConstants.Agender] = GetGenderFromAverage(75);
            weights[CreatureConstants.AnimatedObject_Anvil_Small][CreatureConstants.AnimatedObject_Anvil_Small] = GetCreatureFromAverage(75, 18);
            weights[CreatureConstants.AnimatedObject_Anvil_Tiny][GenderConstants.Agender] = GetGenderFromAverage(75 / 2);
            weights[CreatureConstants.AnimatedObject_Anvil_Tiny][CreatureConstants.AnimatedObject_Anvil_Tiny] = GetCreatureFromAverage(75 / 2, 10);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Colossal][GenderConstants.Agender] = GetGenderFromAverage(8_011_128);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Colossal][CreatureConstants.AnimatedObject_Block_Stone_Colossal] = GetCreatureFromAverage(8_011_128, 36 * 12);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(1_001_391);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan][CreatureConstants.AnimatedObject_Block_Stone_Gargantuan] = GetCreatureFromAverage(1_001_391, 18 * 12);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Huge][GenderConstants.Agender] = GetGenderFromAverage(125_173);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Huge][CreatureConstants.AnimatedObject_Block_Stone_Huge] = GetCreatureFromAverage(125_173, 18 * 12 / 2);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Large][GenderConstants.Agender] = GetGenderFromAverage(18_402);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Large][CreatureConstants.AnimatedObject_Block_Stone_Large] = GetCreatureFromAverage(18_402, 57);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Medium][GenderConstants.Agender] = GetGenderFromAverage(3905);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Medium][CreatureConstants.AnimatedObject_Block_Stone_Medium] = GetCreatureFromAverage(3905, 34);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Small][GenderConstants.Agender] = GetGenderFromAverage(579);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Small][CreatureConstants.AnimatedObject_Block_Stone_Small] = GetCreatureFromAverage(579, 18);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Tiny][GenderConstants.Agender] = GetGenderFromAverage(99);
            weights[CreatureConstants.AnimatedObject_Block_Stone_Tiny][CreatureConstants.AnimatedObject_Block_Stone_Tiny] = GetCreatureFromAverage(99, 10);
            weights[CreatureConstants.AnimatedObject_Block_Wood_Colossal][GenderConstants.Agender] = "1500000";
            weights[CreatureConstants.AnimatedObject_Block_Wood_Colossal][CreatureConstants.AnimatedObject_Block_Wood_Colossal] = RollHelper.GetRollWithFewestDice(1_500_000, 1_726_272, 2_612_736);
            weights[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan][GenderConstants.Agender] = "200000";
            weights[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan][CreatureConstants.AnimatedObject_Block_Wood_Gargantuan] = RollHelper.GetRollWithFewestDice(200_000, 215_784, 326_592);
            weights[CreatureConstants.AnimatedObject_Block_Wood_Huge][GenderConstants.Agender] = "20000";
            weights[CreatureConstants.AnimatedObject_Block_Wood_Huge][CreatureConstants.AnimatedObject_Block_Wood_Huge] = RollHelper.GetRollWithFewestDice(20_000, 26_973, 40_824);
            weights[CreatureConstants.AnimatedObject_Block_Wood_Large][GenderConstants.Agender] = "3000";
            weights[CreatureConstants.AnimatedObject_Block_Wood_Large][CreatureConstants.AnimatedObject_Block_Wood_Large] = RollHelper.GetRollWithFewestDice(3000, 3965, 6002);
            weights[CreatureConstants.AnimatedObject_Block_Wood_Medium][GenderConstants.Agender] = "800";
            weights[CreatureConstants.AnimatedObject_Block_Wood_Medium][CreatureConstants.AnimatedObject_Block_Wood_Medium] = RollHelper.GetRollWithFewestDice(800, 841, 1274);
            weights[CreatureConstants.AnimatedObject_Block_Wood_Small][GenderConstants.Agender] = "120";
            weights[CreatureConstants.AnimatedObject_Block_Wood_Small][CreatureConstants.AnimatedObject_Block_Wood_Small] = RollHelper.GetRollWithFewestDice(120, 124, 189);
            weights[CreatureConstants.AnimatedObject_Block_Wood_Tiny][GenderConstants.Agender] = "20";
            weights[CreatureConstants.AnimatedObject_Block_Wood_Tiny][CreatureConstants.AnimatedObject_Block_Wood_Tiny] = RollHelper.GetRollWithFewestDice(20, 21, 33);
            weights[CreatureConstants.AnimatedObject_Box_Colossal][GenderConstants.Agender] = GetGenderFromAverage(1024);
            weights[CreatureConstants.AnimatedObject_Box_Colossal][CreatureConstants.AnimatedObject_Box_Colossal] = GetCreatureFromAverage(1024, 864);
            weights[CreatureConstants.AnimatedObject_Box_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(512);
            weights[CreatureConstants.AnimatedObject_Box_Gargantuan][CreatureConstants.AnimatedObject_Box_Gargantuan] = GetCreatureFromAverage(512, 432);
            weights[CreatureConstants.AnimatedObject_Box_Huge][GenderConstants.Agender] = GetGenderFromAverage(256);
            weights[CreatureConstants.AnimatedObject_Box_Huge][CreatureConstants.AnimatedObject_Box_Huge] = GetCreatureFromAverage(256, 216);
            weights[CreatureConstants.AnimatedObject_Box_Large][GenderConstants.Agender] = GetGenderFromAverage(128);
            weights[CreatureConstants.AnimatedObject_Box_Large][CreatureConstants.AnimatedObject_Box_Large] = GetCreatureFromAverage(128, 108);
            weights[CreatureConstants.AnimatedObject_Box_Medium][GenderConstants.Agender] = GetGenderFromAverage(64);
            weights[CreatureConstants.AnimatedObject_Box_Medium][CreatureConstants.AnimatedObject_Box_Medium] = GetCreatureFromAverage(64, 57);
            weights[CreatureConstants.AnimatedObject_Box_Small][GenderConstants.Agender] = GetGenderFromAverage(32);
            weights[CreatureConstants.AnimatedObject_Box_Small][CreatureConstants.AnimatedObject_Box_Small] = GetCreatureFromAverage(32, 18);
            weights[CreatureConstants.AnimatedObject_Box_Tiny][GenderConstants.Agender] = GetGenderFromAverage(16);
            weights[CreatureConstants.AnimatedObject_Box_Tiny][CreatureConstants.AnimatedObject_Box_Tiny] = GetCreatureFromAverage(16, 10);
            weights[CreatureConstants.AnimatedObject_Carpet_Colossal][GenderConstants.Agender] = GetGenderFromAverage(1600);
            weights[CreatureConstants.AnimatedObject_Carpet_Colossal][CreatureConstants.AnimatedObject_Carpet_Colossal] = GetCreatureFromAverage(1600, 80 * 12);
            weights[CreatureConstants.AnimatedObject_Carpet_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(800);
            weights[CreatureConstants.AnimatedObject_Carpet_Gargantuan][CreatureConstants.AnimatedObject_Carpet_Gargantuan] = GetCreatureFromAverage(800, 40 * 12);
            weights[CreatureConstants.AnimatedObject_Carpet_Huge][GenderConstants.Agender] = GetGenderFromAverage(400);
            weights[CreatureConstants.AnimatedObject_Carpet_Huge][CreatureConstants.AnimatedObject_Carpet_Huge] = GetCreatureFromAverage(400, 20 * 12);
            weights[CreatureConstants.AnimatedObject_Carpet_Large][GenderConstants.Agender] = GetGenderFromAverage(100);
            weights[CreatureConstants.AnimatedObject_Carpet_Large][CreatureConstants.AnimatedObject_Carpet_Large] = GetCreatureFromAverage(100, 10 * 12);
            weights[CreatureConstants.AnimatedObject_Carpet_Medium][GenderConstants.Agender] = GetGenderFromAverage(25);
            weights[CreatureConstants.AnimatedObject_Carpet_Medium][CreatureConstants.AnimatedObject_Carpet_Medium] = GetCreatureFromAverage(25, 5 * 12);
            weights[CreatureConstants.AnimatedObject_Carpet_Small][GenderConstants.Agender] = GetGenderFromAverage(16);
            weights[CreatureConstants.AnimatedObject_Carpet_Small][CreatureConstants.AnimatedObject_Carpet_Small] = GetCreatureFromAverage(16, 4 * 12);
            weights[CreatureConstants.AnimatedObject_Carpet_Tiny][GenderConstants.Agender] = GetGenderFromAverage(6);
            weights[CreatureConstants.AnimatedObject_Carpet_Tiny][CreatureConstants.AnimatedObject_Carpet_Tiny] = GetCreatureFromAverage(6, 2 * 12 + 6);
            weights[CreatureConstants.AnimatedObject_Carriage_Colossal][GenderConstants.Agender] = GetGenderFromAverage(1600);
            weights[CreatureConstants.AnimatedObject_Carriage_Colossal][CreatureConstants.AnimatedObject_Carriage_Colossal] = GetCreatureFromAverage(1600, 176 * 12);
            weights[CreatureConstants.AnimatedObject_Carriage_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(800);
            weights[CreatureConstants.AnimatedObject_Carriage_Gargantuan][CreatureConstants.AnimatedObject_Carriage_Gargantuan] = GetCreatureFromAverage(4800, 88 * 12);
            weights[CreatureConstants.AnimatedObject_Carriage_Huge][GenderConstants.Agender] = GetGenderFromAverage(400);
            weights[CreatureConstants.AnimatedObject_Carriage_Huge][CreatureConstants.AnimatedObject_Carriage_Huge] = GetCreatureFromAverage(2400, 44 * 12);
            weights[CreatureConstants.AnimatedObject_Carriage_Large][GenderConstants.Agender] = GetGenderFromAverage(100);
            weights[CreatureConstants.AnimatedObject_Carriage_Large][CreatureConstants.AnimatedObject_Carriage_Large] = GetCreatureFromAverage(1200, 22 * 12);
            weights[CreatureConstants.AnimatedObject_Carriage_Medium][GenderConstants.Agender] = GetGenderFromAverage(25);
            weights[CreatureConstants.AnimatedObject_Carriage_Medium][CreatureConstants.AnimatedObject_Carriage_Medium] = GetCreatureFromAverage(600, 11 * 12);
            weights[CreatureConstants.AnimatedObject_Carriage_Small][GenderConstants.Agender] = GetGenderFromAverage(16);
            weights[CreatureConstants.AnimatedObject_Carriage_Small][CreatureConstants.AnimatedObject_Carriage_Small] = GetCreatureFromAverage(300, 6 * 12);
            weights[CreatureConstants.AnimatedObject_Carriage_Tiny][GenderConstants.Agender] = GetGenderFromAverage(6);
            weights[CreatureConstants.AnimatedObject_Carriage_Tiny][CreatureConstants.AnimatedObject_Carriage_Tiny] = GetCreatureFromAverage(150, 3 * 12);
            weights[CreatureConstants.AnimatedObject_Chain_Colossal][GenderConstants.Agender] = GetGenderFromAverage(32);
            weights[CreatureConstants.AnimatedObject_Chain_Colossal][CreatureConstants.AnimatedObject_Chain_Colossal] = GetCreatureFromAverage(32, 160 * 12);
            weights[CreatureConstants.AnimatedObject_Chain_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(16);
            weights[CreatureConstants.AnimatedObject_Chain_Gargantuan][CreatureConstants.AnimatedObject_Chain_Gargantuan] = GetCreatureFromAverage(16, 80 * 12);
            weights[CreatureConstants.AnimatedObject_Chain_Huge][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.AnimatedObject_Chain_Huge][CreatureConstants.AnimatedObject_Chain_Huge] = GetCreatureFromAverage(8, 40 * 12);
            weights[CreatureConstants.AnimatedObject_Chain_Large][GenderConstants.Agender] = GetGenderFromAverage(4);
            weights[CreatureConstants.AnimatedObject_Chain_Large][CreatureConstants.AnimatedObject_Chain_Large] = GetCreatureFromAverage(4, 20 * 12);
            weights[CreatureConstants.AnimatedObject_Chain_Medium][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.AnimatedObject_Chain_Medium][CreatureConstants.AnimatedObject_Chain_Medium] = GetCreatureFromAverage(2, 10 * 12);
            weights[CreatureConstants.AnimatedObject_Chain_Small][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.AnimatedObject_Chain_Small][CreatureConstants.AnimatedObject_Chain_Small] = GetCreatureFromAverage(1, 5 * 12);
            weights[CreatureConstants.AnimatedObject_Chain_Tiny][GenderConstants.Agender] = GetGenderFromAverage(0.5);
            weights[CreatureConstants.AnimatedObject_Chain_Tiny][CreatureConstants.AnimatedObject_Chain_Tiny] = GetCreatureFromAverage(1, 2 * 12 + 6);
            weights[CreatureConstants.AnimatedObject_Chair_Colossal][GenderConstants.Agender] = GetGenderFromAverage(208);
            weights[CreatureConstants.AnimatedObject_Chair_Colossal][CreatureConstants.AnimatedObject_Chair_Colossal] = GetCreatureFromAverage(480, 592);
            weights[CreatureConstants.AnimatedObject_Chair_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(104);
            weights[CreatureConstants.AnimatedObject_Chair_Gargantuan][CreatureConstants.AnimatedObject_Chair_Gargantuan] = GetCreatureFromAverage(240, 296);
            weights[CreatureConstants.AnimatedObject_Chair_Huge][GenderConstants.Agender] = GetGenderFromAverage(52);
            weights[CreatureConstants.AnimatedObject_Chair_Huge][CreatureConstants.AnimatedObject_Chair_Huge] = GetCreatureFromAverage(120, 148);
            weights[CreatureConstants.AnimatedObject_Chair_Large][GenderConstants.Agender] = GetGenderFromAverage(26);
            weights[CreatureConstants.AnimatedObject_Chair_Large][CreatureConstants.AnimatedObject_Chair_Large] = GetCreatureFromAverage(60, 74);
            weights[CreatureConstants.AnimatedObject_Chair_Medium][GenderConstants.Agender] = GetGenderFromAverage(13);
            weights[CreatureConstants.AnimatedObject_Chair_Medium][CreatureConstants.AnimatedObject_Chair_Medium] = GetCreatureFromAverage(13, 37);
            weights[CreatureConstants.AnimatedObject_Chair_Small][GenderConstants.Agender] = GetGenderFromAverage(6);
            weights[CreatureConstants.AnimatedObject_Chair_Small][CreatureConstants.AnimatedObject_Chair_Small] = GetCreatureFromAverage(6, 18);
            weights[CreatureConstants.AnimatedObject_Chair_Tiny][GenderConstants.Agender] = GetGenderFromAverage(3);
            weights[CreatureConstants.AnimatedObject_Chair_Tiny][CreatureConstants.AnimatedObject_Chair_Tiny] = GetCreatureFromAverage(3, 9);
            weights[CreatureConstants.AnimatedObject_Clothes_Colossal][GenderConstants.Agender] = GetGenderFromAverage(64);
            weights[CreatureConstants.AnimatedObject_Clothes_Colossal][CreatureConstants.AnimatedObject_Clothes_Colossal] = GetCreatureFromAverage(64, 1080);
            weights[CreatureConstants.AnimatedObject_Clothes_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(32);
            weights[CreatureConstants.AnimatedObject_Clothes_Gargantuan][CreatureConstants.AnimatedObject_Clothes_Gargantuan] = GetCreatureFromAverage(32, 540);
            weights[CreatureConstants.AnimatedObject_Clothes_Huge][GenderConstants.Agender] = GetGenderFromAverage(16);
            weights[CreatureConstants.AnimatedObject_Clothes_Huge][CreatureConstants.AnimatedObject_Clothes_Huge] = GetCreatureFromAverage(16, 260);
            weights[CreatureConstants.AnimatedObject_Clothes_Large][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.AnimatedObject_Clothes_Large][CreatureConstants.AnimatedObject_Clothes_Large] = GetCreatureFromAverage(8, 130);
            weights[CreatureConstants.AnimatedObject_Clothes_Medium][GenderConstants.Agender] = GetGenderFromAverage(4);
            weights[CreatureConstants.AnimatedObject_Clothes_Medium][CreatureConstants.AnimatedObject_Clothes_Medium] = GetCreatureFromAverage(4, 67);
            weights[CreatureConstants.AnimatedObject_Clothes_Small][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.AnimatedObject_Clothes_Small][CreatureConstants.AnimatedObject_Clothes_Small] = GetCreatureFromAverage(2, 33);
            weights[CreatureConstants.AnimatedObject_Clothes_Tiny][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.AnimatedObject_Clothes_Tiny][CreatureConstants.AnimatedObject_Clothes_Tiny] = GetCreatureFromAverage(1, 16);
            weights[CreatureConstants.AnimatedObject_Ladder_Colossal][GenderConstants.Agender] = GetGenderFromAverage(320);
            weights[CreatureConstants.AnimatedObject_Ladder_Colossal][CreatureConstants.AnimatedObject_Ladder_Colossal] = GetCreatureFromAverage(320, 1920);
            weights[CreatureConstants.AnimatedObject_Ladder_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(160);
            weights[CreatureConstants.AnimatedObject_Ladder_Gargantuan][CreatureConstants.AnimatedObject_Ladder_Gargantuan] = GetCreatureFromAverage(160, 960);
            weights[CreatureConstants.AnimatedObject_Ladder_Huge][GenderConstants.Agender] = GetGenderFromAverage(80);
            weights[CreatureConstants.AnimatedObject_Ladder_Huge][CreatureConstants.AnimatedObject_Ladder_Huge] = GetCreatureFromAverage(80, 480);
            weights[CreatureConstants.AnimatedObject_Ladder_Large][GenderConstants.Agender] = GetGenderFromAverage(40);
            weights[CreatureConstants.AnimatedObject_Ladder_Large][CreatureConstants.AnimatedObject_Ladder_Large] = GetCreatureFromAverage(40, 240);
            weights[CreatureConstants.AnimatedObject_Ladder_Medium][GenderConstants.Agender] = GetGenderFromAverage(20);
            weights[CreatureConstants.AnimatedObject_Ladder_Medium][CreatureConstants.AnimatedObject_Ladder_Medium] = GetCreatureFromAverage(20, 120);
            weights[CreatureConstants.AnimatedObject_Ladder_Small][GenderConstants.Agender] = GetGenderFromAverage(10);
            weights[CreatureConstants.AnimatedObject_Ladder_Small][CreatureConstants.AnimatedObject_Ladder_Small] = GetCreatureFromAverage(10, 60);
            weights[CreatureConstants.AnimatedObject_Ladder_Tiny][GenderConstants.Agender] = GetGenderFromAverage(5);
            weights[CreatureConstants.AnimatedObject_Ladder_Tiny][CreatureConstants.AnimatedObject_Ladder_Tiny] = GetCreatureFromAverage(5, 30);
            weights[CreatureConstants.AnimatedObject_Rope_Colossal][GenderConstants.Agender] = GetGenderFromAverage(160);
            weights[CreatureConstants.AnimatedObject_Rope_Colossal][CreatureConstants.AnimatedObject_Rope_Colossal] = GetCreatureFromAverage(160, 9600);
            weights[CreatureConstants.AnimatedObject_Rope_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(80);
            weights[CreatureConstants.AnimatedObject_Rope_Gargantuan][CreatureConstants.AnimatedObject_Rope_Gargantuan] = GetCreatureFromAverage(80, 4800);
            weights[CreatureConstants.AnimatedObject_Rope_Huge][GenderConstants.Agender] = GetGenderFromAverage(40);
            weights[CreatureConstants.AnimatedObject_Rope_Huge][CreatureConstants.AnimatedObject_Rope_Huge] = GetCreatureFromAverage(40, 2400);
            weights[CreatureConstants.AnimatedObject_Rope_Large][GenderConstants.Agender] = GetGenderFromAverage(20);
            weights[CreatureConstants.AnimatedObject_Rope_Large][CreatureConstants.AnimatedObject_Rope_Large] = GetCreatureFromAverage(20, 1200);
            weights[CreatureConstants.AnimatedObject_Rope_Medium][GenderConstants.Agender] = GetGenderFromAverage(10);
            weights[CreatureConstants.AnimatedObject_Rope_Medium][CreatureConstants.AnimatedObject_Rope_Medium] = GetCreatureFromAverage(10, 50 * 12);
            weights[CreatureConstants.AnimatedObject_Rope_Small][GenderConstants.Agender] = GetGenderFromAverage(5);
            weights[CreatureConstants.AnimatedObject_Rope_Small][CreatureConstants.AnimatedObject_Rope_Small] = GetCreatureFromAverage(5, 25 * 12);
            weights[CreatureConstants.AnimatedObject_Rope_Tiny][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.AnimatedObject_Rope_Tiny][CreatureConstants.AnimatedObject_Rope_Tiny] = GetCreatureFromAverage(2, 144);
            weights[CreatureConstants.AnimatedObject_Rug_Colossal][GenderConstants.Agender] = GetGenderFromAverage(1600);
            weights[CreatureConstants.AnimatedObject_Rug_Colossal][CreatureConstants.AnimatedObject_Rug_Colossal] = GetCreatureFromAverage(1600, 80 * 12);
            weights[CreatureConstants.AnimatedObject_Rug_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(800);
            weights[CreatureConstants.AnimatedObject_Rug_Gargantuan][CreatureConstants.AnimatedObject_Rug_Gargantuan] = GetCreatureFromAverage(800, 40 * 12);
            weights[CreatureConstants.AnimatedObject_Rug_Huge][GenderConstants.Agender] = GetGenderFromAverage(400);
            weights[CreatureConstants.AnimatedObject_Rug_Huge][CreatureConstants.AnimatedObject_Rug_Huge] = GetCreatureFromAverage(400, 20 * 12);
            weights[CreatureConstants.AnimatedObject_Rug_Large][GenderConstants.Agender] = GetGenderFromAverage(100);
            weights[CreatureConstants.AnimatedObject_Rug_Large][CreatureConstants.AnimatedObject_Rug_Large] = GetCreatureFromAverage(100, 10 * 12);
            weights[CreatureConstants.AnimatedObject_Rug_Medium][GenderConstants.Agender] = GetGenderFromAverage(25);
            weights[CreatureConstants.AnimatedObject_Rug_Medium][CreatureConstants.AnimatedObject_Rug_Medium] = GetCreatureFromAverage(25, 5 * 12);
            weights[CreatureConstants.AnimatedObject_Rug_Small][GenderConstants.Agender] = GetGenderFromAverage(16);
            weights[CreatureConstants.AnimatedObject_Rug_Small][CreatureConstants.AnimatedObject_Rug_Small] = GetCreatureFromAverage(16, 4 * 12);
            weights[CreatureConstants.AnimatedObject_Rug_Tiny][GenderConstants.Agender] = GetGenderFromAverage(6);
            weights[CreatureConstants.AnimatedObject_Rug_Tiny][CreatureConstants.AnimatedObject_Rug_Tiny] = GetCreatureFromAverage(6, 2 * 12 + 6);
            weights[CreatureConstants.AnimatedObject_Sled_Colossal][GenderConstants.Agender] = "144";
            weights[CreatureConstants.AnimatedObject_Sled_Colossal][CreatureConstants.AnimatedObject_Sled_Colossal] = "6d8";
            weights[CreatureConstants.AnimatedObject_Sled_Gargantuan][GenderConstants.Agender] = "72";
            weights[CreatureConstants.AnimatedObject_Sled_Gargantuan][CreatureConstants.AnimatedObject_Sled_Gargantuan] = "4d6";
            weights[CreatureConstants.AnimatedObject_Sled_Huge][GenderConstants.Agender] = "36";
            weights[CreatureConstants.AnimatedObject_Sled_Huge][CreatureConstants.AnimatedObject_Sled_Huge] = "3d4";
            weights[CreatureConstants.AnimatedObject_Sled_Large][GenderConstants.Agender] = "18";
            weights[CreatureConstants.AnimatedObject_Sled_Large][CreatureConstants.AnimatedObject_Sled_Large] = "2d3";
            weights[CreatureConstants.AnimatedObject_Sled_Medium][GenderConstants.Agender] = "9";
            weights[CreatureConstants.AnimatedObject_Sled_Medium][CreatureConstants.AnimatedObject_Sled_Medium] = "1d3";
            weights[CreatureConstants.AnimatedObject_Sled_Small][GenderConstants.Agender] = "4";
            weights[CreatureConstants.AnimatedObject_Sled_Small][CreatureConstants.AnimatedObject_Sled_Small] = "1d2";
            weights[CreatureConstants.AnimatedObject_Sled_Tiny][GenderConstants.Agender] = "2";
            weights[CreatureConstants.AnimatedObject_Sled_Tiny][CreatureConstants.AnimatedObject_Sled_Tiny] = "1";
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Colossal][GenderConstants.Agender] = "100*2000*3";
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Colossal][CreatureConstants.AnimatedObject_Statue_Animal_Colossal] = RollHelper.GetRollWithFewestDice(100 * 2000 * 3, 125 * 2000 * 3, 1000 * 2000 * 3);
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan][GenderConstants.Agender] = "10*2000*3";
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan][CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan] = RollHelper.GetRollWithFewestDice(10 * 2000 * 3, 16 * 2000 * 3, 125 * 2000 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Huge][GenderConstants.Agender] = "2000*3";
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Huge][CreatureConstants.AnimatedObject_Statue_Animal_Huge] = RollHelper.GetRollWithFewestDice(2000 * 3, 2 * 2000 * 3, 16 * 2000 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Large][GenderConstants.Agender] = "1000";
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Large][CreatureConstants.AnimatedObject_Statue_Animal_Large] = RollHelper.GetRollWithFewestDice(1000, 500 * 3, 2 * 2000 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Medium][GenderConstants.Agender] = "150";
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Medium][CreatureConstants.AnimatedObject_Statue_Animal_Medium] = RollHelper.GetRollWithFewestDice(150, 60 * 3, 500 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Small][GenderConstants.Agender] = "20";
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Small][CreatureConstants.AnimatedObject_Statue_Animal_Small] = RollHelper.GetRollWithFewestDice(20, 8 * 3, 60 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Tiny][GenderConstants.Agender] = "1";
            weights[CreatureConstants.AnimatedObject_Statue_Animal_Tiny][CreatureConstants.AnimatedObject_Statue_Animal_Tiny] = RollHelper.GetRollWithFewestDice(1, 3, 23);
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal][GenderConstants.Agender] = "100*2000*3";
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal][CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal] = RollHelper.GetRollWithFewestDice(100 * 2000 * 3, 125 * 2000 * 3, 1000 * 2000 * 3);
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan][GenderConstants.Agender] = "10*2000*3";
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan][CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan] = RollHelper.GetRollWithFewestDice(10 * 2000 * 3, 16 * 2000 * 3, 125 * 2000 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge][GenderConstants.Agender] = "2000*3";
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge][CreatureConstants.AnimatedObject_Statue_Humanoid_Huge] = RollHelper.GetRollWithFewestDice(2000 * 3, 2 * 2000 * 3, 16 * 2000 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Large][GenderConstants.Agender] = "1000";
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Large][CreatureConstants.AnimatedObject_Statue_Humanoid_Large] = RollHelper.GetRollWithFewestDice(1000, 500 * 3, 2 * 2000 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium][GenderConstants.Agender] = "150";
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium][CreatureConstants.AnimatedObject_Statue_Humanoid_Medium] = RollHelper.GetRollWithFewestDice(150, 60 * 3, 500 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Small][GenderConstants.Agender] = "20";
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Small][CreatureConstants.AnimatedObject_Statue_Humanoid_Small] = RollHelper.GetRollWithFewestDice(20, 8 * 3, 60 * 3 - 1);
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny][GenderConstants.Agender] = "1";
            weights[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny][CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny] = RollHelper.GetRollWithFewestDice(1, 3, 23);
            weights[CreatureConstants.AnimatedObject_Stool_Colossal][GenderConstants.Agender] = "272";
            weights[CreatureConstants.AnimatedObject_Stool_Colossal][CreatureConstants.AnimatedObject_Stool_Colossal] = "6d8";
            weights[CreatureConstants.AnimatedObject_Stool_Gargantuan][GenderConstants.Agender] = "136";
            weights[CreatureConstants.AnimatedObject_Stool_Gargantuan][CreatureConstants.AnimatedObject_Stool_Gargantuan] = "4d6";
            weights[CreatureConstants.AnimatedObject_Stool_Huge][GenderConstants.Agender] = "68";
            weights[CreatureConstants.AnimatedObject_Stool_Huge][CreatureConstants.AnimatedObject_Stool_Huge] = "3d4";
            weights[CreatureConstants.AnimatedObject_Stool_Large][GenderConstants.Agender] = "34";
            weights[CreatureConstants.AnimatedObject_Stool_Large][CreatureConstants.AnimatedObject_Stool_Large] = "2d3";
            weights[CreatureConstants.AnimatedObject_Stool_Medium][GenderConstants.Agender] = "17";
            weights[CreatureConstants.AnimatedObject_Stool_Medium][CreatureConstants.AnimatedObject_Stool_Medium] = "1d3";
            weights[CreatureConstants.AnimatedObject_Stool_Small][GenderConstants.Agender] = "8";
            weights[CreatureConstants.AnimatedObject_Stool_Small][CreatureConstants.AnimatedObject_Stool_Small] = "1d2";
            weights[CreatureConstants.AnimatedObject_Stool_Tiny][GenderConstants.Agender] = "4";
            weights[CreatureConstants.AnimatedObject_Stool_Tiny][CreatureConstants.AnimatedObject_Stool_Tiny] = "1";
            weights[CreatureConstants.AnimatedObject_Table_Colossal][GenderConstants.Agender] = "2400";
            weights[CreatureConstants.AnimatedObject_Table_Colossal][CreatureConstants.AnimatedObject_Table_Colossal] = RollHelper.GetRollWithFewestDice(2400, 2880, 4000);
            weights[CreatureConstants.AnimatedObject_Table_Gargantuan][GenderConstants.Agender] = "1200";
            weights[CreatureConstants.AnimatedObject_Table_Gargantuan][CreatureConstants.AnimatedObject_Table_Gargantuan] = RollHelper.GetRollWithFewestDice(1200, 1440, 2000);
            weights[CreatureConstants.AnimatedObject_Table_Huge][GenderConstants.Agender] = "600";
            weights[CreatureConstants.AnimatedObject_Table_Huge][CreatureConstants.AnimatedObject_Table_Huge] = RollHelper.GetRollWithFewestDice(600, 720, 1000);
            weights[CreatureConstants.AnimatedObject_Table_Large][GenderConstants.Agender] = "300";
            weights[CreatureConstants.AnimatedObject_Table_Large][CreatureConstants.AnimatedObject_Table_Large] = RollHelper.GetRollWithFewestDice(300, 360, 500);
            weights[CreatureConstants.AnimatedObject_Table_Medium][GenderConstants.Agender] = "150";
            weights[CreatureConstants.AnimatedObject_Table_Medium][CreatureConstants.AnimatedObject_Table_Medium] = RollHelper.GetRollWithFewestDice(150, 180, 250);
            weights[CreatureConstants.AnimatedObject_Table_Small][GenderConstants.Agender] = "75";
            weights[CreatureConstants.AnimatedObject_Table_Small][CreatureConstants.AnimatedObject_Table_Small] = RollHelper.GetRollWithFewestDice(75, 90, 125);
            weights[CreatureConstants.AnimatedObject_Table_Tiny][GenderConstants.Agender] = "37";
            weights[CreatureConstants.AnimatedObject_Table_Tiny][CreatureConstants.AnimatedObject_Table_Tiny] = RollHelper.GetRollWithFewestDice(37, 45, 62);
            weights[CreatureConstants.AnimatedObject_Tapestry_Colossal][GenderConstants.Agender] = GetGenderFromAverage(1600);
            weights[CreatureConstants.AnimatedObject_Tapestry_Colossal][CreatureConstants.AnimatedObject_Tapestry_Colossal] = GetCreatureFromAverage(1600, 80 * 12);
            weights[CreatureConstants.AnimatedObject_Tapestry_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(800);
            weights[CreatureConstants.AnimatedObject_Tapestry_Gargantuan][CreatureConstants.AnimatedObject_Tapestry_Gargantuan] = GetCreatureFromAverage(800, 40 * 12);
            weights[CreatureConstants.AnimatedObject_Tapestry_Huge][GenderConstants.Agender] = GetGenderFromAverage(400);
            weights[CreatureConstants.AnimatedObject_Tapestry_Huge][CreatureConstants.AnimatedObject_Tapestry_Huge] = GetCreatureFromAverage(400, 20 * 12);
            weights[CreatureConstants.AnimatedObject_Tapestry_Large][GenderConstants.Agender] = GetGenderFromAverage(100);
            weights[CreatureConstants.AnimatedObject_Tapestry_Large][CreatureConstants.AnimatedObject_Tapestry_Large] = GetCreatureFromAverage(100, 10 * 12);
            weights[CreatureConstants.AnimatedObject_Tapestry_Medium][GenderConstants.Agender] = GetGenderFromAverage(25);
            weights[CreatureConstants.AnimatedObject_Tapestry_Medium][CreatureConstants.AnimatedObject_Tapestry_Medium] = GetCreatureFromAverage(25, 5 * 12);
            weights[CreatureConstants.AnimatedObject_Tapestry_Small][GenderConstants.Agender] = GetGenderFromAverage(16);
            weights[CreatureConstants.AnimatedObject_Tapestry_Small][CreatureConstants.AnimatedObject_Tapestry_Small] = GetCreatureFromAverage(16, 4 * 12);
            weights[CreatureConstants.AnimatedObject_Tapestry_Tiny][GenderConstants.Agender] = GetGenderFromAverage(6);
            weights[CreatureConstants.AnimatedObject_Tapestry_Tiny][CreatureConstants.AnimatedObject_Tapestry_Tiny] = GetCreatureFromAverage(6, 2 * 12 + 6);
            weights[CreatureConstants.AnimatedObject_Wagon_Colossal][GenderConstants.Agender] = GetGenderFromAverage(14400);
            weights[CreatureConstants.AnimatedObject_Wagon_Colossal][CreatureConstants.AnimatedObject_Wagon_Colossal] = GetCreatureFromAverage(14400, 88 * 12);
            weights[CreatureConstants.AnimatedObject_Wagon_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(7200);
            weights[CreatureConstants.AnimatedObject_Wagon_Gargantuan][CreatureConstants.AnimatedObject_Wagon_Gargantuan] = GetCreatureFromAverage(7200, 44 * 12);
            weights[CreatureConstants.AnimatedObject_Wagon_Huge][GenderConstants.Agender] = GetGenderFromAverage(3600);
            weights[CreatureConstants.AnimatedObject_Wagon_Huge][CreatureConstants.AnimatedObject_Wagon_Huge] = GetCreatureFromAverage(3600, 22 * 12);
            weights[CreatureConstants.AnimatedObject_Wagon_Large][GenderConstants.Agender] = GetGenderFromAverage(1800);
            weights[CreatureConstants.AnimatedObject_Wagon_Large][CreatureConstants.AnimatedObject_Wagon_Large] = GetCreatureFromAverage(1800, 11 * 12);
            weights[CreatureConstants.AnimatedObject_Wagon_Medium][GenderConstants.Agender] = GetGenderFromAverage(900);
            weights[CreatureConstants.AnimatedObject_Wagon_Medium][CreatureConstants.AnimatedObject_Wagon_Medium] = GetCreatureFromAverage(900, 6 * 12);
            weights[CreatureConstants.AnimatedObject_Wagon_Small][GenderConstants.Agender] = GetGenderFromAverage(450);
            weights[CreatureConstants.AnimatedObject_Wagon_Small][CreatureConstants.AnimatedObject_Wagon_Small] = GetCreatureFromAverage(450, 3 * 12);
            weights[CreatureConstants.AnimatedObject_Wagon_Tiny][GenderConstants.Agender] = GetGenderFromAverage(225);
            weights[CreatureConstants.AnimatedObject_Wagon_Tiny][CreatureConstants.AnimatedObject_Wagon_Tiny] = GetCreatureFromAverage(225, 12);
            weights[CreatureConstants.Ankheg][GenderConstants.Female] = GetGenderFromAverage(800);
            weights[CreatureConstants.Ankheg][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = GetCreatureFromAverage(800, 10 * 12);
            weights[CreatureConstants.Annis][GenderConstants.Female] = GetGenderFromAverage(325);
            weights[CreatureConstants.Annis][CreatureConstants.Annis] = GetCreatureFromAverage(325, 8 * 12);
            weights[CreatureConstants.Ape][GenderConstants.Female] = "299";
            weights[CreatureConstants.Ape][GenderConstants.Male] = "299";
            weights[CreatureConstants.Ape][CreatureConstants.Ape] = RollHelper.GetRollWithFewestDice(299, 300, 400);
            weights[CreatureConstants.Ape_Dire][GenderConstants.Female] = "796";
            weights[CreatureConstants.Ape_Dire][GenderConstants.Male] = "796";
            weights[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = RollHelper.GetRollWithFewestDice(796, 800, 1200);
            //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
            weights[CreatureConstants.Aranea][GenderConstants.Female] = "4*12+5";
            weights[CreatureConstants.Aranea][GenderConstants.Male] = "4*12+7";
            weights[CreatureConstants.Aranea][CreatureConstants.Aranea] = "2d8";
            weights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = GetGenderFromAverage(20);
            weights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = GetGenderFromAverage(20);
            weights[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Arrowhawk_Juvenile] = GetCreatureFromAverage(20, 5 * 12);
            weights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = GetGenderFromAverage(100);
            weights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = GetGenderFromAverage(100);
            weights[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Arrowhawk_Adult] = GetCreatureFromAverage(100, 10 * 12);
            weights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = GetGenderFromAverage(800);
            weights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Arrowhawk_Elder] = GetCreatureFromAverage(800, 20 * 12);
            weights[CreatureConstants.Athach][GenderConstants.Female] = GetGenderFromAverage(4500);
            weights[CreatureConstants.Athach][GenderConstants.Male] = GetGenderFromAverage(4500);
            weights[CreatureConstants.Athach][CreatureConstants.Athach] = GetCreatureFromAverage(4500, 18 * 12);
            weights[CreatureConstants.Avoral][GenderConstants.Female] = GetGenderFromAverage(120);
            weights[CreatureConstants.Avoral][GenderConstants.Male] = GetGenderFromAverage(120);
            weights[CreatureConstants.Avoral][GenderConstants.Agender] = GetGenderFromAverage(120);
            weights[CreatureConstants.Avoral][CreatureConstants.Avoral] = GetCreatureFromAverage(120, 6 * 12 + 9);
            weights[CreatureConstants.Azer][GenderConstants.Female] = GetGenderFromAverage(200);
            weights[CreatureConstants.Azer][GenderConstants.Male] = GetGenderFromAverage(200);
            weights[CreatureConstants.Azer][GenderConstants.Agender] = GetGenderFromAverage(200);
            weights[CreatureConstants.Azer][CreatureConstants.Azer] = GetCreatureFromAverage(200, 4 * 12 + 7);
            weights[CreatureConstants.Babau][GenderConstants.Agender] = GetGenderFromAverage(140);
            weights[CreatureConstants.Babau][CreatureConstants.Babau] = GetCreatureFromAverage(140, 6 * 12 + 6);
            weights[CreatureConstants.Baboon][GenderConstants.Female] = GetGenderFromAverage(32);
            weights[CreatureConstants.Baboon][GenderConstants.Male] = GetGenderFromAverage(24);
            weights[CreatureConstants.Baboon][CreatureConstants.Baboon] = RollHelper.GetRollWithFewestDice(22, 82);
            weights[CreatureConstants.Badger][GenderConstants.Female] = "25";
            weights[CreatureConstants.Badger][GenderConstants.Male] = "25";
            weights[CreatureConstants.Badger][CreatureConstants.Badger] = RollHelper.GetRollWithFewestDice(25, 35);
            weights[CreatureConstants.Badger_Dire][GenderConstants.Female] = GetGenderFromAverage(500);
            weights[CreatureConstants.Badger_Dire][GenderConstants.Male] = GetGenderFromAverage(500);
            weights[CreatureConstants.Badger_Dire][CreatureConstants.Badger_Dire] = GetCreatureFromAverage(500, 6 * 12);
            weights[CreatureConstants.Balor][GenderConstants.Agender] = GetGenderFromAverage(4500);
            weights[CreatureConstants.Balor][CreatureConstants.Balor] = GetCreatureFromAverage(4500, 12 * 12);
            weights[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = GetGenderFromAverage(300);
            weights[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = GetCreatureFromAverage(300, 7 * 12);
            weights[CreatureConstants.Barghest][GenderConstants.Agender] = GetGenderFromAverage(180);
            weights[CreatureConstants.Barghest][CreatureConstants.Barghest] = GetCreatureFromAverage(180, 6 * 12);
            weights[CreatureConstants.Barghest_Greater][GenderConstants.Agender] = GetGenderFromAverage(180);
            weights[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = GetCreatureFromAverage(180, 6 * 12);
            weights[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetGenderFromAverage(1800);
            weights[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetGenderFromAverage(1800);
            weights[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetCreatureFromAverage(1800, 9 * 12);
            weights[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = GetGenderFromAverage(225);
            weights[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = GetCreatureFromAverage(225, 6 * 12);
            weights[CreatureConstants.Bebilith][GenderConstants.Agender] = GetGenderFromAverage(4500);
            weights[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = GetCreatureFromAverage(4500, 14 * 12);
            weights[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = GetGenderFromAverage(500);
            weights[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = GetCreatureFromAverage(500, 9 * 12);
            weights[CreatureConstants.Bugbear][GenderConstants.Female] = "200";
            weights[CreatureConstants.Bugbear][GenderConstants.Male] = "200";
            weights[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = RollHelper.GetRollWithFewestDice(200, 250, 350);
            weights[CreatureConstants.Cat][GenderConstants.Female] = GetGenderFromAverage(9); //Small Animal
            weights[CreatureConstants.Cat][GenderConstants.Male] = GetGenderFromAverage(11);
            weights[CreatureConstants.Cat][CreatureConstants.Cat] = GetCreatureFromAverage(10, 18);
            weights[CreatureConstants.Centaur][GenderConstants.Female] = GetGenderFromAverage(2100);
            weights[CreatureConstants.Centaur][GenderConstants.Male] = GetGenderFromAverage(2100);
            weights[CreatureConstants.Centaur][CreatureConstants.Centaur] = GetCreatureFromAverage(2100, 8 * 12);
            weights[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = GetGenderFromAverage(300);
            weights[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = GetCreatureFromAverage(300, 6 * 12);
            weights[CreatureConstants.Criosphinx][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetCreatureFromAverage(800, 120);
            weights[CreatureConstants.Dretch][GenderConstants.Agender] = GetGenderFromAverage(60);
            weights[CreatureConstants.Dretch][CreatureConstants.Dretch] = GetCreatureFromAverage(60, 4 * 12);
            weights[CreatureConstants.Dwarf_Deep][GenderConstants.Female] = "100";
            weights[CreatureConstants.Dwarf_Deep][GenderConstants.Male] = "130";
            weights[CreatureConstants.Dwarf_Deep][CreatureConstants.Dwarf_Deep] = "2d6"; //x7
            weights[CreatureConstants.Dwarf_Duergar][GenderConstants.Female] = "100";
            weights[CreatureConstants.Dwarf_Duergar][GenderConstants.Male] = "130";
            weights[CreatureConstants.Dwarf_Duergar][CreatureConstants.Dwarf_Duergar] = "2d6"; //x7
            weights[CreatureConstants.Dwarf_Hill][GenderConstants.Female] = "100";
            weights[CreatureConstants.Dwarf_Hill][GenderConstants.Male] = "130";
            weights[CreatureConstants.Dwarf_Hill][CreatureConstants.Dwarf_Hill] = "2d6"; //x7
            weights[CreatureConstants.Dwarf_Mountain][GenderConstants.Female] = "100";
            weights[CreatureConstants.Dwarf_Mountain][GenderConstants.Male] = "130";
            weights[CreatureConstants.Dwarf_Mountain][CreatureConstants.Dwarf_Mountain] = "2d6"; //x7
            weights[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(1, 4 * 12);
            weights[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(2, 8 * 12);
            weights[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = GetGenderFromAverage(4);
            weights[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(4, 16 * 12);
            weights[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(8, 32 * 12);
            weights[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = GetGenderFromAverage(10);
            weights[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(10, 36 * 12);
            weights[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = GetGenderFromAverage(12);
            weights[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(12, 40 * 12);
            weights[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = GetGenderFromAverage(80);
            weights[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(80, 4 * 12);
            weights[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = GetGenderFromAverage(750);
            weights[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(750, 8 * 12);
            weights[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = GetGenderFromAverage(6000);
            weights[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(6000, 16 * 12);
            weights[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = GetGenderFromAverage(48_000);
            weights[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(48_000, 32 * 12);
            weights[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = GetGenderFromAverage(54_000);
            weights[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(54_000, 36 * 12);
            weights[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = GetGenderFromAverage(60_000);
            weights[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(60_000, 40 * 12);
            weights[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(1, 4 * 12);
            weights[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(2, 8 * 12);
            weights[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = GetGenderFromAverage(4);
            weights[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(4, 16 * 12);
            weights[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(8, 32 * 12);
            weights[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = GetGenderFromAverage(10);
            weights[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(10, 36 * 12);
            weights[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = GetGenderFromAverage(12);
            weights[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(12, 40 * 12);
            weights[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = GetGenderFromAverage(34);
            weights[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(34, 4 * 12);
            weights[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = GetGenderFromAverage(280);
            weights[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(280, 8 * 12);
            weights[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = GetGenderFromAverage(2250);
            weights[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(2250, 16 * 12);
            weights[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = GetGenderFromAverage(18_000);
            weights[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(18_000, 32 * 12);
            weights[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = GetGenderFromAverage(21_000);
            weights[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(21_000, 36 * 12);
            weights[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = GetGenderFromAverage(24_000);
            weights[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(24_000, 40 * 12);
            weights[CreatureConstants.Elf_Aquatic][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Aquatic][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Aquatic][CreatureConstants.Elf_Aquatic] = "1d6"; //x3
            weights[CreatureConstants.Elf_Drow][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Drow][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Drow][CreatureConstants.Elf_Drow] = "1d6"; //x3
            weights[CreatureConstants.Elf_Gray][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Gray][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Gray][CreatureConstants.Elf_Gray] = "1d6"; //x3
            weights[CreatureConstants.Elf_Half][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Half][GenderConstants.Male] = "100";
            weights[CreatureConstants.Elf_Half][CreatureConstants.Elf_Half] = "2d4"; //x5
            weights[CreatureConstants.Elf_High][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_High][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_High][CreatureConstants.Elf_High] = "1d6"; //x3
            weights[CreatureConstants.Elf_Wild][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Wild][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Wild][CreatureConstants.Elf_Wild] = "1d6"; //x3
            weights[CreatureConstants.Elf_Wood][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Wood][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Wood][CreatureConstants.Elf_Wood] = "1d6"; //x3
            weights[CreatureConstants.Erinyes][GenderConstants.Agender] = GetGenderFromAverage(150);
            weights[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetCreatureFromAverage(150, 6 * 12);
            weights[CreatureConstants.Ettin][GenderConstants.Female] = "912";
            weights[CreatureConstants.Ettin][GenderConstants.Male] = "912";
            weights[CreatureConstants.Ettin][CreatureConstants.Ettin] = "18d20";
            weights[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "290"; //Huge
            weights[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "270";
            weights[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "3d10";
            weights[CreatureConstants.Giant_Hill][GenderConstants.Female] = "290"; //Huge
            weights[CreatureConstants.Giant_Hill][GenderConstants.Male] = "270";
            weights[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Cloud] = "3d10";
            weights[CreatureConstants.Glabrezu][GenderConstants.Agender] = GetGenderFromAverage(5500);
            weights[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = GetCreatureFromAverage(5500, 12 * 12);
            weights[CreatureConstants.Gnoll][GenderConstants.Female] = "250";
            weights[CreatureConstants.Gnoll][GenderConstants.Male] = "250";
            weights[CreatureConstants.Gnoll][CreatureConstants.Bugbear] = RollHelper.GetRollWithFewestDice(250, 280, 320);
            weights[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "1"; //x1
            weights[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "1"; //x1
            weights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "1";
            weights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "1";
            weights[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "1"; //x1
            weights[CreatureConstants.Goblin][GenderConstants.Female] = "25";
            weights[CreatureConstants.Goblin][GenderConstants.Male] = "30";
            weights[CreatureConstants.Goblin][CreatureConstants.Goblin] = "1"; //x1
            weights[CreatureConstants.GreenHag][GenderConstants.Female] = "85";
            weights[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "2d4";
            weights[CreatureConstants.Grig][GenderConstants.Female] = GetGenderFromAverage(1); //Tiny
            weights[CreatureConstants.Grig][GenderConstants.Male] = GetGenderFromAverage(1);
            weights[CreatureConstants.Grig][CreatureConstants.Grig] = GetCreatureFromAverage(1, 18);
            weights[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = GetGenderFromAverage(1); //Tiny
            weights[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = GetGenderFromAverage(1);
            weights[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = GetCreatureFromAverage(1, 18);
            weights[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "1"; //x1
            weights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "1"; //x1
            weights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "1"; //x1
            weights[CreatureConstants.Hellcat_Bezekira][GenderConstants.Agender] = GetGenderFromAverage(900);
            weights[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = GetCreatureFromAverage(900, 9 * 12);
            weights[CreatureConstants.Hezrou][GenderConstants.Agender] = GetGenderFromAverage(750);
            weights[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = GetCreatureFromAverage(750, 8 * 12);
            weights[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetCreatureFromAverage(800, 10 * 12);
            weights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "145";
            weights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "165";
            weights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "2d4"; //x5
            weights[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetGenderFromAverage(600);
            weights[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetCreatureFromAverage(600, 9 * 12);
            weights[CreatureConstants.Horse_Heavy][GenderConstants.Female] = "1695";
            weights[CreatureConstants.Horse_Heavy][GenderConstants.Male] = "1695";
            weights[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = RollHelper.GetRollWithFewestDice(1695, 1700, 2200);
            weights[CreatureConstants.Horse_Light][GenderConstants.Female] = "798";
            weights[CreatureConstants.Horse_Light][GenderConstants.Male] = "798";
            weights[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = RollHelper.GetRollWithFewestDice(798, 800, 1000);
            weights[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = "1695";
            weights[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = "1695";
            weights[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy] = RollHelper.GetRollWithFewestDice(1695, 1700, 2200);
            weights[CreatureConstants.Horse_Light_War][GenderConstants.Female] = "798";
            weights[CreatureConstants.Horse_Light_War][GenderConstants.Male] = "798";
            weights[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light] = RollHelper.GetRollWithFewestDice(798, 800, 1000);
            weights[CreatureConstants.Human][GenderConstants.Female] = "85";
            weights[CreatureConstants.Human][GenderConstants.Male] = "120";
            weights[CreatureConstants.Human][CreatureConstants.Human] = "2d4"; //x5
            weights[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = GetGenderFromAverage(700);
            weights[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = GetCreatureFromAverage(700, 12 * 12);
            weights[CreatureConstants.Imp][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.Imp][CreatureConstants.Imp] = GetCreatureFromAverage(8, 2 * 12);
            weights[CreatureConstants.Kobold][GenderConstants.Female] = "20";
            weights[CreatureConstants.Kobold][GenderConstants.Male] = "25";
            weights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "1"; //x1
            weights[CreatureConstants.Lemure][GenderConstants.Agender] = GetGenderFromAverage(100);
            weights[CreatureConstants.Lemure][CreatureConstants.Lemure] = GetCreatureFromAverage(100, 5 * 12);
            weights[CreatureConstants.Leonal][GenderConstants.Female] = "130";
            weights[CreatureConstants.Leonal][GenderConstants.Male] = "130";
            weights[CreatureConstants.Leonal][CreatureConstants.Leonal] = "2d4";
            weights[CreatureConstants.Lizardfolk][GenderConstants.Female] = "150";
            weights[CreatureConstants.Lizardfolk][GenderConstants.Male] = "150";
            weights[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = RollHelper.GetRollWithFewestDice(150, 200, 250);
            weights[CreatureConstants.Locathah][GenderConstants.Female] = GetGenderFromAverage(175);
            weights[CreatureConstants.Locathah][GenderConstants.Male] = GetGenderFromAverage(175);
            weights[CreatureConstants.Locathah][CreatureConstants.Locathah] = GetCreatureFromAverage(175, 5 * 12);
            weights[CreatureConstants.Marilith][GenderConstants.Female] = GetGenderFromAverage(2 * 2000);
            weights[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetCreatureFromAverage(2 * 2000, 20 * 12);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(1, 4 * 12);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Dust] = GetCreatureFromAverage(2, 4 * 12);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Earth] = GetCreatureFromAverage(80, 4 * 12);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Fire] = GetCreatureFromAverage(1, 4 * 12);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Ice] = GetCreatureFromAverage(30, 4 * 12);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetGenderFromAverage(60);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetGenderFromAverage(60);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetGenderFromAverage(60);
            weights[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Magma] = GetCreatureFromAverage(60, 4 * 12);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Ooze] = GetCreatureFromAverage(30, 4 * 12);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Salt] = GetCreatureFromAverage(80, 4 * 12);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Steam] = GetCreatureFromAverage(2, 4 * 12);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Water] = GetCreatureFromAverage(30, 4 * 12);
            weights[CreatureConstants.Merfolk][GenderConstants.Female] = "135";
            weights[CreatureConstants.Merfolk][GenderConstants.Male] = "145";
            weights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "2d4"; //x5
            weights[CreatureConstants.Minotaur][GenderConstants.Female] = GetGenderFromAverage(700);
            weights[CreatureConstants.Minotaur][GenderConstants.Male] = GetGenderFromAverage(700);
            weights[CreatureConstants.Minotaur][CreatureConstants.Locathah] = GetCreatureFromAverage(700, 8 * 12);
            weights[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = GetGenderFromAverage(8000);
            weights[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = GetCreatureFromAverage(8000, 15 * 12);
            weights[CreatureConstants.Ogre][GenderConstants.Female] = "554";
            weights[CreatureConstants.Ogre][GenderConstants.Male] = "599";
            weights[CreatureConstants.Ogre][CreatureConstants.Ogre] = "1d10";
            weights[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = "554";
            weights[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = "599";
            weights[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre_Merrow] = "1d10";
            weights[CreatureConstants.OgreMage][GenderConstants.Female] = GetGenderFromAverage(700);
            weights[CreatureConstants.OgreMage][GenderConstants.Male] = GetGenderFromAverage(700);
            weights[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = GetCreatureFromAverage(700, 10 * 12);
            weights[CreatureConstants.Orc][GenderConstants.Female] = "120";
            weights[CreatureConstants.Orc][GenderConstants.Male] = "160";
            weights[CreatureConstants.Orc][CreatureConstants.Orc_Half] = "2d6"; //x7
            weights[CreatureConstants.Orc_Half][GenderConstants.Female] = "110";
            weights[CreatureConstants.Orc_Half][GenderConstants.Male] = "150";
            weights[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d6"; //x7
            weights[CreatureConstants.PitFiend][GenderConstants.Agender] = GetGenderFromAverage(800);
            weights[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetCreatureFromAverage(800, 12 * 12);
            weights[CreatureConstants.Pixie][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Pixie][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Pixie][CreatureConstants.Pixie] = GetCreatureFromAverage(30, 21);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = GetCreatureFromAverage(30, 21);
            weights[CreatureConstants.Quasit][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.Quasit][CreatureConstants.Quasit] = GetCreatureFromAverage(8, 18);
            weights[CreatureConstants.Retriever][GenderConstants.Agender] = GetGenderFromAverage(6500);
            weights[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetCreatureFromAverage(6500, 14 * 12);
            weights[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = "5";
            weights[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = RollHelper.GetRollWithMostEvenDistribution(5, 8, 60);
            weights[CreatureConstants.Salamander_Average][GenderConstants.Agender] = "50";
            weights[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = RollHelper.GetRollWithMostEvenDistribution(50, 60, 500);
            weights[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = "400";
            weights[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = RollHelper.GetRollWithMostEvenDistribution(400, 500, 4000);
            weights[CreatureConstants.SeaHag][GenderConstants.Female] = "85";
            weights[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "2d4";
            weights[CreatureConstants.Succubus][GenderConstants.Female] = GetGenderFromAverage(125);
            weights[CreatureConstants.Succubus][GenderConstants.Male] = GetGenderFromAverage(125);
            weights[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetCreatureFromAverage(125, 6 * 12);
            weights[CreatureConstants.Tiefling][GenderConstants.Female] = "85";
            weights[CreatureConstants.Tiefling][GenderConstants.Male] = "120";
            weights[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "2d4"; //x5
            weights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Female] = GetGenderFromAverage(60);
            weights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Male] = GetGenderFromAverage(60);
            weights[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = GetCreatureFromAverage(60, 3 * 12);
            weights[CreatureConstants.Tojanida_Adult][GenderConstants.Female] = GetGenderFromAverage(220);
            weights[CreatureConstants.Tojanida_Adult][GenderConstants.Male] = GetGenderFromAverage(220);
            weights[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = GetCreatureFromAverage(220, 6 * 12);
            weights[CreatureConstants.Tojanida_Elder][GenderConstants.Female] = GetGenderFromAverage(500);
            weights[CreatureConstants.Tojanida_Elder][GenderConstants.Male] = GetGenderFromAverage(500);
            weights[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = GetCreatureFromAverage(500, 9 * 12);
            weights[CreatureConstants.Vrock][GenderConstants.Agender] = GetGenderFromAverage(500);
            weights[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetCreatureFromAverage(500, 8 * 12);
            weights[CreatureConstants.Whale_Baleen][GenderConstants.Female] = GetGenderFromAverage(44 * 2000);
            weights[CreatureConstants.Whale_Baleen][GenderConstants.Male] = GetGenderFromAverage(44 * 2000);
            weights[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = GetCreatureFromAverage(44 * 2000, 45 * 12);
            weights[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetGenderFromAverage(15 * 2000);
            weights[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetGenderFromAverage(45 * 2000);
            weights[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetCreatureFromAverage(30 * 2000, 60 * 12);
            weights[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetGenderFromAverage(4 * 2000);
            weights[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetGenderFromAverage(6 * 2000);
            weights[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Orca] = GetCreatureFromAverage(5 * 2000, 30 * 12);
            weights[CreatureConstants.Wolf][GenderConstants.Female] = "39"; //Medium Animal
            weights[CreatureConstants.Wolf][GenderConstants.Male] = "39";
            weights[CreatureConstants.Wolf][CreatureConstants.Wolf] = "2d12";
            weights[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetGenderFromAverage(120);
            weights[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetCreatureFromAverage(120, 3 * 12);
            weights[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetGenderFromAverage(600);
            weights[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetCreatureFromAverage(600, 5 * 12);
            weights[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetGenderFromAverage(9000);
            weights[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetCreatureFromAverage(9000, 8 * 12);

            return weights;
        }

        public static IEnumerable CreatureWeightsData => GetCreatureWeights().Select(t => new TestCaseData(t.Key, t.Value));

        private static string GetGenderFromAverage(double average)
        {
            var gender = average * 8 / 10;
            return gender.ToString();
        }

        private static string GetCreatureFromAverage(int weightAverage, int heightAverage)
        {
            var lowerMultiplier = heightAverage / 10;
            var upperMultiplier = heightAverage * 3 / 10;

            var lower = weightAverage / 10 / lowerMultiplier;
            var upper = weightAverage * 3 / 10 / upperMultiplier;

            var creature = RollHelper.GetRollWithFewestDice(lower, upper);

            return creature;
        }

        [TestCase(CreatureConstants.Aboleth, GenderConstants.Hermaphrodite, 6500)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Male, 750)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Female, 750)]
        [TestCase(CreatureConstants.Androsphinx, GenderConstants.Male, 800)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Male, 250)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Female, 250)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Male, 500)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Female, 500)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Male, 500)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Female, 500)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Colossal, GenderConstants.Agender, 2400)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Gargantuan, GenderConstants.Agender, 1200)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Huge, GenderConstants.Agender, 600)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Large, GenderConstants.Agender, 300)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Medium, GenderConstants.Agender, 150)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Small, GenderConstants.Agender, 75)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Tiny, GenderConstants.Agender, 37)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Colossal, GenderConstants.Agender, 32)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Gargantuan, GenderConstants.Agender, 16)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Huge, GenderConstants.Agender, 8)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Large, GenderConstants.Agender, 4)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Medium, GenderConstants.Agender, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Small, GenderConstants.Agender, 1)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Tiny, GenderConstants.Agender, 1)]
        [TestCase(CreatureConstants.Avoral, GenderConstants.Agender, 120)]
        [TestCase(CreatureConstants.Babau, GenderConstants.Agender, 140)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Male, 500)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Female, 500)]
        [TestCase(CreatureConstants.Barghest, GenderConstants.Agender, 180)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Male, 1800)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Female, 1800)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Male, 2100)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Female, 2100)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Male, 5000)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Female, 5000)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Male, 1100)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Female, 1100)]
        [TestCase(CreatureConstants.Grig, GenderConstants.Male, 1)]
        [TestCase(CreatureConstants.Grig, GenderConstants.Female, 1)]
        [TestCase(CreatureConstants.Hieracosphinx, GenderConstants.Male, 800)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Male, 175)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Female, 175)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Male, 700)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Female, 700)]
        [TestCase(CreatureConstants.Whale_Baleen, GenderConstants.Male, 44 * 2000)]
        [TestCase(CreatureConstants.Whale_Baleen, GenderConstants.Female, 44 * 2000)]
        [TestCase(CreatureConstants.Whale_Cachalot, GenderConstants.Male, 45 * 2000)]
        [TestCase(CreatureConstants.Whale_Cachalot, GenderConstants.Female, 15 * 2000)]
        [TestCase(CreatureConstants.Whale_Orca, GenderConstants.Male, 6 * 2000)]
        [TestCase(CreatureConstants.Whale_Orca, GenderConstants.Female, 4 * 2000)]
        [TestCase(CreatureConstants.Xorn_Minor, GenderConstants.Agender, 120)]
        [TestCase(CreatureConstants.Xorn_Average, GenderConstants.Agender, 600)]
        [TestCase(CreatureConstants.Xorn_Elder, GenderConstants.Agender, 9000)]
        public void RollCalculationsAreAccurate(string creature, string gender, int average)
        {
            var heights = HeightsTests.GetCreatureHeights();

            var heightMultiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var heightMultiplierAvg = dice.Roll(heights[creature][creature]).AsPotentialAverage();
            var heightMultiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();

            var weights = GetCreatureWeights();

            var baseWeight = dice.Roll(weights[creature][gender]).AsSum();
            var weightMultiplierMin = dice.Roll(weights[creature][creature]).AsPotentialMinimum();
            var weightMultiplierAvg = dice.Roll(weights[creature][creature]).AsPotentialAverage();
            var weightMultiplierMax = dice.Roll(weights[creature][creature]).AsPotentialMaximum();
            var sigma = Math.Max(1, average * 0.05);

            var theoreticalRoll = RollHelper.GetRollWithFewestDice(average * 9 / 10, average * 11 / 10);

            Assert.That(baseWeight, Is.Positive.And.EqualTo(average * 0.8).Within(sigma), $"Base Weight (80%); Theoretical: {theoreticalRoll}");
            Assert.That(heightMultiplierMin * weightMultiplierMin, Is.Positive.And.EqualTo(average * 0.1).Within(sigma), $"Min (10%); Theoretical: {theoreticalRoll}; H:{heightMultiplierMin}, W:{weightMultiplierMin}");
            Assert.That(heightMultiplierAvg * weightMultiplierAvg, Is.Positive.And.EqualTo(average * 0.2).Within(sigma), $"Average (20%); Theoretical: {theoreticalRoll}; H:{heightMultiplierAvg}, W:{weightMultiplierAvg}");
            Assert.That(heightMultiplierMax * weightMultiplierMax, Is.Positive.And.EqualTo(average * 0.3).Within(sigma), $"Max (30%); Theoretical: {theoreticalRoll}; H:{heightMultiplierMax}, W:{weightMultiplierMax}");
            Assert.That(baseWeight + heightMultiplierMin * weightMultiplierMin, Is.Positive.And.EqualTo(average * 0.9).Within(sigma), $"Min (90%); Theoretical: {theoreticalRoll}");
            Assert.That(baseWeight + heightMultiplierAvg * weightMultiplierAvg, Is.Positive.And.EqualTo(average).Within(sigma), $"Average; Theoretical: {theoreticalRoll}");
            Assert.That(baseWeight + heightMultiplierMax * weightMultiplierMax, Is.Positive.And.EqualTo(average * 1.1).Within(sigma), $"Max (110%); Theoretical: {theoreticalRoll}");
        }

        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Colossal, GenderConstants.Agender, 125 * 2000 * 3, 1000 * 2000 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan, GenderConstants.Agender, 16 * 2000 * 3, 125 * 2000 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Huge, GenderConstants.Agender, 2 * 2000 * 3, 16 * 2000 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Large, GenderConstants.Agender, 500 * 3, 2 * 2000 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Medium, GenderConstants.Agender, 60 * 3, 500 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Small, GenderConstants.Agender, 8 * 3, 60 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Tiny, GenderConstants.Agender, 3, 24)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal, GenderConstants.Agender, 125 * 2000 * 3, 1000 * 2000 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan, GenderConstants.Agender, 16 * 2000 * 3, 125 * 2000 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Huge, GenderConstants.Agender, 2 * 2000 * 3, 16 * 2000 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Large, GenderConstants.Agender, 500 * 3, 2 * 2000 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Medium, GenderConstants.Agender, 60 * 3, 500 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Small, GenderConstants.Agender, 8 * 3, 60 * 3)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny, GenderConstants.Agender, 3, 24)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Male, 300, 400)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Female, 300, 400)]
        [TestCase(CreatureConstants.Ape_Dire, GenderConstants.Male, 800, 1200)]
        [TestCase(CreatureConstants.Ape_Dire, GenderConstants.Female, 800, 1200)]
        [TestCase(CreatureConstants.Azer, GenderConstants.Agender, 180, 220)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Male, 22, 82)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Female, 22, 42)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Male, 25, 35)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Female, 25, 35)]
        [TestCase(CreatureConstants.Balor, GenderConstants.Agender, 4000, 4900)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Male, 250, 350)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Female, 250, 350)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Male, 930, 5200)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Female, 930, 5200)]
        [TestCase(CreatureConstants.Gnoll, GenderConstants.Male, 280, 320)]
        [TestCase(CreatureConstants.Gnoll, GenderConstants.Female, 280, 320)]
        [TestCase(CreatureConstants.Lizardfolk, GenderConstants.Male, 200, 250)]
        [TestCase(CreatureConstants.Lizardfolk, GenderConstants.Female, 200, 250)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Male, 600, 690)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Female, 555, 645)]
        [TestCase(CreatureConstants.Salamander_Noble, GenderConstants.Agender, 500, 4000)]
        [TestCase(CreatureConstants.Salamander_Average, GenderConstants.Agender, 60, 500)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, GenderConstants.Agender, 8, 60)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Male, 55, 85)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Female, 55, 85)]
        public void RollCalculationsAreAccurate(string creature, string gender, int min, int max)
        {
            var heights = HeightsTests.GetCreatureHeights();

            var heightMultiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var heightMultiplierAvg = dice.Roll(heights[creature][creature]).AsPotentialAverage();
            var heightMultiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();

            var weights = GetCreatureWeights();

            var baseWeight = dice.Roll(weights[creature][gender]).AsSum();
            var weightMultiplierMin = dice.Roll(weights[creature][creature]).AsPotentialMinimum();
            var weightMultiplierAvg = dice.Roll(weights[creature][creature]).AsPotentialAverage();
            var weightMultiplierMax = dice.Roll(weights[creature][creature]).AsPotentialMaximum();

            var theoreticalRoll = RollHelper.GetRollWithFewestDice(min, max);

            Assert.That(baseWeight + heightMultiplierMin * weightMultiplierMin, Is.Positive.And.EqualTo(min), $"Min; Theoretical: {theoreticalRoll}; Height: {heights[creature][creature]}");
            Assert.That(baseWeight + heightMultiplierAvg * weightMultiplierAvg, Is.Positive.And.EqualTo((min + max) / 2).Within(1), $"Average; Theoretical: {theoreticalRoll}; Height: {heights[creature][creature]}");
            Assert.That(baseWeight + heightMultiplierMax * weightMultiplierMax, Is.Positive.And.EqualTo(max), $"Max; Theoretical: {theoreticalRoll}; Height: {heights[creature][creature]}");
        }
    }
}