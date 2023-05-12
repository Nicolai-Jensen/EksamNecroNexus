using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{

    //An enum to refer to the state of the keys
    public enum BState { Up, Down }


    public class ButtonEvent : GameEvent
    {
        /// <summary>
        /// A property for tracking Key inputs
        /// </summary>
        public Keys Key { get; private set; }

        /// <summary>
        /// A property for tracking the state of the keys
        /// </summary>
        public BState State { get; private set; }

        /// <summary>
        /// A method for observing both keys and the keystates of this event
        /// </summary>
        /// <param name="key">Refers to a Key that can be used</param>
        /// <param name="state">Refers to the State of keys</param>
        public void Notify(Keys key, BState state)
        {
            this.Key = key;
            this.State = state;
            base.Notify();
        }
    }
}
