using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class LvlOneEnemies
    {
        private EnemyFactory enemies;//Enemy Factory for spawning enemies 

        public int CurrentWave { get; set; } //A Variable to track which Wave you are currently on
        public List<Wave> WavesLvlOne { get; set; } //A List of Waves that you can fill with different waves of enemies

        public LvlOneEnemies(Board board)
        {
            CurrentWave = 0;
            WavesLvlOne = new List<Wave>();
            enemies = new EnemyFactory(board); //Instantiates the EnemyFactory and applies which Board they use
            ConstructWaves(); //Calls a method for making all of the waves
        }

        /// <summary>
        /// A Method for setting up all the waves of this Level
        /// </summary>
        public void ConstructWaves()
        {
            //Wave One
            Wave wave = new Wave();

            int thing = 10;
            for (int i = 0; i < thing; i++)
            {
                wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            }

            WavesLvlOne.Add(wave);

            //Wave Two
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));


            WavesLvlOne.Add(wave);

            //Wave Three
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            //Wave Four
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            //Wave Five
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            //Wave Six
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            //Wave Seven
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            //Wave Eight
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            //Wave Nine
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            //Wave Ten
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            //Wave Eleven
            wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));


            WavesLvlOne.Add(wave);
        }

        /// <summary>
        /// A Method to Start the next wave
        /// </summary>
        public void StartNextWave()
        {
            if (CurrentWave < WavesLvlOne.Count)//Makes sure you aren't trying to prematurely start the next wave before the previous one is finished
            {
                WavesLvlOne[CurrentWave].Activated = true;
            }

        }

        /// <summary>
        /// A Method that Checks the current wave to see if it should be spawning enemies or moving on to the next one
        /// </summary>
        public void CheckWave()
        {
            if (CurrentWave < WavesLvlOne.Count && WavesLvlOne[CurrentWave].Finished == true)
            {
                CurrentWave += 1;
            }
            if (CurrentWave < WavesLvlOne.Count)
            {
                WavesLvlOne[CurrentWave].SpawnCycle();
            }

        }

        /// <summary>
        /// Returns a bool that the current wave is active or not
        /// </summary>
        /// <returns></returns>
        public bool ReturnWaveState()
        {
            if (CurrentWave < WavesLvlOne.Count)
            {
                try
                {
                    return WavesLvlOne[CurrentWave].Activated;
                }
                catch (Exception)
                {
                    return true;
                }
            }
            return true;
        }
    }
}
