using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Wave
    {
        private List<GameObject> enemySpawns = new List<GameObject>();
        private bool hasJustSpawned;
        private float spawnCooldown;
        public bool Finished { get; set; }

        public bool Activated { get; set; }

        public Wave()
        {
            Finished = false;
            Activated = false;
        }

        public void AddEnemyToWave(GameObject enemy)
        {
            enemySpawns.Add(enemy);
        }

        public void SpawnCycle()
        {
            if (Activated == true)
            {
                if (enemySpawns.Count > 0)
                {
                    if (hasJustSpawned == false)
                    {
                        LevelOne.AddObject(enemySpawns[0]);
                        enemySpawns.Remove(enemySpawns[0]);
                        hasJustSpawned = true;
                    }
                    else if (hasJustSpawned == true)
                    {
                        spawnCooldown += GameWorld.DeltaTime;
                        if (spawnCooldown >= 1f)
                        {
                            hasJustSpawned = false;
                            spawnCooldown = 0;
                        }
                    }
                }
                if (enemySpawns.Count <= 0)
                {
                    Finished = true;
                }
                
            }         
        }
    }
}
