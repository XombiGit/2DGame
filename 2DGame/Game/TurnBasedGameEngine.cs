﻿using _2DGame.Enemies;
using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2DGame.Game
{
    class TurnBasedGameEngine : IGameEngine
    {
        private static int treasureCount = 0;
        private static String error = "You've hit a wall.  TRY AGAIN";
        private static bool endGame = false;
        private static bool winGame = false;
        private static bool finish = false;
        private static bool powerFound = false;
        private static bool exitFound = false;
        //private static bool exit = false;
        private static int count = 0;
        private static int score = 0;
        public static List<Enemy> nemeses = new List<Enemy>();

        public TurnBasedGameEngine()
        {
            Initiate();
        }

        public void Initiate()
        {
            Player player = new Player(0, 0);
            Enemy vertical = new Enemy(0, 6, Enemy.EnemyType.Vertical.ToString());
            Enemy horizontal = new Enemy(8, 0, Enemy.EnemyType.Horizontal.ToString());
            Enemy random = new Enemy(2, 4, Enemy.EnemyType.Random.ToString());
            Enemy super = new Enemy(3, 5, Enemy.EnemyType.Super.ToString());
            nemeses.Add(vertical);
            nemeses.Add(horizontal);
            nemeses.Add(random);
            nemeses.Add(super);
            //Countdown counter = new Countdown(5, 60, false);
            Level level = new Level();

            while (finish != true)
            {
                String control = Console.ReadKey().KeyChar.ToString();
                //Thread thread = new Thread(() => player.MoveCombatant(Player.currX, Player.currY, control));
                player.MoveCombatant(Player.currX, Player.currY, control);

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
                            CheckGameOutcome();
                        }
                        else
                        {
                            Console.WriteLine("Enemy destroyed");
                            //Level.setPrevEnemyCell(nemeses[i].currX, nemeses[i].currY);
                            
                            Level.setBoardCell(nemeses[i].currX, nemeses[i].currY, Level.PLAYER);
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
                    string type = villain.enemyType.ToString();
                    villain.MoveCombatant(villain.currX, villain.currY, type);
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
                    //Level.setPrevUserCell(Player.prevX, Player.prevY);
                    //Level.setUserCell(Player.currX, Player.currY);
                    if (Level.grid[Player.currX, Player.currY] == Level.TREASURE)
                    {
                        //Level.setTreasureCellFound(Player.currX, Player.currY);
                        //Level.setBoardCell(Player.currX, Player.currY, Level.EMPTY);
                        treasureCount++;

                        if (treasureCount == 5)
                        {
                            //winGame = true;
                            //CheckGameOutcome();
                            //Level.setExit(Level.grid.GetLength(0) - 1, Level.grid.GetLength(0) - 1);
                            Level.setBoardCell(Level.grid.GetLength(0) - 1, Level.grid.GetLength(0) - 1, Level.EXIT);
                        }
                    }

                    if (Level.grid[Player.currX, Player.currY] == Level.POWER)
                    {
                        //Level.setPowerCellUsed(Player.currX, Player.currY);
                        //Level.setBoardCell(Player.currX, Player.currY, Level.EMPTY);
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

                /*if (Level.grid[Player.currX, Player.currY] == Level.TREASURE)
                {
                    //Level.setTreasureCellFound(Player.currX, Player.currY);
                    Level.setBoardCell(Player.currX, Player.currY, Level.EMPTY);
                    treasureCount++;
           
                    if (treasureCount == 5)
                    {
                        //winGame = true;
                        //CheckGameOutcome();
                        //Level.setExit(Level.grid.GetLength(0) - 1, Level.grid.GetLength(0) - 1);
                        Level.setBoardCell(Level.grid.GetLength(0) - 1, Level.grid.GetLength(0) - 1, Level.EXIT);
                    }
                }*/

                /*if (Level.grid[Player.currX, Player.currY] == Level.POWER)
                {
                    //Level.setPowerCellUsed(Player.currX, Player.currY);
                    Level.setBoardCell(Player.currX, Player.currY, Level.EMPTY);
                    Player.invincible = true;
                    Console.WriteLine("You are invincible for 5 moves.  Destroy the enemy !!!");
                    Thread.Sleep(1000);
                }*/

                /*if ((vertical.currX == Player.currX && vertical.currY == Player.currY) ||
                    (horizontal.currX == Player.currX && horizontal.currY == Player.currY) ||
                    (random.currX == Player.currX && random.currY == Player.currY) ||
                    (super.currX == Player.currX && super.currY == Player.currY))*/
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
                            //how to destroy an object 
                            Console.WriteLine("Enemy destroyed");
                            //Level.setPrevEnemyCell(nemeses[i].currX, nemeses[i].currY);
                            Level.setBoardCell(nemeses[i].currX, nemeses[i].currY, Level.PLAYER);
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

                Console.Clear();
                Level.drawGrid(Level.grid);

                if (powerFound == true)
                {
                    powerFound = false;
                    Console.WriteLine("You are invincible for 5 moves.  Destroy the enemy !!!");
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
                Level.drawGrid(Level.grid);
                Console.WriteLine("An enemy has destroyed you.  You have lost the game. :(");
                Console.ReadLine();
            }
        }
    }
}
