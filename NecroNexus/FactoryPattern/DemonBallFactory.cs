using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NecroNexus
{

    public enum DemonBallTier { Tier0, Tier1, Tier2, Tier3 }
    public class DemonBallFactory : Factory
    {

        //Bliver ikke brugt
        public override GameObject Create(Enum type, Vector2 pos)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Collider c = (Collider)go.AddComponent(new Collider());


            switch (type)
            {
                case ArrowTier.Tier0:
                    sr.SetSprite("", 2f, 0, 1f);
                    go.AddComponent(new DemonBall(0, pos, new Vector2(200, 200)));
                    //bliver ikke brugt
                    break;
                case ArrowTier.Tier1:
                    sr.SetSprite("", 2f, Globals.GetRotation(pos), 0.5f);
                    go.AddComponent(new DemonBall(1, pos, new Vector2(200, 200)));
                    break;
                case ArrowTier.Tier2:
                    sr.SetSprite("", 2f, Globals.GetRotation(pos), 0.5f);
                    go.AddComponent(new DemonBall(2, pos, new Vector2(200, 200)));
                    break;
                case ArrowTier.Tier3:
                    sr.SetSprite("", 2f, Globals.GetRotation(pos), 0.5f);
                    go.AddComponent(new DemonBall(2, pos, new Vector2(200, 200)));
                    break;
            }

            return go;
        }

        public GameObject Create(Enum type, Vector2 pos, Vector2 enemyPosition)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());

            Animator animator = (Animator)go.AddComponent(new Animator());

            Collider c = (Collider)go.AddComponent(new Collider());

            go.Tag = "DemonBall";


            string[] frames = new string[4];

            for (int i = 1; i < 5; i++)
            {
                frames[i - 1] = $"Projectiles/DemonBalls/tile00{i}";
            }


            switch (type)
            {
                case DemonBallTier.Tier0:
                    sr.SetSprite("Projectiles/DemonBalls/1", 1.75f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    go.AddComponent(new DemonBall(0, pos, Globals.Direction(enemyPosition, pos)));
                    animator.AddAnimation(BuildAnimation("Idle", frames));

                    break;
                case DemonBallTier.Tier1:
                    sr.SetSprite("Projectiles/DemonBalls/tile001", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    go.AddComponent(new DemonBall(1, pos, Globals.Direction(enemyPosition, pos)));
                    animator.AddAnimation(BuildAnimation("Idle", frames));

                    break;
                case DemonBallTier.Tier2:
                    sr.SetSprite("Projectiles/DemonBalls/tile002", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    go.AddComponent(new DemonBall(2, pos, Globals.Direction(enemyPosition, pos)));
                    animator.AddAnimation(BuildAnimation("Idle", frames));

                    break;
                case DemonBallTier.Tier3:
                    sr.SetSprite("Projectiles/DemonBalls/tile006", 2f, Globals.GetRotationNoMouse(enemyPosition, pos), 0.5f);
                    go.AddComponent(new DemonBall(3, pos, Globals.Direction(enemyPosition, pos)));
                    animator.AddAnimation(BuildAnimation("Idle", frames));

                    break;

            }

            return go;
        }


        private Animation BuildAnimation(string animationName, string[] spriteNames)
        {
            Texture2D[] sprites = new Texture2D[spriteNames.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = Globals.Content.Load<Texture2D>(spriteNames[i]);
            }

            Animation animation = new Animation(animationName, sprites, 9);

            return animation;
        }

    }
}
