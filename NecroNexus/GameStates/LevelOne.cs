using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    public class LevelOne : State
    {
        //A Texture variable for our background
        private Texture2D[] backgroundsprite;

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> addGameObjects = new List<GameObject>();
        private List<GameObject> removedGameObjects = new List<GameObject>();


        //2 variables to control key presses
        private KeyboardState currentKey;
        private KeyboardState previousKey;

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

        }

        /// <summary>
        /// A way to ensure single key presses when taking action
        /// </summary>
        public override void Update()
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);


            spriteBatch.End();
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
