using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//

    public class StartScreen : State
    {
        //A Texture variable for our background
        private Texture2D[] backgroundsprite;

        //2 variables to control key presses
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public StartScreen(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            
            backgroundsprite = new Texture2D[1];
            backgroundsprite[0] = content.Load<Texture2D>("placeholdersprites/background1");
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

            spriteBatch.Draw(backgroundsprite[0], new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

            spriteBatch.End();
        }
    }
}
