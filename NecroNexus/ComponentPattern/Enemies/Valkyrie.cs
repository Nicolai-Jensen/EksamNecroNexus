using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    public class Valkyrie : Enemy
    {
        //An animator component to access animations
        private Animator animator;

        public override bool ToRemove { get; set; }
        public override float Health { get; set; }

        public override float SoulDrop { get; set; }

        public override float Speed {get; set;}

        /// <summary>
        /// Applies a Speed, basedamage, health, board, souldrop and adds the boards list to the Objects pathlist
        /// </summary>
        /// <param name="board">The Board containing the positionLists</param>
        /// <param name="pos">The SpawnPosition of the Object</param>
        public Valkyrie(Board board, Vector2 pos)
        {
            speed = 170;
            baseDamage = 7;
            this.board = board;
            position = pos;
            Health = 25;
            SoulDrop = 6;
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
        /// Override of the TakeDamage Method, this version prevents Physical damage from happening
        /// </summary>
        /// <param name="damage">A Damage variable that contains a damageType and Value</param>
        public override void TakeDamage(Damage damage)
        {
            Damage trueValue = damage;
            if (damage.Type == DamageType.Physical)
            {
                trueValue.Value = 0;
            }
            base.TakeDamage(trueValue);
        }
        public override void BecomeSlowed(Slow slow)
        {
            Slow trueValue = slow;
            if (slow.Type == SlowType.Slowed)
            {
                trueValue.Value = 0;
            }
            base.BecomeSlowed(trueValue);
        }
    }
}
