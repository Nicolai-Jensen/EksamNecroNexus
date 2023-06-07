using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//
    public class SkeletonBrute : Summon
    {
        //List of enemies inside the range of the summon
        public List<GameObject> EnemiesInRange { get; private set; }


        //Used to calculate attackspeed
        private float attackTimer;


        BruteAttackFactory bruteAttackFactory = new BruteAttackFactory();

        private Vector2 velocity;
        private Vector2 ePos; //EnemyPosition

        /// <summary>
        /// Properties that the UI gets, so the player can see the stats of the summon.
        /// </summary>
        public float BruteDamge { get; set; } //skDamage = SkeletonDamage
        public float Range { get { return AttackRangeRadius; } }
        public float FireRate { get { return AttackSpeed; } }
        public int Tier { get; set; } = 0;

        public SkeletonBrute(Vector2 position, float attackRangeRadius, float attackspeed)
            : base(position, attackRangeRadius, attackspeed)
        {
            AttackRangeRadius = attackRangeRadius;
            EnemiesInRange = new List<GameObject>();
            SetTier(0);

        }
        public override void Start()
        {
            GameObject.Transform.Translate(Position);
            GameObject.Tag = "Brute";

            base.Start();
        }

        public void SetValues(int i)
        {
            switch (i)
            {
                case 0:
                    BruteDamge = 1f;
                    break;
                case 1:
                    BruteDamge = 2f;
                    break;
                case 2:
                    BruteDamge = 3f;
                    break;
                case 3:
                    BruteDamge = 4f;
                    break;
            }
        }
        public void SetTier(int i)
        {
            this.Tier = i;
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

                if (attackTimer >= AttackSpeed && IsEnemiesInRange(Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position, AttackRangeRadius) == true)
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
        /// Method that contains a switch case of Tier of the skeleton archer.
        /// depending on the tier, it chooses a case. so if the skeletonarcher is tier 1, it creates a tier1 arrow.
        /// It creates it's attack projectile via the arrowFactory.Create() method.
        /// </summary>
        public override void Attack()
        {
            GameObject bruteAttack = new GameObject();
            switch (Tier)
            {
                case (0):
                    bruteAttack = bruteAttackFactory.Create(BruteAttackTier.Tier0, GameObject.Transform.Position,ePos);
                    break;
                case (1):
                    bruteAttack = bruteAttackFactory.Create(BruteAttackTier.Tier1, GameObject.Transform.Position, ePos);
                    break;
                case (2):
                    bruteAttack = bruteAttackFactory.Create(BruteAttackTier.Tier2, GameObject.Transform.Position, ePos);
                    break;
                case (3):
                    bruteAttack = bruteAttackFactory.Create(BruteAttackTier.Tier3, GameObject.Transform.Position, ePos);
                    break;
            }
            LevelOne.AddObject(bruteAttack);

        }
    }
}
