using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace NecroNexus
{
    public class Audio
    {


        // Splash_intro
        public static Song Splash_intro;


        // SoundEffects
        public static SoundEffect MergeSound;


        // Background Music
        public static Song StartScreenMusic;


        // Method to load audio files and assign them to the struct members
        public static void LoadAudio()
        {
            //// Splash
            //Splash_intro = Globals.Content.Load<Song>(" ");


            //// SoundEffects
            //MergeSound = Globals.Content.Load<SoundEffect>(" ");


            // Music
            StartScreenMusic = Globals.Content.Load<Song>("placeholdersprites/OverWorld");
        }

    }
}
