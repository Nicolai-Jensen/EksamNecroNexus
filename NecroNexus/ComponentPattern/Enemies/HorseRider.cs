using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    public class HorseRider : Enemy
    {
        //An animator component to access animations
        private Animator animator;

        public override bool ToRemove { get; set; }
        public override float Health { get; set; }

        public override float SoulDrop { get; set; }

        public bool HasFallen { get; set; } = false;

        /// <summary>
        /// Applies a Speed, basedamage, health, board, souldrop and adds the boards list to the Objects pathlist
        /// </summary>
        /// <param name="board">The Board containing the positionLists</param>
        /// <param name="pos">The SpawnPosition of the Object</param>
        public HorseRider(Board board, Vector2 pos)
        {
            Speed = 250;
            baseDamage = 4;
            this.board = board;
            position = pos;
            Health = 14;
            SoulDrop = 3;
            foreach (var item in board.PositionList)
            {
                pathList.Add(item);
            }
        }

        public override void Start()
        {
            sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            GameObject.Transform.Position = position;
            currentPosition = GameObject.Transform.Position;
            animator = (Animator)GameObject.GetComponent<Animator>();
            animator.PlayAnimation("Idle");
        }

        /// <summary>
        /// The Update Method, this method runs constantly
        /// </summary>
        public override void Update()
        {
            FindPath();
            Move();
            UpdateDamagedList();
            Death();

            if (sr.Sprite == Globals.Content.Load<Texture2D>("Enemies/Rider/Death/Knight_death11"))
            {
                animator.PlayAnimation("Walk");
                Speed = 60;
                GameObject.Tag = "Enemy";
            }

        }

        public override void FindPath()
        {
            base.FindPath();
        }

        /// <summary>
        /// This Method is used to track if an Enemy has Died or taken damage
        /// </summary>
        public override void Death()
        {
            if (Health <= 0 && HasFallen == false) //Object has Died, gets removed and drops its souls
            {
                GameObject.Tag = "Dead";
                Speed = 0;
                Health = 20;
                animator.PlayAnimation("Death");
                DrawingLevel.UpdateSouls(SoulDrop);
                HasFallen = true;
            }
            else if (Health <= 0 && HasFallen == true)
            {
                ToRemove = true;
                DrawingLevel.UpdateSouls(SoulDrop);
            }
            if (healthModified == true) //Object has been hit, initiate hit feedback
            {
                hit = true;
                sr.Color = Color.Red;
                AudioEffect.HitDamageSound();
                healthModified = false;
            }

            if (hit == true) //The Timer for returning the Object to its correct color from red
            {
                timerForFeedBack += GameWorld.DeltaTime;
                if (timerForFeedBack > 0.1f)
                {
                    sr.Color = Color.White;
                    hit = false;
                    timerForFeedBack = 0;
                }
            }
        }

        public override void TakeDamage(Damage damage)
        {
            base.TakeDamage(damage);
        }
        public override void BecomeSlowed(Slow slow)
        {
            if (Speed >= 125)
            {
                base.BecomeSlowed(slow);
            }

        }
    }
}
