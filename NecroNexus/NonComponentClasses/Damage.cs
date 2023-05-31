using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai----------------------------//

    //An enum to specify DamageTypes in the game
    public enum DamageType { Physical, Magical, Both }

    /// <summary>
    /// This class is used for our damage in the game
    /// </summary>
    public class Damage
    {
        public DamageType Type { get; set; } //Signifies the Damage Type

        public float Value { get; set; } //Determines how much Damage is done

        public Damage(DamageType type, float value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
