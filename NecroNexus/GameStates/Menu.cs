using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NecroNexus
{

    //--------------------------Nicolai Jensen----------------------------//

    public class Menu : State
    {
        //A Texture variable for our background
        private Texture2D[] menuSprites = new Texture2D[8];
        private Rectangle[] menuRec = new Rectangle[16];
        private int clickedStuff = 0;

        //2 variables to control key presses
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        private MouseState mouse;

        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public Menu(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            int menuXPos = 560, menuYPos = 120;
            menuSprites[0] = content.Load<Texture2D>("placeholdersprites/UI/MenuPlaceHolderPng");//background
            menuRec[0] = new Rectangle(menuXPos, menuYPos, 800, 900);//place for background
            menuSprites[1] = content.Load<Texture2D>("placeholdersprites/UI/ButtonPlaceHolderPng");//buttons
            int preYpos = menuYPos;
            for (int i = 1; i <= 4; i++)//places the first 4 buttons 
            {
                preYpos += 175;
                menuRec[i] = new Rectangle(menuXPos + 50, preYpos, 700, 125); // 1 is for loadgame. 2 is for newgame. 3 is for options. 4 is for quitbutton
            }
            menuRec[5] = new Rectangle(menuXPos + 125, menuYPos + 700, 200, 125);//Backbutton
            menuRec[6] = new Rectangle(menuXPos + 475, menuYPos + 700, 200, 125);//LoadUser
            menuRec[7] = new Rectangle(menuXPos + 475, menuYPos + 700, 200, 125);//NewUser

        }

        /// <summary>
        /// A way to ensure single key presses when taking action
        /// </summary>
        public override void Update()
        {
            mouse = Mouse.GetState();//enables you to click with the mouse
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            ClickingOnMenu();
        }

        private void ClickingOnMenu()
        {
            if (menuRec[1].Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed)
            {
                clickedStuff = 1;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            switch (clickedStuff)
            {
                case 0:
                    spriteBatch.Draw(menuSprites[0], menuRec[0], Color.White);

                    for (int i = 1; i <= 5; i++)
                    {
                        spriteBatch.Draw(menuSprites[1], menuRec[i], Color.White);
                    }
                    break;
                case 1:
                    spriteBatch.Draw(menuSprites[0], menuRec[0], Color.White);
                    for (int i = 5; i <= 6; i++)
                    {
                        spriteBatch.Draw(menuSprites[1], menuRec[i], Color.White);
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }


            spriteBatch.End();
        }
    }
}
