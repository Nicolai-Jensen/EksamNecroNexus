using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class CollisionEvent : GameEvent
    {
        /// <summary>
        /// A property that is used to refer to the other object who collided
        /// </summary>
        public GameObject Other { get; set; }

        /// <summary>
        /// The Notify Method used to observe when the event has occured
        /// </summary>
        /// <param name="other"></param>
        public void Notify(GameObject other)
        {
            this.Other = other;

            base.Notify();
        }
    }
}
