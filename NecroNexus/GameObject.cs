using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class GameObject
    {
        //A List of Components to keep track of
        private List<Component> components = new List<Component>();

        //The use of the Transform class to get and move the GameObjects Position
        public Transform Transform { get; set; } = new Transform();


        //This string Tag is used to identify certain GameObjects
        public string Tag { get; set; }

        /// <summary>
        /// A Method used for adding Components to the GameObject's List of Components
        /// </summary>
        /// <param name="component">This parameter should be whatever Component class you want to add</param>
        /// <returns></returns>
        public Component AddComponent(Component component)
        {
            component.GameObject = this;

            components.Add(component);

            return component;
        }

        /// <summary>
        /// A method for finding the correct Component inside the Components list when refering to them
        /// </summary>
        /// <typeparam name="T">The Type of Component</typeparam>
        /// <returns></returns>
        public Component GetComponent<T>() where T : Component
        {
            return components.Find(x => x.GetType() == typeof(T));
        }


        /// <summary>
        /// A method run when GameWorld initializes
        /// This is always run first before Start
        /// </summary>
        public void Awake()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Awake();
            }
        }

        /// <summary>
        /// A method run when GameWorld Loads Content
        /// This is always run after Awake 
        /// </summary>
        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }
        }

        /// <summary>
        /// Updates all components during runtime
        /// The standard Update method we see
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update();
            }
        }

        /// <summary>
        /// Draw Function, Draws out a 2D sprite to use for the Object
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw(spriteBatch);
            }
        }
    }
}
