using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class GameEvent
    {
        //A list of listners to observe events
        private List<IGameListener> listeners = new List<IGameListener>();

        /// <summary>
        /// A method used to attach an event to an Object
        /// </summary>
        /// <param name="listner">listner is the variable GameObject that wants to get notified of the events</param>
        public void Attach(IGameListener listner)
        {
            listeners.Add(listner);
        }

        /// <summary>
        /// A method used to remove an event from an Object
        /// </summary>
        /// <param name="listner">listner is the variable GameObject that wants to get notified of the events</param>
        public void Detach(IGameListener listner)
        {
            listeners.Remove(listner);
        }

        /// <summary>
        /// The Nofity method used to observe if a certain event has occured
        /// </summary>
        public void Notify()
        {
            foreach (IGameListener listener in listeners)
            {
                listener.Notify(this);
            }
        }
    }
}
