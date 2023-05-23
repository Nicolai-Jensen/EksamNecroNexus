using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private Enemy TargetEnemy; //Stores the current target

        private const int MaxTier = 4; //Constant that "locks" the max tier for the tower, so the max tier variable cannot be changed elsewhere.

        public SkeletonArcher(Vector2 position, float attackRangeRadius, float attackspeed, int physicalDamage, int magicDamage)
            : base(position, attackRangeRadius, attackspeed, physicalDamage, magicDamage)
        {
        }

        public int CurrentTier { get; private set; }

        

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            //foreach (Enemy enemy in enemies)
            //{
            //    if(IsEnemyInRange(enemy.Position))
            //    {
            //        Attack(enemy);
            //    }
            //}

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                UpgradeArcher();
                Console.WriteLine("Archer Level:" + CurrentTier); ;
            }

            base.Update();
        }


        public override void Attack(Enemy enemy)
        {
            Vector2 direction = enemy.Position - Position;
            direction.Normalize();

            ArcherArrow arrow = new ArcherArrow(Position, direction, PhysicalDamage);
            arrow.Launch

            base.Attack(enemy);
        }

        /// <summary>
        /// Method with a switch case that checks current tier and controls tier up
        /// </summary>
        public void UpgradeArcher()
        {
            if(CurrentTier >= MaxTier)
            {
                return;
            }

            CurrentTier++;

            switch (CurrentTier)
            {
                case 2:
                    ApplyTier2Upgrades();
                    break;
                case 3:
                    ApplyTier3Upgrades();
                    break;
                case 4:
                    ApplyTier4Upgrades();
                    break;
            }
        }
        /// <summary>
        /// Method for applying upgrades. When called the variables change. 
        /// </summary>
        private void ApplyTier2Upgrades()
        {
            AttackRangeRadius += 20f;
            AttackSpeed *= 0.3f;
            PhysicalDamage += 5;
            MagicDamage += 0;
        }
        private void ApplyTier3Upgrades()
        {
            AttackRangeRadius += 40f;
            AttackSpeed *= 0.5f;
            PhysicalDamage += 10;
            MagicDamage += 0;
        }
        private void ApplyTier4Upgrades()
        {
            AttackRangeRadius += 60f;
            AttackSpeed *= 0.8f;
            PhysicalDamage += 20;
            MagicDamage += 0;
        }

        /// <summary>
        /// Compares how far each of the enemies inside the range is along the track. the enemy furthest in the track, becomes
        /// </summary>
        private Enemy FindFurthestEnemyInRange()
        {
            //puts all enemies in range of the tower, in a list
            List<Enemy> enemiesInRange = GetEnemiesInRange();

            //checks if there are enemies in range
            if(enemiesInRange.Count == 0)
            {
                return null; //no enemies in range
            }

            Enemy furthestEnemy = enemiesInRange[0];
            float furthestDistance = CalculateDistanceAlongTrack(furthestEnemy);
            foreach(Enemy enemy in enemiesInRange)
            {
                float distance = CalculateDistanceAlongTrack(enemy);
                if(distance > furthestDistance)
                {
                    furthestEnemy = enemy;
                    furthestDistance = distance;
                }
            }
            return furthestEnemy;
        }

        private float CalculateDistanceAlongTrack(Enemy enemy)
        {

        }


    }
}
