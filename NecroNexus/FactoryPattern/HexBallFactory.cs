using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//
    public enum HexBallTier { Tier0, Tier1, Tier2, Tier3 }
    public class HexBallFactory : Factory
    {

        /// <summary>
        /// NOT BEING USED.
        /// This method is just here to satisfy the virtual method inside the factory class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public override GameObject Create(Enum type, Vector2 pos)
        {
            GameObject go = new GameObject();
            return go;
        }


        /// <summary>
        /// The reason for this extra create method, is that I needed the enemyPosition.
        /// The method uses a switch case of enums, and then instanciates a gameobject, which is then given spriterenderer component, an animator component and a collider component.
        /// The collision gets attached, and so on for the rest of the tiers.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        /// <param name="enemyPosition"></param>
        /// <returns></returns>
        public GameObject Create(Enum type, Vector2 pos, Vector2 enemyPosition)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Animator animator = (Animator)go.AddComponent(new Animator());

            Collider c;

            HexBall h;

            go.Tag = "HexBall";

            //Loop for sprites in animation
            string[] frames = new string[30];

            for (int i = 1; i < 31; i++)
            {
                frames[i - 1] = $"Projectiles/HexBalls/{i}";
            }



            switch (type)
            {
                case HexBallTier.Tier0:
                    sr.SetSprite("Projectiles/HexBalls/1", 1.75f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    h = (HexBall)go.AddComponent(new HexBall(0, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(h);
                    animator.AddAnimation(BuildAnimation("Idle", frames));

                    break;
                case HexBallTier.Tier1:
                    sr.SetSprite("Projectiles/HexBalls/1", 1.75f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    h = (HexBall)go.AddComponent(new HexBall(1, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(h);
                    animator.AddAnimation(BuildAnimation("Idle", frames));

                    break;
                case HexBallTier.Tier2:
                    sr.SetSprite("Projectiles/HexBalls/1", 1.75f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    h = (HexBall)go.AddComponent(new HexBall(2, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(h);
                    animator.AddAnimation(BuildAnimation("Idle", frames));

                    break;
                case HexBallTier.Tier3:
                    sr.SetSprite("Projectiles/HexBalls/1", 1.75f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    c = (Collider)go.AddComponent(new Collider());
                    h = (HexBall)go.AddComponent(new HexBall(3, pos, Globals.Direction(enemyPosition, pos)));
                    c.CollisionEvent.Attach(h);
                    animator.AddAnimation(BuildAnimation("Idle", frames));

                    break;

            }

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

            Animation animation = new Animation(animationName, sprites, 20);

            return animation;
        }

    }
}
