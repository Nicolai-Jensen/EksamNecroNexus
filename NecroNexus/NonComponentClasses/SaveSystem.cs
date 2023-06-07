using DatabaseRepository;
using DbDomain;
using Microsoft.Xna.Framework;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class SaveSystem
    {
        Repository repo;//Access to the Repository
        LevelOne gameLevel;//Access to the Games Level

        public LvlOneEnemies LevelEnemies { get; set; }

        private TowerSave towerSave;
        private Level level;

        public TowerSave TowerSave
        {
            get { return TowerSave; }
            set { TowerSave = value; }
        }

        public Level Level
        {
            get { return level; }
            set { level = value; }
        }

        public User thisUser { get; set; }

        public List<Level> LevelList { get; set; }
        public List<TowerSave> TowerSaveList { get; set; }

        public SaveSystem(LvlOneEnemies lvlEnemies, Repository repo, LevelOne game)
        {
            level = new Level();
            LevelEnemies = lvlEnemies;
            gameLevel = game; //Parses in game from LevelOne
            this.repo = repo; //Parses in the Repository
        }

        public void UpdateValues()
        {
            level.BaseHP = DrawingLevel.GetCriptHealth;
            level.LevelID = gameLevel.LevelID;
            level.LvlName = gameLevel.LevelName;
            Necromancer nc = (Necromancer)LevelOne.FindObjectOfType<Necromancer>();
            level.Plevel = nc.Tier;
            level.Score = 0;
            level.Souls = DrawingLevel.GetSouls;
            level.UserID = gameLevel.CurrentUser;
            level.Wave = LevelEnemies.CurrentWave;
        }

        /// <summary>
        /// A Method for saving the accumulated Data into the Database
        /// </summary>
        public void SaveGame()
        {
            lock (Globals.lockObject) //A lock to prevent crashes when the gameObjects list is accessed since it is a shared resource
            {
                //Checks if the Save Exists already then either Updates the save or Adds a new Save
                if (repo.CheckLevel(gameLevel.CurrentUser) == 1) //Update
                {
                    UpdateValues();
                    repo.UpdateLevel(Level.LevelID, level.UserID, level.Plevel, level.BaseHP, level.Score, level.Souls, level.Wave);
                    repo.DeleteTowerSave(Level.LevelID, Level.UserID);
                    foreach (var item in LevelOne.gameObjects)
                    {
                        if (item.Transform.Position.X > -5000)
                        {
                            if (item.Tag == "Archer" || item.Tag == "Brute" || item.Tag == "Hex" || item.Tag == "Demon")
                            {
                                repo.AddTowerSave(level.UserID, level.LevelID, item.Tag, item.Transform.Position.X, item.Transform.Position.Y, CheckComponents(item));
                            }
                        }
                    }
                }
                else //Add
                {
                    UpdateValues();
                    repo.AddLevel(Level.LevelID, level.UserID, Level.LvlName, level.Plevel, level.BaseHP, level.Score, level.Souls, level.Wave);
                    repo.DeleteTowerSave(Level.LevelID, Level.UserID);
                    foreach (var item in LevelOne.gameObjects)
                    {
                        if (item.Transform.Position.X > -5000)
                        {
                            if (item.Tag == "Archer" || item.Tag == "Brute" || item.Tag == "Hex" || item.Tag == "Demon")
                            {
                                repo.AddTowerSave(level.UserID, level.LevelID, item.Tag, item.Transform.Position.X, item.Transform.Position.Y, CheckComponents(item));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A Method that when called access the Database and applies its values to the variables in this class, so you can then apply them to the game
        /// </summary>
        public void LoadGame()
        {
            //TODO: Make Loadgame Work and add Checks for user 2 and 3


            //We start with if statements to check which savefile you are on 
            if (level.UserID == 1)//Checks user 1
            {
                LoadLogic();
            }
            if (level.UserID == 2)
            {
                LoadLogic();
            }
            if (level.UserID == 3)
            {
                LoadLogic();
            }
        }

        /// <summary>
        /// A Method for checking the Tier of a Tower Object, used during saving and Loading 
        /// </summary>
        /// <param name="go">Refers to the GameObject you want checked</param>
        /// <returns></returns>
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

        /// <summary>
        /// A Method for converting the strings from the database into the SummonType Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

        public void LoadLogic()
        {
            if (repo.CheckLevel(Level.LevelID) == 1)//Checks which Level the user is playing on
            {
                //Applies all of the Databases data to this classes variables
                level = repo.ReadLevel(Level.LevelID, level.UserID);

                Necromancer nec = (Necromancer)LevelOne.FindObjectOfType<Necromancer>();
                nec.Tier = level.Plevel;
                DrawingLevel.GetSouls = (int)level.Souls;
                DrawingLevel.GetCriptHealth = (int)level.BaseHP;
                LevelEnemies.CurrentWave = level.Wave;

                //Reads the TowerSave Database and tries to spawn each one
                TowerSaveList = repo.ReadTowerSaves();
                SummonFactory fac = new SummonFactory();
                foreach (var item in TowerSaveList)
                {
                    Vector2 pos = new Vector2(item.TowerPosX, item.TowerPosY);
                    GameObject go = fac.CreateFromLoad(CheckType(item.TowerType), pos);
                    if (CheckType(item.TowerType) == SummonType.SkeletonArcher)
                    {
                        SkeletonArcher summon;
                        summon = (SkeletonArcher)go.GetComponent<SkeletonArcher>();
                        summon.SetTier(item.TowerLvl);
                        go.Transform.Translate(pos);

                        foreach (var obj in LevelOne.gameObjects)
                        {
                            if (obj.Tag == "Archer")
                            {
                                SkeletonArcher archer = (SkeletonArcher)obj.GetComponent<SkeletonArcher>();
                                archer.SetTier(item.TowerLvl);
                            }
                        }
                    }
                    else if (CheckType(item.TowerType) == SummonType.SkeletonBrute)
                    {
                        SkeletonBrute summon;
                        summon = (SkeletonBrute)go.GetComponent<SkeletonBrute>();
                        summon.SetTier(item.TowerLvl);
                        go.Transform.Translate(pos);

                        foreach (var obj in LevelOne.gameObjects)
                        {
                            if (obj.Tag == "Brute")
                            {
                                SkeletonBrute brute = (SkeletonBrute)obj.GetComponent<SkeletonBrute>();
                                brute.SetTier(item.TowerLvl);
                            }
                        }
                    }
                    else if (CheckType(item.TowerType) == SummonType.Hex)
                    {
                        Hex summon;
                        summon = (Hex)go.GetComponent<Hex>();
                        summon.SetTier(item.TowerLvl);
                        go.Transform.Translate(pos);

                        foreach (var obj in LevelOne.gameObjects)
                        {
                            if (obj.Tag == "Hex")
                            {
                                Hex hex = (Hex)obj.GetComponent<Hex>();
                                hex.SetTier(item.TowerLvl);
                            }
                        }
                    }
                    else if (CheckType(item.TowerType) == SummonType.Demon)
                    {
                        Demon summon;
                        summon = (Demon)go.GetComponent<Demon>();
                        summon.SetTier(item.TowerLvl);
                        go.Transform.Translate(pos);

                        foreach (var obj in LevelOne.gameObjects)
                        {
                            if (obj.Tag == "Demon")
                            {
                                Demon demon = (Demon)obj.GetComponent<Demon>();
                                demon.SetTier(item.TowerLvl);
                            }
                        }
                    }
                    LevelOne.AddObject(go);
                }
            }
        }

    }
}
