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
        
        private const int MaxTier = 4;

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
            

            base.Attack(enemy);
        }

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
      
    }
}
