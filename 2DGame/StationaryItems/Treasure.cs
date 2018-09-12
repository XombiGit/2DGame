using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.StationaryItems
{

    public class Treasure : IStationaryItems
    {
        public int currX = 0;
        public int currY = 0;

        public Treasure(int x, int y)
        {
            currX = x;
            currY = y;
        }

        public int row
        {
            get
            {
                return currX;
            }
            set
            {
                currX = value;
            }
        }

        public int col
        {
            get
            {
                return currY;
            }
            set
            {
                currY = value;
            }
        }
    }
}
