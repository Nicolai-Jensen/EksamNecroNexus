using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{

    //--------------------------Nicolai Jensen----------------------------//

    class Menu : State
    {
        //A Texture variable for our background
        private Texture2D[] backgroundsprite;

        //2 variables to control key presses
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public Menu()
        {
            
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {

        }

        /// <summary>
        /// A way to ensure single key presses when taking action
        /// </summary>
        public override void Update()
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);


            spriteBatch.End();
        }
    }
}
