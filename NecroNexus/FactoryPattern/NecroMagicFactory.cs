using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//

    //An Enum for the Type of the magic to use
    public enum MagicLevel { BaseTier, Tier1, Tier2, Tier3, Explosion }

    /// <summary>
    /// A class for Creating the magic projectile used by the Necromancer
    /// </summary>
    public class NecroMagicFactory : Factory
    {

        /// <summary>
        /// THis Method Returns a GameObject that it has Created
        /// </summary>
        /// <param name="type">Uses the Enum MagicLevel to determine how to construct the object</param>
        /// <param name="pos">A Vector2 you can use for the creation of certain objects</param>
        /// <returns></returns>
        public override GameObject Create(Enum type, Vector2 pos)
        {
            GameObject go = new GameObject();
            Collider c;
            NecromancerMagic m;
            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Animator animator = (Animator)go.AddComponent(new Animator());

            go.Tag = "NecroMagic"; //Sets a Tag

            

            string[] frames = new string[60]; //Makes a string Array with all of the frames of the fireball sprite
            string[] frames2 = new string[24]; //Makes a string Array with all the frames of the explosion

            for (int i = 1; i < 61; i++)
            {
                frames[i-1] = $"Necromancer/Magic/Fireball/{i}";
            }
            for (int i = 1; i < 25; i++)
            {
                frames2[i - 1] = $"Necromancer/Magic/Explosion/1_{i+4}";
            }

            //Depending on the Enum type Constructs a different version of Necromancer Magic
            switch (type)
            {
                case MagicLevel.BaseTier: //Adds a Sprite, Rotation, Animation, Collider, NecromancerMagic and tier
                    sr.SetSprite("Necromancer/Magic/Fireball/1", 2f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", frames));
                    c = (Collider)go.AddComponent(new Collider());
                    m = (NecromancerMagic)go.AddComponent(new NecromancerMagic(0));
                    c.CollisionEvent.Attach(m);
                    break;
                case MagicLevel.Tier1: //Adds a Sprite, Rotation, Animation, Collider, NecromancerMagic and tier
                    sr.SetSprite("Necromancer/Magic/Fireball/1", 2f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", frames));
                    c = (Collider)go.AddComponent(new Collider());
                    m = (NecromancerMagic)go.AddComponent(new NecromancerMagic(1));
                    c.CollisionEvent.Attach(m);
                    break;
                case MagicLevel.Tier2: //Adds a Sprite, Rotation, Animation, NecromancerMagic and tier
                    sr.SetSprite("Necromancer/Magic/Fireball/1", 4f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", frames));
                    go.AddComponent(new NecromancerMagic(2));
                    break;
                case MagicLevel.Tier3: //Adds a Sprite, Rotation, Animation, NecromancerMagic and tier
                    sr.SetSprite("Necromancer/Magic/Fireball/1", 4f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", frames));
                    go.AddComponent(new NecromancerMagic(3));
                    break;
                case MagicLevel.Explosion: //Adds a Sprite, animation, NecromancerMagic, Collider and adjust its position
                    sr.SetSprite("Necromancer/Magic/Explosion/1_5", 4f, 0, 0.5f);
                    animator.AddAnimation(BuildAnimation("Explode", frames2));
                    m = (NecromancerMagic)go.AddComponent(new NecromancerMagic(new Vector2(0,0), new Vector2(pos.X, pos.Y - 60)));
                    c = (Collider)go.AddComponent(new Collider());
                    c.OffsetY = +60;
                    c.CollisionEvent.Attach(m);
                    break;
            }

            return go;
        }

        /// <summary>
        /// A Different Method used for Creating the offspring fireballs of tier2 and tier3
        /// </summary>
        /// <param name="pos">The Position of the Original Fireball</param>
        /// <returns></returns>
        public GameObject CreateOffSpring(Vector2 pos)
        {
            GameObject go = new GameObject();
            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Animator animator = (Animator)go.AddComponent(new Animator());

            Collider c = (Collider)go.AddComponent(new Collider());
            string[] frames = new string[60];
            string[] frames2 = new string[24];
            for (int i = 1; i < 61; i++)
            {
                frames[i - 1] = $"Necromancer/Magic/Fireball/{i}";
            }
            for (int i = 1; i < 25; i++)
            {
                frames2[i - 1] = $"Necromancer/Magic/Explosion/1_{i + 4}";
            }

            sr.SetSprite("Necromancer/Magic/Fireball/1", 1f, Globals.GetRotation(pos), 0.5f);
            animator.AddAnimation(BuildAnimation("Idle", frames));
            return go;
        }

        /// <summary>
        /// A method used for Building our animation for when we add an animation to our animator for our GameObject
        /// </summary>
        /// <param name="animationName">Refers to the Name we give the animation</param>
        /// <param name="spriteNames">Refers to the names of the Sprites that we use in the animation</param>
        /// <returns></returns>
        private Animation BuildAnimation(string animationName, string[] spriteNames)
        {
            Texture2D[] sprites = new Texture2D[spriteNames.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = Globals.Content.Load<Texture2D>(spriteNames[i]);
            }

            Animation animation = new Animation(animationName, sprites, 18);

            return animation;
        }
    }
}
