using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace NecroNexus
{
    public class Audio
    {



        // Audio 

        /* HOW TO USE :)
          
           Pick the sound you need, (ex StartScreenMusic) 
           and use this following code in your class: 
         
                MediaPlayer.Play(Audio.StartScreenMusic);
                MediaPlayer.Volume = 0.2f;


           NOTE: The audio don't automatically loop.          
         */

        public static Song WaveCleared;

        public static Song NewSoul; //Enemy death

        public static Song StartScreenMusic; //Generally used when not in battle
        public static Song WaveActiveMusic; 

        public static Song SpawnTurret1; //(sorry for the long lead-in)
        public static Song SpawnTurret2;

        public static Song GameLostOverlay; // <  A scream of death leading into :
        public static Song GameLostBackgroundMusic; //  <  The 'GameLost' Music

        public static Song SubtleBlast1; //Used for turrets or the necromancer. Up to u
        public static Song SubtleBlast2; 
        public static Song SubtleBlast3;

        // Method to load audio files and assign them to the struct members
        public static void LoadAudio()
        {

            //Audio
            WaveCleared = Globals.Content.Load<Song>("NexoAudio/WaveCleared");
            NewSoul = Globals.Content.Load<Song>("NexoAudio/NewSoul");
            StartScreenMusic = Globals.Content.Load<Song>("NexoAudio/BackgroundMusic");
            WaveActiveMusic = Globals.Content.Load<Song>("NexoAudio/WaveBackground");
            SpawnTurret1 = Globals.Content.Load<Song>("NexoAudio/SpawnTurret1");
            SpawnTurret2 = Globals.Content.Load<Song>("NexoAudio/SpawnTurret2");
            GameLostOverlay = Globals.Content.Load<Song>("NexoAudio/GameLost");
            GameLostBackgroundMusic = Globals.Content.Load<Song>("NexoAudio/GameLostPart2");
            SubtleBlast1 = Globals.Content.Load<Song>("NexoAudio/TurretShotSound1");
            SubtleBlast2 = Globals.Content.Load<Song>("NexoAudio/TurretShotSound2");
            SubtleBlast3 = Globals.Content.Load<Song>("NexoAudio/TurretShotSound3");





        }

    }
}
