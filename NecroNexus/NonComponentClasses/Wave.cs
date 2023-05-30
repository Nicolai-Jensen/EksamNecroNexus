using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai----------------------------//

    /// <summary>
    /// The Wave class is used as a List of GameObjects that has access to bools and Methods that let you check or activate functionality.
    /// This is used to have a collection of enemies you can then spawn from
    /// </summary>
    public class Wave
    {
        private List<GameObject> enemySpawns = new List<GameObject>(); //A List of Objects

        private bool hasJustSpawned; //A bool we use for activating and deactivating a timer and spawn
        private float spawnCooldown; //A float value that determines the cooldown in SpawnCycle

        //A bool to check if the Waves GameObject list has become emptied out
        public bool Finished { get; set; }

        //A bool to check if the wave is currently in effect
        public bool Activated { get; set; }

        public Wave()
        {
            Finished = false;
            Activated = false;
        }

        /// <summary>
        /// This Method adds an Object to the Waves list
        /// </summary>
        /// <param name="enemy">The Object you are adding to the list</param>
        public void AddEnemyToWave(GameObject enemy)
        {
            enemySpawns.Add(enemy);
        }

        /// <summary>
        /// This Method when called spawns each object in the Waves List overtime
        /// </summary>
        public void SpawnCycle()
        {
            if (Activated == true)//Checks if it is allowed to start spawning
            {
                if (enemySpawns.Count > 0)//Makes sure the list is not empty
                {
                    if (hasJustSpawned == false)//Makes sure the Cycle is not on cooldown
                    {
                        LevelOne.AddObject(enemySpawns[0]);//Spawns the first Object in the list
                        enemySpawns.Remove(enemySpawns[0]);//Removes the first Object from the list
                        hasJustSpawned = true;//Puts the spawn on cooldown
                    }
                    else if (hasJustSpawned == true)//Checks if the spawn is on cooldown
                    {
                        spawnCooldown += GameWorld.DeltaTime;//Counts up
                        if (spawnCooldown >= 1f)//when the timer reaches one second
                        {
                            hasJustSpawned = false;//Remove the cooldown
                            spawnCooldown = 0;//Reset the Timer
                        }
                    }
                }
                if (enemySpawns.Count <= 0)//Checks if the Waves list is empty
                {
                    //Uses a method to return a bool value, then marks itself as finished
                   if (CheckIfEnemyExist() == false)
                   {
                        Finished = true;
                   }
                }
                
            }         
        }

        /// <summary>
        /// We use this method to make sure the Objects list in the Level is absent of any enemies or has enemies
        /// </summary>
        /// <returns></returns>
        public bool CheckIfEnemyExist()
        {
            foreach (var item in LevelOne.gameObjects)
            {
                if(item.Tag == "Enemy")
                {
                    return true;
                }
            }
            return false;
        }

    }
}
