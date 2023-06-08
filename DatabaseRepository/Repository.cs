using DbDomain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public class Repository : IRepository
    {
        /// Her bygges databasen
        /// 

        private readonly IMapper mapper;

        private readonly IDbProvider provider;

        private IDbConnection connection;

        public Repository( IDbProvider provider, IMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        public void CreateDatabaseTables()
        {
            //Drop all tables:
            //ret senere så tables ikke bliver dropped ved start, men Kun ved New Game;
            var cmd = new SQLiteCommand($"DROP TABLE Level", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"DROP TABLE Tower", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"DROP TABLE TowerSave", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand($"DROP TABLE User", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();


            //Create Tables:
            //Fejl? prøve at sætte '+'erne sammen til enkelte linjer:)

            //User:

            cmd = new SQLiteCommand($"CREATE TABLE User (" +
                $"UserID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                $"UserName STRING);",
                (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            //Level:
            cmd = new SQLiteCommand($"CREATE TABLE Level (" +
                $"LevelId INT PRIMARY KEY, " +
                $"UserId INT, " +
                $"LvlName STRING, " +
                $"Plevel INT,  " +
                $"BaseHP FLOAT, " +
                $"Score FLOAT, " +
                $"Souls FLOAT, " +
                $"Wave INT, " +
                $"FOREIGN KEY(UserId) references User(UserID));", 
                (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            //Tower:
            cmd = new SQLiteCommand($"CREATE TABLE Tower (" +
                $"TowerID INT PRIMARY KEY, " +
                $"TowerType STRING UNIQUE);", 
                (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            //Tower Save:
            cmd = new SQLiteCommand($"CREATE TABLE TowerSave (" +
                $"UserID INT, " +
                $"LevelID INT, " +
                $"TowerType STRING, " +
                $"TowerPosX FLOAT, " +
                $"TowerPosY FLOAT, " +
                $"TowerLvl INT, " +
                $"FOREIGN KEY(UserId) references User(UserId), " +
                $"FOREIGN KEY(LevelID) references Level(LevelID), " +
                $"FOREIGN KEY(TowerType) references Tower(TowerType));", 
                (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();

            
        }

        public void CreateTowers()
        {
            AddTower(1, "SkeletonArcher");
            AddTower(2, "Hex");
            AddTower(3, "SkeletonBrute");
            AddTower(4, "Demon");
        }

        public void Open()
        {
            if (connection == null)
            {
                connection = provider.CreateConnection();
            }

            connection.Open();
        }

        public void Close()
        {
            connection.Close();

        }

      

        //Adding USER
        public void AddUser(string userName)
        {
            //tilføj UserId ved New Game;
            var cmd = new SQLiteCommand($"INSERT INTO User (UserName) " +
                $"values ('{userName}')", (SQLiteConnection)connection);
            
            cmd.ExecuteNonQuery();

        }

        //Adding LEVEL
        public void AddLevel(int levelId, int userId, string lvlName, int pLvl, float baseHp, float score, float souls, int wave)
        {
            //insert
            var cmd = new SQLiteCommand($"INSERT INTO Level (LevelId, UserId, LvlName, PLevel, BaseHp, Score, Souls, Wave) " +
                $"values ( '{levelId}','{userId}','{lvlName}','{pLvl}','{baseHp}','{score}','{souls}','{wave}')", (SQLiteConnection)connection);

            cmd.ExecuteNonQuery();
        }

        //Adding TOWER
        public void AddTower(int towerId, string towerType)
        {
            var cmd = new SQLiteCommand($"INSERT INTO Tower (TowerId, TowerType) " +
                $"values ('{towerId}','{towerType}')", (SQLiteConnection)connection);

            cmd.ExecuteNonQuery();
        }

        //Adding Tower Save
        public void AddTowerSave(int userId, int levelId, string towerType, float towerPosX, float towerPosY, int towerLvl)
        {
            var cmd = new SQLiteCommand($"INSERT INTO TowerSave (UserId, LevelId, TowerType, TowerPosX, TowerPosY, TowerLvl) " +
                $"values ('{userId}','{levelId}','{towerType}','{towerPosX}','{towerPosY}','{towerLvl}')", (SQLiteConnection)connection);

            cmd.ExecuteNonQuery();
        }

        //Reading USER
        public User ReadUser(int userId)
        {
            var cmd = new SQLiteCommand($"SELECT * FROM User WHERE UserID = '{userId}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapUserFromReader(reader).First();
            return result;
        }
        public List<User> ReadAllUsers()
        {
            var cmd = new SQLiteCommand($"SELECT * FROM User", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapUserFromReader(reader);
            return result;
        }

        //Reading LEVEL
        public Level ReadLevel(int levelId, int userId)
        {
            var cmd = new SQLiteCommand($"SELECT * FROM Level WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapLevelFromReader(reader).First();
            return result;
        }
        public List<Level> ReadAllLevels()
        {
            var cmd = new SQLiteCommand($"SELECT * FROM Level", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapLevelFromReader(reader);
            return result;
        }

        //Reading TOWER
        public Tower ReadTower(string towerType)
        {
            var cmd = new SQLiteCommand($"SELECT * FROM Tower WHERE TowerType = '{towerType}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapTowerFromReader(reader).First();
            return result;

        }
        public List<Tower> ReadAllTower()
        {
            var cmd = new SQLiteCommand($"SELECT * FROM Tower", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapTowerFromReader(reader);
            return result;
        }

        //Reading TOWER SAVE
        public TowerSave ReadTowerSave(int userId, int levelId)
        {
            var cmd = new SQLiteCommand($"SELECT * FROM TowerSave WHERE UserID = '{userId}' AND LevelID = '{levelId}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapTowerSaveFromReader(reader).First();
            return result;
        }

        public List<TowerSave> ReadTowerSaves()
        {
            var cmd = new SQLiteCommand($"SELECT * FROM TowerSave", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapTowerSaveFromReader(reader);
            return result;
        }

        //Update LEVEL

        public int CheckLevel(int i)
        {
            int result = 0;
            using (var cmd = new SQLiteCommand($"SELECT EXISTS(SELECT 1 FROM Level WHERE UserId = {i})", (SQLiteConnection)connection))
            {
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }

        public void UpdateUser(int userId, string nameNew)
        {
            var cmd = new SQLiteCommand($"UPDATE User SET UserName='{nameNew}' WHERE UserID = '{userId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdateLevel(int levelId, int userId, int pLvlNew, float baseHpNew, float scoreNew, float soulsNew, int waveNew)
        {
            
            var cmd = new SQLiteCommand($"UPDATE Level SET Plevel='{pLvlNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand($"UPDATE Level SET BaseHP='{baseHpNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand($"UPDATE Level SET Score='{scoreNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand($"UPDATE Level SET Souls='{soulsNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand($"UPDATE Level SET Wave='{waveNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
           
        }

        //Update TOWER SAVE
        public void UpdateTowerSave(int userId, int levelId, string towerTypeNew, float towerPosXNew, float towerPosYNew, int towerLvlNew)
        {
            var cmd = new SQLiteCommand($"UPDATE TowerSave SET TowerType='{towerTypeNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand($"UPDATE TowerSave SET TowerPosX='{towerPosXNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand($"UPDATE TowerSave SET TowerPosY='{towerPosYNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand($"UPDATE TowerSave SET TowerLvl='{towerLvlNew}' WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }


        //Delete USER
        public void DeleteUser(int userId)
        {
            var cmd = new SQLiteCommand($"DELETE FROM User WHERE UserID = '{userId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        //Delete LEVEL
        public void DeleteLevel(int levelId, int userId)
        {
            var cmd = new SQLiteCommand($"DELETE FROM Level WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        //Delete Tower Save
        public void DeleteTowerSave(int levelId, int userId)
        {
            var cmd = new SQLiteCommand($"DELETE FROM TowerSave WHERE UserID = '{userId}' AND LevelId = '{levelId}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        
    }
}
