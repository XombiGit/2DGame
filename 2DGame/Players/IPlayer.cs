﻿using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.Players
{
    interface IPlayer
    {
        int row { get; set; }
        int col { get; set; }
        int prevRow { get; set; }
        int prevCol { get; set; }

        void MoveCombatant(ILevel level, int row, int col, string move);
    }
}
