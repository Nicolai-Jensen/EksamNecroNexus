using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //Enumerations that determine which summontype is created.
    public enum SummonType { SkeletonArcher, SkeletonBrute, Demon, Hex}
    public class SummonFactory : Factory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public override GameObject Create(Enum type, Vector2 position)
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePos = mouseState.Position.ToVector2();

            GameObject go = new GameObject();
            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());
            Animator animator = (Animator)go.AddComponent(new Animator());

            switch (type)
            {
                case SummonType.SkeletonArcher:
                    sr.SetSprite("Summons/SkeletonArcher/tile000", 2.5f, 0, 0.6f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Summons/SkeletonArcher/tile000", "Summons/SkeletonArcher/tile001", "Summons/SkeletonArcher/tile002", "Summons/SkeletonArcher/tile003" }));
                    go.AddComponent(new SkeletonArcher(position, 250f, 1f));
                    LevelOne.AddObject(go);
                    break;

                case SummonType.SkeletonBrute:
                    sr.SetSprite("Summons/SkeletonBrute/tile000", 2f, 0, 0.6f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Summons/SkeletonBrute/tile000", "Summons/SkeletonBrute/tile001", "Summons/SkeletonBrute/tile002", "Summons/SkeletonBrute/tile003" }));
                    go.AddComponent(new SkeletonBrute(position, 50f, 2f));
                    LevelOne.AddObject(go);
                    break;

                case SummonType.Hex:
                    sr.SetSprite("Summons/Hex/tile000", 2.5f, 0, 0.6f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Summons/Hex/tile000", "Summons/Hex/tile001", "Summons/Hex/tile002", "Summons/Hex/tile003" }));
                    go.AddComponent(new Hex(position, 175f, 0.1f));
                    LevelOne.AddObject(go);
                    break;

                case SummonType.Demon:
                    sr.SetSprite("Summons/Demon/tile000", 2.5f, 0, 0.6f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Summons/Demon/tile000", "Summons/Demon/tile001", "Summons/Demon/tile002", "Summons/Demon/tile003", "Summons/Demon/tile004", "Summons/Demon/tile005", "Summons/Demon/tile005", "Summons/Demon/tile006", "Summons/Demon/tile007" }));
                    go.AddComponent(new Demon(position, 200f, 4f));
                    LevelOne.AddObject(go);
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

            Animation animation = new Animation(animationName, sprites, 5);

            return animation;
        }

    }
}
