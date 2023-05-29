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
            List<Level> towerList;

            var mapper = new Mapper();
            var provider = new DbProvider("Data Source=NNDatabase.db; Version=3; New=False");
            var repository = new Repository(provider, mapper);

            repository.Open();
            towerList = repository.ReadAllLevels();

            foreach (var item in towerList)
            {
                Console.WriteLine($"LevelID:{item.LevelID}\n UserID: {item.UserID}\n BaseHp: {item.BaseHP}");
               
            }
            Console.ReadLine();

        }
    }
}
