using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Grunt : Enemy
    {

        //An animator component to access animations
        private Animator animator;
        
        public override bool ToRemove { get; set; }

        public Grunt(Board board)
        {
            speed = 100;
            this.board = board;
            foreach (var item in board.PositionList)
            {
                pathList.Add(item);
            }
        }

        public override void Start()
        {
            GameObject.Transform.Position = new Vector2(700, GameWorld.ScreenSize.Y / 2);
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
        }

        public override void FindPath()
        {
            base.FindPath();
        }

    }
}
