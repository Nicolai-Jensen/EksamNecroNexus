using Microsoft.Xna.Framework;
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
        public Texture2D SummonSprite { get; set; }


        public Vector2 Position { get; set; }
        public float AttackRange { get; set; }
        public float AttackSpeed { get; set; }
        public int AttackDamage { get; set; }
        public float Scale { get; set; }


        public Summon(Texture2D summonSprite, Vector2 position, float attackrange, float attackspeed, int attackDamage)
        {
            SummonSprite = summonSprite;
            Position = position;
            AttackRange = attackrange;
            AttackDamage = attackDamage;
            AttackSpeed = attackspeed;

            AttackSpeed = 1f;
            Scale = 1f;
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



        //public bool IsEnemyInRange(Enemy enemy)
        //{
        //    //float distance = Vector2.Distance(Position, enemy.Position);
        //    //return distance <= AttackRange;
        //}

        public virtual void Attack(Enemy enemy)
        {
            //Vector2 direction = enemy.Position - Position;
            //direction.Normalize();

            //Projectile projectile = new Projectile(Position, direction, AttackDamage);
            //projectile.Launch();
        }
    }
}
