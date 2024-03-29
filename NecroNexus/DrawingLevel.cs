﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NecroNexus
{
    public class DrawingLevel
    {
        //Adds some Rectangles, Sprites and Fonts
        private Rectangle[] clickableButRec = new Rectangle[24];
        private Texture2D[] UISprites = new Texture2D[24];
        private Texture2D[] upgradeSpritesArray = new Texture2D[20];

        //Variables to track information
        private int menuButClicked = 0;
        private int whichUpgradeClicked = 0;
        private float timer;
        private float timer1;

        private bool[] presseddowntopleft = { false, false, false, false };
        private bool[] isHoveringOverIcon = { false, false, false, false };

        private bool escPressed = false;


        private MouseState previousMouse;
        private MouseState currentMouse;

        //private GameSaveLevelOne level;
        private Necromancer nc;
        private SkeletonArcher sk;
        private Hex hx;
        private SkeletonBrute brute;
        private Demon dm;
        private SkeletonBrute bt;
        private SummonFactory summons;
        private SpriteFont showLevelInfo;
        private LevelOne levelone;
        private GameWorld game;

        public int MenuButClicked { get { return menuButClicked; } set { menuButClicked = value; } }
        public static int GetCriptHealth { get; set; } = 100;
        public static int GetSouls { get; set; } = 15;


        public DrawingLevel(GameWorld game, LevelOne levelone)
        {
            this.game = game;
            this.levelone = levelone;
            summons = new SummonFactory();

        }
        public void LoadContent(ContentManager content)
        {
            #region UI place
            UISprites[0] = content.Load<Texture2D>("placeholdersprites/UI/BackGroundWithoutEdge");
            clickableButRec[0] = new Rectangle(0, 880, 1920, 200);
            UISprites[1] = content.Load<Texture2D>("placeholdersprites/UI/CharSpriteLV0");//CharImagelv0
            UISprites[2] = content.Load<Texture2D>("placeholdersprites/UI/CharSpriteLV1");//CharImagelv1
            UISprites[3] = content.Load<Texture2D>("placeholdersprites/UI/CharSpriteLV2");//CharImagelv2
            UISprites[4] = content.Load<Texture2D>("placeholdersprites/UI/CharSpriteLV3");//CharImagelv3
            UISprites[5] = content.Load<Texture2D>("placeholdersprites/UI/SummonsLevelOne");//SummonButton
            UISprites[6] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeLevelOne");//UpgradeButton
            UISprites[7] = content.Load<Texture2D>("placeholdersprites/UI/NextWaveBut");//NextWave
            UISprites[8] = content.Load<Texture2D>("placeholdersprites/UI/HealthSoulsWave");//Wave, Souls amount and wave count
            UISprites[9] = content.Load<Texture2D>("placeholdersprites/UI/PauseGameBut");//ActivLevelPauseButton
            UISprites[10] = content.Load<Texture2D>("placeholdersprites/UI/SummonsSkeletonArcherPurchase");//SkeletonArcherButton
            UISprites[11] = content.Load<Texture2D>("placeholdersprites/UI/SummonsHexPurchase");//HexButton
            UISprites[12] = content.Load<Texture2D>("placeholdersprites/UI/SummonsSkeletonPurchase");//SkeletonBruteButton
            UISprites[13] = content.Load<Texture2D>("placeholdersprites/UI/SummonsDemonPurchase");//DemonButton
            UISprites[14] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/SkeliArcherUpgradeIconBut");//ArcherUpgrade
            UISprites[15] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/HexUpgradeIconBut");//HexUpgrade
            UISprites[16] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/SkeliBruteUpgradeIconBut");//BruteUpgrade
            UISprites[17] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/DemonUpgradeIconBut");//DemonUpgrade
            UISprites[18] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/PlayerUpgradeIconBut");//PlayerUpgrade
            UISprites[19] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/InvetoryTitleUpgrade");//UpgradeButton
            UISprites[20] = content.Load<Texture2D>("Summons/SkeletonArcher/tile000");//SkeliArcher HoverIcon
            UISprites[21] = content.Load<Texture2D>("Summons/Hex/tile000");//Hex HoverIcon
            UISprites[22] = content.Load<Texture2D>("Summons/SkeletonBrute/tile000");//SkeliBrute HoverIcon
            UISprites[23] = content.Load<Texture2D>("Summons/Demon/tile000");//Demon HoverIcon
            clickableButRec[1] = new Rectangle(5, 885, 320, 190);//Rec for Char image
            clickableButRec[2] = new Rectangle(330, 885, 687, 190);//Rec for Summons Button
            clickableButRec[3] = new Rectangle(1022, 885, 697, 190);//Rec for Upgrade Button
            clickableButRec[4] = new Rectangle(1724, 885, 191, 190);//Rec for NextWave Button

            clickableButRec[5] = new Rectangle(420, 255, 1040, 551);//Upgrade or Summons click background
            clickableButRec[6] = new Rectangle(470, 305, 445, 200);//TopLeft Summon
            clickableButRec[7] = new Rectangle(470, 555, 445, 200);//ButtomLeft Summon
            clickableButRec[8] = new Rectangle(965, 305, 445, 200);//TopRight Summon
            clickableButRec[9] = new Rectangle(965, 555, 445, 200);//ButtomRight Summon

            int xPos = 470, yPos = 305;
            for (int i = 10; i <= 14; i++)//Summons icons for upgrade
            {
                //10 for first image, 11 for second image, 12 for third image, 13 for fourth image and 14 for fifth image
                clickableButRec[i] = new Rectangle(xPos, yPos, 125, 125);
                xPos += (75 + 125);
            }
            clickableButRec[15] = new Rectangle(470, 469, 624, 287);//See information about upgrade rec
            clickableButRec[16] = new Rectangle(1192, 638, 180, 75);// Upgrade button
            clickableButRec[17] = new Rectangle(125, 0, 500, 75);//Health,Souls,Wave
            clickableButRec[18] = new Rectangle(1800, 0, 50, 50);//PauseGame
            showLevelInfo = content.Load<SpriteFont>("placeholdersprites/UI/UITextElements/File");
            #endregion
            #region Updrage Sprite
            for (int i = 0; i < 20; i++)
            {
                /*
                0 - 3 is Archer
                4 - 7 is Hex
                8 - 11 is Brute
                12 - 15 is Demon
                16 - 19 is player
                Üse clickablebutrec 15 for size
                */
                upgradeSpritesArray[i] = content.Load<Texture2D>($"placeholdersprites/UI/UpgradeUIelements/UpgradeSpritesTier{i}");

            }
            #endregion

        }

        public void Update()
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();//enables you to click with the currentMouse
            CheckingIfClicked();

            nc = (Necromancer)levelone.GetChar().GetComponent<Necromancer>();
            sk = (SkeletonArcher)GetSummonGo(1).GetComponent<SkeletonArcher>();
            hx = (Hex)GetSummonGo(2).GetComponent<Hex>();
            bt = (SkeletonBrute)GetSummonGo(3).GetComponent<SkeletonBrute>();
            dm = (Demon)GetSummonGo(4).GetComponent<Demon>();

            //Close Menu
            if (menuButClicked != 0 && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                menuButClicked = 0;
                escPressed = true;
            }
            else if (escPressed == true && Keyboard.GetState().IsKeyUp(Keys.Escape)) { escPressed = false; }
            //If menu closed call method PausingGame
            if (escPressed == false && MenuButClicked == 0 && Keyboard.GetState().IsKeyDown(Keys.Escape)) { PausingGame(); }
        }

        /// <summary>
        /// This class handles the entire interation between the user and the UI, the information here is then used down in DrawingUI
        /// menuButClicked == 2 is for the summons menu
        /// menuButClicked == 3 is the the upgrade menu.
        /// menuButClicked == 4 is for the next wave button.
        /// --------------------------Thorbjørn----------------------------//
        /// </summary>
        private void CheckingIfClicked()
        {
            if (previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                //Open the summons menu
                if (menuButClicked == 0 && clickableButRec[2].Contains(currentMouse.X, currentMouse.Y)
                    || menuButClicked == 3 && clickableButRec[2].Contains(currentMouse.X, currentMouse.Y))
                {
                    if (!presseddowntopleft[0] && !presseddowntopleft[1] && !presseddowntopleft[2] && !presseddowntopleft[3])
                    { AudioEffect.ButtonClickingSound(); menuButClicked = 2; }
                    else return;
                }
                else if (menuButClicked == 2 && clickableButRec[2].Contains(currentMouse.X, currentMouse.Y)) { menuButClicked = 0; }
                //buy summons menu.
                if (menuButClicked == 2)
                {
                    timer += GameWorld.DeltaTime;

                    //So you can close the summons menu if clicked on again.
                    if (timer >= 0.25f && menuButClicked == 2 && clickableButRec[2].Contains(currentMouse.X, currentMouse.Y)) { menuButClicked = 0; timer = 0; }

                    //archer
                    if (clickableButRec[6].Contains(currentMouse.X, currentMouse.Y))
                    {
                        if (GetSouls >= 15) { GetSouls -= 15; menuButClicked = 0; presseddowntopleft[0] = true; AudioEffect.ButtonClickingSound(); } else { return; }
                    }
                    //Hex
                    if (clickableButRec[7].Contains(currentMouse.X, currentMouse.Y))
                    {
                        if (GetSouls >= 20) { GetSouls -= 20; menuButClicked = 0; presseddowntopleft[1] = true; AudioEffect.ButtonClickingSound(); } else { return; }
                    }
                    ////Brute
                    //if (clickableButRec[8].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                    //{
                    //    if (GetSouls >= 30) { GetSouls -= 30; menuButClicked = 0; presseddowntopleft[2] = true; AudioEffect.ButtonClickingSound(); } else { return; }
                    //}
                    //Demon
                    if (clickableButRec[9].Contains(currentMouse.X, currentMouse.Y))
                    {
                        if (GetSouls >= 40) { GetSouls -= 40; menuButClicked = 0; presseddowntopleft[3] = true; AudioEffect.ButtonClickingSound(); } else { return; }
                    }
                }
                //Open the upgrade menu
                if (menuButClicked == 0 && clickableButRec[3].Contains(currentMouse.X, currentMouse.Y)
                    || menuButClicked == 2 && clickableButRec[3].Contains(currentMouse.X, currentMouse.Y))
                {
                    if (!presseddowntopleft[0] && !presseddowntopleft[1] && !presseddowntopleft[2] && !presseddowntopleft[3])
                    { AudioEffect.ButtonClickingSound(); menuButClicked = 3; }
                    else return;
                }
                else if (menuButClicked == 3 && clickableButRec[3].Contains(currentMouse.X, currentMouse.Y)) { menuButClicked = 0; }



                //Choose upgrade
                if (menuButClicked == 3)
                {
                    timer += GameWorld.DeltaTime;
                    //So you can close the upgrade menu if clicked on again.
                    if (timer >= 0.5f && menuButClicked == 3 && clickableButRec[3].Contains(currentMouse.X, currentMouse.Y)) { AudioEffect.ButtonClickingSound(); menuButClicked = 0; timer = 0; }
                    //Skeleton arhcer Upgrade icon.
                    if (clickableButRec[10].Contains(currentMouse.X, currentMouse.Y)) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 1; }

                    //Hex Upgrade icon
                    if (clickableButRec[11].Contains(currentMouse.X, currentMouse.Y)) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 2; }

                    //Skeleton Brute Upgrade icon.
                    //if (clickableButRec[12].Contains(currentMouse.X, currentMouse.Y)) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 3; }
                    //Demon Upgrade icon.
                    if (clickableButRec[13].Contains(currentMouse.X, currentMouse.Y)) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 4; }

                    //Player Upgrade icon.
                    if (clickableButRec[14].Contains(currentMouse.X, currentMouse.Y)) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 5; }

                    //UpgradeButtonClicked
                    if (clickableButRec[16].Contains(currentMouse.X, currentMouse.Y))
                    {
                        AudioEffect.ButtonClickingSound();
                        if (whichUpgradeClicked == 0)
                        {
                            return;
                        }
                        else // if an icon and the upgrade button has been clicked
                        {
                            switch (whichUpgradeClicked)
                            {
                                case 1://Skeleton Archer Upgrade.
                                    switch (sk.Tier)
                                    {
                                        case 0: //level 0 to 1.
                                            if (GetSouls >= 10)
                                            {
                                                GetSouls -= 10;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Archer")
                                                    {
                                                        sk = (SkeletonArcher)item.GetComponent<SkeletonArcher>();
                                                        sk.SetTier(1);
                                                    }
                                                }
                                            }
                                            break;
                                        case 1: //level 1 to 2.
                                            if (GetSouls >= 20)
                                            {
                                                GetSouls -= 20;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Archer")
                                                    {
                                                        sk = (SkeletonArcher)item.GetComponent<SkeletonArcher>();
                                                        sk.SetTier(2);
                                                    }
                                                }
                                            }
                                            break;
                                        case 2: //level 2 to 3.
                                            if (GetSouls >= 30)
                                            {
                                                GetSouls -= 30;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Archer")
                                                    {
                                                        sk = (SkeletonArcher)item.GetComponent<SkeletonArcher>();
                                                        sk.SetTier(3);
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                    break;
                                case 2: //Hex Upgrade.
                                    switch (hx.Tier)
                                    {
                                        case 0: //level 0 to 1.
                                            if (GetSouls >= 10)
                                            {
                                                GetSouls -= 10;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Hex")
                                                    {
                                                        hx = (Hex)item.GetComponent<Hex>();
                                                        hx.SetTier(1);
                                                    }
                                                }
                                            }
                                            break;
                                        case 1: //level 1 to 2.
                                            if (GetSouls >= 20)
                                            {
                                                GetSouls -= 20;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Hex")
                                                    {
                                                        hx = (Hex)item.GetComponent<Hex>();
                                                        hx.SetTier(2);
                                                    }
                                                }
                                            }
                                            break;
                                        case 2: //level 2 to 3.
                                            if (GetSouls >= 30)
                                            {
                                                GetSouls -= 30;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Hex")
                                                    {
                                                        hx = (Hex)item.GetComponent<Hex>();
                                                        hx.SetTier(3);
                                                    }
                                                }
                                            }
                                            break;

                                    }
                                    break;
                                case 3: //Skeleton Brute Upgrade.
                                    //switch (bt.Tier)
                                    //{

                                    //    case 0: //level 0 to 1.
                                    //        if (GetSouls >= 20)
                                    //        {
                                    //            GetSouls -= 20;
                                    //            menuButClicked = 0;
                                    //            foreach (var item in LevelOne.gameObjects)
                                    //            {
                                    //                if (item.Tag == "Brute")
                                    //                {
                                    //                    bt = (SkeletonBrute)item.GetComponent<SkeletonBrute>();
                                    //                    bt.SetTier(1);
                                    //                }
                                    //            }
                                    //        }
                                    //        break;
                                    //    case 1: //level 1 to 2.
                                    //        if (GetSouls >= 30)
                                    //        {
                                    //            GetSouls -= 30;
                                    //            menuButClicked = 0;
                                    //            foreach (var item in LevelOne.gameObjects)
                                    //            {
                                    //                if (item.Tag == "Brute")
                                    //                {
                                    //                    bt = (SkeletonBrute)item.GetComponent<SkeletonBrute>();
                                    //                    bt.SetTier(2);
                                    //                }
                                    //            }
                                    //        }
                                    //        break;
                                    //    case 2: //level 2 to 3.
                                    //        if (GetSouls >= 50)
                                    //        {
                                    //            GetSouls -= 50;
                                    //            menuButClicked = 0;
                                    //            foreach (var item in LevelOne.gameObjects)
                                    //            {
                                    //                if (item.Tag == "Brute")
                                    //                {
                                    //                    bt = (SkeletonBrute)item.GetComponent<SkeletonBrute>();
                                    //                    bt.SetTier(3);
                                    //                }
                                    //            }
                                    //        }
                                    //        break;
                                    //}
                                    //break;
                                case 4: //Demon Upgrade.
                                    switch (dm.Tier)
                                    {

                                        case 0: //level 0 to 1.
                                            if (GetSouls >= 20)
                                            {
                                                GetSouls -= 20;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Demon")
                                                    {
                                                        dm = (Demon)item.GetComponent<Demon>();
                                                        dm.SetTier(1);
                                                    }
                                                }
                                            }
                                            break;
                                        case 1: //level 1 to 2.
                                            if (GetSouls >= 30)
                                            {
                                                GetSouls -= 30;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Demon")
                                                    {
                                                        dm = (Demon)item.GetComponent<Demon>();
                                                        dm.SetTier(2);
                                                    }
                                                }
                                            }
                                            break;
                                        case 2: //level 2 to 3.
                                            if (GetSouls >= 50)
                                            {
                                                GetSouls -= 50;
                                                menuButClicked = 0;
                                                foreach (var item in LevelOne.gameObjects)
                                                {
                                                    if (item.Tag == "Demon")
                                                    {
                                                        dm = (Demon)item.GetComponent<Demon>();
                                                        dm.SetTier(3);
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                    break;
                                case 5: //Player Upgrade.
                                    switch (nc.Tier)
                                    {
                                        case 0: //level 0 to 1.
                                            if (GetSouls >= 10) { GetSouls -= 10; nc.Tier = 1; menuButClicked = 0; }
                                            break;
                                        case 1: //level 1 to 2.

                                            if (GetSouls >= 20) { GetSouls -= 20; nc.Tier = 2; menuButClicked = 0; }
                                            break;
                                        case 2: //level 2 to 3.
                                            if (GetSouls >= 30) { GetSouls -= 30; nc.Tier = 3; menuButClicked = 0; }
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }
                //Checks to see if you click the pause button.
                if (clickableButRec[18].Contains(currentMouse.X, currentMouse.Y))
                {
                    PausingGame();
                }
            }
            //Checks to see if you have clicked the nextwave
            if (menuButClicked == 4 || clickableButRec[4].Contains(currentMouse.X, currentMouse.Y) && (previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released))
            {
                menuButClicked = 4;
                //Start next wave
                timer += GameWorld.DeltaTime;
                if (timer > 0.5f)
                {
                    AudioEffect.ButtonClickingSound();
                    levelone.level.StartNextWave();
                    menuButClicked = 0;
                    timer = 0;
                }
            }
            HoveringSummons();

            //Checks to see if you have lost the game.
            if (GetCriptHealth <= 0)
            {
                game.ChangeState(game.LostGame);
            }
        }
        private void PausingGame()
        {
            AudioEffect.ButtonClickingSound();
            game.ChangeState(game.PauseMenuState);
        }
        /// <summary>
        /// This class is called when the player has clicked on a summon they want to buy. 
        /// It checks the which summon has been clicked and then toggels an array that i then used in DrawingUI for drawing the hovering icons.
        /// --------------------------Thorbjørn----------------------------//
        /// </summary>
        private void HoveringSummons()
        {
            //So you can't click in the area of the UI.
            if (currentMouse.Y <= 880)
            {
                if (presseddowntopleft[0] == true)
                {
                    timer1 += GameWorld.DeltaTime;
                    if (timer1 >= 0.5f && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        presseddowntopleft[0] = false;
                        timer1 = 0f;
                        AudioEffect.SummonTurret1();
                        summons.Create(SummonType.SkeletonArcher, new Vector2(currentMouse.X, currentMouse.Y));
                    }
                }
                if (presseddowntopleft[1] == true)
                {
                    timer1 += GameWorld.DeltaTime;
                    if (timer1 >= 0.5f && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        presseddowntopleft[1] = false;
                        timer1 = 0f;
                        AudioEffect.SummonTurret2();
                        summons.Create(SummonType.Hex, new Vector2(currentMouse.X, currentMouse.Y));
                    }
                }
                if (presseddowntopleft[2] == true)
                {
                    timer1 += GameWorld.DeltaTime;
                    if (timer1 >= 0.5f && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        presseddowntopleft[2] = false;
                        timer1 = 0f;
                        AudioEffect.SummonTurret1();
                        summons.Create(SummonType.SkeletonBrute, new Vector2(currentMouse.X, currentMouse.Y));
                    }
                }
                if (presseddowntopleft[3] == true)
                {
                    timer1 += GameWorld.DeltaTime;
                    if (timer1 >= 0.5f && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        presseddowntopleft[3] = false;
                        timer1 = 0f;
                        AudioEffect.SummonTurret2();
                        summons.Create(SummonType.Demon, new Vector2(currentMouse.X, currentMouse.Y));
                    }
                }
            }
            //makes so you can only click when the Upgrades tab has been clicked
            if (menuButClicked == 2)
            {
                if (clickableButRec[6].Contains(currentMouse.X, currentMouse.Y)) { isHoveringOverIcon[0] = true; }
                else { isHoveringOverIcon[0] = false; }
                if (clickableButRec[7].Contains(currentMouse.X, currentMouse.Y)) { isHoveringOverIcon[1] = true; }
                else { isHoveringOverIcon[1] = false; }
                if (clickableButRec[8].Contains(currentMouse.X, currentMouse.Y)) { isHoveringOverIcon[2] = true; }
                else { isHoveringOverIcon[2] = false; }
                if (clickableButRec[9].Contains(currentMouse.X, currentMouse.Y)) { isHoveringOverIcon[3] = true; }
                else { isHoveringOverIcon[3] = false; }
            }
        }
        /// <summary>
        /// When called it handles the drawing of the UI for the active level.
        /// --------------------------Thorbjørn----------------------------//
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawingUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UISprites[0], clickableButRec[0], Color.White);
            //Health, Souls and Wave count
            spriteBatch.Draw(UISprites[8], clickableButRec[17], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);
            spriteBatch.DrawString(showLevelInfo, GetCriptHealth.ToString(), new Vector2(205, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(showLevelInfo, GetSouls.ToString(), new Vector2(315, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(showLevelInfo, levelone.level.CurrentWave.ToString() + " / 10", new Vector2(540, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

            //ActivLevelPauseButton
            spriteBatch.Draw(UISprites[9], clickableButRec[18], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);

            //for showing the player level
            switch (nc.Tier)
            {
                case 0:
                    //Chance this so the player icon changes with the level
                    spriteBatch.Draw(UISprites[1], clickableButRec[1], Color.White);//Char Image
                    break;
                case 1:
                    //Chance this so the player icon changes with the level
                    spriteBatch.Draw(UISprites[2], clickableButRec[1], Color.White);//Char Image
                    break;
                case 2:
                    //Chance this so the player icon changes with the level
                    spriteBatch.Draw(UISprites[3], clickableButRec[1], Color.White);//Char Image
                    break;
                case 3:
                    //Chance this so the player icon changes with the level
                    spriteBatch.Draw(UISprites[4], clickableButRec[1], Color.White);//Char Image
                    break;
            }

            if (menuButClicked != 2 && clickableButRec[2].Contains(currentMouse.X, currentMouse.Y))
            {
                spriteBatch.Draw(UISprites[5], clickableButRec[2], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);//SummonsBut
            }
            else if (menuButClicked != 3 && clickableButRec[3].Contains(currentMouse.X, currentMouse.Y))
            {
                spriteBatch.Draw(UISprites[6], clickableButRec[3], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);//Upgrade summon and playerBut
            }
            else if (menuButClicked != 4 && clickableButRec[4].Contains(currentMouse.X, currentMouse.Y))
            {
                spriteBatch.Draw(UISprites[7], clickableButRec[4], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 1f);//Next WaveBut
            }

            if (menuButClicked == 2) { }
            else { spriteBatch.Draw(UISprites[5], clickableButRec[2], Color.White); }//SummonsBut
            if (menuButClicked == 3) { }
            else { spriteBatch.Draw(UISprites[6], clickableButRec[3], Color.White); }//Upgrade summon and playerButton
            if (menuButClicked == 4) { }
            else { spriteBatch.Draw(UISprites[7], clickableButRec[4], Color.White); }//Next WaveBut

            switch (menuButClicked)
            {
                case 2://Summons
                    #region Summons
                    spriteBatch.Draw(UISprites[5], clickableButRec[2], Color.DarkGray);//SummonBut
                    spriteBatch.Draw(UISprites[0], clickableButRec[5], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);//Backgroundboxs for selection of summons

                    if (clickableButRec[6].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[10], clickableButRec[6], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//Archer TopLeft.
                    }
                    else { spriteBatch.Draw(UISprites[10], clickableButRec[6], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f); }//Archer TopLeft.
                    spriteBatch.DrawString(showLevelInfo, sk.SkDamage.ToString(), new Vector2(765, 330), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, sk.Range.ToString(), new Vector2(765, 385), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, sk.FireRate.ToString(), new Vector2(765, 440), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    if (clickableButRec[7].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[11], clickableButRec[7], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//ButtomLeft.
                    }
                    else { spriteBatch.Draw(UISprites[11], clickableButRec[7], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f); }//ButtomLeft.
                    spriteBatch.DrawString(showLevelInfo, hx.HexDamage.ToString(), new Vector2(770, 575), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, hx.Range.ToString(), new Vector2(770, 630), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, hx.FireRate.ToString(), new Vector2(770, 685), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    if (clickableButRec[8].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[12], clickableButRec[8], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//Topright.
                    }
                    else { spriteBatch.Draw(UISprites[12], clickableButRec[8], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f); }//Topright.
                    spriteBatch.DrawString(showLevelInfo, bt.BruteDamage.ToString(), new Vector2(1260, 330), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, bt.Range.ToString(), new Vector2(1260, 385), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, bt.FireRate.ToString(), new Vector2(1260, 440), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    if (clickableButRec[9].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[13], clickableButRec[9], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//ButtomRight.
                    }
                    else { spriteBatch.Draw(UISprites[13], clickableButRec[9], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f); }//ButtomRight.
                    spriteBatch.DrawString(showLevelInfo, dm.DemonDamage.ToString(), new Vector2(1260, 575), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, dm.Range.ToString(), new Vector2(1260, 630), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, dm.FireRate.ToString(), new Vector2(1260, 685), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    if (isHoveringOverIcon[0] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "\n15", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1.75f, SpriteEffects.None, 1f);
                    }
                    else if (isHoveringOverIcon[1] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "\n20", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1.75f, SpriteEffects.None, 1f);
                    }
                    else if (isHoveringOverIcon[2] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "\nUnavailable :(", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1.75f, SpriteEffects.None, 1f);
                    }
                    else if (isHoveringOverIcon[3] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "\n40", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1.75f, SpriteEffects.None, 1f);
                    }
                    break;
                #endregion
                case 3://Upgrade
                    #region Upgrade
                    spriteBatch.Draw(UISprites[6], clickableButRec[3], Color.DarkGray);
                    spriteBatch.Draw(UISprites[0], clickableButRec[5], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);

                    //clickableButRec 10 for first image, 11 for second image, 12 for third image, 13 for fourth image and 14 for fifth image
                    int b = 14;
                    for (int i = 10; i < 15; i++)
                    {
                        if (clickableButRec[i].Contains(currentMouse.X, currentMouse.Y))
                        {
                            spriteBatch.Draw(UISprites[b], clickableButRec[i], null, Color.LightGray, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                            b++;
                        }
                        else { spriteBatch.Draw(UISprites[b], clickableButRec[i], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f); b++; }
                    }
                    b = 14;


                    if (clickableButRec[16].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[19], clickableButRec[16], null, Color.LightGray, 0f, new Vector2(0), SpriteEffects.None, 0.91f);//Upgrade Button
                    }
                    else { spriteBatch.Draw(UISprites[19], clickableButRec[16], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f); }//Upgrade Button

                    switch (whichUpgradeClicked)
                    {
                        case 1://Archer
                            spriteBatch.DrawString(showLevelInfo, sk.SkDamage.ToString(), new Vector2(730, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, sk.Range.ToString(), new Vector2(730, 605), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, sk.FireRate.ToString(), new Vector2(730, 660), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                            spriteBatch.DrawString(showLevelInfo, sk.Range.ToString(), new Vector2(1030, 605), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, sk.FireRate.ToString(), new Vector2(1030, 660), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            switch (sk.Tier)
                            {
                                case 0:
                                    spriteBatch.Draw(upgradeSpritesArray[0], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 2.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 10.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 1:
                                    spriteBatch.Draw(upgradeSpritesArray[1], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 3.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 20.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 2:
                                    spriteBatch.Draw(upgradeSpritesArray[2], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 4.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 30.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 3:
                                    spriteBatch.Draw(upgradeSpritesArray[3], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, sk.SkDamage.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "Max", new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                            }
                            break;
                        case 2://Hex
                            spriteBatch.DrawString(showLevelInfo, hx.HexDamage.ToString(), new Vector2(730, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, hx.Range.ToString(), new Vector2(730, 605), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, hx.FireRate.ToString(), new Vector2(730, 660), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                            spriteBatch.DrawString(showLevelInfo, hx.Range.ToString(), new Vector2(1030, 605), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, hx.FireRate.ToString(), new Vector2(1030, 660), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            switch (hx.Tier)
                            {
                                case 0:
                                    spriteBatch.Draw(upgradeSpritesArray[4], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 6.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 10.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 1:
                                    spriteBatch.Draw(upgradeSpritesArray[5], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 10.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 20.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 2:
                                    spriteBatch.Draw(upgradeSpritesArray[6], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 15.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 30.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 3:
                                    spriteBatch.Draw(upgradeSpritesArray[7], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, hx.HexDamage.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "Max".ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                            }
                            break;
                        case 3://Brute
                            spriteBatch.DrawString(showLevelInfo, bt.BruteDamage.ToString(), new Vector2(730, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, bt.Range.ToString(), new Vector2(730, 605), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, bt.FireRate.ToString(), new Vector2(730, 660), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                            spriteBatch.DrawString(showLevelInfo, bt.Range.ToString(), new Vector2(1030, 605), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, bt.FireRate.ToString(), new Vector2(1030, 660), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            switch (bt.Tier)
                            {
                                case 0:
                                    spriteBatch.Draw(upgradeSpritesArray[8], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 6.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 10.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 1:
                                    spriteBatch.Draw(upgradeSpritesArray[9], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 10.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 20.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 2:
                                    spriteBatch.Draw(upgradeSpritesArray[10], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 15.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 30.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 3:
                                    spriteBatch.Draw(upgradeSpritesArray[11], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, bt.BruteDamage.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "Max".ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                            }
                            break;
                        case 4:// Demon
                            spriteBatch.DrawString(showLevelInfo, dm.DemonDamage.ToString(), new Vector2(730, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, dm.Range.ToString(), new Vector2(730, 605), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, dm.FireRate.ToString(), new Vector2(730, 660), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                            spriteBatch.DrawString(showLevelInfo, dm.Range.ToString(), new Vector2(1030, 605), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            spriteBatch.DrawString(showLevelInfo, dm.FireRate.ToString(), new Vector2(1030, 660), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                            switch (dm.Tier)
                            {
                                case 0:
                                    spriteBatch.Draw(upgradeSpritesArray[12], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 15.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 20.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 1:
                                    spriteBatch.Draw(upgradeSpritesArray[13], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 20.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 30.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 2:
                                    spriteBatch.Draw(upgradeSpritesArray[14], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 40.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, 50.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                                case 3:
                                    spriteBatch.Draw(upgradeSpritesArray[15], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, dm.DemonDamage.ToString(), new Vector2(1030, 550), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "Max", new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                            }
                            break;
                        case 5://Player
                            switch (nc.Tier)
                            {
                                case 0:
                                    spriteBatch.Draw(upgradeSpritesArray[16], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 10.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "+ Homing", new Vector2(950, 625), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    break;
                                case 1:
                                    spriteBatch.Draw(upgradeSpritesArray[17], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 20.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "+ Damage", new Vector2(950, 625), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "+ Double", new Vector2(950, 650), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    break;
                                case 2:
                                    spriteBatch.Draw(upgradeSpritesArray[18], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, 30.ToString(), new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "+ Damage", new Vector2(950, 625), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    spriteBatch.DrawString(showLevelInfo, "+ Explosion", new Vector2(950, 650), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                                    break;
                                case 3:
                                    spriteBatch.Draw(upgradeSpritesArray[19], clickableButRec[15], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                                    spriteBatch.DrawString(showLevelInfo, "Max", new Vector2(1250, 550), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1f);
                                    break;
                            }
                            break;
                    }
                    break;
                #endregion
                case 4://NextWave
                    spriteBatch.Draw(UISprites[7], clickableButRec[4], Color.DarkGray);
                    break;
            }
            if (presseddowntopleft[0] == true)
            {
                spriteBatch.Draw(UISprites[20], new Vector2(currentMouse.X, currentMouse.Y), null, Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0.9f);
            }
            else if (presseddowntopleft[1] == true)
            {
                spriteBatch.Draw(UISprites[21], new Vector2(currentMouse.X, currentMouse.Y), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
            }
            else if (presseddowntopleft[2] == true)
            {
                spriteBatch.Draw(UISprites[22], new Vector2(currentMouse.X, currentMouse.Y), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
            }
            else if (presseddowntopleft[3] == true)
            {
                spriteBatch.Draw(UISprites[23], new Vector2(currentMouse.X, currentMouse.Y), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.9f);
            }

        }

        /// <summary>
        /// When called you get back the gameobject for which summon you want to grab.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GameObject GetSummonGo(int value)
        {
            switch (value)
            {
                case 1:
                    foreach (GameObject item in LevelOne.gameObjects)
                    {
                        if (item.Tag == "Archer")
                        {
                            return item;
                        }
                    }
                    return null;
                case 2:
                    foreach (GameObject item in LevelOne.gameObjects)
                    {
                        if (item.Tag == "Hex")
                        {
                            return item;
                        }
                    }
                    return null;
                case 3:
                    foreach (GameObject item in LevelOne.gameObjects)
                    {
                        if (item.Tag == "Brute")
                        {
                            return item;
                        }
                    }
                    return null;
                case 4:
                    foreach (GameObject item in LevelOne.gameObjects)
                    {
                        if (item.Tag == "Demon")
                        {
                            return item;
                        }
                    }
                    return null;
            }
            return null;
        }

        /// <summary>
        /// A Static method for Updating the Levels Soul Count
        /// </summary>
        /// <param name="value">The amount added to the existing soul count</param>
        public static void UpdateSouls(float value)
        {
            GetSouls += (int)value;
        }

        /// <summary>
        /// A Static method for Updating the Health of your Base in the Level
        /// </summary>
        /// <param name="value">The value you want removed from the current Value</param>
        public static void UpdateHealth(int value)
        {
            GetCriptHealth -= value;
        }



    }

}
