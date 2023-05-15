using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDomain;
using DatabaseRepository;

namespace TestProgramNN
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Tower> towerList;

            var mapper = new Mapper();
            var provider = new DbProvider("Data Source=NNDatabase.db; Version=3; New=False");
            var repository = new Repository(provider, mapper);

            repository.Open();
            repository.CreateDatabaseTables();
            repository.CreateTowers();
            towerList = repository.ReadAllTower();

            foreach (var item in towerList)
            {
                Console.WriteLine($"TowerID:{item.TowerID}\n TowerType: {item.TowerType}");
               
            }
            Console.ReadLine();

        }
    }
}
