using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace NecroNexus
{
    /// <summary>
    /// handels the all the audio inputs though out the game.
    /// </summary>
    public static class AudioEffect
    {

        private static SoundEffect WaveCleared;

        private static SoundEffect NewSoul; //Enemy death

        private static Song StartScreenMusic; //Generally used when not in battle
        private static Song WaveActiveMusic;
        private static SoundEffect HitSound;

        private static SoundEffect SpawnTurret1; //(sorry for the long lead-in)
        private static SoundEffect SpawnTurret2;

        private static SoundEffect GameLostOverlay; // <  A scream of death leading into :
        private static Song GameLostBackgroundMusic; //  <  The 'GameLost' Music

        private static SoundEffect SubtleBlast1; //Used for turrets, the necromancer, Up to u
        private static SoundEffect SubtleBlast2; 
        private static SoundEffect SubtleBlast3;
        private static SoundEffect SubtleCast;

        private static SoundEffect Explosion1; //Lookin for a more demonic sound? try the "spawn turret" above.
        private static SoundEffect Explosion2;
        private static SoundEffect Explosion3;

        private static SoundEffect ButtonPressed;
        private static readonly float masterVolume = 0.2f;

        // Method to load audio files and assign them to the struct members
        public static void LoadAudio()
        {

            //Audio
            WaveCleared = Globals.Content.Load<SoundEffect>("NexoAudio/WaveCleared");
            NewSoul = Globals.Content.Load<SoundEffect>("NexoAudio/NewSoul");
            StartScreenMusic = Globals.Content.Load<Song>("NexoAudio/BetweenWaves2");
            WaveActiveMusic = Globals.Content.Load<Song>("NexoAudio/WaveBackground");
            SpawnTurret1 = Globals.Content.Load<SoundEffect>("NexoAudio/SpawnTurret1");
            SpawnTurret2 = Globals.Content.Load<SoundEffect>("NexoAudio/SpawnTurret2");
            GameLostOverlay = Globals.Content.Load<SoundEffect>("NexoAudio/GameLost");
            GameLostBackgroundMusic = Globals.Content.Load<Song>("NexoAudio/GameLostPart2");
            SubtleBlast1 = Globals.Content.Load<SoundEffect>("NexoAudio/TurretShotSound1");
            SubtleBlast2 = Globals.Content.Load<SoundEffect>("NexoAudio/TurretShotSound2");
            SubtleBlast3 = Globals.Content.Load<SoundEffect>("NexoAudio/TurretShotSound3");
            Explosion1 = Globals.Content.Load<SoundEffect>("NexoAudio/Explosion1");
            Explosion2 = Globals.Content.Load<SoundEffect>("NexoAudio/Explosion2");
            Explosion3 = Globals.Content.Load<SoundEffect>("NexoAudio/Explosion3");
            ButtonPressed = Globals.Content.Load<SoundEffect>("NexoAudio/ButtonPressed");
            HitSound = Globals.Content.Load<SoundEffect>("NexoAudio/HitSound");
            SubtleCast = Globals.Content.Load<SoundEffect>("NexoAudio/SubtleCast");

        }
        public static void PlayBackgroundMus()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = masterVolume;
            MediaPlayer.Play(AudioEffect.WaveActiveMusic);
        }
        public static void PlayNoneCombatMusic()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = masterVolume;
            MediaPlayer.Play(AudioEffect.StartScreenMusic);
        }
        public static void PlayerWaveCleared()
        {
            WaveCleared.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void PlayEnemyDeath()
        {
            NewSoul.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void SummonTurret1()
        {
            SpawnTurret1.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void SummonTurret2()
        {
            SpawnTurret2.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void PlayGameOverEffect()
        {
            GameLostOverlay.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void PlayGameOverSong()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = masterVolume;
            MediaPlayer.Play(AudioEffect.GameLostBackgroundMusic);
        }
        public static void PlaySubtleBlast1()
        {
            SubtleBlast1.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void PlaySubtleBlast2()
        {
            SubtleBlast2.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void PlaySubtleBlast3()
        {
            SubtleBlast3.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void PlayCast()
        {
            SubtleCast.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void PlayExplosion1()
        {
            Explosion1.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void PlayExplosion2()
        {
            Explosion2.Play(0.1f, 0.0f, 0.0f);
        }
        public static void PlayExplosion3()
        {
            Explosion3.Play(masterVolume, 0.0f, 0.0f);
        }
        public static void ButtonClickingSound()
        {

            ButtonPressed.Play(masterVolume,0.0f,0.0f);
            
        }

        public static void HitDamageSound()
        {
            HitSound.Play(0.1f, 0.0f, 0.0f);
        }

    }
}
