using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Animation
    {
        /// <summary>
        /// A property that determines the FPS of the animation
        /// </summary>
        public float FPS { get; private set; }

        /// <summary>
        /// A property that determines the Animation we want to use
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A property used to get the sprites for the animation
        /// </summary>
        public Texture2D[] Sprites { get; private set; }


        /// <summary>
        /// The Constructor for the Animation class, sets the 3 properties
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sprites"></param>
        /// <param name="fps"></param>
        public Animation(string name, Texture2D[] sprites, float fps)
        {
            this.Sprites = sprites;
            this.Name = name;
            this.FPS = fps;
        }
    }
}
