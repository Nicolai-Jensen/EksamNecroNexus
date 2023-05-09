using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDomain
{
    public class TowerSave
    {
        public int UserID { get; set; }
        public int LevelID { get; set; }
        public string TowerType { get; set; }
        public float TowerPosX { get; set; }
        public float TowerPosY { get; set; }
        public int TowerLvl { get; set; }
    }
}
