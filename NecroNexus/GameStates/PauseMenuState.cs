﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NecroNexus
{
    //--------------------------Thorbjørn----------------------------//
    public class PauseMenuState : State
    {
        //A Texture variable for our background
        private Texture2D[] menuSprites = new Texture2D[8];
        private Rectangle[] menuRec = new Rectangle[16];
        private SpriteFont spriteFont;
        private int clickedStuff = 0;


        //2 variables to control key presses

        private MouseState previousMouse;
        private MouseState currentMouse;
        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public PauseMenuState(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            int menuXPos = 560, menuYPos = 120;
            menuSprites[0] = content.Load<Texture2D>("placeholdersprites/UI/BackGroundWithoutEdge");//background
            menuRec[0] = new Rectangle(menuXPos, menuYPos, 800, 900);//place for background
            spriteFont = content.Load<SpriteFont>("placeholdersprites/UI/File");
            menuSprites[1] = content.Load<Texture2D>("placeholdersprites/UI/BackBut");
            menuSprites[2] = content.Load<Texture2D>("placeholdersprites/UI/OptionsBut");
            menuSprites[3] = content.Load<Texture2D>("placeholdersprites/UI/MainMenuBut");
            int preYpos = menuYPos+50;
            for (int i = 1; i < 4; i++)//places the first 4 buttons 
            {
                preYpos += 200;
                menuRec[i] = new Rectangle(menuXPos + 50, preYpos, 700, 125); // 1 is for Back to game. 2 is for options. 3 is for quitbutton
            }
        }

        /// <summary>
        /// A way to ensure single key presses when taking action
        /// </summary>
        public override void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();//enables you to click with the currentMouse
            Clicking();
        }
        private void Clicking()
        {
            if (clickedStuff == 0 && menuRec[1].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing the go back to level
            {
                clickedStuff = 0;
                game.ChangeState(game.LevelOne);
            }
            if (clickedStuff == 0 && menuRec[3].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//Press for going back to mainmenu
            {
                clickedStuff = 0;
                game.ChangeState(game.Menu);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            DrawingMenu(spriteBatch);
            spriteBatch.End();
        }
        private void DrawingMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuSprites[0], menuRec[0], Color.White);
            switch (clickedStuff)
            {
                case 0://MainMenu

                    for (int i = 1; i < 4; i++)
                    {
                        spriteBatch.Draw(menuSprites[i], menuRec[i], Color.White);
                    }
                    break;
            }
        }
    }
}