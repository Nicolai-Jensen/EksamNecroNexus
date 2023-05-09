using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDomain
{
    public class Level
    {
        public int LevelID { get; set; }

        public int UserID { get; set; }

        public string LvlName { get; set; }
        public int Plevel { get; set; }
        public float BaseHP { get; set; }
        public float Score { get; set; }
        public float Souls { get; set; }

        public int Wave { get; set; }
    }
}
