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
using _2DGame.Logging;

namespace _2DGame.Game
{
    public class GameEngine : IGameEngine
    {
        private static int treasureCount = 0;
        private static String error = "You've hit a wall.  TRY AGAIN";
        private static bool endGame = false;
        private static bool winGame = false;
        private static bool finish = false;
        private static bool auto = false;
        private static bool powerFound = false;
        private static bool exitFound = false;
        private static bool enemyDestroyed = false;
        private static bool timeEnd = false;
        private static int count = 0;
        private static int score = 0;
        public List<List<Enemy>> ArchEnemy = new List<List<Enemy>>();
        public List<List<Treasure>> Cache = new List<List<Treasure>>();
        public List<List<PowerUp>> Booster = new List<List<PowerUp>>();
        public List<char[,]> Cells = new List<char[,]>();
        public List<int> TreasureMax = new List<int>();
        private int TreasureTotal;

        public static List<Enemy> nemeses = new List<Enemy>();
        public List<Treasure> valuables = new List<Treasure>();
        public List<PowerUp> abilities = new List<PowerUp>();
        public char[,] Matrix;
        public readonly IRenderer _renderer;
        string DataFile = @"C:\Users\UnknownUser\Desktop\LevelParameters.txt";
        public int LevelNum = 0;
        private Thread enemyThread; 
        private Thread playerThread;
        private Thread levelThread;
        private Thread counterThread;
        public bool EndThread = false;
        public readonly ILogger _logger;

        public GameEngine(IRenderer renderer, ILogger logger)
        {
            _renderer = renderer;
            _logger = logger;
            Initiate();
        }

        public void Initiate()
        {
            while (true)
            {
                Player player = new Player(0, 0);

                ILevel level = LoadLevelFromFile();

                _renderer.DrawGrid(level);

                winGame = false;

                _logger.LogInfo(nameof(GameEngine), nameof(GameEngine.Initiate), $"Starting Level");

                startThreads(level, player);

                counterThread.Join();
                enemyThread.Join();
                levelThread.Join();
                playerThread.Join();                
            }   
        }

        private void startThreads(ILevel level, Player player)
        {
            //
            // Starting enemy thread
            // This is confusing, I need an enemy object to run the thread ??????
            //Thread verticalEnemyThread = new Thread(() => UpdateEnemyThread(vertical));
            //verticalEnemyThread.Name = "Vertical";
            //verticalEnemyThread.Start();
            enemyThread = new Thread(() => UpdateEnemyThread(level));
            enemyThread.Name = "Enemies";
            enemyThread.Start();

            //
            // Starting render thread (redraw level)
            //
            levelThread = new Thread(() => UpdateLevel(level));
            levelThread.Name = "Level";
            levelThread.Start();


            playerThread = new Thread(() => UpdatePlayer(level, player));
            playerThread.Name = "Player";
            playerThread.Start();

            counterThread = new Thread(() => UpdateCounter(_renderer.Counter));
            counterThread.Name = "Counter";
            counterThread.Start();
        }

        public static void UpdateCounter(Countdown counter)
        {
            timeEnd = counter.tickTock(counter.Minute, counter.Second);
        }
        /// <summary>
        /// Function that happens in the UpdatePlayer thread
        /// </summary>
        public static void UpdatePlayer(ILevel level, Player player)
        {
            while (winGame != true)
            {
                String control = Console.ReadKey(true).KeyChar.ToString();
                lock (level)
                {
                    player.MoveCombatant(level, Player.currX, Player.currY, control);
                }
                
            }
        }

        /// <summary>
        /// Function that happens in the UpdateEnemy Thread
        /// </summary>
        public static void UpdateEnemyThread(ILevel level)
        {
            while (winGame != true)
            {
                //lock(level)
                {
                    for (int i = 0; i <= nemeses.Count - 1; i++)
                    {
                        EnemyType type = nemeses[i].enemyType;
                        nemeses[i].MoveCombatant(level, nemeses[i].currX, nemeses[i].currY, type);
                        //vertical.MoveCombatant(vertical.currX, vertical.currY, vertical.nemesis);
                    }
                }


                Thread.Sleep(700);
            }
        }

        /// <summary>
        /// Function that redraws the level
        /// </summary>

        public void UpdateLevel(ILevel level)
        {
            while (winGame != true)
            {
                /*if (Player.invincible == true)
                {
                    count++;

                    if (count == 5)
                    {
                        Player.invincible = false;
                        count = 0;
                    }
                }*/
                //TODO - FIX
                //Level level = new Level(null, null, null, null);

               // lock (level)
                {

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
                                //Console.WriteLine("Enemy destroyed");
                                level.SetBoardCell(nemeses[i].currX, nemeses[i].currY, Level.PLAYER);
                                nemeses.Remove(nemeses[i]);
                                score += 100;
                                enemyDestroyed = true;
                            }
                        }
                    }

