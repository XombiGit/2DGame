using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DGame.Levels;

namespace _2DGame.Game
{
    public class ConsoleRenderer : IRenderer
    {
        public Countdown Counter { get; set; }

        public ConsoleRenderer()
        {
            Counter = new Countdown(3, 60, false);
        }



        public void DrawGrid(Level level)
        {
            Console.SetCursorPosition(0, 0);
            for (int x = 0; x < level.grid.GetLength(0); x++)
            {
                for (int y = 0; y < level.grid.GetLength(1); y++)
                {
                    if (level.grid[x, y] == Level.PLAYER)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (level.grid[x, y] == Level.TREASURE)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (level.grid[x, y] == Level.ENEMY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (level.grid[x, y] == Level.POWER)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (level.grid[x, y] == Level.EXIT)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else if (level.grid[x, y] == Level.EMPTY)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(level.grid[x, y]);
                    Console.ResetColor();

                    if (y == level.grid.GetLength(1) - 1)
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
    }
}
