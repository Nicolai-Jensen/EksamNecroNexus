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
        private Vector2 Position { get; set; }
        private Vector2 Direction { get; set; }
        private float Speed { get; set; }
        private float Damage { get; set; }

        public ArcherArrow(Vector2 position, Vector2 direction, float damage)
        {
            this.Position = position;
            this.Direction = direction;
            this.Speed = 5.0f;
            this.Damage = damage;
        }
        
        public void Launch()
        {
            Vector2 velocity = Direction * Speed;

            Position += velocity;

        }
    }
}
