using DatabaseRepository;
using DbDomain;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NecroNexus
{
    public class GameSaveLevelOne
    {
        private EnemyFactory enemies;

        Repository repo;
        LevelOne gameLevel;
        public int Level { get; set; } = 1;
        public int Save { get; set; }
        public string LevelName { get; set; } = "GraveYard";
        public int PlayerTier { get; set; }
        public float Score { get; set; }
        public float Souls { get; set; }
        public float BaseHP { get; set; }

        public int CurrentWave { get; set; }

        public List<Wave> WavesLvlOne { get; set; }

        public GameSaveLevelOne(Board board, Repository repo, LevelOne game)
        {
            gameLevel = game;
            this.repo = repo;
            CurrentWave = 0;
            WavesLvlOne = new List<Wave>();
            enemies = new EnemyFactory(board);
            ConstructWaves();
        }

        public void SaveGame()
        {
            lock (Globals.lockObject)
            {
                if (repo.CheckLevel(gameLevel.CurrentUser) == 1)
                {
                    Necromancer nc = (Necromancer)gameLevel.GetChar().GetComponent<Necromancer>();
                    repo.UpdateLevel(Level, gameLevel.CurrentUser, nc.Tier, LevelOne.GetCriptHealth, 0, LevelOne.GetSouls, CurrentWave);
                    foreach (var item in LevelOne.gameObjects)
                    {
                        if (item.Tag == "Archer" || item.Tag == "Brute" || item.Tag == "Hex" || item.Tag == "Demon")
                        {
                            repo.UpdateTowerSave(gameLevel.CurrentUser, Level, item.Tag, item.Transform.Position.X, item.Transform.Position.Y, CheckComponents(item));
                        }
                    }

                }
                else
                {
                    Necromancer nc = (Necromancer)gameLevel.GetChar().GetComponent<Necromancer>();
                    repo.AddLevel(Level, gameLevel.CurrentUser, LevelName, nc.Tier, LevelOne.GetCriptHealth, 0, LevelOne.GetSouls, CurrentWave);
                    foreach (var item in LevelOne.gameObjects)
                    {
                        if (item.Tag == "Archer" || item.Tag == "Brute" || item.Tag == "Hex" || item.Tag == "Demon")
                        {
                            repo.AddTowerSave(gameLevel.CurrentUser, Level, item.Tag, item.Transform.Position.X, item.Transform.Position.Y, CheckComponents(item));
                        }
                    }
                }
            }

        }

        public void LoadGame()
        {
            
            if (gameLevel.CurrentUser == 1)
            {
                if (repo.CheckLevel(Level) == 1)
                {
                    Level thisLevel = repo.ReadLevel(Level, gameLevel.CurrentUser);
                    PlayerTier = thisLevel.Plevel;
                    Score = thisLevel.Score;
                    Souls = thisLevel.Souls;
                    BaseHP = thisLevel.BaseHP;
                    CurrentWave = thisLevel.Wave;

                    foreach (var item in LevelOne.gameObjects)
                    {
                        if (item.Tag == "Player")
                        {
                            Necromancer nec = (Necromancer)item.GetComponent<Necromancer>();
                            nec.Tier = PlayerTier;
                        }
                    }
                    LevelOne.GetSouls = (int)Souls;
                    LevelOne.GetCriptHealth = (int)BaseHP;
                    List<TowerSave> towerList = repo.ReadTowerSaves();
                    SummonFactory fac = new SummonFactory();
                    foreach (var item in towerList)
                    {
                        GameObject go = fac.Create(CheckType(item.TowerType), new Vector2(item.TowerPosX, item.TowerPosY));
                        if (CheckType(item.TowerType) == SummonType.SkeletonArcher)
                        {
                            SkeletonArcher summon;
                            summon = (SkeletonArcher)go.GetComponent<SkeletonArcher>();
                            summon.SetTier(item.TowerLvl);
                        }
                        else if (CheckType(item.TowerType) == SummonType.SkeletonBrute)
                        {
                            SkeletonBrute summon;
                            summon = (SkeletonBrute)go.GetComponent<SkeletonBrute>();
                            summon.SetTier(item.TowerLvl);
                        }
                        else if (CheckType(item.TowerType) == SummonType.Hex)
                        {
                            Hex summon;
                            summon = (Hex)go.GetComponent<Hex>();
                            summon.SetTier(item.TowerLvl);
                        }
                        else if (CheckType(item.TowerType) == SummonType.Demon)
                        {
                            Demon summon;
                            summon = (Demon)go.GetComponent<Demon>();
                            summon.SetTier(item.TowerLvl);
                        }
                        LevelOne.AddObject(go);
                    }
                }
                else
                {

                }
                    
            }
        }

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
        
        public int CheckComponents(GameObject go)
        {
            if (go.HasComponent<SkeletonArcher>())
            {
                SkeletonArcher summon = (SkeletonArcher)go.GetComponent<SkeletonArcher>();
                return summon.Tier;
            }
            else if (go.HasComponent<SkeletonBrute>())
            {
                SkeletonBrute summon = (SkeletonBrute)go.GetComponent<SkeletonBrute>();
                return summon.Tier;
            }
            else if (go.HasComponent<Hex>())
            {
                Hex summon = (Hex)go.GetComponent<Hex>();
                return summon.Tier;
            }
            else if (go.HasComponent<Demon>())
            {
                Demon summon = (Demon)go.GetComponent<Demon>();
                return summon.Tier;
            }
            return 0;
        }

        public SummonType CheckType(string value)
        {
            if (value == "Archer")
            {
                return SummonType.SkeletonArcher;
            }
            else if (value == "Brute")
            {
                return SummonType.SkeletonBrute;
            }
            else if (value == "Hex")
            {
                return SummonType.Hex;
            }
            else if (value == "Demon")
            {
                return SummonType.Demon;
            }
            return 0;
        }

    }
}
