using DbDomain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace NecroNexus
{

    //--------------------------Thorbjørn----------------------------//
    /// <summary>
    /// The Menu class all the basic things in a menu it currently has 4 buttons, loadgame, newgame, options and Quit.
    /// Loadgame shows you the current saves. New Game allows the user to make a new a savegame. the options button is currently empty and awaits further exspansion
    /// Quit button closses the game
    /// </summary>
    public class Menu : State
    {
        //A Texture variable for our background
        private Texture2D[] menuSprites = new Texture2D[17];
        //Rectangel array for all the menu items used for size and placement
        private Rectangle[] menuRec = new Rectangle[16];
        private SpriteFont spriteFont;// for the text
        private string[] placeHolderName = { "Empty", "Empty", "Empty" };//Where the username is stored
        private int clickedStuff = 0;// Which menu has been clicked 
        private int WhichMenuClicked = 0;//0 for nothing pressed. 1 for Loadgame pressed. 2 for newgame pressed
        private int drawdiffent = 0;//Which loadgame or new game bare has been clicked.

        //2 variables to get mouse information.
        private MouseState previousMouse;
        private MouseState currentMouse;


        //Used to see which newgame or loadgame has been clicked
        public int Drawdiffent { get { return drawdiffent; } }
        //Used to reset the menu when called from PauseMenuState
        public int ClickedStuff{get { return clickedStuff; } set { clickedStuff = value; }}
        //Used to reset the menu when called from PauseMenuState
        public int WhichMenuClickede { get { return WhichMenuClicked; } set { WhichMenuClicked = value; } }

        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public Menu(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            this.game.Repository.Open();

        }
        /// <summary>
        /// When a player starts the game up it trys to make a database if there isnt one.
        /// </summary>
        public override void Initialize()
        {
            try
            {
                game.Repository.CreateDatabaseTables();

            }
            catch (System.Exception)
            {
                return;
            }

        }
        /// <summary>
        /// This class loads all the sprites and basic information so that it can be used later.
        /// It gets information from the database about which users there are.
        /// </summary>
        public override void LoadContent()
        {
            List<User> userList;
            //We know that an error will occurre if the user database is empty so by putting it in a try catch we can make sure it doesnt crash the game if it is empty
            try
            {
                userList = game.Repository.ReadAllUsers();
                foreach (User user in userList)
                {
                    if (user.UserID <= 3)
                    {
                        placeHolderName[user.UserID - 1] = user.UserName;
                    }
                }
            }
            catch (System.Exception)
            {
                //this "Vector2 ip" is useless and is just used to make sure that the rest of load content is called.
                Vector2 ip = new Vector2(0, 0);
            }
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
            //Background for the menu, placeholder for now, change if we get another picture.
            menuSprites[16] = content.Load<Texture2D>("Backgrounds/NecroBackgroundUpdatedPlain");
        }

        /// <summary>
        /// Used to get the mouse
        /// </summary>
        public override void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();//enables you to click with the currentMouse
            ClickingOnMenu();
        }
        /// <summary>
        /// Handels the 
        /// </summary>
        private void ClickingOnMenu()
        {
            if (WhichMenuClicked == 0 && menuRec[1].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing the LoadGameButton
            {
                AudioEffect.ButtonClickingSound();
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
                            game.LevelOne = new LevelOne(this.game, graphicsDevice, content);
                            game.LevelOne.LoadContent();
                            game.ChangeState2(game.LevelOne);
                        }
                        break;
                    case 5:
                        if (placeHolderName[1] != "Empty")
                        {
                            game.LevelOne = new LevelOne(this.game, graphicsDevice, content);
                            game.LevelOne.LoadContent();
                            game.ChangeState2(game.LevelOne);
                        }
                        break;
                    case 6:
                        if (placeHolderName[2] != "Empty")
                        {
                            game.LevelOne = new LevelOne(this.game, graphicsDevice, content);
                            game.LevelOne.LoadContent();
                            game.ChangeState2(game.LevelOne);
                        }
                        break;
                }
                return;
            }

            if (WhichMenuClicked == 0 && menuRec[2].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing new game
            {
                AudioEffect.ButtonClickingSound();
                clickedStuff = 2;
                WhichMenuClicked = 2;
            }
            if (WhichMenuClicked == 0 && menuRec[3].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing options.
            {
                AudioEffect.ButtonClickingSound();
                clickedStuff = 3;
                WhichMenuClicked = 3;
            }
            if (WhichMenuClicked == 0 && menuRec[4].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing the Quit
            {
                AudioEffect.ButtonClickingSound();
                game.Exit();
            }
            if (menuRec[5].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//For pressing the back Button
            {
                AudioEffect.ButtonClickingSound();
                clickedStuff = 0;
                WhichMenuClicked = 0;
            }
            if (WhichMenuClicked == 1 && menuRec[9].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//1 loadGameBut
            {
                AudioEffect.ButtonClickingSound();
                drawdiffent = 4;
            }
            if (WhichMenuClicked == 1 && menuRec[10].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//2 loadGameBut
            {
                AudioEffect.ButtonClickingSound();
                drawdiffent = 5;
            }
            if (WhichMenuClicked == 1 && menuRec[11].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//3 loadGamebut
            {
                AudioEffect.ButtonClickingSound();
                drawdiffent = 6;
            }
            if (WhichMenuClicked == 2 && menuRec[12].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//1 newGameBut
            {
                AudioEffect.ButtonClickingSound();
                drawdiffent = 7;
            }
            if (WhichMenuClicked == 2 && menuRec[13].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//2 newGameBut
            {
                AudioEffect.ButtonClickingSound();
                drawdiffent = 8;
            }
            if (WhichMenuClicked == 2 && menuRec[14].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)//3 newGameBut
            {
                AudioEffect.ButtonClickingSound();
                drawdiffent = 9;
            }
            if (menuRec[7].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)// when pressing new button in newgame
            {
                AudioEffect.ButtonClickingSound();
                if (drawdiffent == 7 || drawdiffent == 8 || drawdiffent == 9)
                {
                    game.ChangeState(game.NewCharState);
                }
            }
        }

        /// <summary>
        /// When called it takes the first slot and changes the name.
        /// By using a try catch, we read the the username with an id of 1 if there isnt a user we then set user to null and add the new user.
        /// if there is an user, we overwrite the name with a new name.
        /// </summary>
        /// <param name="saveName"></param>
        public void ChangeNameLoadoneSaveone(string saveName)
        {
            placeHolderName[0] = saveName;
            User user;
            
            try
            {
                user = game.Repository.ReadUser(1);
            }
            catch (System.Exception)
            {

                user = null;
            }
            if (user != null)
            {
                game.Repository.UpdateUser(1, saveName);
            }
            else { game.Repository.AddUser(placeHolderName[0]); }

        }
        /// <summary>
        /// When called it takes the second slot and changes the name.
        /// By using a try catch, we read the the username with an id of 2 if there isnt a user we then set user to null and add the new user.
        /// if there is an user, we overwrite the name with a new name.
        /// </summary>
        /// <param name="saveName"></param>
        public void ChangeNameLoadtwoSavetwo(string saveName)
        {
            placeHolderName[1] = saveName;
            User user;
            try
            {
                user = game.Repository.ReadUser(2);
            }
            catch (System.Exception)
            {

                user = null;
            }
            if (user != null)
            {
                game.Repository.UpdateUser(2, saveName);
            }
            else { game.Repository.AddUser(placeHolderName[1]); }

        }
        /// <summary>
        /// When called it takes the thrid slot and changes the name.
        /// By using a try catch, we read the the username with an id of 3 if there isnt a user we then set user to null and add the new user.
        /// if there is an user, we overwrite the name with a new name.
        /// </summary>
        /// <param name="saveName"></param>
        public void ChangeNameLoadthreeSavethree(string saveName)
        {
            placeHolderName[2] = saveName;
            User user;
            try
            {
                user = game.Repository.ReadUser(3);
            }
            catch (System.Exception)
            {

                user = null;
            }
            if (user != null)
            {
                game.Repository.UpdateUser(3, saveName);
            }
            else { game.Repository.AddUser(placeHolderName[2]); }
        }
        /// <summary>
        /// Handels the drawing of the UI. It calls another methode, DrawingMenu to make it eaiser on the eyes to read.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            DrawingMenu(spriteBatch);
            spriteBatch.End();
        }
        /// <summary>
        /// When called it draws the UI for the menu.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawingMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuSprites[16], new Rectangle(0, 0, 1920, 1080), Color.White);
            spriteBatch.Draw(menuSprites[0], menuRec[0], Color.White);
            switch (clickedStuff)//Navigating the menu
            {
                case 0://MainMenu

                    for (int i = 1; i <= 4; i++)
                    {
                        if (menuRec[i].Contains(currentMouse.X, currentMouse.Y)) { spriteBatch.Draw(menuSprites[i], menuRec[i], Color.LightGray); }
                        else { spriteBatch.Draw(menuSprites[i], menuRec[i], Color.White); }
                    }
                    break;
                #region LoadGame Button
                case 1://LoadGame
                    spriteBatch.Draw(menuSprites[0], menuRec[8], Color.Gray);//background
                    if (menuRec[5].Contains(currentMouse.X, currentMouse.Y)) { spriteBatch.Draw(menuSprites[6], menuRec[5], Color.LightGray); }//Back
                    else { spriteBatch.Draw(menuSprites[6], menuRec[5], Color.White); }//Back
                    if (menuRec[6].Contains(currentMouse.X, currentMouse.Y)) { spriteBatch.Draw(menuSprites[7], menuRec[6], Color.LightGray); }//Load
                    else { spriteBatch.Draw(menuSprites[7], menuRec[6], Color.White); }//Load

                    for (int i = 9; i < 12; i++)//LoadUser
                    {
                        if (menuRec[i].Contains(currentMouse.X, currentMouse.Y)) { spriteBatch.Draw(menuSprites[5], menuRec[i], Color.LightGray); }
                        else { spriteBatch.Draw(menuSprites[5], menuRec[i], Color.White); }

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

                    spriteBatch.Draw(menuSprites[0], menuRec[8], Color.Gray);//background
                    if (menuRec[5].Contains(currentMouse.X, currentMouse.Y)) { spriteBatch.Draw(menuSprites[6], menuRec[5], Color.LightGray); }//Back
                    else { spriteBatch.Draw(menuSprites[6], menuRec[5], Color.White); }//Back
                    if (menuRec[7].Contains(currentMouse.X, currentMouse.Y)) { spriteBatch.Draw(menuSprites[8], menuRec[7], Color.LightGray); }///New user button
                    else { spriteBatch.Draw(menuSprites[8], menuRec[7], Color.White); }//New user button

                    for (int i = 12; i < 15; i++)//Draws 3 clickable buttons to so one can select which user slot one wants to use.
                    {
                        if (menuRec[i].Contains(currentMouse.X, currentMouse.Y)) { spriteBatch.Draw(menuSprites[5], menuRec[i], Color.LightGray); }
                        else { spriteBatch.Draw(menuSprites[5], menuRec[i], Color.White); }
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
