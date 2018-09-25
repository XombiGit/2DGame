using _2DGame.Enemies;
using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _2DGame.DataParsers;
using _2DGame.StationaryItems;

namespace _2DGame.Game
{
    class TurnBasedGameEngine : IGameEngine
    {
        private int treasureCount = 0;
        private int TreasureTotal;
        private static String error = "You've hit a wall.  TRY AGAIN";
        private static bool endGame = false;
        private static bool winGame = false;
        private static bool finish = false;
        private static bool powerFound = false;
        private static bool exitFound = false;
        //private static bool exit = false;
        private static int count = 0;
        private static int score = 0;
        public List<List<Enemy>> ArchEnemy = new List<List<Enemy>>();
        public List<List<Treasure>> Cache = new List<List<Treasure>>();
        public List<List<PowerUp>> Booster = new List<List<PowerUp>>();
        public List<char[,]> Cells = new List<char[,]>();
        public List<int> TreasureMax = new List<int>();

        public static List<Enemy> nemeses = new List<Enemy>();
        public List<Treasure> valuables = new List<Treasure>();
        public List<PowerUp> abilities = new List<PowerUp>();
        public char[,] Matrix;
        public readonly IRenderer _renderer;
        string DataFile = @"C:\Users\UnknownUser\Desktop\LevelParameters.txt";

        public TurnBasedGameEngine(IRenderer renderer)
        {
            _renderer = renderer;
            Initiate();
        }

