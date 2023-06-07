using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//

    /// <summary>
    /// Sadly, there was not time enough to give this summon, the functionality we wanted.
    /// </summary>
    /// 
    public class SkeletonBrute : Summon
    {
        public List<GameObject> EnemiesInRange { get; private set; }
        private bool loaded;

        public float skDamge { get; set; }
        public float Range { get { return AttackRangeRadius; } }
        public float FireRate { get { return AttackSpeed; } }
        public int Tier { get; set; } = 0;

        public SkeletonBrute(Vector2 position, float attackRangeRadius, float attackspeed) : base(position, attackRangeRadius, attackspeed)
        {
        }

        public SkeletonBrute(bool load, Vector2 position, float attackRangeRadius, float attackspeed) : base(position, attackRangeRadius, attackspeed)
        {
            loaded = load;
            AttackRangeRadius = attackRangeRadius;
            EnemiesInRange = new List<GameObject>();
        }
        public override void Start()
        {
            if (loaded == false)
            {
                GameObject.Transform.Translate(Position);
            }
            GameObject.Tag = "Brute";

            base.Start();
        }
        public void SetTier(int i)
        {
            this.Tier = i;
        }
    }
}
