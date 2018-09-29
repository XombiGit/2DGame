using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.Game
{
    interface IGameEngine
    {
        void Initiate();

        ILevel LoadLevelFromFile();
    }
}
