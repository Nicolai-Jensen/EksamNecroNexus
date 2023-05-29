using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NecroNexus
{
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
            currentSave = 900;
            thread = new Thread(ThreadMethod);
            thread.IsBackground = true;
            currentSave = this.lvlOne.CurrentWave;
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                thread.Start();
            }
        }

        public void Stop()
        {
            
            if (isRunning)
            {
                isRunning = false;
                thread.Join(); 
            }
        }

        

        private void ThreadMethod()
        {
            
            while (isRunning)
            {
                if (lvlOne.ReturnWaveState() == false)
                {
                    if(lvlOne.CurrentWave != currentSave)
                    {
                        lvlOne.SaveGame();
                        currentSave = lvlOne.CurrentWave;
                    }
                }
            }
        }
    }
}
