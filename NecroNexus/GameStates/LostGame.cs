using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NecroNexus.GameStates
{
    /// <summary>
    /// This state is shown when the player loses the game. So far the only thing this class allows the player to do is go back to the main menu.
    /// Thorbjørn
    /// </summary>
    public class LostGame : State
    {
        private Texture2D[] sprites = new Texture2D[4];
        private Rectangle[] spritesRec = new Rectangle[4];
        //2 variables to get mouse information.
        private MouseState previousMouse;
        private MouseState currentMouse;
        public LostGame(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            sprites[0] = content.Load<Texture2D>("Backgrounds/GameOverFullScreen");
            spritesRec[0] = new Rectangle(0,0,1920,1080);
            sprites[1] = content.Load<Texture2D>("placeholdersprites/UI/MainMenuBut");
            spritesRec[1] = new Rectangle(770,50,400,75);
        }
        /// <summary>
        /// Handles the mouse input.
        /// </summary>
        public override void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();//enables you to click with the currentMouse

            if (spritesRec[1].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                game.Menu.ClickedStuff = 0;
                game.Menu.WhichMenuClickede = 0;
                game.ChangeState(game.Menu);
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(sprites[0], spritesRec[0], Color.White);
            if (spritesRec[1].Contains(currentMouse.X, currentMouse.Y))
            {
                spriteBatch.Draw(sprites[1], spritesRec[1], Color.LightGray);
            }
            else { spriteBatch.Draw(sprites[1], spritesRec[1], Color.White); }

            spriteBatch.End();
        }
    }
}
