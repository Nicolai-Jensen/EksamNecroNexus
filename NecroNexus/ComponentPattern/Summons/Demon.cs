﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Demon : Summon
    {
        public Demon(Vector2 position, float attackRangeRadius, float attackspeed) : base(position, attackRangeRadius, attackspeed)
        {
        }
        public override void Start()
        {
            GameObject.Transform.Translate(Position);
            base.Start();
        }
    }
}
