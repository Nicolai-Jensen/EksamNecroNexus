using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{


    //--------------------------Nicolai Jensen----------------------------//

    /// <summary>
    /// Abstract superclass for all of our GameStates, gives the neccessary 4 methods to work like a psuedoGameWorld
    /// </summary>
    public abstract class State
    {

        protected ContentManager content;
        protected GameWorld game;
        protected GraphicsDevice graphicsDevice;

        public State(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            this.content = content;
        }



        public abstract void Initialize();

        public abstract void LoadContent();


        public abstract void Draw(SpriteBatch spriteBatch);


        public abstract void Update();

    }
}
