using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class NecroBuilder : IBuilder
    {

        //Creates a GameObject variable for reference
        private GameObject gameObject;

        /// <summary>
        /// A method used for building the player GameObject
        /// </summary>
        public void BuildGameObject()
        {
            //Creates a new gameObject
            gameObject = new GameObject();

            //Calls BuildComponents Method
            BuildComponents();

            //Adds an Animator variable so we can assign animations and build them to to the GameObject
            Animator animator = (Animator)gameObject.GetComponent<Animator>();

            //Assigns new animations to the GameObject
            animator.AddAnimation(BuildAnimation("Standing", new string[] { "placeholdersprites/EldenRingIcon" }));
            animator.AddAnimation(BuildAnimation("Right", new string[] { "placeholdersprites/EldenRingIcon" }));
            animator.AddAnimation(BuildAnimation("Left", new string[] { "placeholdersprites/EldenRingIcon" }));
            animator.AddAnimation(BuildAnimation("JumpUp", new string[] { "placeholdersprites/EldenRingIcon" }));
        }

        /// <summary>
        /// A method for adding Components to our GameObject
        /// </summary>
        private void BuildComponents()
        {
            //Adds the Meerkat Component to our GameObject
            Necromancer p = (Necromancer)gameObject.AddComponent(new Necromancer());
            //Adds a SpriteRenderer Component to our GameObject
            gameObject.AddComponent(new SpriteRenderer());
            //Adds an Animator Component to our GameObject
            gameObject.AddComponent(new Animator());
            //Adds a Collider Component to our GameObject
            Collider c = (Collider)gameObject.AddComponent(new Collider());
            //Attaches a CollisionEvent to our GameObject
            c.CollisionEvent.Attach(p);
            //Sets the Size of the GameObjects CollisionBox
            c.Size1 = 5.5f;
            c.Size2 = 7.5f;
            c.Size3 = 3;
            c.Size4 = 3;

            //Attacks a CollisionEvent again to our GameObject
            c.CollisionEvent.Attach(p);
        }

        /// <summary>
        /// A method used for Building our animation for when we add an animation to our animator for our GameObject
        /// </summary>
        /// <param name="animationName">Refers to the Name we give the animation</param>
        /// <param name="spriteNames">Refers to the names of the Sprites that we use in the animation</param>
        /// <returns></returns>
        private Animation BuildAnimation(string animationName, string[] spriteNames)
        {
            Texture2D[] sprites = new Texture2D[spriteNames.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = Globals.Content.Load<Texture2D>(spriteNames[i]);
            }

            Animation animation = new Animation(animationName, sprites, 9);

            return animation;
        }

        /// <summary>
        /// A method used for calling our GameObject Once we've buildt it
        /// </summary>
        /// <returns></returns>
        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
