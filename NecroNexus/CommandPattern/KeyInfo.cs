using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    /// <summary>
    /// A Simple class to keep track of the information of Keys
    /// </summary>
    public class KeyInfo
    {
        /// <summary>
        /// This Property gets/sets a bool that refers to whether a button is down or up
        /// </summary>
        public bool IsDown { get; set; }

        /// <summary>
        /// This property gets/sets which Key is being used/refered to
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// The Constructor for Keyinfo, simply takes an key and sees if its used
        /// </summary>
        /// <param name="key">Refers to the key you want info for</param>
        public KeyInfo(Keys key)
        {
            this.Key = key;
        }
    }
}
