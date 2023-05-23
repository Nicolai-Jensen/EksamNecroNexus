using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class ArcherArrow : Component
    {
        private float speed;
        private Vector2 position;
        private Vector2 velocity;

        private float Speed { get; set; }

        private Damage damage;
        private int tier;
        

        public ArcherArrow(int tier)
        {
            this.tier = tier;

            switch (this.tier)
            {
                case (0):
                    ApplyTierZero();
                    break;
                case (1):
                    ApplyTier1();
                    break;
                case (2):
                    ApplyTier2();
                    break;
                case (3):
                    ApplyTier3();
                    break;

            }

        }

        

        public override void Update()
        {
            Move();
        }


        public void Move()
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
        }



       

        /// <summary>
        /// Method for applying upgrades. When called the variables change. 
        /// </summary>
        public void ApplyTierZero()
        {
            
            damage = new Damage(DamageType.Physical, 0.5f);
        }

        private void ApplyTier1()
        {
            damage = new Damage(DamageType.Physical, 1f);

        }

        private void ApplyTier2()
        {
            damage = new Damage(DamageType.Physical, 1.5f);

        }

        private void ApplyTier3()
        {
            damage = new Damage(DamageType.Physical, 2f);

        }


    }
}
