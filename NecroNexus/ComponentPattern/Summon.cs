using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public abstract class Summon : Component
    {
        public Vector2 Position { get; set; }
        public float AttackRange { get; set; }
        public int Damage { get; set; }


        public Summon(Vector2 position, float attackrange)
        {
            Position = position;
            AttackRange = attackrange;
        }



        //public bool IsEnemyInRange(Enemy enemy)
        //{
        //    float distance = Vector2.Distance(Position, enemy.Position);
        //    return distance <= AttackRange;
        //}


        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
