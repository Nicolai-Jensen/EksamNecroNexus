﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NecroNexus
{
    //--------------------------Thorbjørn----------------------------//
    public class NewCharState : State
    {
        //A Texture variable for our background
        private Texture2D[] backgroundsprite = new Texture2D[3];
        private Rectangle finalizeButRec;
        private SpriteFont spriteFont;

        private MyWpfControl myWpfControl;
        //2 variables to control key presses
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        private MouseState previousMouse;
        private MouseState currentMouse;
        /// <summary>
        /// The States Constructor which applies the picture that is shown
        /// </summary>
        public NewCharState(GameWorld game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            backgroundsprite[0] = content.Load<Texture2D>("placeholdersprites/UI/BackGroundWithoutEdge");
            backgroundsprite[1] = content.Load<Texture2D>("placeholdersprites/UI/FinalizeBut");
            backgroundsprite[2] = content.Load<Texture2D>("placeholdersprites/UI/EmptyBut");
            spriteFont = content.Load<SpriteFont>("placeholdersprites/UI/File");
            finalizeButRec = new Rectangle(800, 500, 300, 100);
            myWpfControl = new MyWpfControl(spriteFont);
        }

        /// <summary>
        /// Used to make sure that we can find and use the mouse position and then it calls a methode in Menu 
        /// where it then sets the name accrodingly to what the player wrote
        /// </summary>
        public override void Update()
        {
            myWpfControl.Update();
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();//enables you to click with the currentMouse
            if (finalizeButRec.Contains(currentMouse.X, currentMouse.Y) && previousMouse.LeftButton == ButtonState.Pressed && currentMouse.LeftButton == ButtonState.Released)
            {
                if (game.Menu.Drawdiffent == 7){game.Menu.ChangeNameLoadoneSaveone(myWpfControl.CurrentText);}
                if (game.Menu.Drawdiffent == 8){game.Menu.ChangeNameLoadtwoSavetwo(myWpfControl.CurrentText);} 
                if (game.Menu.Drawdiffent == 9){game.Menu.ChangeNameLoadthreeSavethree(myWpfControl.CurrentText);}
                game.ChangeState(game.LevelOne);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(backgroundsprite[0], new Rectangle(600, 270, 600, 400), Color.White);
            spriteBatch.Draw(backgroundsprite[2], new Rectangle(800, 345, 350, 100), Color.Gray);
            spriteBatch.Draw(backgroundsprite[1], finalizeButRec, Color.White);
            spriteBatch.DrawString(spriteFont, "Name", new Vector2(675, 370), Color.White);
            myWpfControl.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}