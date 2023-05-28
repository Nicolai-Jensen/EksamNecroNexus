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
            animator.AddAnimation(BuildAnimation("Standing", new string[] { "Necromancer/Idle/tile000", "Necromancer/Idle/tile001", "Necromancer/Idle/tile002", "Necromancer/Idle/tile003", "Necromancer/Idle/tile004", "Necromancer/Idle/tile005", "Necromancer/Idle/tile006", "Necromancer/Idle/tile007" }));
            animator.AddAnimation(BuildAnimation("Run", new string[] { "Necromancer/Run/tile000", "Necromancer/Run/tile001", "Necromancer/Run/tile002", "Necromancer/Run/tile003", "Necromancer/Run/tile004", "Necromancer/Run/tile005", "Necromancer/Run/tile006", "Necromancer/Run/tile007" }));
            animator.AddAnimation(BuildAnimation("Shoot", new string[] { "Necromancer/AttackOne/tile000", "Necromancer/AttackOne/tile001", "Necromancer/AttackOne/tile002", "Necromancer/AttackOne/tile003", "Necromancer/AttackOne/tile004", "Necromancer/AttackOne/tile005", "Necromancer/AttackOne/tile006", "Necromancer/AttackOne/tile007", "Necromancer/AttackTwo/tile000", "Necromancer/AttackTwo/tile001", "Necromancer/AttackTwo/tile002", "Necromancer/AttackTwo/tile003", "Necromancer/AttackTwo/tile004", "Necromancer/AttackTwo/tile005", "Necromancer/AttackTwo/tile006", "Necromancer/AttackTwo/tile007" }));
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
