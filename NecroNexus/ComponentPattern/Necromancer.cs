using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

        private InputHandler inputHandler;

        //A Dictionary used when adding usable keys from InputHandler
        private Dictionary<Keys, BState> movementKeys = new Dictionary<Keys, BState>();

        /// <summary>
        /// The Awake method for the Meerkat
        /// </summary>
        public override void Awake()
        {
            //Sets its speed
            speed = 400;
        }

        /// <summary>
        /// The Start Method for the Meerkat
        /// </summary>
        public override void Start()
        {
            //Adds SpriteRenderer Component so we get access to drawing sprites
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            

            //Sets the Start Position of the Meerkat and the field values needed for jumps to work
            GameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);

            //Adds the Animator Component and the adds the Keys to the Dictionary
            animator = (Animator)GameObject.GetComponent<Animator>();

        }

        /// <summary>
        /// The Update Method, this method runs constantly
        /// </summary>
        public override void Update()
        {
            //Activates Inputhandlers Execute method
            inputHandler.Execute(this);

            

        }

        /// <summary>
        /// The Move method controls Meerkats velocity(direction) and applies a speed to it
        /// It is also checking which animation it should be using
        /// </summary>
        /// <param name="velocity"></param>
        public void Move(Vector2 velocity)
        {
            ////This if is here to disable movement while Jumping
            //if (jumping == false)
            //{
            //    //Normalize velocity for proper movement
            //    if (velocity != Vector2.Zero)
            //    {
            //        velocity.Normalize();
            //    }

            //    //Applies the speed to the direction
            //    velocity *= speed;

            //    //Applies the velocity to the Meerkat
            //    GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);

            //    //Checks the direction the Meerkat is moving and applies the correct animation
            //    if (velocity.X > 0 && jumping == false)
            //    {
            //        animator.PlayAnimation("Right");
            //    }
            //    else if (velocity.X < 0 && jumping == false)
            //    {
            //        animator.PlayAnimation("Left");

            //    }
            //}



        }

        /// <summary>
        /// A simple method that ensures the Meerkat stays within the Bounds of the screen
        /// </summary>
        public void ScreenJail()
        {

            ////Horizontal bounds, you can walk from one side to the other 
            //if (GameObject.Transform.Position.X < -25)
            //{
            //    GameObject.Transform.Position = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width + 24, GameObject.Transform.Position.Y);
            //}
            //if (GameObject.Transform.Position.X > GameWorld.Instance.GraphicsDevice.Viewport.Width + 25)
            //{
            //    GameObject.Transform.Position = new Vector2(-24, GameObject.Transform.Position.Y);
            //}

            ////Vertical bounds, they stop you from proceeding out of bounds (These needed extra code to protect the integrity of the jump function so it didn't break)
            //if (GameObject.Transform.Position.Y > GameWorld.Instance.GraphicsDevice.Viewport.Height - tileHeight)
            //{
            //    jumping = false;
            //    GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, GameWorld.Instance.GraphicsDevice.Viewport.Height - tileHeight);
            //    previousPosition = currentPosition;
            //}
            //if (GameObject.Transform.Position.Y < 60)
            //{
            //    jumping = false;
            //    GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, 60);
            //    previousPosition = currentPosition;
            //}
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

                movementKeys[be.Key] = be.State;

            }
        }
        

    }
}
