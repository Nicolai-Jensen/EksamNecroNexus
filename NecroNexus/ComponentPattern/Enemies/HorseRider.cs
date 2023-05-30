using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class HorseRider : Enemy
    {
        //An animator component to access animations
        private Animator animator;

        public override bool ToRemove { get; set; }
        public override float Health { get; set; }

        public override float SoulDrop { get; set; }
        public HorseRider(Board board, Vector2 pos)
        {
            speed = 250;
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
        }

        /// <summary>
        /// The Update Method, this method runs constantly
        /// </summary>
        public override void Update()
        {

            animator.PlayAnimation("Idle");
            FindPath();
            Move();
            UpdateDamagedList();
            Death();
        }

        public override void FindPath()
        {
            base.FindPath();
        }

        public override void TakeDamage(Damage damage)
        {
            base.TakeDamage(damage);
        }
    }
}
