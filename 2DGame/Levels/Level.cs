using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.Levels
{

    class Level
    {
        public const char PLAYER = '1';
        public const char TREASURE = '$';
        public const char ENEMY = 'X';
        public const char POWER = '!';
        public const char EXIT = '^';
        public const char EMPTY = '0';

        public static char[,] grid = new char[10, 10];
        public static bool[,] treasureCell = new bool[10, 10];
        public static bool[,] userCell = new bool[10, 10];
        public static bool[,] enemyCell = new bool[10, 10];
        public static bool[,] powerCell = new bool[10, 10];
        public static bool[,] exitCell = new bool[10, 10];

        static Random numGen = new Random();
        int pos = numGen.Next(20);
        public static Countdown Counter = new Countdown(1, 60, false);

        public Level()
        {
            //Initialize Countdown thread in this class
            //undo static for this class
            //this.counter = counter;
            setGrid();
            setTreasureCell();
            setPowerCell(7, 5);
            setUserCell(0, 0);
            setEnemyCell(0, 6);
            setEnemyCell(8, 0);
            setEnemyCell(2, 4);
            setEnemyCell(3, 5);
            DrawGrid(grid);
        }

        public static void DrawGrid(char[,] grid)
        {
            Console.SetCursorPosition(0, 0);
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == PLAYER)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (grid[x, y] == TREASURE)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (grid[x, y] == ENEMY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (grid[x, y] == POWER)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (grid[x, y] == EXIT)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (grid[x, y] == EMPTY)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(grid[x, y]);
                    Console.ResetColor();

                    if (y == grid.GetLength(1) - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine("{0}:{1}", Counter.Minute, Counter.Second);
        }

        public static void setGrid()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    grid[x, y] = EMPTY;
                }
            }
        }

        //When to use static vs. non static
        //More than one random position but only need to static method to set treasure cells
        public static void setTreasureCell()
        {
            /*treasureCell[0, 3] = true;
            treasureCell[9, 7] = true;
            treasureCell[1, 2] = true;
            treasureCell[1, 9] = true;
            treasureCell[5, 3] = true;*/
            grid[0, 3] = TREASURE;
            grid[8, 9] = TREASURE;
            grid[9, 6] = TREASURE;
            grid[1, 9] = TREASURE;
            grid[5, 3] = TREASURE;
        }

        public static void setExit(int row, int col)
        {
            //exitCell[row, col] = true;
            grid[row, col] = EXIT;
        }

        public static void setPowerCell(int row, int col)
        {
            //powerCell[row, col] = true;
            grid[row, col] = POWER;
        }

        public static void setPowerCellUsed(int row, int col)
        {
            //powerCell[row, col] = false;
            grid[row, col] = EMPTY;
        }

        public static void setUserCell(int row, int col)
        {
            //userCell[row, col] = true;
            grid[row, col] = PLAYER;
        }

        public static void setPrevUserCell(int row, int col)
        {
            //userCell[row, col] = false;
            grid[row, col] = EMPTY;
        }

        public static void setTreasureCellFound(int row, int col)
        {
            //treasureCell[row, col] = false;
            grid[row, col] = EMPTY;
        }

        public static void setEnemyCell(int row, int col)
        {
            //enemyCell[row, col] = true;
            grid[row, col] = ENEMY;
        }

        public static void setPrevEnemyCell(int row, int col)
        {
            //enemyCell[row, col] = false;
            grid[row, col] = EMPTY;
        }

        public static void setBoardCell(int row, int col, char element)
        {
            //Lock the grid variable
            grid[row, col] = element;
        }
    }
}
