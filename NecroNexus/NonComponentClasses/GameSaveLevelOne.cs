//using DatabaseRepository;
//using DbDomain;
//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace NecroNexus
//{
//    //--------------------------Nicolai----------------------------//

//    /// <summary>
//    /// This Class is used to accumulate all of the information needed for the Level to function
//    /// This includes, Summon Data, Level Data, Board and Wave spawns, Activated bools and so on
//    /// </summary>
//    public class GameSaveLevelOne
//    {
//        //private EnemyFactory enemies;//Enemy Factory for spawning enemies 

//        Repository repo;//Access to the Repository
//        LevelOne gameLevel;//Access to the Games Level
//        public int Level { get; set; } = 1; //A Variable meant to mimic the Primary ID of the Level in the Database
//        public int Save { get; set; } //A Variable meant to mimic the Primary ID of the User in the Database
//        public string LevelName { get; set; } = "GraveYard"; //The Name of the Level
//        public int PlayerTier { get; set; } //A Variable to save and apply the Necromancers upgrade level
//        public float Score { get; set; } //A variable to track Score with and apply a loaded score
//        public float Souls { get; set; } //A Variable to save and apply the saves souls accumulation
//        public float BaseHP { get; set; } //A Variable to mimic the BaseHP of the database and apply it to the Level

//        //public int CurrentWave { get; set; } //A Variable to track which Wave you are currently on

//        //public List<Wave> WavesLvlOne { get; set; } //A List of Waves that you can fill with different waves of enemies

//        public GameSaveLevelOne(Board board, Repository repo, LevelOne game)
//        {
//            gameLevel = game; //Parses in game from LevelOne
//            this.repo = repo; //Parses in the Repository
//            //CurrentWave = 0;
//            //WavesLvlOne = new List<Wave>();
//            //enemies = new EnemyFactory(board); //Instantiates the EnemyFactory and applies which Board they use
//            /*ConstructWaves();*/ //Calls a method for making all of the waves
//        }

//        /// <summary>
//        /// A Method for saving the accumulated Data into the Database
//        /// </summary>
//        public void SaveGame()
//        {
//            lock (Globals.lockObject) //A lock to prevent crashes when the gameObjects list is accessed since it is a shared resource
//            {
//                //Checks if the Save Exists already then either Updates the save or Adds a new Save
//                if (repo.CheckLevel(gameLevel.CurrentUser) == 1) //Update
//                {
//                    Necromancer nc = (Necromancer)gameLevel.GetChar().GetComponent<Necromancer>();
//                    repo.UpdateLevel(Level, gameLevel.CurrentUser, nc.Tier, DrawingLevel.GetCriptHealth, 0, DrawingLevel.GetSouls, CurrentWave);
//                    foreach (var item in LevelOne.gameObjects)
//                    {
//                        if (item.Tag == "Archer" || item.Tag == "Brute" || item.Tag == "Hex" || item.Tag == "Demon")
//                        {
//                            repo.UpdateTowerSave(gameLevel.CurrentUser, Level, item.Tag, item.Transform.Position.X, item.Transform.Position.Y, CheckComponents(item));
//                        }
//                    }
//                }
//                else //Add
//                {
//                    Necromancer nc = (Necromancer)gameLevel.GetChar().GetComponent<Necromancer>();
//                    repo.AddLevel(Level, gameLevel.CurrentUser, LevelName, nc.Tier, DrawingLevel.GetCriptHealth, 0, DrawingLevel.GetSouls, CurrentWave);
//                    foreach (var item in LevelOne.gameObjects)
//                    {
//                        if (item.Tag == "Archer" || item.Tag == "Brute" || item.Tag == "Hex" || item.Tag == "Demon")
//                        {
//                            repo.AddTowerSave(gameLevel.CurrentUser, Level, item.Tag, item.Transform.Position.X, item.Transform.Position.Y, CheckComponents(item));
//                        }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// A Method that when called access the Database and applies its values to the variables in this class, so you can then apply them to the game
//        /// </summary>
//        public void LoadGame()
//        {
//            //TODO: Make Loadgame Work and add Checks for user 2 and 3


//            //We start with if statements to check which savefile you are on 
//            if (gameLevel.CurrentUser == 1)//Checks user 1
//            {
//                if (repo.CheckLevel(Level) == 1)//Checks which Level the user is playing on
//                {
//                    //Applies all of the Databases data to this classes variables
//                    Level thisLevel = repo.ReadLevel(Level, gameLevel.CurrentUser);
//                    PlayerTier = thisLevel.Plevel;
//                    Score = thisLevel.Score;
//                    Souls = thisLevel.Souls;
//                    BaseHP = thisLevel.BaseHP;
//                    CurrentWave = thisLevel.Wave;

//                    foreach (var item in LevelOne.gameObjects)
//                    {
//                        if (item.Tag == "Player")
//                        {
//                            Necromancer nec = (Necromancer)item.GetComponent<Necromancer>();
//                            nec.Tier = PlayerTier;
//                        }
//                    }
//                    DrawingLevel.GetSouls = (int)Souls;
//                    DrawingLevel.GetCriptHealth = (int)BaseHP;