        public void Initiate()
        {
            CustomDataParser Parser = new CustomDataParser();
            string[] Filename = Parser.ReadFile(DataFile);
            int Count = Parser.ParseLevel(Filename);

            for (int x = 0; x < Count; x++)
            {
                finish = false;
                Player player = new Player(0, 0);

                TreasureMax = Parser.ParseTreasureCount(Filename);
                TreasureTotal = TreasureMax.ElementAt(x);

                Cells = Parser.ParseGridSize(Filename);
                Matrix = Cells.ElementAt(x);

                ArchEnemy = Parser.ParseEnemies(Filename);
                nemeses = ArchEnemy.ElementAt(x);

                Cache = Parser.ParseTreasure(Filename);
                valuables = Cache.ElementAt(x);

                Booster = Parser.ParsePower(Filename);
                abilities = Booster.ElementAt(x);

                Level level = new Level(Matrix, nemeses, valuables, abilities);

                _renderer.DrawGrid(level);

                while (finish != true)
                {
                    String control = Console.ReadKey().KeyChar.ToString();
                    //Thread thread = new Thread(() => player.MoveCombatant(Player.currX, Player.currY, control));
                    player.MoveCombatant(level, Player.currX, Player.currY, control);

                    //Console.WriteLine("User: {0}, {1}", User.currX, User.currY);

                    //Is there a way to check collision for all enemies instead of individuallly ?
                    /*if ((vertical.currX == Player.currX && vertical.currY == Player.currY) ||
                       (horizontal.currX == Player.currX && horizontal.currY == Player.currY) ||
                       (random.currX == Player.currX && random.currY == Player.currY) ||
                       (super.currX == Player.currX && super.currY == Player.currY))*/

                    /*if (Player.invincible == true)
                    {
                        count++;

                        if (count == 5)
                        {
                            Player.invincible = false;
                            count = 0;
                        }
                    }*/

                    for (int i = nemeses.Count - 1; i >= 0; i--)
                    {
                        if (nemeses[i].currX == Player.currX && nemeses[i].currY == Player.currY)
                        {
                            if (Player.invincible == false)
                            {
                                endGame = true;
                                CheckGameOutcome(level);
                            }
                            else
                            {
                                Console.WriteLine("Enemy destroyed");
                                //Level.setPrevEnemyCell(nemeses[i].currX, nemeses[i].currY);

                                level.setBoardCell(nemeses[i].currX, nemeses[i].currY, Level.PLAYER);
                                nemeses.Remove(nemeses[i]);
                                score += 100;

                                /*count++;
                                    Console.WriteLine(count);
                                    System.Threading.Thread.Sleep(3000);
                                if (count == 5)
                                {
                                    Player.invincible = false;
                                    count = 0;
                                }*/
                            }
                        }
                    }

                    foreach (Enemy villain in nemeses)
                    {
                        //string type = nemeses[i].enemyType;
                        //nemeses[i].MoveCombatant(nemeses[i].currX, nemeses[i].currY, type);
                        //EnemyType type = villain.enemyType;
                        villain.MoveCombatant(level, villain.currX, villain.currY, villain.enemyType);
                    }

                    //vertical.MoveCombatant(vertical.currX, 6, (Enemy.EnemyType.Vertical).ToString());
                    //horizontal.MoveCombatant(8, horizontal.currY, (Enemy.EnemyType.Horizontal).ToString());
                    //random.MoveCombatant(random.currX, random.currY, (Enemy.EnemyType.Random).ToString());
                    //super.MoveCombatant(super.currX, super.currY, (Enemy.EnemyType.Super).ToString());
                    //Console.WriteLine("Vertical: {0}, {1}", super.currX, super.currY);
                    //super.MoveCombatant(super.currX, super.currY, (Enemy.EnemyType.Super).ToString());
                    //Console.WriteLine("Horizontal: {0}, {1}", super.currX, super.currY);
                    //Console.WriteLine("Random: {0}, {1}", random.currX, random.currY);


                    if (Player.currX < 0)
                    {
                        Console.WriteLine(error);
                        Thread.Sleep(1000);
                        Player.currX = Player.currX + 1;
                        level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }
                    else if (Player.currY < 0)
                    {
                        Console.WriteLine(error);
                        Thread.Sleep(1000);
                        Player.currY = Player.currY + 1;
                        level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }
                    else if (Player.currX >= (level.grid.GetLength(0)))
                    {
                        Console.WriteLine(error);
                        Thread.Sleep(1000);
                        Player.currX = Player.currX - 1;
                        level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }
                    else if (Player.currY >= (level.grid.GetLength(0)))
                    {
                        Console.WriteLine(error);
                        Thread.Sleep(1000);
                        Player.currY = Player.currY - 1;
                        level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }
                    else
                    {
                        //Level.setPrevUserCell(Player.prevX, Player.prevY);
                        //Level.setUserCell(Player.currX, Player.currY);
                        if (level.grid[Player.currX, Player.currY] == Level.TREASURE)
                        {
                            //Level.setTreasureCellFound(Player.currX, Player.currY);
                            //Level.setBoardCell(Player.currX, Player.currY, Level.EMPTY);
                            treasureCount++;

                            if (treasureCount == TreasureTotal)
                            {
                                //winGame = true;
                                //CheckGameOutcome();
                                //Level.setExit(Level.grid.GetLength(0) - 1, Level.grid.GetLength(0) - 1);
                                level.setBoardCell(level.grid.GetLength(0) - 1, level.grid.GetLength(0) - 1, Level.EXIT);
                            }
                        }

                        if (level.grid[Player.currX, Player.currY] == Level.POWER)
                        {
                            //Level.setPowerCellUsed(Player.currX, Player.currY);
                            //Level.setBoardCell(Player.currX, Player.currY, Level.EMPTY);
                            powerFound = true;
                            Player.invincible = true;
                        }

                        if (level.grid[Player.currX, Player.currY] == Level.EXIT)
                        {
                            exitFound = true;
                            winGame = true;
                        }

                        level.setBoardCell(Player.prevX, Player.prevY, Level.EMPTY);
                        level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }

                    for (int i = nemeses.Count - 1; i >= 0; i--)
                    {
                        if (nemeses[i].currX == Player.currX && nemeses[i].currY == Player.currY)
                        {

                            if (Player.invincible == false)
                            {
                                endGame = true;
                                CheckGameOutcome(level);
                            }
                            else
                            {
                                //how to destroy an object 
                                Console.WriteLine("Enemy destroyed");
                                //Level.setPrevEnemyCell(nemeses[i].currX, nemeses[i].currY);
                                level.setBoardCell(nemeses[i].currX, nemeses[i].currY, Level.PLAYER);
                                nemeses.Remove(nemeses[i]);
                                score += 100;
                            }
                        }
                    }

                    //Console.Clear();
                    _renderer.DrawGrid(level);

                    if (powerFound == true)
                    {
                        powerFound = false;
                        Console.WriteLine("You are invincible for 5 moves.  Destroy the enemy !!!");
                    }

                    if (exitFound == true)
                    {
                        exitFound = false;
                        CheckGameOutcome(level);
                    }
                }
            }

        }

        public void CheckGameOutcome(Level level)
        {
            if (winGame == true)
            {
                treasureCount = 0;
                Console.WriteLine("You collected all treasure and escaped the maze. CONGRATULATIONS champion !!!");
                Console.WriteLine("Score: {0} points", score);
                Thread.Sleep(3000);
                finish = true;
            }
            else if (endGame == true)
            {
                level.setBoardCell(Player.currX, Player.currY, Level.ENEMY);
                _renderer.DrawGrid(level);
                Console.WriteLine("An enemy has destroyed you.  You have lost the game. :(");
                Console.ReadLine();
            }
        }
    }
}
