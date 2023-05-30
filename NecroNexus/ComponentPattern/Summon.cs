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


        public float AttackRangeRadius { get; set; }


        public Summon(Vector2 position, float attackRangeRadius, float attackspeed)
        {
            Position = position;
            AttackRangeRadius = attackRangeRadius;
            AttackSpeed = attackspeed;
        }

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
