using _2DGame.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DGame.DataParsers;
using _2DGame.Players;
using _2DGame.StationaryItems;

namespace _2DGame.Levels 
{

    public class Level : ILevel
    {
        public const char PLAYER = '1';
        public const char TREASURE = '$';
        public const char ENEMY = 'X';
        public const char POWER = '!';
        public const char EXIT = '^';
        public const char EMPTY = '0';

        public static List<Enemy> enemies = new List<Enemy>();
        public static List<PowerUp> powers = new List<PowerUp>();
        public static List<Treasure> treasures = new List<Treasure>();

        public static int length = 0;
        public static int width = 0;
        public int LevelNum = 0;
        public char[,] grid;

        static Random numGen = new Random();
        int pos = numGen.Next(20);

        public Level(char[,] GridSize, List<Enemy> Opponents, List<Treasure> Riches, List<PowerUp> Skills)
        {

            grid = GridSize;
            enemies = Opponents;
            treasures = Riches;
            powers = Skills;

            clearGrid(grid);

            setupEnemies(enemies, grid);
            setTreasureCell(treasures, grid);
            setPowerCell(powers, grid);
            setUserCell(0, 0, grid);
        }


        private void clearGrid(char[,] level)
        {
            grid = level;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = Level.EMPTY;
                }
            }
        }

        public int GridX
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int GridY
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }

        public char[,] LevelGrid
        {
            get
            {
                return grid;
            }

            set
            {
                grid = value;
            }
        }

        public int LevelID
        {
            get
            {
                return LevelNum;
            }

            set
            {
                LevelNum = value;
            }
        }

        public List<Enemy> Foes
        {
            get
            {
                return enemies;
            }

            set
            {
                enemies = value;
            }
        }

        public List<Treasure> Gems
        {
            get
            {
                return treasures;
            }

            set
            {
                treasures = value;
            }
        }

        public List<PowerUp> Enhancers

        {
            get
            {
                return powers;
            }

            set
            {
                powers = value;
            }
        }

        //When to use static vs. non static
        //More than one random position but only need to static method to set treasure cells
        public void setTreasureCell(List<Treasure> treasures, char[,] level)
        {
            grid = level;
            foreach (Treasure Gem in treasures)
            {
                //treasureCell[row, col] = false;
                grid[Gem.currX, Gem.currY] = TREASURE;
            }
            /*treasureCell[0, 3] = true;
            treasureCell[9, 7] = true;
            treasureCell[1, 2] = true;
            treasureCell[1, 9] = true;
            treasureCell[5, 3] = true;*/
            //grid[0, 3] = TREASURE;
            //grid[8, 9] = TREASURE;
            //grid[9, 6] = TREASURE;
            //grid[1, 9] = TREASURE;
            //grid[5, 3] = TREASURE;
            //grid[row, col] = TREASURE;
        }

        public void setExit(int row, int col)
        {
            //exitCell[row, col] = true;
            grid[row, col] = EXIT;
        }

        public void setPowerCell(List<PowerUp> Powers, char[,] level)
        {
            grid = level;
            foreach (PowerUp Invincible in Powers)
            {//powerCell[row, col] = true;
                grid[Invincible.currX, Invincible.currY] = POWER;
            }
        }

        public void setupEnemies(List<Enemy> Enemies, char[,] level)
        {
            grid = level;
            foreach (Enemy Opponent in Enemies)
            {//powerCell[row, col] = true;
                grid[Opponent.currX, Opponent.currY] = ENEMY;
            }
        }

        public void setPowerCellUsed(int row, int col)
        {
            //powerCell[row, col] = false;
            grid[row, col] = EMPTY;
        }

        public void setUserCell(int row, int col, char[,] level)
        {
            grid = level;
            //userCell[row, col] = true;
            grid[row, col] = PLAYER;
        }

        public void setPrevUserCell(int row, int col)
        {
            //userCell[row, col] = false;
            grid[row, col] = EMPTY;
        }

        public void setTreasureCellFound(int row, int col)
        {
            //treasureCell[row, col] = false;
            grid[row, col] = EMPTY;
            
        }

        public void setEnemyCell(int row, int col)
        {
            //enemyCell[row, col] = true;
            grid[row, col] = ENEMY;
        }

        public void setPrevEnemyCell(int row, int col)
        {
            //enemyCell[row, col] = false;
            grid[row, col] = EMPTY;
        }

        public void setBoardCell(int row, int col, char element)
        {
            //Lock the grid variable
            grid[row, col] = element;
        }
    }
}
