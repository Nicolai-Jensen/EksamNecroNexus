using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class SkeletonBrute : Summon
    {
        public SkeletonBrute(Texture2D summonSprite, Vector2 position, float attackrange, float attackspeed, int attackDamage) : base(summonSprite, position, attackrange, attackspeed, attackDamage)
        {
        }
    }
}
