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

        void ReadFile();
        void ParseLevel(string[] splitArray);
        void ParseEnemies(string[] splitArray);
        void ParseTreasure(string[] splitArray);
        void ParsePowers(string[] splitArray);
    }
}
