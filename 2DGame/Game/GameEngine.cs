using _2DGame.Enemies;
using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public static List<Enemy> nemeses = new List<Enemy>();

        public GameEngine()
        {
            Initiate();
        }

        public void Initiate()
        {
            Player player = new Player(0, 0);
            Enemy vertical = new Enemy(0, 6, Enemy.EnemyType.Vertical.ToString());
            //Enemy horizontal = new Enemy(8, 0, Enemy.EnemyType.Horizontal.ToString());
            //Enemy random = new Enemy(2, 4, Enemy.EnemyType.Random.ToString());
            //Enemy super = new Enemy(3, 5, Enemy.EnemyType.Super.ToString());
            //Countdown counter = new Countdown(0, 20, false);
            //nemeses.Add(vertical);
            //nemeses.Add(horizontal);
            //nemeses.Add(random);
            //nemeses.Add(super);
            Level level = new Level();
            nemeses = Level.enemies;

            //
            // Starting enemy thread
            // This is confusing, I need an enemy object to run the thread ??????
            //Thread verticalEnemyThread = new Thread(() => UpdateEnemyThread(vertical));
            //verticalEnemyThread.Name = "Vertical";
            //verticalEnemyThread.Start();
            Thread enemyThread = new Thread(new ThreadStart(UpdateEnemyThread));
            enemyThread.Name = "Enemies";
            enemyThread.Start();

            //
            // Starting render thread (redraw level)
            //
            Thread levelThread = new Thread(() => UpdateLevel());
            levelThread.Name = "Level";
            levelThread.Start();


            Thread playerThread = new Thread(() => UpdatePlayer(player));
            playerThread.Name = "Player";
            playerThread.Start();

            Thread counterThread = new Thread(() => UpdateCounter(Level.Counter));
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
        public static void UpdatePlayer(Player player)
        {
            while (finish != true)
            {
                String control = Console.ReadKey(true).KeyChar.ToString();
                player.MoveCombatant(Player.currX, Player.currY, control);
            }
        }

        /// <summary>
        /// Function that happens in the UpdateEnemy Thread
        /// </summary>
        public static void UpdateEnemyThread()
        {
            while (finish != true)
            {
                for(int i = 0; i <= nemeses.Count-1; i++)
                {
                    string type = nemeses[i].enemyType;
                    nemeses[i].MoveCombatant(nemeses[i].currX, nemeses[i].currY, type);
                    //vertical.MoveCombatant(vertical.currX, vertical.currY, vertical.nemesis);
                    Thread.Sleep(700);
                }
            }
        }

        /// <summary>
        /// Function that redraws the level
        /// </summary>

        public static void UpdateLevel()
        {
            while (finish != true)
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

                for (int i = nemeses.Count - 1; i >= 0; i--)
                {
                    if (nemeses[i].currX == Player.currX && nemeses[i].currY == Player.currY)
                    {
                        if (Player.invincible == false)
                        {
                            endGame = true;
                            CheckGameOutcome();
                        }
                        else
                        {
                            Console.WriteLine("Enemy destroyed");
                            Level.setBoardCell(nemeses[i].currX, nemeses[i].currY, Level.PLAYER);
                            nemeses.Remove(nemeses[i]);
                            score += 100;
                            enemyDestroyed = true;
                        }
                    }
                }

                if (Player.currX < 0)
                {
                    Console.WriteLine(error);
                    Thread.Sleep(1000);
                    Player.currX = Player.currX + 1;
                    Level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                }
                else if (Player.currY < 0)
                {
                    Console.WriteLine(error);
                    Thread.Sleep(1000);
                    Player.currY = Player.currY + 1;
                    Level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                }
                else if (Player.currX >= (Level.grid.GetLength(0)))
                {
                    Console.WriteLine(error);
                    Thread.Sleep(1000);
                    Player.currX = Player.currX - 1;
                    Level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                }
                else if (Player.currY >= (Level.grid.GetLength(0)))
                {
                    Console.WriteLine(error);
                    Thread.Sleep(1000);
                    Player.currY = Player.currY - 1;
                    Level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                }
                else
                {
                    if (Level.grid[Player.currX, Player.currY] == Level.TREASURE)
                    {
                        treasureCount++;
              
                        if (treasureCount == 5)
                        {
                            Level.setBoardCell(Level.grid.GetLength(0) - 1, Level.grid.GetLength(0) - 1, Level.EXIT);
                        }
                    }

                    if (Level.grid[Player.currX, Player.currY] == Level.POWER)
                    {
                        powerFound = true;
                        Player.invincible = true;
                    }

                    if (Level.grid[Player.currX, Player.currY] == Level.EXIT)
                    {
                        exitFound = true;
                        winGame = true;
                    }

                    Level.setBoardCell(Player.prevX, Player.prevY, Level.EMPTY);
                    Level.setBoardCell(Player.currX, Player.currY, Level.PLAYER);
                }

                for (int i = nemeses.Count - 1; i >= 0; i--)
                {
                    if (nemeses[i].currX == Player.currX && nemeses[i].currY == Player.currY)
                    {

                        if (Player.invincible == false)
                        {
                            endGame = true;
                            CheckGameOutcome();
                        }
                        else
                        {
                            Console.WriteLine("Enemy destroyed");
                            Level.setBoardCell(nemeses[i].currX, nemeses[i].currY, Level.PLAYER);
                            nemeses.Remove(nemeses[i]);
                            score += 100;
                            enemyDestroyed = true;
                        }
                    }
                }

                Level.DrawGrid(Level.grid);
                Thread.Sleep(16);

                if(timeEnd == true)
                {
                    CheckGameOutcome();
                }

                if (powerFound == true)
                {
                    powerFound = false;
                    Console.WriteLine("You are invincible for 5 moves.  Destroy the enemy !!!");
                    Thread.Sleep(1000);
                }

                if (exitFound == true)
                {
                    exitFound = false;
                    CheckGameOutcome();
                }
            }
        }

        public static void CheckGameOutcome()
        {
            if (winGame == true)
            {
                Console.WriteLine("You collected all treasure and escaped the maze. CONGRATULATIONS champion !!!");
                Console.WriteLine("Score: {0} points", score);
                Console.ReadLine();
            }
            else if (endGame == true)
            {
                Level.setBoardCell(Player.currX, Player.currY, Level.ENEMY);
                Level.DrawGrid(Level.grid);
                Console.WriteLine("An enemy has destroyed you.  You have lost the game. :(");
                Console.ReadLine();
            }
            else if(timeEnd == true)
            {
                Console.WriteLine("You ran out of time. GAME OVER !!!");
                Console.ReadLine();
            }
        }
    }
}