                    if (Player.currX < 0)
                    {
                        //Console.WriteLine(error);
                        Thread.Sleep(1000);
                        Player.currX = Player.currX + 1;
                        level.SetBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }
                    else if (Player.currY < 0)
                    {
                        //Console.WriteLine(error);
                        Thread.Sleep(1000);
                        Player.currY = Player.currY + 1;
                        level.SetBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }
                    else if (Player.currX >= (level.Grid.GetLength(0)))
                    {
                        //Console.WriteLine(error);
                        Thread.Sleep(1000);
                        Player.currX = Player.currX - 1;
                        level.SetBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }
                    else if (Player.currY >= (level.Grid.GetLength(0)))
                    {
                        //Console.WriteLine(error);
                        Thread.Sleep(1000);
                        Player.currY = Player.currY - 1;
                        level.SetBoardCell(Player.currX, Player.currY, Level.PLAYER);
                    }
                    else
                    {
                        if (level.Grid[Player.currX, Player.currY] == Level.TREASURE)
                        {
                            treasureCount++;

                            if (treasureCount == TreasureTotal)
                            {
                                level.SetBoardCell(level.Grid.GetLength(0) - 1, level.Grid.GetLength(0) - 1, Level.EXIT);
                            }
                        }

                        if (level.Grid[Player.currX, Player.currY] == Level.POWER)
                        {
                            powerFound = true;
                            Player.invincible = true;
                        }

                        if (level.Grid[Player.currX, Player.currY] == Level.EXIT)
                        {
                            exitFound = true;
                            winGame = true;
                        }

                        level.SetBoardCell(Player.prevX, Player.prevY, Level.EMPTY);
                        level.SetBoardCell(Player.currX, Player.currY, Level.PLAYER);
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
                                //Console.WriteLine("Enemy destroyed");
                                level.SetBoardCell(nemeses[i].currX, nemeses[i].currY, Level.PLAYER);
                                nemeses.Remove(nemeses[i]);
                                score += 100;
                                enemyDestroyed = true;
                            }
                        }
                    }

                    _renderer.DrawGrid(level);
                    Thread.Sleep(32);

                    if (timeEnd == true)
                    {
                        CheckGameOutcome(level);
                    }

                    if (powerFound == true)
                    {
                        powerFound = false;
                        //Console.WriteLine("You are invincible for 5 moves.  Destroy the enemy !!!");
                        Thread.Sleep(1000);
                    }

                    if (exitFound == true)
                    {
                        exitFound = false;
                        CheckGameOutcome(level);
                    }
                }
            }
        }

        public void CheckGameOutcome(ILevel level)
        {
            //Level level = null;

            if (winGame == true)
            {
                //Console.WriteLine("You collected all treasure and escaped the maze. CONGRATULATIONS champion !!!");
                //Console.WriteLine("Score: {0} points", score);
                Thread.Sleep(3000);
                finish = true;
                LevelNum++;

                winGame = true;
                _renderer.Counter.Stop();
            }
            else if (endGame == true)
            {
                level.SetBoardCell(Player.currX, Player.currY, Level.ENEMY);
                _renderer.DrawGrid(level);
                //Console.WriteLine("An enemy has destroyed you.  You have lost the game. :(");
                Console.ReadLine();
            }
            else if(timeEnd == true)
            {
                //Console.WriteLine("You ran out of time. GAME OVER !!!");
                Console.ReadLine();
            }
        }

        public ILevel LoadLevelFromFile()
        {
            CustomDataParser Parser = new CustomDataParser();
            string[] Filename = Parser.ReadFile(DataFile);
            int Count = Parser.ParseLevel(Filename);

            finish = false;

            TreasureMax = Parser.ParseTreasureCount(Filename);
            TreasureTotal = TreasureMax.ElementAt(LevelNum);

            Cells = Parser.ParseGridSize(Filename);
            Matrix = Cells.ElementAt(LevelNum);

            ArchEnemy = Parser.ParseEnemies(Filename);
            nemeses = ArchEnemy.ElementAt(LevelNum);

            Cache = Parser.ParseTreasure(Filename);
            valuables = Cache.ElementAt(LevelNum);

            Booster = Parser.ParsePower(Filename);
            abilities = Booster.ElementAt(LevelNum);

            return new Level(Matrix, nemeses, valuables, abilities);
        }
    }
}
