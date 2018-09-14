using _2DGame.Enemies;
using _2DGame.StationaryItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.DataParsers
{
    interface IDataParser
    {
        int TreasureCount { get; set; }
        string[] ReadFile(string filename);
        int ParseLevel(string[] splitArray);
        List<List<Enemy>> ParseEnemies(string[] splitArray);
        List<List<Treasure>> ParseTreasure(string[] splitArray);
        List<List<PowerUp>> ParsePower(string[] splitArray);
        List<char[,]> ParseGridSize(string[] splitArray);
    }
}
