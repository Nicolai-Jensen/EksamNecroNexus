using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace NecroNexus
{
    public class Audio
    {



        // Audio 

        /* HOW TO USE :)
          
           Pick the sound you need, (ex ButtonPressed) 
           and use this following code in your class:

         
                MediaPlayer.Play(Audio.ButtonPressed);
                MediaPlayer.Volume = 0.2f;


           NOTE: The audio don't automatically loop. 

           - B

         */

        public static Song WaveCleared;

        public static Song NewSoul; //Enemy death

        public static Song StartScreenMusic; //Generally used when not in battle
        public static Song WaveActiveMusic; 

        public static Song SpawnTurret1; //(sorry for the long lead-in)
        public static Song SpawnTurret2;

        public static Song GameLostOverlay; // <  A scream of death leading into :
        public static Song GameLostBackgroundMusic; //  <  The 'GameLost' Music

        public static Song SubtleBlast1; //Used for turrets, the necromancer, Up to u
        public static Song SubtleBlast2; 
        public static Song SubtleBlast3;

        public static Song Explosion1; //Lookin for a more demonic sound? try the "spawn turret" above.
        public static Song Explosion2;
        public static Song Explosion3;

        public static Song ButtonPressed;

        // Method to load audio files and assign them to the struct members
        public static void LoadAudio()
        {

            //Audio
            WaveCleared = Globals.Content.Load<Song>("NexoAudio/WaveCleared");
            NewSoul = Globals.Content.Load<Song>("NexoAudio/NewSoul");
            StartScreenMusic = Globals.Content.Load<Song>("NexoAudio/BetweenWaves2");
            WaveActiveMusic = Globals.Content.Load<Song>("NexoAudio/WaveBackground");
            SpawnTurret1 = Globals.Content.Load<Song>("NexoAudio/SpawnTurret1");
            SpawnTurret2 = Globals.Content.Load<Song>("NexoAudio/SpawnTurret2");
            GameLostOverlay = Globals.Content.Load<Song>("NexoAudio/GameLost");
            GameLostBackgroundMusic = Globals.Content.Load<Song>("NexoAudio/GameLostPart2");
            SubtleBlast1 = Globals.Content.Load<Song>("NexoAudio/TurretShotSound1");
            SubtleBlast2 = Globals.Content.Load<Song>("NexoAudio/TurretShotSound2");
            SubtleBlast3 = Globals.Content.Load<Song>("NexoAudio/TurretShotSound3");
            Explosion1 = Globals.Content.Load<Song>("NexoAudio/Explosion1");
            Explosion2 = Globals.Content.Load<Song>("NexoAudio/Explosion2");
            Explosion3 = Globals.Content.Load<Song>("NexoAudio/Explosion3");
            ButtonPressed = Globals.Content.Load<Song>("NexoAudio/ButtonPressed");




        }

    }
}
