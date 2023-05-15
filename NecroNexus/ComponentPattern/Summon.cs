using Microsoft.Xna.Framework;
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
        public float AttackRange { get; set; }
        public int AttackDamage { get; set; }


        public Summon(Vector2 position, float attackrange, int attackDamage)
        {
            Position = position;
            AttackRange = attackrange;
            AttackDamage = attackDamage;
        }
        


        public override void Update()
        {
            //TODO: tilføj den rigtige list

            foreach (Enemy enemy in )
            {
                if (IsEnemyInRange(enemy))
                {
                   Shoot(enemy);
                }
            }  
        }



        public bool IsEnemyInRange(Enemy enemy)
        {
            float distance = Vector2.Distance(Position, enemy.Position);
            return distance <= AttackRange;
        }

        private void Shoot(Enemy enemy)
        {
            Vector2 direction = enemy.Position - Position;
            direction.Normalize();

            Projectile projectile = new Projectile(Position, direction, AttackDamage);
            projectile.Launch();
        }
    }
}
