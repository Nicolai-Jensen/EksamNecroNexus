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
            menuRec[5] = new Rectangle(menuXPos + 125, menuYPos + 750, 200, 125);//Backbutton
            menuRec[6] = new Rectangle(menuXPos + 475, menuYPos + 750, 200, 125);//LoadUser
            menuRec[7] = new Rectangle(menuXPos + 475, menuYPos + 750, 200, 125);//NewUser
            menuRec[8] = new Rectangle(menuXPos + 100, menuYPos + 50, 600, 650);

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
            if (menuRec[1].Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed)//For pressing the LoadGameButton
            {
                clickedStuff = 1;
            }
            if (menuRec[2].Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed)//For pressing new game
            {
                clickedStuff = 2;
            }
            if (menuRec[3].Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed)//For pressing options.
            {
                clickedStuff = 3;
            }
            if (menuRec[4].Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed)//For pressing the back Button
            {
                //QuitGame when you figure it out.
            }
            if (menuRec[5].Contains(mouse.X, mouse.Y) && mouse.LeftButton == ButtonState.Pressed)//For pressing the back Button
            {
                clickedStuff = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(menuSprites[0], menuRec[0], Color.White);
            switch (clickedStuff)//Navigating the menu
            {
                case 0://MainMenu

                    for (int i = 1; i <= 4; i++)
                    {
                        spriteBatch.Draw(menuSprites[1], menuRec[i], Color.White);
                    }
                    break;
                case 1://LoadGame
                    spriteBatch.Draw(menuSprites[1], menuRec[8], Color.Gray);

                    for (int i = 5; i <= 7; i++)
                    {
                        spriteBatch.Draw(menuSprites[1], menuRec[i], Color.White);
                    }
                    break;
                case 2://NewGame
                    spriteBatch.Draw(menuSprites[1], menuRec[5], Color.White);
                    spriteBatch.Draw(menuSprites[1], menuRec[7], Color.White);
                    break;
                case 3://Options
                    spriteBatch.Draw(menuSprites[1], menuRec[5], Color.White);

                    break;
            }


            spriteBatch.End();
        }
    }
}
