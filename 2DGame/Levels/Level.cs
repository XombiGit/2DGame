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

        //public static bool[,] treasureCell = new bool[10, 10];
        //public static bool[,] userCell = new bool[10, 10];
        //public static bool[,] enemyCell = new bool[10, 10];
        //public static bool[,] powerCell = new bool[10, 10];
        //public static bool[,] exitCell = new bool[10, 10];
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<PowerUp> powers = new List<PowerUp>();
        public static List<Treasure> treasures = new List<Treasure>();

        public static Countdown Counter = new Countdown(3, 60, false);
        public static int length = 0;
        public static int width = 0;
        public int LevelNum = 0;
        public static char[,] grid;

        static Random numGen = new Random();
        int pos = numGen.Next(20);

        string DataFile = @"C:\Users\UnknownUser\Desktop\LevelParameters.txt";
        CustomDataParser Data = new CustomDataParser();

        public Level(char[,] GridSize, List<Enemy> Opponents, List<Treasure> Riches, List<PowerUp> Skills)
        {
            //Player player = new Player(0, 0);
            //Enemy vertical = new Enemy(0, 6, Enemy.EnemyType.Vertical.ToString());
            //Enemy horizontal = new Enemy(8, 0, Enemy.EnemyType.Horizontal.ToString());
            //Enemy random = new Enemy(2, 4, Enemy.EnemyType.Random.ToString());
            //Enemy super = new Enemy(3, 5, Enemy.EnemyType.Super.ToString());
            //enemies.Add(vertical);
            //enemies.Add(horizontal);
            //enemies.Add(random);
            //enemies.Add(super);
            //grid = new char[10, 10];
            grid = GridSize;
            enemies = Opponents;
            treasures = Riches;
            powers = Skills;

            setGrid(grid);
            //Data.ReadFile(DataFile);
            //ParseEnemies(splitter);
            //ParseTreasure(splitter);
            //ParsePower(splitter);

            setupEnemies(enemies, grid);
            setTreasureCell(treasures, grid);
            setPowerCell(powers, grid);
            setUserCell(0, 0, grid);

            //for(int i = 0; i < enemies.Count; i++)
            //{
                //setEnemyCell(enemies[i].currX, enemies[i].currY);
            //}
            //setEnemyCell(0, 6);
            //setEnemyCell(8, 0);
            //setEnemyCell(2, 4);
            //setEnemyCell(3, 5);
            DrawGrid(grid);
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

        public static void DrawGrid(char[,] Xgrid)
        {
            Console.SetCursorPosition(0, 0);
            for (int x = 0; x < Xgrid.GetLength(0); x++)
            {
                for (int y = 0; y < Xgrid.GetLength(1); y++)
                {
                    if (Xgrid[x, y] == PLAYER)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (Xgrid[x, y] == TREASURE)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (Xgrid[x, y] == ENEMY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (Xgrid[x, y] == POWER)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (Xgrid[x, y] == EXIT)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (Xgrid[x, y] == EMPTY)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(Xgrid[x, y]);
                    Console.ResetColor();

                    if (y == Xgrid.GetLength(1) - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            if (Counter.Second == 60)
            {
                //There is an issue with the time shortening down to 3 digits instead of 4.  Leftover 0
                Console.Write("\r{0}:00", Counter.Minute);
            }
            else
            {
                Console.Write("\r{0:D1}:{1:D2}", Counter.Minute, Counter.Second);
            }
        }

        public static void setGrid(char[,] level)
        {
            for (int x = 0; x < level.GetLength(0); x++)
            {
                for (int y = 0; y < level.GetLength(1); y++)
                {
                   level[x, y] = EMPTY;
                }
            }
        }

        //When to use static vs. non static
        //More than one random position but only need to static method to set treasure cells
        public static void setTreasureCell(List<Treasure> treasures, char[,] level)
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

        public static void setExit(int row, int col)
        {
            //exitCell[row, col] = true;
            grid[row, col] = EXIT;
        }

        public static void setPowerCell(List<PowerUp> Powers, char[,] level)
        {
            grid = level;
            foreach (PowerUp Invincible in Powers)
            {//powerCell[row, col] = true;
                grid[Invincible.currX, Invincible.currY] = POWER;
            }
        }

        public static void setupEnemies(List<Enemy> Enemies, char[,] level)
        {
            grid = level;
            foreach (Enemy Opponent in Enemies)
            {//powerCell[row, col] = true;
                grid[Opponent.currX, Opponent.currY] = ENEMY;
            }
        }

        public static void setPowerCellUsed(int row, int col)
        {
            //powerCell[row, col] = false;
            grid[row, col] = EMPTY;
        }

        public static void setUserCell(int row, int col, char[,] level)
        {
            grid = level;
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
