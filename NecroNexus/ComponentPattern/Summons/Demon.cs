using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//
    public class Demon : Summon
    {
        //List of enemies inside the range of the summon
        public List<GameObject> EnemiesInRange { get; private set; }




        //Used to calculate attackspeed
        private float attackTimer;

        DemonBallFactory DemonBallFactory = new DemonBallFactory();
        private Vector2 velocity;
        private Vector2 ePos;

        /// <summary>
        /// Properties that the UI gets, so the player can see the stats of the summon.
        /// </summary>
        public float Range { get { return AttackRangeRadius; } }
        public float FireRate { get { return AttackSpeed; } }
        public float demonDamge { get; set; }
        public int Tier { get; set; } = 0;

        /// <summary>
        /// Constructor that makes a new list, that hold the enemies inside the range.
        /// Sets the summon's tier to 0, when created.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="attackRangeRadius"></param>
        /// <param name="attackspeed"></param>
        public Demon(Vector2 position, float attackRangeRadius, float attackspeed)
            : base(position, attackRangeRadius, attackspeed)
        {
            AttackRangeRadius = attackRangeRadius;
            EnemiesInRange = new List<GameObject>();
            SetTier(0);

        }
        /// <summary>
        /// Sets the Damage value, so it can be written to UI.
        /// </summary>
        /// <param name="i"></param>
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

        /// <summary>
        /// used to set the tier, for calling different tiers of the archer
        /// </summary>
        /// <param name="i"></param>
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

        /// <summary>
        /// Updated method that contains an if statement, that find the closest object via the FindClosestObject(),
        /// and then sets the individual summons attack timer.
        /// </summary>
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


        /// <summary>
        /// A bool that makes a float from the distance between the summon's position, and the target (enemy) position.
        /// Then if distance is less than attackRange, return true.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="attackRangeRadius"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Method that contains a switch case of Tier of the Demon.
        /// depending on the tier, it chooses a case. so if the Demon is tier 1, it creates a tier1 DemonBall.
        /// It creates it's attack projectile via the DemonBallFactory.Create() method.
        /// </summary>
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








