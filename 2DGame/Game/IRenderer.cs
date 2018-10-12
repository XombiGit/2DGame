using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.Game
{
    public interface IRenderer
    {
        Countdown Counter { get; set; }

        void DrawGrid(ILevel grid);
        void InitWindow(ILevel grid);
        void ResetGrid(ILevel grid);
    }
}
