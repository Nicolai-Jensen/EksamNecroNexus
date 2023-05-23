using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Collider : Component
    {
        //This Field is used when we need to draw out our collisionBox
        private Texture2D texture;

        //this spriteRenderer variable is needed to get access to a Gameobjects sprite 
        private SpriteRenderer spriteRenderer;

        //The Collider Component needs access to the ObserverPattern CollisionEvent and this is a variable for that
        public CollisionEvent CollisionEvent { get; set; } = new CollisionEvent();

        //These fields are used for the CollisionBox to determine its size in comparison to the sprite
        private float size1 = 2;
        private float size2 = 2;
        private int size3 = 1;
        private int size4 = 1;

        /// <summary>
        /// Some Properties for the size fields so that they can be changed individually 
        /// </summary>
        public float Size1 { get { return size1; } set { size1 = value; } }
        public float Size2 { get { return size2; } set { size2 = value; } }
        public int Size3 { get { return size3; } set { size3 = value; } }
        public int Size4 { get { return size4; } set { size4 = value; } }

        /// <summary>
        /// Colliders Start method
        /// </summary>
        public override void Start()
        {
            //Refers our spriteRenderer Variable to the corresponding gameobjects sprite
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent<SpriteRenderer>();

            //Sets a red pixel as a texture that we use when looking at our collisionbox
            texture = Globals.Content.Load<Texture2D>("Pixel");
        }

        /// <summary>
        /// Property that draws out a box around the sprite of a gameobject, this can be changed with the size properties
        /// </summary>
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle
                    (
                        (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / Size1),
                        (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / Size2),
                        spriteRenderer.Sprite.Width / Size3,
                        spriteRenderer.Sprite.Height / Size4
                    );
            }
        }

        /// <summary>
        /// The Update method for Collider
        /// </summary>
        public override void Update()
        {

            CheckCollision();
        }

        /// <summary>
        /// The Draw Method for collider, currently only used for tracking the hitboxes during debugging
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {

            DrawRectangle(CollisionBox, spriteBatch);
        }

        /// <summary>
        /// The method for drawing out our hitboxes during debugging, this is not viewable during showcases
        /// </summary>
        /// <param name="collisionBox">Referes to the Rectangle in use</param>
        /// <param name="spriteBatch">Uses spritebatch given by monogame to draw out the ractangles lines</param>
        private void DrawRectangle(Rectangle collisionBox, SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        /// <summary>
        /// A method for checking if the current object is colliding with another object
        /// </summary>
        private void CheckCollision()
        {
            foreach (Collider other in LevelOne.Colliders)
            {
                //This if statement ensures Objects can not collide with themselves
                if (other != this && other.CollisionBox.Intersects(CollisionBox))
                {
                    CollisionEvent.Notify(other.GameObject);
                }
            }
        }
    }
}
