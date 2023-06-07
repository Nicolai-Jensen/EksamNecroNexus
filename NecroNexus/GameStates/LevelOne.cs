using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen & Thorbjørn----------------------------//
    public class LevelOne : State
    {
        //A Texture variable for our background
        private Texture2D BackgroundFront;
        private Texture2D BackgroundPlain;
        private Texture2D BackgroundWall1;
        

        //Drawing walls

        public Vector2 BDWallPos;
        public Vector2 EHWallPos;
        float amount = 0.5f;


        //Adds the needed classes to LevelOne
        public static Board boardOne;
        public LvlOneEnemies level;
        public SaveSystem levelSave;
        //public static GameSaveLevelOne level;
        private Map map;
        private AutoSave autoSave;
        private SummonFactory summons;

        private DrawingLevel drawingLevel;

        private SpriteFont showLevelInfo;

        //These Properties are used for the Resources in the game

        public int CurrentUser { get; set; }
        public bool Loaded { get; set; }


        //The Lists used to add load and remove gameObject aswell as the Collider list
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> addGameObjects = new List<GameObject>();
        public static List<GameObject> removedGameObjects = new List<GameObject>();
        public static List<Collider> Colliders { get; private set; } = new List<Collider>();


        //2 variables to control key presses & mouse presses
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        public int LevelID { get; set; } = 1;
        public string LevelName { get; set; } = "Graveyard";

        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public LevelOne(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content, int user) : base(game, graphicsDevice, content)
        {
            map = new Map(); //Adds the map
            boardOne = new Board(new Vector2(700, GameWorld.ScreenSize.Y / 2)); //Adds a Board
            summons = new SummonFactory(); //Adds a SummonFactory
            boardOne.LevelOneBoard(map.ReturnPos(map.Graph1())); //Sets the board by adding the correct vector2 list gotten through pathfinding the nodes
            level = new LvlOneEnemies(boardOne);
            levelSave = new SaveSystem(level, game.Repository, this);
            //level = new GameSaveLevelOne(boardOne, game.Repository, this); //Adds the Levels save
            autoSave = new AutoSave(levelSave); //Instaniates the AutoSave
            drawingLevel = new DrawingLevel(game,this);
            foreach (var item in gameObjects)//Removes any lasting gameObjects from previous iterations of LevelOne
            {
                RemoveObject(item);
            }
            Cleanup();
            CurrentUser = user;
        }

        public override void Initialize()
        {
            //Builds the Necromancer and sets one of each tower outside of the screen to reference
            Director director = new Director(new NecroBuilder());
            gameObjects.Add(summons.Create(SummonType.SkeletonArcher, new Vector2(-3000, -3000)));
            gameObjects.Add(summons.Create(SummonType.SkeletonBrute, new Vector2(-3000, -3000)));
            gameObjects.Add(summons.Create(SummonType.Hex, new Vector2(-3000, -3000)));
            gameObjects.Add(summons.Create(SummonType.Demon, new Vector2(-3000, -3000)));
            gameObjects.Add(director.Construct());
            InputHandler.Instance.AttachPlayer((Necromancer)FindObjectOfType<Necromancer>());

            levelSave.UpdateValues();

            //game.Repository.Open();//Opens the Repository Connection
            if (Loaded == true) //Loads a game if you chose Load
            {
                levelSave.LoadGame();
                Cleanup();
            }
            
            autoSave.Start(); //Starts the AutoSave Thread

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
            BackgroundWall1 = content.Load<Texture2D>("Backgrounds/NecroWall");

            drawingLevel.LoadContent(content);

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

            drawingLevel.Update();

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            level.CheckWave(); //Checks if enemies can spawn

            GameObjectsToRemove(); //Calls A Method to destroy ToRemove items
            Cleanup(); //Calls cleanUp
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
          
            drawingLevel.DrawingUI(spriteBatch);

            spriteBatch.Draw(BackgroundPlain, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.1f);

            //BD wall
            BDWallPos = Vector2.Lerp(new Vector2(1575, 175), new Vector2(1400, 225), amount);

            if (map.WallBD == true)
            {
                spriteBatch.Draw(BackgroundWall1, BDWallPos, null, Color.White, 0f, new Vector2(BackgroundWall1.Width / 2f , BackgroundWall1.Height / 2f), 1f, SpriteEffects.None, 0.99f);
            }

            //EH wall
            EHWallPos = Vector2.Lerp(new Vector2(700, 225), new Vector2(700, 625), amount);

            if (map.WallEH == true)
            {
                spriteBatch.Draw(BackgroundWall1, EHWallPos, null, Color.White, 0f, new Vector2(BackgroundWall1.Width / 2f, BackgroundWall1.Height / 2f), 1f, SpriteEffects.None, 0.99f);
            }


            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }
            spriteBatch.Draw(BackgroundFront, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.89f);

            spriteBatch.End();
        }

        /// <summary>
        /// Adds an Object to the list of objects waiting to be added to the ObjectsList
        /// </summary>
        /// <param name="go">The Object you want to have added</param>
        public static void AddObject(GameObject go)
        {
            addGameObjects.Add(go);
        }

        /// <summary>
        /// Adds an Object to the list of objects waiting to be removed from the ObjectsList
        /// </summary>
        /// <param name="go"></param>
        public static void RemoveObject(GameObject go)
        {
            removedGameObjects.Add(go);
        }

        /// <summary>
        /// Cleans Up the Objects list by adding/removing objects from the other 2 lists and their Colliders
        /// </summary>
        private void Cleanup()
        {
            lock (Globals.lockObject)
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
        }

        /// <summary>
        /// A Method for Finding the first instance of an Object in the list that has the desired Component
        /// </summary>
        /// <typeparam name="T">Whichever Component you wish to find</typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// This Method checks through all of the gameobjects list for Projectiles and enemies to see if they have to be Added to the Remove List
        /// </summary>
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