//                    //Reads the TowerSave Database and tries to spawn each one
//                    List<TowerSave> towerList = repo.ReadTowerSaves();
//                    SummonFactory fac = new SummonFactory();
//                    foreach (var item in towerList)
//                    {
//                        GameObject go = fac.Create(CheckType(item.TowerType), new Vector2(item.TowerPosX, item.TowerPosY));
//                        if (CheckType(item.TowerType) == SummonType.SkeletonArcher)
//                        {
//                            SkeletonArcher summon;
//                            summon = (SkeletonArcher)go.GetComponent<SkeletonArcher>();
//                            summon.SetTier(item.TowerLvl);
//                        }
//                        else if (CheckType(item.TowerType) == SummonType.SkeletonBrute)
//                        {
//                            SkeletonBrute summon;
//                            summon = (SkeletonBrute)go.GetComponent<SkeletonBrute>();
//                            summon.SetTier(item.TowerLvl);
//                        }
//                        else if (CheckType(item.TowerType) == SummonType.Hex)
//                        {
//                            Hex summon;
//                            summon = (Hex)go.GetComponent<Hex>();
//                            summon.SetTier(item.TowerLvl);
//                        }
//                        else if (CheckType(item.TowerType) == SummonType.Demon)
//                        {
//                            Demon summon;
//                            summon = (Demon)go.GetComponent<Demon>();
//                            summon.SetTier(item.TowerLvl);
//                        }
//                        LevelOne.AddObject(go);
//                    }
//                }
//                else
//                {

//                }
                    
//            }
//        }

//        /// <summary>
//        /// A Method for setting up all the waves of this Level
//        /// </summary>
//        //public void ConstructWaves()
//        //{
//        //    //Wave One
//        //    Wave wave = new Wave();

//        //    int thing = 10;
//        //    for (int i = 0; i < thing; i++)
//        //    {
//        //        wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    }

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Two
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));


//        //    WavesLvlOne.Add(wave);

//        //    //Wave Three
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Four
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Five
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Six
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.ArmoredGrunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Seven
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Grunt, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Eight
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Knight, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Nine
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Ten
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.HorseRider, new Vector2(1725, -10)));

//        //    WavesLvlOne.Add(wave);

//        //    //Wave Eleven
//        //    wave = new Wave();

//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Cleric, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Valkyrie, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
//        //    wave.AddEnemyToWave(enemies.Create(EnemyType.Paladin, new Vector2(1725, -10)));
 

//        //    WavesLvlOne.Add(wave);
//        //}

//        /// <summary>
//        /// A Method to Start the next wave
//        /// </summary>
//        //public void StartNextWave()
//        //{
//        //    if (CurrentWave < WavesLvlOne.Count)//Makes sure you aren't trying to prematurely start the next wave before the previous one is finished
//        //    {
//        //        WavesLvlOne[CurrentWave].Activated = true;
//        //    }
            
//        //}

//        /// <summary>
//        /// A Method that Checks the current wave to see if it should be spawning enemies or moving on to the next one
//        /// </summary>
//        //public void CheckWave()
//        //{
//        //    if (CurrentWave < WavesLvlOne.Count && WavesLvlOne[CurrentWave].Finished == true)
//        //    {
//        //        CurrentWave += 1;
//        //    }
//        //    if (CurrentWave < WavesLvlOne.Count)
//        //    {
//        //        WavesLvlOne[CurrentWave].SpawnCycle();
//        //    }
            
//        //}

//        /// <summary>
//        /// Returns a bool that the current wave is active or not
//        /// </summary>
//        /// <returns></returns>
//        //public bool ReturnWaveState()
//        //{
//        //    if (CurrentWave < WavesLvlOne.Count)
//        //    {
//        //        try
//        //        {
//        //            return WavesLvlOne[CurrentWave].Activated;
//        //        }
//        //        catch (Exception)
//        //        {
//        //            return true;
//        //        }   
//        //    }
//        //    return true;
//        //}
        
//        /// <summary>
//        /// A Method for checking the Tier of a Tower Object, used during saving and Loading 
//        /// </summary>
//        /// <param name="go">Refers to the GameObject you want checked</param>
//        /// <returns></returns>
//        public int CheckComponents(GameObject go)
//        {
//            if (go.HasComponent<SkeletonArcher>())
//            {
//                SkeletonArcher summon = (SkeletonArcher)go.GetComponent<SkeletonArcher>();
//                return summon.Tier;
//            }
//            else if (go.HasComponent<SkeletonBrute>())
//            {
//                SkeletonBrute summon = (SkeletonBrute)go.GetComponent<SkeletonBrute>();
//                return summon.Tier;
//            }
//            else if (go.HasComponent<Hex>())
//            {
//                Hex summon = (Hex)go.GetComponent<Hex>();
//                return summon.Tier;
//            }
//            else if (go.HasComponent<Demon>())
//            {
//                Demon summon = (Demon)go.GetComponent<Demon>();
//                return summon.Tier;
//            }
//            return 0;
//        }

//        /// <summary>
//        /// A Method for converting the strings from the database into the SummonType Enums
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public SummonType CheckType(string value)
//        {
//            if (value == "Archer")
//            {
//                return SummonType.SkeletonArcher;
//            }
//            else if (value == "Brute")
//            {
//                return SummonType.SkeletonBrute;
//            }
//            else if (value == "Hex")
//            {
//                return SummonType.Hex;
//            }
//            else if (value == "Demon")
//            {
//                return SummonType.Demon;
//            }
//            return 0;
//        }

//    }
//}
