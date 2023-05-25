using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NecroNexus
{
    public class SkeletonArcher : Summon
    {

        private Enemy TargetEnemy; //Stores the current target

        private const int MaxTier = 4; //Constant that "locks" the max tier for the tower, so the max tier variable cannot be changed elsewhere.

        public List<GameObject> EnemiesInRange { get; private set; }

        public int CurrentTier { get; private set; }

        private float attackTimer;

        ArrowFactory arrowFactory = new ArrowFactory();

        public int Tier { get; set; } = 0;

        public SkeletonArcher(Vector2 position, float attackRangeRadius, float attackspeed ) 
            : base(position, attackRangeRadius, attackspeed)
        {
            
            EnemiesInRange = new List<GameObject>();
                
        }


        private void SetTier(int i)
        {
            this.Tier = i;
        }

        public override void Start()
        {
            GameObject.Transform.Translate(Position);
            base.Start();
        }

        public override void Update()
        {
            attackTimer += GameWorld.DeltaTime;

            if (attackTimer >= 1f)
            {
                Attack();
                attackTimer = 0f;
            }
            base.Update();
        }

        

        private bool IsEnemiesInRange(Vector2 position, float attackRangeRadius)
        {
            return ((position - this.GameObject.Transform.Position).Length() <= attackRangeRadius);
        }

       

        public override void Attack()
        {
            GameObject arrow = new GameObject();
            switch (Tier)
            {
                case (0):
                    arrow = arrowFactory.Create(ArrowTier.Tier0, GameObject.Transform.Position);
                    break;
                case (1):
                    arrow = arrowFactory.Create(ArrowTier.Tier1, GameObject.Transform.Position);
                    break;
                case (2):
                    arrow = arrowFactory.Create(ArrowTier.Tier2, GameObject.Transform.Position);
                    break;
                case (3):
                    arrow = arrowFactory.Create(ArrowTier.Tier3, GameObject.Transform.Position);
                    break;
            }

            LevelOne.AddObject(arrow);

        }

      
        

      

        


    }
}
