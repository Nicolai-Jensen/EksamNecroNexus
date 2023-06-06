using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NecroNexus
{
    //--------------------------Nicolai----------------------------//

    /// <summary>
    /// A AutoSave Class that has a thread working in it
    /// </summary>
    public class AutoSave
    {
        private Thread thread;
        private bool isRunning;
        SaveSystem save;
        private int currentSave;

        public AutoSave(SaveSystem save) 
        {
            isRunning = false; 
            this.save = save;
            thread = new Thread(ThreadMethod);
            thread.IsBackground = true;//Sets the Thread to be a background thread so that we can close the game without needing to close this thread first
            
        }

        /// <summary>
        /// Starts the Thread
        /// </summary>
        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                currentSave = this.save.Level.Wave;
                thread.Start();
            }
        }

        /// <summary>
        /// Stops the Thread
        /// </summary>
        public void Stop()
        {
            
            if (isRunning)
            {
                isRunning = false;
                thread.Join(); 
            }
        }

        
        /// <summary>
        /// This method contains what the thread is running while it is active
        /// </summary>
        private void ThreadMethod()
        {
            
            while (isRunning)
            {
                if (save.LevelEnemies.ReturnWaveState() == false)
                {
                    if(save.LevelEnemies.CurrentWave != currentSave)
                    {
                        save.SaveGame(); //Calls Savegame automatically when if currentwave is swapped
                        currentSave = save.LevelEnemies.CurrentWave;
                    }
                }
            }
        }
    }
}
