using Microsoft.Xna.Framework;
using NecroNexus.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class SkeletonArcher : Summon
    {
        public SkeletonArcher(Vector2 position, float attackrange, float attackSpeed, int attackDamage) : base(position, attackSpeed, attackrange, attackDamage)
        {
            AttackSpeed = 2.0f;
            
        }

        public override void Attack(Enemy enemy)
        {
            Vector2 direction = enemy.Position - Position;
            direction.Normalize();

            Projectile archerArrow = new Projectile(Position, direction, AttackDamage * 2);
            archerArrow.Launch();
        }
    }
}
