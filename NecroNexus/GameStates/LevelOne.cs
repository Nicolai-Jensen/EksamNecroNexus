﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    public class LevelOne : State
    {
        //A Texture variable for our background

        private Texture2D[] backgroundsprite;
        private Board boardOne;
        private GameSaveLevelOne level;
        private Map map;

        private SummonFactory summons;

        private Rectangle[] clickableButRec = new Rectangle[24];
        private Texture2D[] UISprites = new Texture2D[24];
        private SpriteFont showLevelInfo;
        private int menuButClicked = 0;
        private int whichUpgradeClicked = 0;
        private float timer;
        private bool[] presseddowntopleft = { false, false, false, false };
        private bool[] isHoveringOverIcon = { false, false, false, false };
        public int GetCriptHealth { get; set; }
        public int GetSouls { get; set; }
        public int GetWaveCount { get; set; }



        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> addGameObjects = new List<GameObject>();
        public static List<GameObject> removedGameObjects = new List<GameObject>();
        public static List<Collider> Colliders { get; private set; } = new List<Collider>();


        //2 variables to control key presses
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        private MouseState previousMouse;
        private MouseState currentMouse;

        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public LevelOne(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            map = new Map();
            boardOne = new Board(new Vector2(700, GameWorld.ScreenSize.Y / 2));
            summons = new SummonFactory();
        }

        public override void Initialize()
        {          
            boardOne.LevelOneBoard(map.ReturnPos(map.Graph1()));
            level = new GameSaveLevelOne(boardOne);
            Director director = new Director(new NecroBuilder());



            gameObjects.Add(director.Construct());


            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Awake();
            }
        }

        public override void LoadContent()
        {
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
            UISprites[14] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/DemonUpgradeIconBut");//DemonUpgrade
            UISprites[15] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/HexUpgradeIconBut");//HexUpgrade
            UISprites[16] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/SkeliArcherUpgradeIconBut");//ArcherUpgrade
            UISprites[17] = content.Load<Texture2D>("placeholdersprites/UI/UpgradeUIelements/SkeliBruteUpgradeIconBut");//BruteUpgrade
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

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Start();
            }

        }

        /// <summary>
        /// A way to ensure single key presses when taking action
        /// </summary>
        public override void Update()
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();//enables you to click with the currentMouse
            CheckingIfClicked();

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            level.CheckWave();

            GameObjectsToRemove();
            Cleanup();
        }
        private void CheckingIfClicked()
        {
            //Open the summons menu
            if (clickableButRec[2].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            { if (!presseddowntopleft[0] && !presseddowntopleft[1] && !presseddowntopleft[2] && !presseddowntopleft[3]) { menuButClicked = 2; } else return; }
            if (menuButClicked == 2)
            {
                if (clickableButRec[6].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    menuButClicked = 0;
                    presseddowntopleft[0] = true;
                }
                if (clickableButRec[7].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    menuButClicked = 0;
                    presseddowntopleft[1] = true;
                }
                if (clickableButRec[8].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    menuButClicked = 0;
                    presseddowntopleft[2] = true;
                }
                if (clickableButRec[9].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    menuButClicked = 0;
                    presseddowntopleft[3] = true;
                }
            }

            HoveringSummons();

            //Open the upgrade menu
            if (clickableButRec[3].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            { if (!presseddowntopleft[0] && !presseddowntopleft[1] && !presseddowntopleft[2] && !presseddowntopleft[3]) { menuButClicked = 3; } else return; }
            if (menuButClicked == 4 || clickableButRec[4].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                //Start next wave
                menuButClicked = 4;
                timer += GameWorld.DeltaTime;
                if (timer > 0.5f)
                {
                    level.StartNextWave();
                    menuButClicked = 0;
                    timer = 0;
                }

            }
            if (menuButClicked == 3)//Choose upgrade
            {
                if (clickableButRec[10].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                { whichUpgradeClicked = 1; }
                if (clickableButRec[11].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 2; }
                if (clickableButRec[12].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 3; }
                if (clickableButRec[13].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 4; }
                if (clickableButRec[14].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 5; }
                //UpgradeButtonClicked
                if (clickableButRec[16].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    if (whichUpgradeClicked == 0)
                    {
                        return;
                    }
                    else //Make a check to see the if you have enough monz
                    {
                        menuButClicked = 0;
                        switch (whichUpgradeClicked)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                        }

                    }


                }

            }
            if (clickableButRec[18].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                game.ChangeState(game.PauseMenuState);
            }
        }
        private void HoveringSummons()
        {
            if (currentMouse.Y <= 880)
            {
                if (presseddowntopleft[0] == true)
                {
                    timer += GameWorld.DeltaTime;
                    if (timer >= 0.5f && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        presseddowntopleft[0] = false;
                        timer = 0f;
                        summons.Create(SummonType.SkeletonArcher, new Vector2(currentMouse.X, currentMouse.Y));
                    }
                }
                if (presseddowntopleft[1] == true)
                {
                    timer += GameWorld.DeltaTime;
                    if (timer >= 0.5f && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        presseddowntopleft[1] = false;
                        timer = 0f;
                        summons.Create(SummonType.Hex, new Vector2(currentMouse.X, currentMouse.Y));
                    }
                }
                if (presseddowntopleft[2] == true)
                {
                    timer += GameWorld.DeltaTime;
                    if (timer >= 0.5f && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        presseddowntopleft[2] = false;
                        timer = 0f;
                        summons.Create(SummonType.SkeletonBrute, new Vector2(currentMouse.X, currentMouse.Y));
                    }
                }
                if (presseddowntopleft[3] == true)
                {
                    timer += GameWorld.DeltaTime;
                    if (timer >= 0.5f && currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        presseddowntopleft[3] = false;
                        timer = 0f;
                        summons.Create(SummonType.Demon, new Vector2(currentMouse.X, currentMouse.Y));
                    }
                }
            }
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

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            DrawingUI(spriteBatch);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }
        /// <summary>
        /// When called it handels the drawing of the UI for the active level.
        /// Thorbjørn
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawingUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UISprites[0], clickableButRec[0], Color.White);
            spriteBatch.Draw(UISprites[8], clickableButRec[17], Color.White);//Health, Souls and Wave count
            spriteBatch.DrawString(showLevelInfo, GetCriptHealth.ToString(), new Vector2(205, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(showLevelInfo, GetSouls.ToString(), new Vector2(315, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(showLevelInfo, GetWaveCount.ToString() + "/ 10", new Vector2(540, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

            spriteBatch.Draw(UISprites[9], clickableButRec[18], Color.White);//ActivLevelPauseButton

            //Chance this so the player icon changes with the level
            spriteBatch.Draw(UISprites[1], clickableButRec[1], Color.White);//Char Image

            if (menuButClicked == 2) { }
            else { spriteBatch.Draw(UISprites[5], clickableButRec[2], Color.White); }//SummonsBut
            if (menuButClicked == 3) { }
            else { spriteBatch.Draw(UISprites[6], clickableButRec[3], Color.White); }//Upgrade summon and playerBut
            if (menuButClicked == 4) { }
            else { spriteBatch.Draw(UISprites[7], clickableButRec[4], Color.White); }//Next WaveBut

            switch (menuButClicked)
            {
                case 2://Summons

                    spriteBatch.Draw(UISprites[5], clickableButRec[2], Color.DarkGray);//SummonBut
                    spriteBatch.Draw(UISprites[0], clickableButRec[5], Color.White);//Backgroundboxs for selection of summons

                    spriteBatch.Draw(UISprites[10], clickableButRec[6], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);//TopLeft.
                    spriteBatch.DrawString(showLevelInfo, "attack", new Vector2(765, 330), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, "range", new Vector2(765, 385), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, "firerate", new Vector2(765, 440), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    spriteBatch.Draw(UISprites[11], clickableButRec[7], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);//ButtomLeft.
                    spriteBatch.DrawString(showLevelInfo, "attack", new Vector2(770, 575), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, "range", new Vector2(770, 630), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, "firerate", new Vector2(770, 685), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    spriteBatch.Draw(UISprites[12], clickableButRec[8], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);//Topright.
                    spriteBatch.DrawString(showLevelInfo, "attack", new Vector2(1260, 330), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, "range", new Vector2(1260, 385), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, "firerate", new Vector2(1260, 440), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    spriteBatch.Draw(UISprites[13], clickableButRec[9], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);//ButtomRight.
                    spriteBatch.DrawString(showLevelInfo, "attack", new Vector2(1260, 575), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, "range", new Vector2(1260, 630), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, "firerate", new Vector2(1260, 685), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    if (isHoveringOverIcon[0] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "Price", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    }
                    if (isHoveringOverIcon[1] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "Price", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    }
                    if (isHoveringOverIcon[2] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "Price", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    }
                    if (isHoveringOverIcon[3] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "Price", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    }


                    break;
                case 3://Upgrade
                    spriteBatch.Draw(UISprites[6], clickableButRec[3], Color.DarkGray);
                    spriteBatch.Draw(UISprites[0], clickableButRec[5], Color.White);

                    //clickableButRec 10 for first image, 11 for second image, 12 for third image, 13 for fourth image and 14 for fifth image
                    spriteBatch.Draw(UISprites[14], clickableButRec[10], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(UISprites[15], clickableButRec[11], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(UISprites[16], clickableButRec[12], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(UISprites[17], clickableButRec[13], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                    spriteBatch.Draw(UISprites[18], clickableButRec[14], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);


                    spriteBatch.Draw(UISprites[19], clickableButRec[16], null, Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);//Upgrade Button

                    switch (whichUpgradeClicked)
                    {
                        case 1:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                            break;
                        case 2:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.Red, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                            break;
                        case 3:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.Yellow, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                            break;
                        case 4:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.Green, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                            break;
                        case 5:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.Blue, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                            break;
                    }

                    break;
                case 4://NextWave
                    spriteBatch.Draw(UISprites[7], clickableButRec[4], Color.DarkGray);
                    break;
            }
            if (presseddowntopleft[0] == true)
            {
                spriteBatch.Draw(UISprites[20], new Vector2(currentMouse.X, currentMouse.Y), Color.White);
            }
            else if (presseddowntopleft[1] == true)
            {
                spriteBatch.Draw(UISprites[21], new Vector2(currentMouse.X, currentMouse.Y), Color.White);
            }
            else if (presseddowntopleft[2] == true)
            {
                spriteBatch.Draw(UISprites[22], new Vector2(currentMouse.X, currentMouse.Y), Color.White);
            }
            else if (presseddowntopleft[3] == true)
            {
                spriteBatch.Draw(UISprites[23], new Vector2(currentMouse.X, currentMouse.Y), Color.White);
            }

        }

        public static void AddObject(GameObject go)
        {
            addGameObjects.Add(go);
        }

        public static void RemoveObject(GameObject go)
        {
            removedGameObjects.Add(go);
        }
        private void Cleanup()
        {
            foreach (GameObject go in addGameObjects)
            {
                gameObjects.Add(go);
                go.Awake();
                go.Start();

                Collider c = (Collider)(go).GetComponent<Collider>();
                if (c != null)
                {
                    Colliders.Add(c);
                }
            }

            foreach (GameObject go in removedGameObjects)
            {
                Collider c = (Collider)(go).GetComponent<Collider>();
                gameObjects.Remove(go);

                if (c != null)
                {
                    Colliders.Remove(c);
                }
            }
            removedGameObjects.Clear();
            addGameObjects.Clear();
        }

        public static Component FindObjectOfType<T>() where T : Component
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Component c = gameObject.GetComponent<T>();

                if (c != null)
                {
                    return c;
                }
            }

            return null;
        }

        public void GameObjectsToRemove()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Component component = gameObject.GetComponent<NecromancerMagic>();

                if (component != null)
                {
                    if(component.ToRemove || component.GameObject.Transform.Position.X > GameWorld.ScreenSize.X + 1000 || component.GameObject.Transform.Position.X < -1000 || component.GameObject.Transform.Position.Y > GameWorld.ScreenSize.Y + 1000 || component.GameObject.Transform.Position.Y < -1000)
                    removedGameObjects.Add(gameObject);
                }
            }
        }

    }
}
