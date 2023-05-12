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
        private Vector2 position;
        private Vector2 direction;
        private object attackDamage;

        public ArcherArrow(Vector2 position, Vector2 direction, object attackDamage)
        {
            this.position = position;
            this.direction = direction;
            this.attackDamage = attackDamage;
        }
    }
}
