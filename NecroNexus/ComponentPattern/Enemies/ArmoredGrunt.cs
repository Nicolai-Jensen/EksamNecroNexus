using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    public class ArmoredGrunt : Enemy
    {
        //An animator component to access animations
        private Animator animator;

        public override bool ToRemove { get; set; }

        public override float Health { get; set; }

        public override float SoulDrop { get; set; }

        /// <summary>
        /// Applies a Speed, basedamage, health, board, souldrop and adds the boards list to the Objects pathlist
        /// </summary>
        /// <param name="board">The Board containing the positionLists</param>
        /// <param name="pos">The SpawnPosition of the Object</param>
        public ArmoredGrunt(Board board, Vector2 pos)
        {
            Speed = 90;
            baseDamage = 2;
            this.board = board;
            position = pos;
            Health = 7;
            SoulDrop = 2;
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

        /// <summary>
        /// Override of the TakeDamage Method, this version reduces physical damage by 25%
        /// </summary>
        /// <param name="damage">A Damage variable that contains a damageType and Value</param>
        public override void TakeDamage(Damage damage)
        {
            Damage trueValue = damage;
            if (damage.Type == DamageType.Physical)
            {
                trueValue.Value = damage.Value / 4 * 3;
            }
            base.TakeDamage(trueValue);
        }
        public override void BecomeSlowed(Slow slow)
        {
            if (Speed >= 50)
            {
                base.BecomeSlowed(slow);
            }

        }
    }
}

