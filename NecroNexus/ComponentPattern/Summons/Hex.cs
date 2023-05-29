﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace NecroNexus
{
    public class Hex : Summon
    {
        public List<GameObject> EnemiesInRange { get; private set; }

        public int CurrentTier { get; private set; }

        private float attackTimer;

        HexBallFactory HexBallFactory = new HexBallFactory();
        private Vector2 velocity;
        private Vector2 ePos;
        public float hexDamge { get; set; }
        public float Range { get { return AttackRangeRadius; } }
        public float FireRate { get { return AttackSpeed; } }

        public int Tier { get; set; } = 0;

        public Hex(Vector2 position, float attackRangeRadius, float attackspeed)
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
                    hexDamge = 2f;
                    break;
                case 1:
                    hexDamge = 4f;
                    break;
                case 2:
                    hexDamge = 6f;
                    break;
                case 3:
                    hexDamge = 8f;
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
            GameObject.Tag = "Hex";
            base.Start();
        }

        public override void Update()
        {
            attackTimer += GameWorld.DeltaTime;
            if (Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position) != null)
            {

                if (attackTimer >= AttackSpeed && IsEnemiesInRange(Globals.FindClosestObject(LevelOne.gameObjects, GameObject.Transform.Position).Transform.Position, 175f) == true)
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
            GameObject hexAttack = new GameObject();
            switch (Tier)
            {
                case (0):
                    hexAttack = HexBallFactory.Create(HexBallTier.Tier0, GameObject.Transform.Position, ePos);
                    break;
                case (1):
                    hexAttack = HexBallFactory.Create(HexBallTier.Tier1, GameObject.Transform.Position, ePos);
                    break;
                case (2):
                    hexAttack = HexBallFactory.Create(HexBallTier.Tier2, GameObject.Transform.Position, ePos);
                    break;
                case (3):
                    hexAttack = HexBallFactory.Create(HexBallTier.Tier3, GameObject.Transform.Position, ePos);
                    break;
            }

            LevelOne.AddObject(hexAttack);

        }


    }
}








