using DbDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public interface IRepository
    {
        //Add
        void AddUser(string userName);

        void AddLevel(int levelId, int userId, string lvlName, int pLvl, float baseHp, float score, float souls, int wave);

        void AddTower(int towerId, string towerType);

        void AddTowerSave(int userId, int levelId, string towerType, float towerPosX, float towerPosY, int towerLvl);


        //Read
        User ReadUser(int userId);
        List<User> ReadAllUsers();

        Level ReadLevel(int levelId, int userId);
        List<Level> ReadAllLevels();

        Tower ReadTower(string towerType);
        List<Tower> ReadAllTower();

        TowerSave ReadTowerSave(int userId, int levelId);
        List<TowerSave> ReadTowerSaves();


        //Update Level & TowerSave (NOTE:without tower & user)
        
        void UpdateLevel(int levelId, int userId, int pLvlNew, float baseHpNew, float scoreNew, float soulsNew, int waveNew);

        void UpdateTowerSave(int userId, int levelId, string towerTypeNew, float towerPosXNew, float towerPosYNew, int towerLvlNew);

        //Delete

        void DeleteUser(int userId);
        void DeleteLevel(int levelId, int userId);
        void DeleteTowerSave(int levelId, int userId);

        void Open();

        void Close();



    }
}
