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
        

        public SkeletonArcher(Texture2D summonSprite, Vector2 position, float attackRange, float attackSpeed, int attackDamage)
            : base(summonSprite, position, attackRange, attackSpeed, attackDamage)
        {
            attackDamage = 10;
            attackRange = 8f;
            attackSpeed = 5f;
        }

        public override void Attack(Enemy enemy)
        {
            //Vector2 direction = enemy.Position - Position;
            //direction.Normalize();

            //Projectile archerArrow = new Projectile(Position, direction, AttackDamage * 2);
            //archerArrow.Launch();
        }

        public override void Upgrade()
        {
            if (upgradeLevel < 5)
            {
                upgradeLevel++;
                switch (upgradeLevel)
                {
                    case 2:
                        AttackDamage += 15;
                        AttackRange += 12f;
                        AttackSpeed += 9f;
                        break;

                    case 3:
                        AttackDamage += 20;
                        AttackRange += 14f;
                        AttackSpeed += 15f;
                        break;


                    case 4:
                        AttackDamage += 35;
                        AttackRange += 17f;
                        AttackSpeed += 20f;
                        break;

                }
            }
            else
            {

            }
        }
    }
}
