using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    //--------------------------Nicolai----------------------------//

    /// <summary>
    /// This class sets up a list of Vector2's that act as a "board" that enemies can use to follow
    /// </summary>
    public class Board
    {
        //The instance and the Get for the list of Vector2s
        private List<Vector2> positionList = new List<Vector2>();
        public List<Vector2> PositionList
        {
            get { return positionList; }
        }

        public Vector2 SpawnPosition { get; set; }//A Property to determine the starting position of the enemies using the board

        /// <summary>
        /// This Constructor sets the spawnPosition
        /// </summary>
        /// <param name="spawnPosition">The Vector2 that is used for spawnPosition</param>
        public Board(Vector2 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }

        /// <summary>
        /// A method that takes a list of Vector2s and adds them to the boards list
        /// </summary>
        /// <param name="list"></param>
        public void LevelOneBoard(List<Vector2> list)
        {
            foreach (var item in list)
            {
                positionList.Add(item);
            }
        }

    }
}
