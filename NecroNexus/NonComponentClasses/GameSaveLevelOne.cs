using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class GameSaveLevelOne
    {
        private EnemyFactory enemies;

        public int Level { get; set; }
        public int Save { get; set; }
        public string LevelName { get; set; }
        public int PlayerTier { get; set; }
        public float Score { get; set; }
        public float Souls { get; set; }
        public float BaseHP { get; set; }

        public int CurrentWave { get; set; }

        public List<Wave> WavesLvlOne { get; set; }

        public GameSaveLevelOne(Board board)
        {
            CurrentWave = 0;
            WavesLvlOne = new List<Wave>();
            enemies = new EnemyFactory(board);
            ConstructWaves();
        }

        public void SaveGame()
        {
            LevelOne.AddObject(enemies.Create(EnemyType.Grunt, new Vector2(500, 500)));
        }

        public void LoadGame()
        {

        }

        public void ConstructWaves()
        {
            Wave wave = new Wave();

            wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
            wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));

            WavesLvlOne.Add(wave);

            wave = new Wave();

            int thing = 10;
            for (int i = 0; i < thing; i++)
            {
                wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
            }
            

            WavesLvlOne.Add(wave);

            wave = new Wave();

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
        }

        public void StartNextWave()
        {
            if (CurrentWave < WavesLvlOne.Count)
            {
                WavesLvlOne[CurrentWave].Activated = true;
            }
            
        }

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

        public bool ReturnWaveState()
        {
            if (CurrentWave < WavesLvlOne.Count)
            {
                return WavesLvlOne[CurrentWave].Activated;
            }
            return true;
        }
        
    }
}
