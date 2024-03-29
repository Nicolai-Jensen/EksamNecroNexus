﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//

    //An EnumType for easily accessing the different enemies
    public enum EnemyType { Grunt, ArmoredGrunt, Knight, HorseRider, Cleric, Paladin, Valkyrie }
    public class EnemyFactory : Factory
    {
        private Board board; //A board reference

        /// <summary>
        /// The Factories Constructor takes a board it applies to the Enemies pathfinding
        /// </summary>
        /// <param name="board"></param>
        public EnemyFactory(Board board)
        {
            this.board = board;
        }

        /// <summary>
        /// The Create Method for constructing Individual Enemies
        /// </summary>
        /// <param name="type">Which Type of Enemy is made</param>
        /// <param name="pos">The Spawn Position of the Enemy</param>
        /// <returns></returns>
        public override GameObject Create(Enum type, Vector2 pos)
        {

            GameObject go = new GameObject();

            SpriteRenderer sr = (SpriteRenderer)go.AddComponent(new SpriteRenderer());
            
            Animator animator = (Animator)go.AddComponent(new Animator());

            Collider c = (Collider)go.AddComponent(new Collider());

            go.Tag = "Enemy";

            switch (type)
            {
                case EnemyType.Grunt: //Adds A Grunt Enemy and its animation while adjusts the hitbox size
                    sr.SetSprite("Enemies/Grunt/tile000", 3f, 0, 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Enemies/Grunt/tile000", "Enemies/Grunt/tile001", "Enemies/Grunt/tile002", "Enemies/Grunt/tile003" }));
                    c.WidthMultiplier = 2;
                    c.HeightMultiplier = 2;
                    c.OffsetX = -5; c.OffsetY = 0;
                    go.Tag = "Enemy";
                    go.AddComponent(new Grunt(board, pos));
                    break;
                case EnemyType.ArmoredGrunt: //Adds A ArmoredGrunt Enemy and its animation while adjusts the hitbox size
                    sr.SetSprite("Enemies/AGrunt/tile000", 3f, 0, 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Enemies/AGrunt/tile000", "Enemies/AGrunt/tile001", "Enemies/AGrunt/tile002", "Enemies/AGrunt/tile003" }));
                    c.WidthMultiplier = 2;
                    c.HeightMultiplier = 2;
                    c.OffsetX = -5; c.OffsetY = 0;
                    go.Tag = "Enemy";
                    go.AddComponent(new ArmoredGrunt(board, pos));
                    break;
                case EnemyType.Knight: //Adds A Knight Enemy and its animation while adjusts the hitbox size
                    sr.SetSprite("Enemies/Knight/tile000", 3f, 0, 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Enemies/Knight/tile000", "Enemies/Knight/tile001", "Enemies/Knight/tile002", "Enemies/Knight/tile003" }));
                    c.WidthMultiplier = 2;
                    c.HeightMultiplier = 2;
                    c.OffsetX = -5; c.OffsetY = 0;
                    go.Tag = "Enemy";
                    go.AddComponent(new Knight(board, pos));
                    break;
                case EnemyType.HorseRider: //Adds A HorseRider Enemy and its animation while adjusts the hitbox size
                    sr.SetSprite("Enemies/Rider/Gallop/Knight_gallop1", 2f, 0, 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Enemies/Rider/Gallop/Knight_gallop1", "Enemies/Rider/Gallop/Knight_gallop2", "Enemies/Rider/Gallop/Knight_gallop3", "Enemies/Rider/Gallop/Knight_gallop4", "Enemies/Rider/Gallop/Knight_gallop5" }));
                    animator.AddAnimation(BuildAnimation("Death", new string[] { "Enemies/Rider/Death/Knight_death1", "Enemies/Rider/Death/Knight_death2", "Enemies/Rider/Death/Knight_death3", "Enemies/Rider/Death/Knight_death4", "Enemies/Rider/Death/Knight_death5", "Enemies/Rider/Death/Knight_death6", "Enemies/Rider/Death/Knight_death7", "Enemies/Rider/Death/Knight_death8", "Enemies/Rider/Death/Knight_death9", "Enemies/Rider/Death/Knight_death10", "Enemies/Rider/Death/Knight_death11" }));
                    animator.AddAnimation(BuildAnimation("Walk", new string[] { "Enemies/Rider/Walk/Spearman_run1", "Enemies/Rider/Walk/Spearman_run2", "Enemies/Rider/Walk/Spearman_run3", "Enemies/Rider/Walk/Spearman_run4", "Enemies/Rider/Walk/Spearman_run5", "Enemies/Rider/Walk/Spearman_run6", "Enemies/Rider/Walk/Spearman_run7", "Enemies/Rider/Walk/Spearman_run8", "Enemies/Rider/Walk/Spearman_run9", "Enemies/Rider/Walk/Spearman_run10" }));
                    c.WidthMultiplier = 1;
                    c.HeightMultiplier = 1;
                    c.OffsetX = 0; c.OffsetY = 20;
                    go.Tag = "Enemy";
                    go.AddComponent(new HorseRider(board, pos));
                    break;
                case EnemyType.Cleric: //Adds A Cleric Enemy and its animation while adjusts the hitbox size
                    sr.SetSprite("Enemies/Cleric/tile000", 3f, 0, 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Enemies/Cleric/tile000", "Enemies/Cleric/tile001", "Enemies/Cleric/tile002", "Enemies/Cleric/tile003" }));
                    c.WidthMultiplier = 2.5f;
                    c.HeightMultiplier = 2.5f;
                    c.OffsetX = 0; c.OffsetY = 0;
                    go.Tag = "Enemy";
                    go.AddComponent(new Cleric(board, pos));
                    break;
                case EnemyType.Paladin: //Adds A Paladin Enemy and its animation while adjusts the hitbox size
                    sr.SetSprite("Enemies/Paladin/tile000", 4f, 0, 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Enemies/Paladin/tile000", "Enemies/Paladin/tile001", "Enemies/Paladin/tile002", "Enemies/Paladin/tile003" }));
                    c.WidthMultiplier = 3;
                    c.HeightMultiplier = 4;
                    c.OffsetX = -10; c.OffsetY = 0;
                    go.Tag = "Enemy";
                    go.AddComponent(new Paladin(board, pos));
                    break;
                case EnemyType.Valkyrie: //Adds A Valkyrie Enemy and its animation while adjusts the hitbox size
                    sr.SetSprite("Enemies/Valkyrie/tile000", 3f, 0, 0.5f);
                    animator.AddAnimation(BuildAnimation("Idle", new string[] { "Enemies/Valkyrie/tile000", "Enemies/Valkyrie/tile001", "Enemies/Valkyrie/tile002", "Enemies/Valkyrie/tile003" }));
                    c.WidthMultiplier = 2.5f;
                    c.HeightMultiplier = 2;
                    c.OffsetX = 0; c.OffsetY = 0;
                    go.Tag = "Enemy";
                    go.AddComponent(new Valkyrie(board, pos));
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

            Animation animation = new Animation(animationName, sprites, 9);

            return animation;
        }
    }
}
