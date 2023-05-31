using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{

    //--------------------------Nicolai Jensen----------------------------//

    /// <summary>
    /// The SuperClass for our Components, comes with the mandatory methods used in all Components
    /// </summary>
    public class Component
    {
        /// <summary>
        /// GameObject property used to access GameObject
        /// </summary>
        public GameObject GameObject { get; set; }

        //A property to indicate this component should be removed from the game
        public virtual bool ToRemove { get; set; }

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }


    }
}

