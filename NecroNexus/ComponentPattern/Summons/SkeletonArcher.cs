using Microsoft.Xna.Framework;

namespace NecroNexus
{
    public class SkeletonArcher : Summon
    {

        private Enemy TargetEnemy; //Stores the current target

        private const int MaxTier = 4; //Constant that "locks" the max tier for the tower, so the max tier variable cannot be changed elsewhere.

        public SkeletonArcher(Vector2 position, float attackRangeRadius, float attackspeed ) 
            : base(position, attackRangeRadius, attackspeed)
        {

        }

        public int CurrentTier { get; private set; }

        

        public override void Start()
        {
            GameObject.Transform.Translate(Position);
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

            base.Update();
        }


        public override void Attack(Enemy enemy)
        {
            base.Attack(enemy);
        }

      
        

      

        


    }
}
