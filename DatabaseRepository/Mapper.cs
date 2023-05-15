using DbDomain;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public class Mapper : IMapper
    {
        public List<Level> MapLevelFromReader(SQLiteDataReader reader)
        {
            var result = new List<Level>();
            while (reader.Read())
            {
                var levelId = reader.GetInt32(0);
                var userId = reader.GetInt32(1);
                var lvlName = reader.GetString(2);
                var pLvl = reader.GetInt32(3);
                var baseHp = reader.GetFloat(4);
                var score = reader.GetFloat(5);
                var souls = reader.GetFloat(6);
                var wave = reader.GetInt32(7);

                result.Add(new Level()
                {   LevelID = levelId, 
                    UserID = userId, 
                    LvlName = lvlName, 
                    Plevel = pLvl, 
                    BaseHP = baseHp, 
                    Score = score, 
                    Souls = souls, 
                    Wave = wave 
                });
            }
            return result;

        }

        public List<Tower> MapTowerFromReader(SQLiteDataReader reader)
        {
            var result = new List<Tower>();
            while (reader.Read())
            {
                var towerId = reader.GetInt32(0);
                var towerType = reader.GetString(1);

                result.Add(new Tower()
                {   TowerID = towerId, 
                    TowerType = towerType 
                });
            }
            return result;

        }

        public List<TowerSave> MapTowerSaveFromReader(SQLiteDataReader reader)
        {
            var result = new List<TowerSave>();
            while (reader.Read())
            {
                var userId = reader.GetInt32(0);
                var levelId = reader.GetInt32(1);
                var towerType = reader.GetString(2);
                var towerPosX = reader.GetFloat(3);
                var towerPosY = reader.GetFloat(4);
                var towerLvl = reader.GetInt32(5);

                result.Add(new TowerSave() 
                {   UserID = userId, 
                    LevelID = levelId, 
                    TowerType = towerType, 
                    TowerPosX = towerPosX, 
                    TowerPosY = towerPosY, 
                    TowerLvl = towerLvl 
                });
            }
            return result;


        }

        public List<User> MapUserFromReader(SQLiteDataReader reader)
        {
            var result = new List<User>();
            while (reader.Read())
            {
                var userId = reader.GetInt32(0);
                var userName = reader.GetString(1);

                result.Add(new User()
                {
                    UserID = userId,
                    UserName = userName 
                });
            }
            return result;
        }
    }
}
