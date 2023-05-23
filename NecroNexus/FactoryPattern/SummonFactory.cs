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

    public enum SummonType { SkeletonArcher, SkeletonBrute, Demon, Hex}
    public class SummonFactory : Factory
    {
        public override GameObject Create(Enum type)
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
                    go.AddComponent(new SkeletonArcher(mousePos, 100f, 3f));
                    break;

                case SummonType.SkeletonBrute:
                    sr.SetSprite("Enemies/Valkyrie/tile000", 5f, 0, 0.6f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Summons/SkeletonBrute/tile000", "Summons/SkeletonBrute/tile001", "Summons/SkeletonBrute/tile002", "Summons/SkeletonBrute/tile003" }));
                    go.AddComponent(new SkeletonArcher(mousePos, 100f, 2f));
                    break;

                case SummonType.Hex:
                    sr.SetSprite("Enemies/Valkyrie/tile000", 5f, 0, 0.6f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Enemies/Valkyrie/tile000", "Enemies/Valkyrie/tile001", "Enemies/Valkyrie/tile002", "Enemies/Valkyrie/tile003" }));
                    go.AddComponent(new SkeletonArcher(mousePos, 100f, 3f));
                    break;

                case SummonType.Demon:
                    sr.SetSprite("Enemies/Valkyrie/tile000", 3f, 0, 0.6f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Summons/Demon/tile000", "Summons/Demon/tile001", "Summons/Demon/tile002", "Summons/Demon/tile003", "Summons/Demon/tile004", "Summons/Demon/tile005", "Summons/Demon/tile005", "Summons/Demon/tile006", "Summons/Demon/tile007" }));
                    go.AddComponent(new SkeletonArcher(mousePos, 100f, 3f));
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
