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
        public SkeletonArcher(Vector2 position, float attackrange) : base(position, attackrange)
        {
        }


        private void LaunchArrow(Enemy enemy)
        {
            Vector2 direction = enemy.Position - Position;
            direction.Normalize();

            ArcherArrow arrow = new ArcherArrow(Position, direction, AttackDamage);
            arrow.Launch();
        }
    }
}
