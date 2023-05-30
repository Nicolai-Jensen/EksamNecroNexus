using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Globalization;

namespace NecroNexus
{
    //--------------------------Thorbjørn----------------------------//
    /// <summary>
    /// Handles the input when the player types their desiered name
    /// </summary>
    public class MyWpfControl
    {
        private SpriteFont font;
        private string currentText = "";
        private string previousText;


        public string CurrentText => currentText;

        public MyWpfControl(SpriteFont font)
        {
            this.font = font;
        }
        private bool keyReleased = true;

        /// <summary>
        /// enables you to write a name for the user.
        /// </summary>
        public void Update()
        {
            previousText = currentText;

            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            if (pressedKeys.Length > 0)
            {
                if (keyReleased)
                {
                    // Only handle key input when a key is released
                    keyReleased = false;

                    Keys firstPressedKey = pressedKeys[0];

                    if (firstPressedKey == Keys.Back && currentText.Length > 0)
                    {
                        // Remove the last character from currentText
                        currentText = currentText.Substring(0, currentText.Length - 1);
                    }
                    else
                    {
                        string keyString = firstPressedKey.ToString();
                        if (keyString.Length == 1)
                        {
                            // Append the pressed key to currentText
                            currentText += keyString;
                        }
                    }
                }
            }
            else
            {
                // Reset the keyReleased flag when no keys are pressed
                keyReleased = true;
            }
        }
        /// <summary>
        /// Handels the drawing of the inputed text.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the currentText using spriteBatch and the assigned font
            spriteBatch.DrawString(font, currentText, new Vector2(800, 380), Color.White,0f,new Vector2(0,0),1f,SpriteEffects.None,0.9f);
        }
    }
}

