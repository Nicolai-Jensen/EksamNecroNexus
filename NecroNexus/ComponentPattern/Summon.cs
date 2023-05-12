using Microsoft.Xna.Framework;
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
        public int Damage { get; set; }


        public Summon(Vector2 position, float attackrange)
        {
            Position = position;
            AttackRange = attackrange;
        }



       

        public override void Update(List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
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

            Projectile projectile = new Projectile, directiom, AttackDamage);
            projectile.Launch
        }
    }
}
