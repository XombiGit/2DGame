﻿using _2DGame.Enemies;
using _2DGame.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame
{
    public class Player : ICombatant
    {
        public static int currX = 0;
        public static int currY = 0;
        public static int prevX = 0;
        public static int prevY = 0;
        public static bool invincible = false;
        public int count = 0;

        public Player(int row, int col)
        {
            currX = row;
            currY = col;
            Level.setUserCell(currX, currY);
        }
        int ICombatant.row
        {
            get
            {
                return currX;
            }
            set
            {
                currX = value;
            }
        }

        int ICombatant.col
        {
            get
            {
                return currY;
            }
            set
            {
                currY = value;
            }
        }

        int ICombatant.prevRow
        {
            get
            {
                return prevX;
            }
            set
            {
                prevX = value;
            }
        }

        int ICombatant.prevCol
        {
            get
            {
                return prevY;
            }
            set
            {
                prevY = value;
            }
        }

        public void MoveCombatant(int row, int col, string move)
        {

            prevX = row;
            prevY = col;
            Level.setPrevUserCell(Player.prevX, Player.prevY);


            switch (move)
            {
                case "w":
                    row -= 1;
                    break;

                case "a":
                    col -= 1;
                    break;

                case "s":
                    row += 1;
                    break;

                case "d":
                    col += 1;
                    break;

                default:
                    Console.WriteLine("You can only press w(UP), a(LEFT), s(DOWN), and d(RIGHT)");
                    System.Threading.Thread.Sleep(3000);
                    break;
            }

            currX = row;
            currY = col;

            if (invincible == true)
            {
                count++;

                if (count == 5)
                {
                    Player.invincible = false;
                    count = 0;
                }
            }
        }
    }
}
