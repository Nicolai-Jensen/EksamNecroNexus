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

        public Board()
        {
        }

        public void LevelOneBoard()
        {
            Vector2 position1 = new Vector2(480, 427);
            positionList.Add(position1);
            Vector2 position2 = new Vector2(1440, 427);
            positionList.Add(position2);
            Vector2 position3 = new Vector2(960, 640);
            positionList.Add(position3);
            Vector2 position4 = new Vector2(480, 854);
            positionList.Add(position4);
            Vector2 position5 = new Vector2(1440, 854);
            positionList.Add(position5);
        }
    }
}
