using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Hex : Summon
    {
        public Hex(Vector2 position, float attackrange, float attackspeed, int attackDamage) : base(position, attackrange, attackspeed, attackDamage)
        {
        }
    }
}
