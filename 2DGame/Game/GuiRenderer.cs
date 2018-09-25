using _2DGame.Levels;
using GUITest;
using System.Windows;
using System.Windows.Threading;

namespace _2DGame.Game
{
    class GuiRenderer : IRenderer
    {
        public Countdown Counter { get; set; }

        LevelWindow _window;

        public GuiRenderer(LevelWindow window)
        {
            _window = window;
        }

        public void DrawGrid(Level level)
        {
            _window.Dispatcher.Invoke(() =>
            {
                for (int x = 0; x < level.grid.GetLength(0); x++)
                {
                    for (int y = 0; y < level.grid.GetLength(1); y++)
                    {
                        if (level.grid[x, y] == Level.PLAYER)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkOrange;
                        }
                        else if (level.grid[x, y] == Level.TREASURE)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.YellowGreen;
                        }
                        else if (level.grid[x, y] == Level.ENEMY)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkRed;
                        }
                        else if (level.grid[x, y] == Level.POWER)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkBlue;
                        }
                        else if (level.grid[x, y] == Level.EXIT)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkMagenta;
                        }
                        else if (level.grid[x, y] == Level.EMPTY)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.GhostWhite;
                        }
                        _window.Labels[x,y].Content = level.grid[x, y];
                        //Console.ResetColor();

                        if (y == level.grid.GetLength(1) - 1)
                        {
                            //Console.WriteLine();
                        }
                    }
                }
                //if (Counter.Second == 60)
                {
                    //There is an issue with the time shortening down to 3 digits instead of 4.  Leftover 0
                    //Console.Write("\r{0}:00", Counter.Minute);
                }
                //else
                {
                    //Console.Write("\r{0:D1}:{1:D2}", Counter.Minute, Counter.Second);
                }
            });

        }
    }
}
