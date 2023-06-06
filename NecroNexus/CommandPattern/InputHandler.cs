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
    public class InputHandler
    {
        /// <summary>
        /// A singleton for the Inputhandler to make it easier to interact with
        /// </summary>
        private static InputHandler instance;

        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputHandler();
                }
                return instance;
            }
        }
        // A Dictionary for the Keybinds that we make
        private Dictionary<KeyInfo, ICommand> keybinds = new Dictionary<KeyInfo, ICommand>();

        // Makes a new ButtonEvent variable for Inputhandler to use
        private ButtonEvent buttonEvent = new ButtonEvent();

        Necromancer player;

        /// <summary>
        /// The Constructor for the Inputhandler
        /// </summary>
        public InputHandler()
        {
            //Creates a player variable and finds the player object within the LevelOne list
            player = (Necromancer)LevelOne.FindObjectOfType<Necromancer>();

            //Attaches ButtonEvent to the player reference
            buttonEvent.Attach(player);

            //Creates keybinds for whatever Commands we want them attached to
            keybinds.Add(new KeyInfo(Keys.D), new MoveCommand(new Vector2(1, 0)));
            keybinds.Add(new KeyInfo(Keys.A), new MoveCommand(new Vector2(-1, 0)));
            keybinds.Add(new KeyInfo(Keys.W), new MoveCommand(new Vector2(0, -1)));
            keybinds.Add(new KeyInfo(Keys.S), new MoveCommand(new Vector2(0, 1)));
            keybinds.Add(new KeyInfo(Keys.Space), new ShootCommand());

        }

        /// <summary>
        /// Executes the keybinds for the player object when pressing the keybinds
        /// </summary>
        /// <param name="player"></param>
        public void Execute(Necromancer player)
        {
            //creates a keyState variable and checks the State of the key
            KeyboardState keyState = Keyboard.GetState();

            //Checks each keybinds key info to see their state
            foreach (KeyInfo keyInfo in keybinds.Keys)
            {
                //checks if the Key is being pressed and sets the keyinfo to notify that it is being pressed
                if (keyState.IsKeyDown(keyInfo.Key))
                {
                    keybinds[keyInfo].Execute(player);
                    buttonEvent.Notify(keyInfo.Key, BState.Down);
                    keyInfo.IsDown = true;

                }

                //checks if the key is no longer being pressed and sets the keyinfo to notify that it is no longer down
                if (!keyState.IsKeyDown(keyInfo.Key) && keyInfo.IsDown == true)
                {
                    buttonEvent.Notify(keyInfo.Key, BState.Up);
                }
            }
        }

        public void AttachPlayer(Necromancer necromancer)
        {
            player = necromancer;
            buttonEvent.Attach(player);
        }
    }
}
