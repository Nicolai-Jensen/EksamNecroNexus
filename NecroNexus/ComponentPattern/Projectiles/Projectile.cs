using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus.ComponentPattern
{
    public class ArcherArrow
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public float Damage { get; set; }

        public ArcherArrow(Vector2 position, Vector2 direction, float damage)
        {
            Position = position;
            Direction = direction;
            Speed = 5.0f;
            Damage = damage;
        }

        public void Launch()
        {
            Position += Direction * Speed;

        }
    }
}
