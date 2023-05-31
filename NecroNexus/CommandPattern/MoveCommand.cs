using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai Jensen----------------------------//
    public class MoveCommand : ICommand
    {
        //The Direction reference field
        private Vector2 velocity;

        /// <summary>
        /// This classes constructor sets the velocity field
        /// </summary>
        /// <param name="velocity">The direction in which we want to move</param>
        public MoveCommand(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        /// <summary>
        /// The method that is used to proceed with the Command for the Component
        /// </summary>
        /// <param name="player"></param>
        public void Execute(Necromancer player)
        {
            player.Move(velocity);
        }
    }
}
