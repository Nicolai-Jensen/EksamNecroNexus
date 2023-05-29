using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{

    public enum DamageType { Physical, Magical, Both }
    public class Damage
    {
        public DamageType Type { get; set; }

        public float Value { get; set; }

        public Damage(DamageType type, float value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
