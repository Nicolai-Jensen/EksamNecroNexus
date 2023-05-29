using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    class Animator : Component
    {
        /// <summary>
        /// A property used to determine which place in the animation array is being refered to
        /// </summary>
        public int CurrentIndex { get; private set; }

        // A field used for our timer
        private float timeElapsed;

        // A SpriteRenderer Variable to access Sprites
        private SpriteRenderer spriteRenderer;

        // A Dictionary for our animations that uses the Animation class and a string
        public Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        // An Animation variable to refer to the current animation being played
        private Animation currentAnimation;

        /// <summary>
        /// The Start method for our animator
        /// </summary>
        public override void Start()
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// The Update method for our animator
        /// </summary>
        public override void Update()
        {
            //Simulates time and tracks it
            timeElapsed += GameWorld.DeltaTime;

            //Sets the current Index to be equal to Time passed multiplied by the currentAnimation and the FPS of that animation
            CurrentIndex = (int)(timeElapsed * currentAnimation.FPS);

            //Resets the Timer and index if it reaches the end of the animation
            if (CurrentIndex > currentAnimation.Sprites.Length - 1)
            {
                timeElapsed = 0;
                CurrentIndex = 0;
            }

            //Sets the sprite to be the current sprite referenced in the index
            spriteRenderer.Sprite = currentAnimation.Sprites[CurrentIndex];
        }


        /// <summary>
        /// The AddAnimation method for collider, this method adds an animation to the component
        /// </summary>
        /// <param name="animation">A variable for the Animation class</param>
        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);

            if (currentAnimation == null)
            {
                currentAnimation = animation;
            }
        }

        /// <summary>
        /// A method used for playing an animation for a Component
        /// </summary>
        /// <param name="animationName">Refers to the name of the animation, like "Foward" or "left"</param>
        public void PlayAnimation(string animationName)
        {
            if (animationName != currentAnimation.Name)
            {
                currentAnimation = animations[animationName];
                timeElapsed = 0;
                CurrentIndex = 0;
            }
        }
    }
}
