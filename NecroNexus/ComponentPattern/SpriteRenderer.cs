using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class SpriteRenderer : Component
    {
        /// <summary>
        /// A property used to get and set a sprite texture in the Draw method
        /// </summary>
        public Texture2D Sprite { get; set; }

        /// <summary>
        /// A property used to get and set an origin in the Draw method
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// A property used to get and set a layerdepth in the Draw method
        /// </summary>
        public float SortOrder { get; set; }

        /// <summary>
        /// A property used to get and set a scale in the Draw method
        /// </summary>
        public float Scale { get; set; }

        public float Rotation { get; set; }

        /// <summary>
        /// The Start method for this Component
        /// </summary>
        public override void Start()
        {
            //Sets the Origin to the sprites middle
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }

        /// <summary>
        /// A method used for choosing the different fields used for Draw, for whatever Object you attach this Component to
        /// </summary>
        /// <param name="spriteName">This is the string that determines the sprite used</param>
        /// <param name="scale">This is the scale of the sprite that determines its size</param>
        /// <param name="sortOrder">This is the Layerdepth of the Sprite</param>
        public void SetSprite(string spriteName, float scale, float rotation, float sortOrder)
        {
            Sprite = Globals.Content.Load<Texture2D>(spriteName);
            this.Scale = scale;
            this.SortOrder = sortOrder;
            this.Rotation = rotation;
        }

        /// <summary>
        /// The Draw Meethod used to draw out a sprite for the Game
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, SortOrder);
        }
    }
}
