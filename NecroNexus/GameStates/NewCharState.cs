using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NecroNexus
{
    //--------------------------Thorbjørn----------------------------//
    public class NewCharState : State
    {
        //A Texture variable for our background
        private Texture2D[] backgroundsprite = new Texture2D[3];
        private Rectangle finalizeButRec;
        private SpriteFont spriteFont;

        private MyWpfControl myWpfControl;
        //2 variables to control key presses
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        private MouseState previousMouse;
        private MouseState currentMouse;
        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public NewCharState(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            backgroundsprite[0] = content.Load<Texture2D>("placeholdersprites/UI/MenuPlaceHolderPng");
            spriteFont = content.Load<SpriteFont>("placeholdersprites/UI/File");
            finalizeButRec = new Rectangle(250, 250, 200, 75);
            myWpfControl = new MyWpfControl(spriteFont);
        }

        /// <summary>
        /// A way to ensure single key presses when taking action
        /// </summary>
        public override void Update()
        {
            myWpfControl.Update();
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();//enables you to click with the currentMouse
            if (finalizeButRec.Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                if (game.Menu.Drawdiffent == 7){game.Menu.ChangeNameLoadoneSaveone(myWpfControl.CurrentText);}
                if (game.Menu.Drawdiffent == 8){game.Menu.ChangeNameLoadtwoSavetwo(myWpfControl.CurrentText);} 
                if (game.Menu.Drawdiffent == 9){game.Menu.ChangeNameLoadthreeSavethree(myWpfControl.CurrentText);}
                game.ChangeState(game.Menu);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(backgroundsprite[0], new Rectangle(560, 270, 600, 400), Color.White);
            spriteBatch.Draw(backgroundsprite[0], finalizeButRec, Color.Gray);
            spriteBatch.DrawString(spriteFont, "Name", new Vector2(0, 0), Color.Black);
            myWpfControl.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
