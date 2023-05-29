using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NecroNexus
{
    public class Demon : Summon
    {
        public List<GameObject> EnemiesInRange { get; private set; }

        public int CurrentTier { get; private set; }

        private float attackTimer;

        DemonBallFactory DemonBallFactory = new DemonBallFactory();
        private Vector2 velocity;
        private Vector2 ePos;
        public float demonDamge { get; set; }
        public float Range { get { return AttackRangeRadius; } }
        public float FireRate { get { return AttackSpeed; } }

        public int Tier { get; set; } = 0;

        public Demon(Vector2 position, float attackRangeRadius, float attackspeed)
            : base(position, attackRangeRadius, attackspeed)
        {
            AttackRangeRadius = attackRangeRadius;
            EnemiesInRange = new List<GameObject>();
            SetTier(0);

        }
        public void SetValues(int i)
        {
            switch (i)
            {
                case 0:
                    demonDamge = 10f;
                    break;
                case 1:
                    demonDamge = 15f;
                    break;
                case 2:
                    demonDamge = 20f;
                    break;
                case 3:
                    demonDamge = 40f;
                    break;
            }
        }

        public void SetTier(int i)
        {
            this.Tier = i;
        }

        public override void Start()
        {
            GameObject.Transform.Translate(Position);
            GameObject.Tag = "Demon";
            base.Start();
        }

        public override void Update()
        {
            attackTimer += GameWorld.DeltaTime;
            if (Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position) != null)
            {

                if (attackTimer >= AttackSpeed && IsEnemiesInRange(Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position, 200f) == true)
                {
                    velocity = Globals.Direction(Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position, GameObject.Transform.Position);
                    ePos = Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position;
                    Attack();
                    attackTimer = 0f;
                }
            }
            SetValues(Tier);
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
            GameObject demonBall = new GameObject();
            switch (Tier)
            {
                case (0):
                    demonBall = DemonBallFactory.Create(DemonBallTier.Tier0, GameObject.Transform.Position, ePos);
                    break;
                case (1):
                    demonBall = DemonBallFactory.Create(DemonBallTier.Tier1, GameObject.Transform.Position, ePos);
                    break;
                case (2):
                    demonBall = DemonBallFactory.Create(DemonBallTier.Tier2, GameObject.Transform.Position, ePos);
                    break;
                case (3):
                    demonBall = DemonBallFactory.Create(DemonBallTier.Tier3, GameObject.Transform.Position, ePos);
                    break;
            }

            LevelOne.AddObject(demonBall);

        }


    }
}








