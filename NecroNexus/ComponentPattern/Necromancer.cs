using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//

    public class Necromancer : Component, IGameListener
    {

        //Fields

        //Speed Value, used for velocity in the Move method
        private float speed;

        //An animator component to access animations
        private Animator animator;
        private SpriteRenderer sr;
        private string currentAnimation;

        private bool hasCastedMagic;
        private float castingMagicCooldown;
        public int Tier { get; set; } = 3;

        //A Dictionary used when adding usable keys from InputHandler
        private Dictionary<Keys, BState> controlKeys = new Dictionary<Keys, BState>();
        

        private NecroMagicFactory magic;

        public NecroMagicFactory Magic
        {
            get { return magic; }
        }

        /// <summary>
        /// The Awake method for the Meerkat
        /// </summary>
        public override void Awake()
        {
            //Sets its speed
            speed = 400;

            magic = new NecroMagicFactory();
        }

        /// <summary>
        /// The Start Method for the Meerkat
        /// </summary>
        public override void Start()
        {
            //Adds SpriteRenderer Component so we get access to drawing sprites
            sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite("Necromancer/Idle/tile000", 1f, 0, 1);


            //Sets the Start Position of the Meerkat and the field values needed for jumps to work
            GameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);

            //Adds the Animator Component and the adds the Keys to the Dictionary
            animator = (Animator)GameObject.GetComponent<Animator>();
            if (!controlKeys.ContainsKey(Keys.Space)) {
                controlKeys.Add(Keys.A, BState.Up);
                controlKeys.Add(Keys.D, BState.Up);
                controlKeys.Add(Keys.W, BState.Up);
                controlKeys.Add(Keys.S, BState.Up);
                controlKeys.Add(Keys.Space, BState.Up);
            }
            
            
        }

        /// <summary>
        /// The Update Method, this method runs constantly
        /// </summary>
        public override void Update()
        {
            //Activates Inputhandlers Execute method
            InputHandler.Instance.Execute(this);

            //Makes sure that the Idle animation is played under the right conditions
            if ((controlKeys[Keys.A] == BState.Up && controlKeys[Keys.D] == BState.Up && controlKeys[Keys.S] == BState.Up && controlKeys[Keys.W] == BState.Up
                || controlKeys[Keys.A] == BState.Down && controlKeys[Keys.D] == BState.Down && controlKeys[Keys.S] == BState.Up && controlKeys[Keys.W] == BState.Up
                || controlKeys[Keys.A] == BState.Down && controlKeys[Keys.D] == BState.Down && controlKeys[Keys.S] == BState.Down && controlKeys[Keys.W] == BState.Down
                || controlKeys[Keys.A] == BState.Up && controlKeys[Keys.D] == BState.Up && controlKeys[Keys.S] == BState.Down && controlKeys[Keys.W] == BState.Down))
            {
                animator.PlayAnimation("Standing");
            }

            Timer();

            //Calls some other methods that need to be constantly used
            ScreenJail();
        }

        public void Timer()
        {
            switch (Tier)
            {
                case(0):
                    if (hasCastedMagic == true)
                    {
                        castingMagicCooldown += GameWorld.DeltaTime;
                        if (castingMagicCooldown >= 1f)
                        {
                            hasCastedMagic = false;
                            castingMagicCooldown = 0;
                        }
                    }
                    break;
                case (1):
                    if (hasCastedMagic == true)
                    {
                        castingMagicCooldown += GameWorld.DeltaTime;
                        if (castingMagicCooldown >= 0.8f)
                        {
                            hasCastedMagic = false;
                            castingMagicCooldown = 0;
                        }
                    }
                    break;
                case (2):
                    if (hasCastedMagic == true)
                    {
                        castingMagicCooldown += GameWorld.DeltaTime;
                        if (castingMagicCooldown >= 0.6f)
                        {
                            hasCastedMagic = false;
                            castingMagicCooldown = 0;
                        }
                    }
                    break;
                case (3):
                    if (hasCastedMagic == true)
                    {
                        castingMagicCooldown += GameWorld.DeltaTime;
                        if (castingMagicCooldown >= 0.4f)
                        {
                            hasCastedMagic = false;
                            castingMagicCooldown = 0;
                        }
                    }
                    break;
            }
            
        }
        public void ActivateMagicCast()
        {
            if (hasCastedMagic == false)
            {
                GameObject magic = new GameObject();

                switch (Tier)
                {
                    case 0:
                        magic = Magic.Create(MagicLevel.BaseTier, GameObject.Transform.Position);
                        break;
                    case 1:
                        magic = Magic.Create(MagicLevel.Tier1, GameObject.Transform.Position);
                        break;
                    case 2:
                        magic = Magic.Create(MagicLevel.Tier2, GameObject.Transform.Position);
                        break;
                    case 3:
                        magic = Magic.Create(MagicLevel.Tier3, GameObject.Transform.Position);
                        break;
                }
                LevelOne.AddObject(magic);
                hasCastedMagic = true;
            }
        }
    


        /// <summary>
        /// The Move method controls the velocity(direction) and applies a speed to it
        /// It is also checking which animation it should be using
        /// </summary>
        /// <param name="velocity"></param>
        public void Move(Vector2 velocity)
        {  
            //Normalize velocity for proper movement
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            //Applies the speed to the direction
            velocity *= speed;

            //Applies the velocity to the Object
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);

            //Checks the direction the Necromancer is moving and applies the correct animation
            if (velocity.X > 0)
            {
                sr.SpriteEffects = SpriteEffects.None;
                animator.PlayAnimation("Run");
            }
            else if (velocity.X < 0)
            {
                sr.SpriteEffects = SpriteEffects.FlipHorizontally;
                animator.PlayAnimation("Run");
            }

            //Checks the direction the Necromancer is moving and applies the correct animation
            if (velocity.Y > 0)
            {
                animator.PlayAnimation("Run");
            }
            else if (velocity.Y < 0)
            {
                animator.PlayAnimation("Run");

            }
        }

        /// <summary>
        /// A simple method that ensures the Meerkat stays within the Bounds of the screen
        /// </summary>
        public void ScreenJail()
        {

            //Horizontal bounds, you can walk from one side to the other 
            if (GameObject.Transform.Position.X < -25)
            {
                GameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X + 24, GameObject.Transform.Position.Y);
            }
            if (GameObject.Transform.Position.X > GameWorld.ScreenSize.X + 25)
            {
                GameObject.Transform.Position = new Vector2(-24, GameObject.Transform.Position.Y);
            }

            //Vertical bounds, they stop you from proceeding out of bounds (These needed extra code to protect the integrity of the jump function so it didn't break)
            if (GameObject.Transform.Position.Y > GameWorld.ScreenSize.Y)
            {
                
                GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, GameWorld.ScreenSize.Y);
            }
            if (GameObject.Transform.Position.Y < GameWorld.ScreenSize.Y - GameWorld.ScreenSize.Y)
            {
                GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, 1);

            }
        }

        /// <summary>
        /// The Notify method is used by the IGameListner to "moniter" events that happen during runtime
        /// </summary>
        /// <param name="gameEvent">A parameter for tracking the GameEvents such as Collision and Buttons</param>
        public void Notify(GameEvent gameEvent)
        {

            //The Button event used by InputHandler to track button inputs
            if (gameEvent is ButtonEvent)
            {
                ButtonEvent be = (gameEvent as ButtonEvent);

                controlKeys[be.Key] = be.State;

            }
        }
        

    }
}
