using DbDomain;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseRepository
{
    public interface IMapper
    {
        List<User> MapUserFromReader(SQLiteDataReader reader);

        List<Tower> MapTowerFromReader(SQLiteDataReader reader);

        List<TowerSave> MapTowerSaveFromReader(SQLiteDataReader reader);

        List<Level> MapLevelFromReader(SQLiteDataReader reader);

    }
}
