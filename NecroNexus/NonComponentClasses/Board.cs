using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroNexus
{
    public class Board
    {
        private List<Vector2> positionList = new List<Vector2>();

        public List<Vector2> PositionList
        {
            get { return positionList; }
        }

        public Vector2 SpawnPosition { get; set; }

        public Board(Vector2 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }

        public void LevelOneBoard(List<Vector2> list)
        {
            foreach (var item in list)
            {
                positionList.Add(item);
            }
        }

    }
}
