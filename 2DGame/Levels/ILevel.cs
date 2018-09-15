using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DGame.Enemies;
using _2DGame.StationaryItems;

namespace _2DGame.Levels
{
    public interface ILevel
    {
        int LevelID { get; set; }
        int GridX { get; set; }
        int GridY { get; set; }
        char[,] LevelGrid { get; set; }
        List<Enemy> Foes { get; set; }
        List<Treasure> Gems { get; set; }
        List<PowerUp> Enhancers { get; set; }
    }
}
