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
        int NumLevels { get; set; }
        int NumEnemies { get; set; }
        int NumTreasures { get; set; }
        int NumPowers { get; set; }

        string[] ReadFile(string filename);
        void ParseLevel(string[] splitArray);
        List<List<Enemy>> ParseEnemies(string[] splitArray);
        List<Treasure> ParseTreasure(string[] splitArray);
        List<PowerUp> ParsePower(string[] splitArray);
        char[,] ParseGridSize(string[] splitArray);
    }
}
