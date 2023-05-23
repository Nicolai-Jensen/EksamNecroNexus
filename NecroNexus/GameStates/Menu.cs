﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NecroNexus
{

    //--------------------------Thorbjørn----------------------------//

    public class Menu : State
    {
        //A Texture variable for our background
        private Texture2D[] menuSprites = new Texture2D[16];
        private Rectangle[] menuRec = new Rectangle[16];
        private SpriteFont spriteFont;
        private string[] placeHolderName = { "Empty", "Empty", "Empty" };
        private int clickedStuff = 0;// Which menu has been clicked 
        private int WhichMenuClicked = 0;//0 for nothing pressed. 1 for Loadgame pressed. 2 for newgame pressed
        private int drawdiffent = 0;

        //2 variables to control key presses

        private MouseState previousMouse;
        private MouseState currentMouse;

        public int Drawdiffent { get { return drawdiffent; } }

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
            spriteFont = content.Load<SpriteFont>("placeholdersprites/UI/File");
            int menuXPos = 560, menuYPos = 120;
            menuSprites[0] = content.Load<Texture2D>("placeholdersprites/UI/BackGroundWithoutEdge");//background
            menuRec[0] = new Rectangle(menuXPos, menuYPos, 800, 900);//place for background
            menuSprites[1] = content.Load<Texture2D>("placeholdersprites/UI/LoadGameBut");//LoadGameButton
            menuSprites[2] = content.Load<Texture2D>("placeholdersprites/UI/NewGameBut");//NewGameButton
            menuSprites[3] = content.Load<Texture2D>("placeholdersprites/UI/OptionsBut");//OptionsButton
            menuSprites[4] = content.Load<Texture2D>("placeholdersprites/UI/QuitBut");//QuitGameButton
            menuSprites[5] = content.Load<Texture2D>("placeholdersprites/UI/EmptyBut");//Empty button
            menuSprites[6] = content.Load<Texture2D>("placeholdersprites/UI/BackBut");//Go back in the menu Button
            menuSprites[7] = content.Load<Texture2D>("placeholdersprites/UI/LoadBut");//Load the user Button.
            menuSprites[8] = content.Load<Texture2D>("placeholdersprites/UI/NewBut");//Make a new user Button.
            int preYpos = menuYPos;
            for (int i = 1; i <= 4; i++)//places the first 4 buttons
            {
                preYpos += 175;
                menuRec[i] = new Rectangle(menuXPos + 100, preYpos, 600, 150); // 1 is for loadgame. 2 is for newgame. 3 is for options. 4 is for quitbutton
            }
            menuRec[5] = new Rectangle(menuXPos + 125, menuYPos + 750, 200, 100);//Backbutton
            menuRec[6] = new Rectangle(menuXPos + 475, menuYPos + 750, 200, 100);//LoadUser
            menuRec[7] = new Rectangle(menuXPos + 475, menuYPos + 750, 200, 100);//NewUser
            menuRec[8] = new Rectangle(menuXPos + 100, menuYPos + 50, 600, 650); //BackGround for loadGame & NewGame
            int loadYPos = menuYPos + 70;
            for (int i = 9; i < 12; i++)//Drawing the loadgameboxes
            {
                /* 9 is for loadsave1
                 * 10 is for loadsave2
                 * 11 is for loadsave3
                 */
                menuRec[i] = new Rectangle(menuXPos + 110, loadYPos, 580, 180);
                loadYPos += 210;
            }
            loadYPos = menuYPos + 70;
            for (int i = 12; i < 15; i++)//Drawing the loadgameboxes
            {
                /* 12 is for newsave1
                 * 13 is for newsave2
                 * 14 is for newsave3
                 */
                menuRec[i] = new Rectangle(menuXPos + 110, loadYPos, 580, 180);
                loadYPos += 210;
            }
        }

        /// <summary>
        /// A way to ensure single key presses when taking action
        /// </summary>
        public override void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();//enables you to click with the currentMouse
            ClickingOnMenu();
        }

        private void ClickingOnMenu()
        {
            if (WhichMenuClicked == 0 && menuRec[1].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing the LoadGameButton
            {
                clickedStuff = 1;
                WhichMenuClicked = 1;
            }
            // Loading the game with a user name
            if (WhichMenuClicked == 1 && menuRec[6].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing the LoadGameButton
            {
                switch (drawdiffent)
                {
                    case 4:
                        if (placeHolderName[0] != "Empty")
                        {
                            game.ChangeState2(game.LevelOne);
                        }
                        break;
                    case 5:
                        if (placeHolderName[1] != "Empty")
                        {
                            game.ChangeState2(game.LevelOne);
                        }
                        break;
                    case 6:
                        if (placeHolderName[2] != "Empty")
                        {
                            game.ChangeState2(game.LevelOne);
                        }
                        break;
                }
                return;
            }

            if (WhichMenuClicked == 0 && menuRec[2].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing new game
            {
                clickedStuff = 2;
                WhichMenuClicked = 2;
            }
            if (WhichMenuClicked == 0 && menuRec[3].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing options.
            {
                clickedStuff = 3;
                WhichMenuClicked = 3;
            }
            if (WhichMenuClicked == 0 && menuRec[4].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing the Quit
            {
                game.Exit();
            }
            if (menuRec[5].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing the back Button
            {
                clickedStuff = 0;
                WhichMenuClicked = 0;
            }
            if (WhichMenuClicked == 1 && menuRec[9].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//1 loadGameBut
            {
                drawdiffent = 4;
            }
            if (WhichMenuClicked == 1 && menuRec[10].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//2 loadGameBut
            {
                drawdiffent = 5;
            }
            if (WhichMenuClicked == 1 && menuRec[11].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//3 loadGamebut
            {
                drawdiffent = 6;
            }
            if (WhichMenuClicked == 2 && menuRec[12].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//1 newGameBut
            {
                drawdiffent = 7;
            }
            if (WhichMenuClicked == 2 && menuRec[13].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//2 newGameBut
            {
                drawdiffent = 8;
            }
            if (WhichMenuClicked == 2 && menuRec[14].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//3 newGameBut
            {
                drawdiffent = 9;
            }
            if (menuRec[7].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                if (drawdiffent == 7 || drawdiffent == 8 || drawdiffent == 9)
                {
                    game.ChangeState(game.NewCharState);
                }
            }
        }

        public void ChangeNameLoadoneSaveone(string saveName)
        {
            placeHolderName[0] = saveName;
        }
        public void ChangeNameLoadtwoSavetwo(string saveName)
        {
            placeHolderName[1] = saveName;

        }
        public void ChangeNameLoadthreeSavethree(string saveName)
        {
            placeHolderName[2] = saveName;

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
            switch (clickedStuff)//Navigating the menu
            {
                case 0://MainMenu

                    for (int i = 1; i <= 4; i++)
                    {
                        spriteBatch.Draw(menuSprites[i], menuRec[i], Color.White);
                    }
                    break;
                #region LoadGame Button
                case 1://LoadGame
                    spriteBatch.Draw(menuSprites[0], menuRec[8], Color.Gray);//background
                    spriteBatch.Draw(menuSprites[6], menuRec[5], Color.White);//Back
                    spriteBatch.Draw(menuSprites[7], menuRec[6], Color.White);//Load
                    
                    for (int i = 9; i < 12; i++)//LoadUser
                    {
                        spriteBatch.Draw(menuSprites[5], menuRec[i], Color.White);

                        switch (drawdiffent)//clicked user
                        {
                            case 4:
                                spriteBatch.Draw(menuSprites[5], menuRec[9], Color.DarkGray);
                                spriteBatch.DrawString(spriteFont, placeHolderName[0], new Vector2(menuRec[9].X + 10, menuRec[9].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                                break;
                            case 5:
                                spriteBatch.Draw(menuSprites[5], menuRec[10], Color.DarkGray);
                                spriteBatch.DrawString(spriteFont, placeHolderName[1], new Vector2(menuRec[10].X + 10, menuRec[10].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                                break;
                            case 6:
                                spriteBatch.Draw(menuSprites[5], menuRec[11], Color.DarkGray);
                                spriteBatch.DrawString(spriteFont, placeHolderName[2], new Vector2(menuRec[11].X + 10, menuRec[11].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                                break;
                        }
                        spriteBatch.DrawString(spriteFont, placeHolderName[0], new Vector2(menuRec[9].X + 10, menuRec[9].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                        spriteBatch.DrawString(spriteFont, placeHolderName[1], new Vector2(menuRec[10].X + 10, menuRec[10].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                        spriteBatch.DrawString(spriteFont, placeHolderName[2], new Vector2(menuRec[11].X + 10, menuRec[11].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                    }

                    break;
                #endregion
                #region NewGame Button
                case 2://NewGame
                    spriteBatch.Draw(menuSprites[6], menuRec[5], Color.White);//Back button
                    spriteBatch.Draw(menuSprites[8], menuRec[7], Color.White);//New user button
                    spriteBatch.Draw(menuSprites[0], menuRec[8], Color.Gray);//background
                    for (int i = 12; i < 15; i++)//Draws 3 clickable buttons to so one can select which user slot one wants to use.
                    {
                        spriteBatch.Draw(menuSprites[5], menuRec[i], Color.White);
                        switch (drawdiffent)//clicked user
                        {
                            case 7:
                                spriteBatch.Draw(menuSprites[5], menuRec[12], Color.DarkGray);
                                spriteBatch.DrawString(spriteFont, placeHolderName[0], new Vector2(menuRec[12].X + 10, menuRec[12].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                                break;
                            case 8:
                                spriteBatch.Draw(menuSprites[5], menuRec[13], Color.DarkGray);
                                spriteBatch.DrawString(spriteFont, placeHolderName[1], new Vector2(menuRec[13].X + 10, menuRec[13].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                                break;
                            case 9:
                                spriteBatch.Draw(menuSprites[5], menuRec[14], Color.DarkGray);
                                spriteBatch.DrawString(spriteFont, placeHolderName[2], new Vector2(menuRec[14].X + 10, menuRec[14].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                                break;
                        }
                        spriteBatch.DrawString(spriteFont, placeHolderName[0], new Vector2(menuRec[12].X + 10, menuRec[12].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                        spriteBatch.DrawString(spriteFont, placeHolderName[1], new Vector2(menuRec[13].X + 10, menuRec[13].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
                        spriteBatch.DrawString(spriteFont, placeHolderName[2], new Vector2(menuRec[14].X + 10, menuRec[14].Y + 65), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);

                    }
                    break;
                #endregion
                case 3://Options
                    spriteBatch.Draw(menuSprites[6], menuRec[5], Color.White);

                    break;
            }

        }
    }
}
