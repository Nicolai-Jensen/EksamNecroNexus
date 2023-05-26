using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class ArmoredGrunt : Enemy
    {
        //An animator component to access animations
        private Animator animator;

        public override bool ToRemove { get; set; }

        public override float Health { get; set; }

        public override float SoulDrop { get; set; }
        public ArmoredGrunt(Board board, Vector2 pos)
        {
            speed = 90;
            this.board = board;
            position = pos;
            Health = 1;
            foreach (var item in board.PositionList)
            {
                pathList.Add(item);
            }
        }

        public override void Start()
        {
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
            Death();
        }

        public override void FindPath()
        {
            base.FindPath();
        }
    }
}
