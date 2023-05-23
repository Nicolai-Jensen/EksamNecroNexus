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

        private Texture2D[] backgroundsprite;
        private Board boardOne;
        private GameSaveLevelOne level;
       
        private SummonFactory summons;

        private Rectangle[] clickableButRec = new Rectangle[24];
        private Texture2D[] UISprites = new Texture2D[24];
        private int menuButClicked = 0;
        private int whichUpgradeClicked = 0;
        private float timer;


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
            boardOne = new Board(new Vector2(700, GameWorld.ScreenSize.Y / 2));
            
          
            summons = new SummonFactory();

        }

        public override void Initialize()
        {
            boardOne.LevelOneBoard();
            level = new GameSaveLevelOne(boardOne);
            Director director = new Director(new NecroBuilder());



            gameObjects.Add(director.Construct());
           
            gameObjects.Add(summons.Create(SummonType.SkeletonArcher));

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Awake();
            }
        }

        public override void LoadContent()
        {
            UISprites[0] = content.Load<Texture2D>("placeholdersprites/UI/MenuPlaceHolderPng");
            clickableButRec[0] = new Rectangle(0, 880, 1920, 200);
            UISprites[1] = content.Load<Texture2D>("placeholdersprites/UI/ButtonPlaceHolderPng");//buttons
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
            clickableButRec[18] = new Rectangle(1845, 0, 75, 75);//PauseGame


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
            if (clickableButRec[2].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 2; }
            if (menuButClicked == 2)
            {
                if (clickableButRec[6].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 0; }
                if (clickableButRec[7].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 0; }
                if (clickableButRec[8].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 0; }
                if (clickableButRec[9].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 0; }
            }
            //Open the upgrade menu
            if (clickableButRec[3].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 3; }
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
                if (clickableButRec[10].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 1; }
                if (clickableButRec[11].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 2; }
                if (clickableButRec[12].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 3; }
                if (clickableButRec[13].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 4; }
                if (clickableButRec[14].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { whichUpgradeClicked = 5; }

                if (clickableButRec[16].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
                {
                    //Make a check to see the if you have enough monz
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
                    menuButClicked = 0;
                }

            }
            if (clickableButRec[18].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                game.ChangeState(game.PauseMenuState);
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

        private void DrawingUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UISprites[0], clickableButRec[0], Color.White);
            spriteBatch.Draw(UISprites[0], clickableButRec[17], Color.White);
            spriteBatch.Draw(UISprites[0], clickableButRec[18], Color.White);
            for (int i = 1; i < 5; i++)
            {
                spriteBatch.Draw(UISprites[1], clickableButRec[i], Color.White);

                switch (menuButClicked)
                {
                    case 2://Summons
                        spriteBatch.Draw(UISprites[1], clickableButRec[2], Color.DarkGray);
                        spriteBatch.Draw(UISprites[0], clickableButRec[5], Color.White);
                        for (int a = 6; a <= 9; a++)
                        {
                            spriteBatch.Draw(UISprites[1], clickableButRec[a], clickableButRec[a], Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                        }
                        break;
                    case 3://Upgrade
                        spriteBatch.Draw(UISprites[1], clickableButRec[3], Color.DarkGray);
                        spriteBatch.Draw(UISprites[0], clickableButRec[5], Color.White);
                        for (int b = 10; b <= 14; b++)
                        {
                            spriteBatch.Draw(UISprites[1], clickableButRec[b], clickableButRec[b], Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);
                        }
                        spriteBatch.Draw(UISprites[1], clickableButRec[16], clickableButRec[16], Color.White, 0f, new Vector2(0), SpriteEffects.None, 0.9f);

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
                        spriteBatch.Draw(UISprites[1], clickableButRec[4], Color.DarkGray);
                        break;
                }
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

                if (component != null && component.ToRemove)
                {
                    removedGameObjects.Add(gameObject);
                }
            }
        }

    }
}
