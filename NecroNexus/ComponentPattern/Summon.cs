﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NecroNexus.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public abstract class Summon : Component
    {


        public Vector2 Position { get; set; }
        public float AttackRangeCenter { get; set; }
        public float AttackSpeed { get; set; }
        public int MagicAttackDamage { get; set; }
        public int PhysicalAttackDamage { get; set; }
        public float Scale { get; set; }
        public float RangeRadius { get; set; }


        public Summon(Vector2 position, float attackrange, float attackspeed, int physicalAttackDamage, int magicAttackDamage)
        {
            
            Position = position;
            AttackRangeCenter = attackrange;
            PhysicalAttackDamage = physicalAttackDamage;
            AttackSpeed = attackspeed;

            AttackSpeed = 1f;
            Scale = 1f;
        }

        public override void Start()
        {
            //Adds SpriteRenderer Component so we get access to drawing sprites
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite("placeholdersprites/EldenRingIcon", 0.2f, 0, 1);



            GameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);



        }

        //TODO: Ret når enemies er klar

        //public override void Update()
        //{
        //    //TODO: tilføj den rigtige list

        //    foreach (Enemy enemy in )
        //    {
        //        if (IsEnemyInRange(enemy))
        //        {
        //            Shoot(enemy);
        //        }
        //    }
        //}



        public bool IsEnemyInRange(Vector2 enemyPosition)
        {
            float distance = Vector2.Distance(Position, enemyPosition);

            if(distance <= RangeRadius)
            {
                return true;
            }
            return false;
        }


        public virtual void Attack(Enemy enemy)
        {
            //Vector2 direction = enemy.Position - Position;
            //direction.Normalize();

            //ArcherArrow projectile = new ArcherArrow(Position, direction, MagicAttackDamage);
            //projectile.Launch();
        }
    }
}
