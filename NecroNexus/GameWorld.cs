using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NecroNexus
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //State variables to reference and swap states
        private State currentState;
        private State nextState;
        private State nextState2;
        private bool gameStarted = false;

        private Menu menu;
        private StartScreen startScreen;
        private LevelOne levelOne;
        private NewCharState newCharState;
        private PauseMenuState pausedMenuState;

        //A Vector2 to save our screen size on
        private static Vector2 screenSize;

        //A property variable for DeltaTime
        public static float DeltaTime { get; private set; }

        //Properties
        public static Vector2 ScreenSize
        {
            get
            {
                return screenSize;
            }
        }

        public bool GameStarted
        {
            get { return gameStarted; }
            set { gameStarted = value; }
        }

        //A property used to get access to what the currentState is in other classes
        public State CurrentState
        {
            get
            {
                return currentState;
            }
        }
        public State NextState
        {
            get
            {
                return nextState;
            }
            set
            {
                nextState = value;
            }
        }

        public LevelOne LevelOne
        {
            get { return levelOne; }
        }
        
        public PauseMenuState PauseMenuState { get { return pausedMenuState; } }
        public NewCharState NewCharState { get { return newCharState; } }
        public Menu Menu { get { return menu; } }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Globals.Content = Content;


            //Sets the games ScreenSize and applies it to our variable
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }



        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Audio.LoadAudio();
            menu = new Menu(this, _graphics.GraphicsDevice, Content);
            startScreen = new StartScreen(this, _graphics.GraphicsDevice, Content);
            levelOne = new LevelOne(this, _graphics.GraphicsDevice, Content);
            newCharState = new NewCharState(this, _graphics.GraphicsDevice, Content);
            pausedMenuState = new PauseMenuState(this, _graphics.GraphicsDevice, Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ChangeState(levelOne);
            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Applies a simulation of time to DeltaTime
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //An if statement that changesstates if the ChangeState method has been used
            if (nextState != null)
            {
                //changes state into the next one
                currentState = nextState;

                //Calls initialize and loadContent in the new state
                currentState.Initialize();
                currentState.LoadContent();
                //sets nextState to null
                nextState = null;
            }

            if (nextState2 != null)
            {
                //changes state into the next one
                currentState = nextState2;

                //sets nextState to null
                nextState2 = null;
            }


            //calls the currentstates update
            currentState.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            currentState.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        /// <summary>
        /// A Method for Changing which state you are in
        /// </summary>
        /// <param name="state">New State</param>
        public void ChangeState(State state)
        {
            //Sets NextState to a State
            nextState = state;
        }

        public void ChangeState2(State state)
        {
            nextState2 = state;
        }
    }
}