using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NecroNexus
{
    public class SkeletonArcher : Summon
    {

      
        public List<GameObject> EnemiesInRange { get; private set; }

        public int CurrentTier { get; private set; }

        private float attackTimer;

        ArrowFactory arrowFactory = new ArrowFactory();
        private Vector2 velocity;
        private Vector2 ePos;

        public int Tier { get; set; } = 0;

        public SkeletonArcher(Vector2 position, float attackRangeRadius, float attackspeed)
            : base(position, attackRangeRadius, attackspeed)
        {
            AttackRangeRadius = attackRangeRadius;
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
             if (Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position) != null)
             {

                  if (attackTimer >= 1f && IsEnemiesInRange(Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position, 250f) == true)
                  {
                    velocity = Globals.Direction(Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position, GameObject.Transform.Position);
                    ePos = Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position;
                    Attack();
                    attackTimer = 0f;
                  }
          
             } 
             else
             {
                 return;
             }

            base.Update();
        }



        public bool IsEnemiesInRange(Vector2 position, float attackRangeRadius)
        {
            float distance = Vector2.Distance(position, GameObject.Transform.Position);

            if (distance <= attackRangeRadius)
            {
                return true;
            }
            else
                return false;
           
        }



        public override void Attack()
        {
            GameObject arrow = new GameObject();
            switch (Tier)
            {
                case (0):
                    arrow = arrowFactory.Create(ArrowTier.Tier0, GameObject.Transform.Position, ePos );
                    break;
                case (1):
                    arrow = arrowFactory.Create(ArrowTier.Tier1, GameObject.Transform.Position, ePos);
                    break;
                case (2):
                    arrow = arrowFactory.Create(ArrowTier.Tier2, GameObject.Transform.Position, ePos);
                    break;
                case (3):
                    arrow = arrowFactory.Create(ArrowTier.Tier3, GameObject.Transform.Position, ePos);
                    break;
            }

            LevelOne.AddObject(arrow);

        }


    }
}






    

