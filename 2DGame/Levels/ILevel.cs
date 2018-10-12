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
        char[,] Grid { get; set; }
        char[,] PrevGrid { get; set; }
        List<Enemy> Foes { get; set; }
        List<Treasure> Gems { get; set; }
        List<PowerUp> Enhancers { get; set; }

        void SetTreasureCell(List<Treasure> treasures, char[,] level);
        void SetExit(int row, int col);
        void SetPowerCell(List<PowerUp> Powers, char[,] level);
        void SetupEnemies(List<Enemy> Enemies, char[,] level);
        void SetUserCell(int row, int col, char[,] level);
        void SetPrevUserCell(int row, int col);
        void SetBoardCell(int row, int col, char element);
    }
}
