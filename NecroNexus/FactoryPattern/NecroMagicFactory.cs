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
    public enum MagicLevel { BaseTier, Tier1, Tier2, Tier3, Explosion }

    public class NecroMagicFactory : Factory
    {

        public override GameObject Create(Enum type, Vector2 pos)
        {
            GameObject go = new GameObject();
            Collider c;
            NecromancerMagic m;
            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Animator animator = (Animator)go.AddComponent(new Animator());

            go.Tag = "NecroMagic";

            

            string[] frames = new string[60];
            string[] frames2 = new string[24];

            for (int i = 1; i < 61; i++)
            {
                frames[i-1] = $"Necromancer/Magic/Fireball/{i}";
            }
            for (int i = 1; i < 25; i++)
            {
                frames2[i - 1] = $"Necromancer/Magic/Explosion/1_{i+4}";
            }

            switch (type)
            {
                case MagicLevel.BaseTier:
                    sr.SetSprite("Necromancer/Magic/Fireball/1", 2f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", frames));
                    c = (Collider)go.AddComponent(new Collider());
                    m = (NecromancerMagic)go.AddComponent(new NecromancerMagic(0));
                    c.CollisionEvent.Attach(m);
                    break;
                case MagicLevel.Tier1:
                    sr.SetSprite("Necromancer/Magic/Fireball/1", 2f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", frames));
                    c = (Collider)go.AddComponent(new Collider());
                    m = (NecromancerMagic)go.AddComponent(new NecromancerMagic(1));
                    c.CollisionEvent.Attach(m);
                    break;
                case MagicLevel.Tier2:
                    sr.SetSprite("Necromancer/Magic/Fireball/1", 4f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", frames));
                    go.AddComponent(new NecromancerMagic(2));
                    break;
                case MagicLevel.Tier3:
                    sr.SetSprite("Necromancer/Magic/Fireball/1", 4f, Globals.GetRotation(Globals.ReturnPlayerPosition()), 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", frames));
                    go.AddComponent(new NecromancerMagic(3));
                    break;
                case MagicLevel.Explosion:
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
