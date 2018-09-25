using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.Enemies
{
    interface ICombatant
    {
        int row { get; set; }
        int col { get; set; }
        int prevRow { get; set; }
        int prevCol { get; set; }

        void MoveCombatant(Level level, int row, int col, EnemyType move);
    }
}
