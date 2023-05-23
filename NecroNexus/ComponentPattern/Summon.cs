using Microsoft.Xna.Framework;

namespace NecroNexus
{
    public abstract class Summon : Component
    {


        public Vector2 Position { get; set; }
        public Vector2 AttackRangeCenter => Position;

        public float AttackSpeed { get; set; }
        
        public float AttackRangeRadius { get; set; }



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



        public bool IsEnemyInRange(Vector2 enemyPosition)
        {
            float distance = Vector2.Distance(Position, enemyPosition);

            if(distance <= AttackRangeRadius)
            {
                return true; 
            }
            return false;
        }


        public virtual void Attack(Enemy enemy)
        {
            //Vector2 direction = enemy.Position - Position;
            //direction.Normalize();

            //ArcherArrow projectile = new ArcherArrow(Position, direction, MagicDamage);
            //projectile.Launch();
        }

        /// <summary>
        /// Compares how far each of the enemies inside the range is along the track. the enemy furthest in the track, becomes the current target.
        /// </summary>
        //private Enemy FindFurthestEnemyInRange()
        //{
        //    //puts all enemies in range of the tower, in a list
        //    List<Enemy> enemiesInRange = GetEnemiesInRange();

        //    //checks if there are enemies in range
        //    if(enemiesInRange.Count == 0)
        //    {
        //        return null; //no enemies in range
        //    }

        //    Enemy furthestEnemy = enemiesInRange[0];
        //    float furthestDistance = CalculateDistanceAlongTrack(furthestEnemy);
        //    foreach(Enemy enemy in enemiesInRange)
        //    {
        //        float distance = CalculateDistanceAlongTrack(enemy);
        //        if(distance > furthestDistance)
        //        {
        //            furthestEnemy = enemy;
        //            furthestDistance = distance;
        //        }
        //    }
        //    return furthestEnemy;
        //}

        //private float CalculateDistanceAlongTrack(Enemy enemy)
        //{

        //}
    }
}
