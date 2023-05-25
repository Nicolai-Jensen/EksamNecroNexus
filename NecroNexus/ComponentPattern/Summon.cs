using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NecroNexus
{
    public abstract class Summon : Component
    {


        public Vector2 Position { get; set; }
        public Vector2 AttackRangeCenter => Position;

        public float AttackSpeed { get; set; }
        
        public float AttackRangeRadius { get; set; }

        public List<GameObject> enemiesInRange = new List<GameObject>();
        public Enemy CurrentTarget { get; set; }



        public Summon(Vector2 position, float attackRangeRadius, float attackspeed)
        {
            
            Position = position;
            AttackRangeRadius = attackRangeRadius;
           
            AttackSpeed = attackspeed;

            AttackSpeed = 1f;
        }



        //TODO: Ret når enemies er klar

        //public override void Update()
        //{
        //    //TODO: tilføj den rigtige list

        //    foreach (Enemy enemy in )
        //    {
        //        if (IsEnemyInRange(enemy))
        //        {
        //            Shoot(enemy);
        //        }
        //    }
        //}



        public void EnemyInRange(GameObject enemy)
        {
            enemiesInRange.Add(enemy);
        }


        public virtual void Attack(Enemy enemy)
        {
            if(CurrentTarget != null)
            {
                //attack
            }
        }

        
    }
}
