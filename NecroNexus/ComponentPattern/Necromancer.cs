using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            previousPosition = GameObject.Transform.Position;
            currentPosition = GameObject.Transform.Position;

            //Adds the Animator Component and the adds the Keys to the Dictionary
            animator = (Animator)GameObject.GetComponent<Animator>();
            movementKeys.Add(Keys.A, BState.Up);
            movementKeys.Add(Keys.D, BState.Up);
            movementKeys.Add(Keys.W, BState.Up);
            movementKeys.Add(Keys.S, BState.Up);
        }
    }
}
