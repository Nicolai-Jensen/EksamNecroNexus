using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Enemy : Component
    {
        public Vector2 Position { get; set; }

        public float Health { get; set; }
        public float Size { get; set; }

        public Enemy(Vector2 position, float health, float size)
        {
            Position = position;
            Health = health;
            Size = size;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Destroy();
            }
        }

        private void Destroy()
        {

        }
    }
}
