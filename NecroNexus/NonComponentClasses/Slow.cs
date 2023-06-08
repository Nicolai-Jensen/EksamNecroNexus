using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //***********//KASPER KNUDSEN//***********//

    //An enum to specify SlowTypes in the game
    public enum SlowType { Slowed }

    /// <summary>
    /// This class is used for slowing enemies in the game
    /// </summary>
    public class Slow
    {
        public SlowType Type { get; set; } //Signifies the Slow Type

        public float Value { get; set; } //Determines how much the enemy is slowed

        public Slow(SlowType type, float value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
