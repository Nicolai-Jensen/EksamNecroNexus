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
        private Rectangle[] clickableButRec = new Rectangle[12];
        private Texture2D[] UISprites = new Texture2D[12];
        private int menuButClicked = 0;
        private float timer;

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> addGameObjects = new List<GameObject>();
        private List<GameObject> removedGameObjects = new List<GameObject>();


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

        }

        public override void Initialize()
        {

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
        }
        private void CheckingIfClicked()
        {
            //Open the summons menu
            if (clickableButRec[2].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 2; }
            //Open the upgrade menu
            if (clickableButRec[3].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released) { menuButClicked = 3; }
            if (menuButClicked == 4 || clickableButRec[4].Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                //Start next wave
                menuButClicked = 4;
                timer += GameWorld.DeltaTime;
                if (timer > 0.5f)
                {
                    menuButClicked = 0;
                    timer = 0;
                }

            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
            DrawingUI(spriteBatch);
            spriteBatch.End();
        }

        private void DrawingUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UISprites[0], clickableButRec[0], Color.White);
            for (int i = 1; i < 5; i++)
            {
                spriteBatch.Draw(UISprites[1], clickableButRec[i], Color.White);
                switch (menuButClicked)
                {
                    case 2:
                        spriteBatch.Draw(UISprites[1], clickableButRec[2], Color.DarkGray);
                        spriteBatch.Draw(UISprites[0], clickableButRec[5], Color.White);
                        for (int a = 6; a <= 9; a++)
                        {
                            spriteBatch.Draw(UISprites[1], clickableButRec[a], clickableButRec[a], Color.White, 0f, new Vector2(0),SpriteEffects.None,0.9f);
                        }
                        break;
                    case 3:
                        spriteBatch.Draw(UISprites[1], clickableButRec[3], Color.DarkGray);
                        break;
                    case 4:
                        spriteBatch.Draw(UISprites[1], clickableButRec[4], Color.DarkGray);
                        break;
                }
            }

        }

        public void AddObject(GameObject go)
        {
            addGameObjects.Add(go);
        }

        public void RemoveObject(GameObject go)
        {
            removedGameObjects.Add(go);
        }


        public Component FindObjectOfType<T>() where T : Component
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
    }
}
