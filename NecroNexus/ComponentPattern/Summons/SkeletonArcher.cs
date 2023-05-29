using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NecroNexus
{
    public class SkeletonArcher : Summon
    {
        //List of enemies inside the range of the summon
        public List<GameObject> EnemiesInRange { get; private set; }

        //public int CurrentTier { get; private set; }

        //Used to calculate attackspeed
        private float attackTimer;

        /// <summary>
        /// Atributes for the arrows
        /// </summary>
        ArrowFactory arrowFactory = new ArrowFactory();
        private Vector2 velocity;
        private Vector2 ePos; //EnemyPosition

        /// <summary>
        /// Properties that the UI gets, so the player can see the stats of the summon.
        /// </summary>
        public float skDamge { get; set; } //SkeletonDamage
        public float Range { get { return AttackRangeRadius; } }
        public float FireRate { get{ return AttackSpeed; } }
        public int Tier { get; set; } = 0;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="attackRangeRadius"></param>
        /// <param name="attackspeed"></param>
        public SkeletonArcher(Vector2 position, float attackRangeRadius, float attackspeed)
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
                    skDamge = 1f;
                    break;
                case 1:
                    skDamge = 2f;
                    break;
                case 2:
                    skDamge = 3f;
                    break;
                case 3:
                    skDamge = 4f;
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
            GameObject.Tag = "Archer";
            base.Start();
        }


        /// <summary>
        /// Updated method that contains the if statement, that checks if the summon should attack, and the summons attack timer.
        /// </summary>
        public override void Update()
        {
            attackTimer += GameWorld.DeltaTime;
             if (Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position) != null)
             {

                  if (attackTimer >= AttackSpeed && IsEnemiesInRange(Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position, 250) == true)
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
        /// Method that contains a switch case of the different tiers of arrows.
        /// </summary>
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






    

