using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Transform
    {

        /// <summary>
        /// This Property is used to get and set a Gameobjects position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// This Method is used to Move GameObjects
        /// </summary>
        /// <param name="translation">A Given Vector2 that is then used as a new position</param>
        public void Translate(Vector2 translation)
        {
            if (!float.IsNaN(translation.X) && !float.IsNaN(translation.Y))
            {
                Position += translation;
            }
        }
    }
}
