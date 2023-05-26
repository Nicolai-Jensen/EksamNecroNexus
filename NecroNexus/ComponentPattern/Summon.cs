using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace NecroNexus
{
    public abstract class Summon : Component
    {


        public Vector2 Position { get; set; }
        public Vector2 AttackRangeCenter => Position;

        public float AttackSpeed { get; set; }

        private float attackTimer;
        
        public float AttackRangeRadius { get; set; }




        public Summon(Vector2 position, float attackRangeRadius, float attackspeed)
        {
            
            Position = position;
            AttackRangeRadius = attackRangeRadius;
            AttackSpeed = attackspeed;
            attackTimer = 0f;
        }


       

        public virtual void ShootArrow()
        {
           
            
        }

        public override void Update()
        {


           
        }





        public virtual void Attack()
        {
          

        }

        
    }
}
