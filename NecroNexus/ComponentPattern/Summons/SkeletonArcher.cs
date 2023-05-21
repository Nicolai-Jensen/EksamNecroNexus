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
    public class SkeletonArcher : Summon
    {
        public SkeletonArcher(Texture2D summonSprite, Vector2 position, float attackrange, float attackspeed, int attackDamage) : base(summonSprite, position, attackrange, attackspeed, attackDamage)
        {
            
        }

        public override void Attack(Enemy enemy)
        {
            //Vector2 direction = enemy.Position - Position;
            //direction.Normalize();

            //Projectile archerArrow = new Projectile(Position, direction, AttackDamage * 2);
            //archerArrow.Launch();
        }
      
    }
}
a