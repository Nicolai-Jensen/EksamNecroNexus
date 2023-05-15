using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    public class LevelOne : State
    {
        //A Texture variable for our background
        private Texture2D[] backgroundsprite;

        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> addGameObjects = new List<GameObject>();
        public static List<GameObject> removedGameObjects = new List<GameObject>();
        public static List<Collider> Colliders { get; private set; } = new List<Collider>();


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
            Director director = new Director(new NecroBuilder());

            gameObjects.Add(director.Construct());

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Awake();
            }
        }

        public override void LoadContent()
        {
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
            

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            Cleanup();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }

            spriteBatch.End();
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
