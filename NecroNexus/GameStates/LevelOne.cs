using Microsoft.Xna.Framework;
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

        //private Texture2D[] backgroundsprite;

        private Texture2D BackgroundFront;
        private Texture2D BackgroundPlain;

        private Board boardOne;
        private GameSaveLevelOne level;
        private Map map;
        private AutoSave autoSave;

        private SummonFactory summons;

        private Rectangle[] clickableButRec = new Rectangle[24];
        private Texture2D[] UISprites = new Texture2D[24];
        private SpriteFont showLevelInfo;
        private int menuButClicked = 0;
        private int whichUpgradeClicked = 0;
        private float timer;
        private float timer1;
        
        private byte necroUpgrade = 0;
        private bool[] presseddowntopleft = { false, false, false, false };
        private bool[] isHoveringOverIcon = { false, false, false, false };
        public int MenuButClicked { get { return menuButClicked; } set { menuButClicked = value; } }

        public static int GetCriptHealth { get; set; } = 100;
        public static int GetSouls { get; set; } = 10;
        public int GetWaveCount { get; set; }
        public int CurrentUser { get; set; }
        public bool Loaded { get; set; }
        private Necromancer nc;



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

            boardOne.LevelOneBoard(map.ReturnPos(map.Graph1()));
            level = new GameSaveLevelOne(boardOne, game.Repository, this);
            autoSave = new AutoSave(level);
            foreach (var item in gameObjects)
            {
                RemoveObject(item);
            }
        }



        public override void Initialize()
        {
            game.Repository.Open();
            if (Loaded == true)
            {
                level.LoadGame();
            }
            autoSave.Start();
            Director director = new Director(new NecroBuilder());
            gameObjects.Add(summons.Create(SummonType.SkeletonArcher, new Vector2(-3000, -3000)));
            gameObjects.Add(summons.Create(SummonType.SkeletonBrute, new Vector2(-3000, -3000)));
            gameObjects.Add(summons.Create(SummonType.Hex, new Vector2(-3000, -3000)));
            gameObjects.Add(summons.Create(SummonType.Demon, new Vector2(-3000, -3000)));

            gameObjects.Add(director.Construct());

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Awake();
            }
        }
        /// <summary>
        /// Loads all the sprites, set the rectangles and loads all the gameobjects used.
        /// </summary>
        public override void LoadContent()
        {
            BackgroundFront = content.Load<Texture2D>("Backgrounds/NecroBackgroundUpdatedFront");
            BackgroundPlain = content.Load<Texture2D>("Backgrounds/NecroBackgroundUpdatedPlain");

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
        /// <summary>
        /// This class handles the entire interation between the user and the UI, the information here is then used down in DrawingUI
        /// menuButClicked == 2 is for the summons menu
        /// menuButClicked == 3 is the the upgrade menu.
        /// menuButClicked == 4 is for the next wave button.
        /// Thorbjørn
        /// </summary>
        private void CheckingIfClicked()
        {
            //Open the summons menu
            if (clickableButRec[2].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            { if (!presseddowntopleft[0] && !presseddowntopleft[1] && !presseddowntopleft[2] && !presseddowntopleft[3]) { AudioEffect.ButtonClickingSound(); menuButClicked = 2; } else return; }
            //buy summons menu.
            if (menuButClicked == 2)
            {
                timer += GameWorld.DeltaTime;

                //So you can close the summons menu if clicked on again.
                if (timer >= 0.25f && menuButClicked == 2 && clickableButRec[2].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 0; timer = 0; }

                if (clickableButRec[6].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    AudioEffect.ButtonClickingSound();
                    if (GetSouls >= 10) { GetSouls -= 10; menuButClicked = 0; presseddowntopleft[0] = true; } else { return; }

                }
                if (clickableButRec[7].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    AudioEffect.ButtonClickingSound();
                    if (GetSouls >= 20) { GetSouls -= 20; menuButClicked = 0; presseddowntopleft[1] = true; } else { return; }

                }
                if (clickableButRec[8].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    AudioEffect.ButtonClickingSound();
                    if (GetSouls >= 30) { GetSouls -= 30; menuButClicked = 0; presseddowntopleft[2] = true; } else { return; }

                }
                if (clickableButRec[9].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    AudioEffect.ButtonClickingSound();
                    if (GetSouls >= 40) { GetSouls -= 40; menuButClicked = 0; presseddowntopleft[3] = true; } else { return; }

                }
            }

            HoveringSummons();

            //Open the upgrade menu
            if (clickableButRec[3].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            { if (!presseddowntopleft[0] && !presseddowntopleft[1] && !presseddowntopleft[2] && !presseddowntopleft[3]) { AudioEffect.ButtonClickingSound(); menuButClicked = 3; } else return; }
            if (menuButClicked == 4 || clickableButRec[4].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {

                //Start next wave
                timer += GameWorld.DeltaTime;
                if (timer > 0.5f)
                {
                    menuButClicked = 4;
                    level.StartNextWave();
                    menuButClicked = 0;
                    timer = 0;
                    AudioEffect.ButtonClickingSound();
                }

            }

            if (menuButClicked == 3)//Choose upgrade
            {
                timer += GameWorld.DeltaTime;
                //So you can close the upgrade menu if clicked on again.
                if (timer >= 0.5f && menuButClicked == 3 && clickableButRec[3].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { AudioEffect.ButtonClickingSound(); menuButClicked = 0; timer = 0; }
                //Skeleton arhcer Upgrade icon.
                if (clickableButRec[10].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 1; }

                //Hex Upgrade icon
                if (clickableButRec[11].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 2; }

                //Skeleton Brute Upgrade icon.
                if (clickableButRec[12].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 3; }
                //Demon Upgrade icon.
                if (clickableButRec[13].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 4; }

                //Player Upgrade icon.
                if (clickableButRec[14].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { AudioEffect.ButtonClickingSound(); whichUpgradeClicked = 5; }

                //UpgradeButtonClicked
                if (clickableButRec[16].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    AudioEffect.ButtonClickingSound();
                    if (whichUpgradeClicked == 0)
                    {
                        return;
                    }
                    else // if an icon and the upgrade button has been clicked
                    {
                        menuButClicked = 0;
                        switch (whichUpgradeClicked)
                        {
                            case 1://Skeleton Archer Upgrade.
                                break;
                            case 2: //Hex Upgrade.
                                break;
                            case 3: //Skeleton Brute Upgrade.
                                break;
                            case 4: //Demon Upgrade.
                                break;
                            case 5: //Player Upgrade.
                                nc = (Necromancer)GetChar().GetComponent<Necromancer>();
                                switch (nc.Tier)
                                {
                                    case 0: //level 0 to 1.
                                        if (GetSouls >= 10) { GetSouls -= 10; nc.Tier = 1; } else { menuButClicked = 3; }
                                        break;
                                    case 1: //level 1 to 2.
                                        if (GetSouls >= 20) { GetSouls -= 20; nc.Tier = 2; } else { menuButClicked = 3; }
                                        break;
                                    case 2: //level 2 to 3.
                                        if (GetSouls >= 30) { GetSouls -= 30; nc.Tier = 3; } else { menuButClicked = 3; }
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            if (clickableButRec[18].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                AudioEffect.ButtonClickingSound();
                game.Repository.Close();
                game.ChangeState(game.PauseMenuState);
            }
        }
        /// <summary>
        /// This class is called when the player has clicked on a summon they want to buy. 
        /// It checks the which summon has been clicked and then toggels an array that i then used in DrawingUI for drawing the hovering icons.
        /// Thorbjørn
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            DrawingUI(spriteBatch);


            spriteBatch.Draw(BackgroundPlain, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }
            spriteBatch.Draw(BackgroundFront, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.89f);

            spriteBatch.End();
        }
        /// <summary>
        /// When called it handles the drawing of the UI for the active level.
        /// Thorbjørn
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawingUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UISprites[0], clickableButRec[0], Color.White);
            spriteBatch.Draw(UISprites[8], clickableButRec[17], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//Health, Souls and Wave count
            spriteBatch.DrawString(showLevelInfo, GetCriptHealth.ToString(), new Vector2(205, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(showLevelInfo, GetSouls.ToString(), new Vector2(315, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(showLevelInfo, level.CurrentWave+1.ToString() + " / 10", new Vector2(540, 20), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

            spriteBatch.Draw(UISprites[9], clickableButRec[18], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);//ActivLevelPauseButton

            //for showing the player level
            nc = (Necromancer)GetChar().GetComponent<Necromancer>();
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

                    spriteBatch.Draw(UISprites[5], clickableButRec[2], Color.DarkGray);//SummonBut
                    spriteBatch.Draw(UISprites[0], clickableButRec[5], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);//Backgroundboxs for selection of summons

                    SkeletonArcher sk = (SkeletonArcher)GetSummonGo(1).GetComponent<SkeletonArcher>();
                    if (clickableButRec[6].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[10], clickableButRec[6], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//Archer TopLeft.
                    }
                    else { spriteBatch.Draw(UISprites[10], clickableButRec[6], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f); }//Archer TopLeft.
                    spriteBatch.DrawString(showLevelInfo, sk.skDamge.ToString(), new Vector2(765, 330), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, sk.Range.ToString(), new Vector2(765, 385), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, sk.FireRate.ToString(), new Vector2(765, 440), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    Hex hx = (Hex)GetSummonGo(2).GetComponent<Hex>();
                    if (clickableButRec[7].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[11], clickableButRec[7], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//ButtomLeft.
                    }
                    else { spriteBatch.Draw(UISprites[11], clickableButRec[7], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f); }//ButtomLeft.
                    spriteBatch.DrawString(showLevelInfo, hx.hexDamge.ToString(), new Vector2(770, 575), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, hx.Range.ToString(), new Vector2(770, 630), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, hx.FireRate.ToString(), new Vector2(770, 685), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    SkeletonBrute brute = (SkeletonBrute)GetSummonGo(3).GetComponent<SkeletonBrute>();
                    if (clickableButRec[8].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[12], clickableButRec[8], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//Topright.
                    }
                    else { spriteBatch.Draw(UISprites[12], clickableButRec[8], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f); }//Topright.
                    spriteBatch.DrawString(showLevelInfo, brute.skDamge.ToString(), new Vector2(1260, 330), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, brute.Range.ToString(), new Vector2(1260, 385), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, brute.FireRate.ToString(), new Vector2(1260, 440), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    Demon demon = (Demon)GetSummonGo(4).GetComponent<Demon>();
                    if (clickableButRec[9].Contains(currentMouse.X, currentMouse.Y))
                    {
                        spriteBatch.Draw(UISprites[13], clickableButRec[9], null, Color.LightGray, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f);//ButtomRight.
                    }
                    else { spriteBatch.Draw(UISprites[13], clickableButRec[9], null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0.91f); }//ButtomRight.
                    spriteBatch.DrawString(showLevelInfo, demon.demonDamge.ToString(), new Vector2(1260, 575), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, demon.Range.ToString(), new Vector2(1260, 630), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    spriteBatch.DrawString(showLevelInfo, demon.FireRate.ToString(), new Vector2(1260, 685), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

                    if (isHoveringOverIcon[0] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "10", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    }
                    else if (isHoveringOverIcon[1] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "20", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    }
                    else if (isHoveringOverIcon[2] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "30", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    }
                    else if (isHoveringOverIcon[3] == true)
                    {
                        spriteBatch.DrawString(showLevelInfo, "40", new Vector2(currentMouse.X, currentMouse.Y), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                    }


                    break;
                case 3://Upgrade
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
                        case 1:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                            break;
                        case 2:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.Red, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                            break;
                        case 3:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.Yellow, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                            break;
                        case 4:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.Green, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                            break;
                        case 5:
                            spriteBatch.Draw(UISprites[1], clickableButRec[15], clickableButRec[15], Color.Blue, 0f, new Vector2(0), SpriteEffects.None, 0.91f);
                            break;
                    }

                    break;
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
        /// When called it returns the player
        /// </summary>
        /// <returns></returns>
        public GameObject GetChar()
        {
            foreach (GameObject item in gameObjects)
            {
                if (item.Tag == "Player")
                {
                    return item;
                }
            }
            return null;
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
                    foreach (GameObject item in gameObjects)
                    {
                        if (item.Tag == "Archer")
                        {
                            return item;
                        }
                    }
                    return null;
                case 2:
                    foreach (GameObject item in gameObjects)
                    {
                        if (item.Tag == "Hex")
                        {
                            return item;
                        }
                    }
                    return null;
                case 3:
                    foreach (GameObject item in gameObjects)
                    {
                        if (item.Tag == "Brute")
                        {
                            return item;
                        }
                    }
                    return null;
                case 4:
                    foreach (GameObject item in gameObjects)
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

        public static void UpdateSouls(float value)
        {
            GetSouls += (int)value;
        }

        public static void UpdateHealth(int value)
        {
            GetCriptHealth -= value;
        }

        public void GameObjectsToRemove()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Component component = gameObject.GetComponent<NecromancerMagic>();

                if (component != null)
                {
                    if (component.ToRemove || component.GameObject.Transform.Position.X > GameWorld.ScreenSize.X + 50 || component.GameObject.Transform.Position.X < -50 || component.GameObject.Transform.Position.Y > GameWorld.ScreenSize.Y + 50 || component.GameObject.Transform.Position.Y < -50)
                        RemoveObject(gameObject);
                }

                component = gameObject.GetComponent<ArcherArrow>();

                if (component != null)
                {
                    if (component.ToRemove || component.GameObject.Transform.Position.X > GameWorld.ScreenSize.X + 50 || component.GameObject.Transform.Position.X < -50 || component.GameObject.Transform.Position.Y > GameWorld.ScreenSize.Y + 50 || component.GameObject.Transform.Position.Y < -50)
                        RemoveObject(gameObject);
                }

                component = gameObject.GetComponent<DemonBall>();

                if (component != null)
                {
                    if (component.ToRemove || component.GameObject.Transform.Position.X > GameWorld.ScreenSize.X + 50 || component.GameObject.Transform.Position.X < -50 || component.GameObject.Transform.Position.Y > GameWorld.ScreenSize.Y + 50 || component.GameObject.Transform.Position.Y < -50)
                        RemoveObject(gameObject);
                }

                component = gameObject.GetComponent<HexBall>();

                if (component != null)
                {
                    if (component.ToRemove || component.GameObject.Transform.Position.X > GameWorld.ScreenSize.X + 50 || component.GameObject.Transform.Position.X < -50 || component.GameObject.Transform.Position.Y > GameWorld.ScreenSize.Y + 50 || component.GameObject.Transform.Position.Y < -50)
                        RemoveObject(gameObject);
                }


                if (gameObject.Tag == "Enemy")
                {
                    if (gameObject.HasComponent<Grunt>())
                    {
                        Grunt enemy = (Grunt)gameObject.GetComponent<Grunt>();
                        if (enemy.ToRemove)
                        {
                            RemoveObject(gameObject);
                        }
                    }
                    else if (gameObject.HasComponent<ArmoredGrunt>())
                    {
                        ArmoredGrunt enemy = (ArmoredGrunt)gameObject.GetComponent<ArmoredGrunt>();
                        if (enemy.ToRemove)
                        {
                            RemoveObject(gameObject);
                        }
                    }
                    else if (gameObject.HasComponent<Knight>())
                    {
                        Knight enemy = (Knight)gameObject.GetComponent<Knight>();
                        if (enemy.ToRemove)
                        {
                            RemoveObject(gameObject);
                        }
                    }
                    else if (gameObject.HasComponent<HorseRider>())
                    {
                        HorseRider enemy = (HorseRider)gameObject.GetComponent<HorseRider>();
                        if (enemy.ToRemove)
                        {
                            RemoveObject(gameObject);
                        }
                    }
                    else if (gameObject.HasComponent<Cleric>())
                    {
                        Cleric enemy = (Cleric)gameObject.GetComponent<Cleric>();
                        if (enemy.ToRemove)
                        {
                            RemoveObject(gameObject);
                        }
                    }
                    else if (gameObject.HasComponent<Paladin>())
                    {
                        Paladin enemy = (Paladin)gameObject.GetComponent<Paladin>();
                        if (enemy.ToRemove)
                        {
                            RemoveObject(gameObject);
                        }
                    }
                    else if (gameObject.HasComponent<Valkyrie>())
                    {
                        Valkyrie enemy = (Valkyrie)gameObject.GetComponent<Valkyrie>();
                        if (enemy.ToRemove)
                        {
                            RemoveObject(gameObject);
                        }
                    }
                }
            }
        }

    }
}
