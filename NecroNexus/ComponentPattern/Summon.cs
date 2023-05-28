using Microsoft.Xna.Framework;

namespace NecroNexus
{

    /// <summary>
    /// An abstract class that the summon inherit from.
    /// </summary>
    public abstract class Summon : Component
    {

        public Vector2 Position { get; set; }

        public float AttackSpeed { get; set; }

        //Used to calculate attackspeed.
        private float attackTimer;

        public float AttackRangeRadius { get; set; }


        public Summon(Vector2 position, float attackRangeRadius, float attackspeed)
        {
            Position = position;
            AttackRangeRadius = attackRangeRadius;
            AttackSpeed = attackspeed;
            attackTimer = 0f;
        }
        //#cb00ff

        /// <summary>
        /// Update that is inherited from component class. The inheriting summons are using this.
        /// </summary>
        public override void Update()
        {

        }

        /// <summary>
        /// Attack method that inherited classes can override.
        /// </summary>
        public virtual void Attack()
        {

        }
    }
}
