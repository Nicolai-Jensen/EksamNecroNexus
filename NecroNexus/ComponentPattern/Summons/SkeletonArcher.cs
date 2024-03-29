﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//
    public class SkeletonArcher : Summon
    {
        //List of enemies inside the range of the summon
        public List<GameObject> EnemiesInRange { get; private set; }


        //Used to calculate attackspeed
        private float attackTimer;
        private bool loaded;

        
        ArrowFactory arrowFactory = new ArrowFactory();
        private Vector2 velocity;
        private Vector2 ePos; //EnemyPosition

        /// <summary>
        /// Properties that the UI gets, so the player can see the stats of the summon.
        /// </summary>
        public float SkDamage { get; set; } //SkDamage = SkeletonDamage
        public float Range { get { return AttackRangeRadius; } }
        public float FireRate { get{ return AttackSpeed; } }
        public int Tier { get; set; } = 0;

        /// <summary>
        /// Constructor that makes a new list, that hold the enemies inside the range.
        /// Sets the summon's tier to 0, when created.
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

        public SkeletonArcher(bool load, Vector2 position, float attackRangeRadius, float attackspeed) : base(position, attackRangeRadius, attackspeed)
        {
            loaded = load;
            AttackRangeRadius = attackRangeRadius;
            EnemiesInRange = new List<GameObject>();
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
                    SkDamage = 1f;
                    break;
                case 1:
                    SkDamage = 2f;
                    break;
                case 2:
                    SkDamage = 3f;
                    break;
                case 3:
                    SkDamage = 4f;
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
            if (loaded == false)
            {
                GameObject.Transform.Translate(Position);
            }
            GameObject.Tag = "Archer";
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






    

