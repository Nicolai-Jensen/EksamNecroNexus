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
        GameSaveLevelOne lvlOne;
        private int currentSave;

        public AutoSave(GameSaveLevelOne lvlOne) 
        {
            isRunning = false; 
            this.lvlOne = lvlOne;
            thread = new Thread(ThreadMethod);
            thread.IsBackground = true;//Sets the Thread to be a background thread so that we can close the game without needing to close this thread first
            currentSave = this.lvlOne.CurrentWave;
        }

        /// <summary>
        /// Starts the Thread
        /// </summary>
        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
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
                if (lvlOne.ReturnWaveState() == false)
                {
                    if(lvlOne.CurrentWave != currentSave)
                    {
                        lvlOne.SaveGame(); //Calls Savegame automatically when if currentwave is swapped
                        currentSave = lvlOne.CurrentWave;
                    }
                }
            }
        }
    }
}
