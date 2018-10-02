using _2DGame.Levels;
using GUITest;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace _2DGame.Game
{
    class GuiRenderer : IRenderer
    {
        //public Grid DynamicGrid = new Grid();
        public System.Windows.Controls.Label[,] Labels;
        public Countdown Counter { get; set; }

        LevelWindow _window;

        public GuiRenderer(LevelWindow window)
        {
            _window = window;
        }

        public void InitWindow(ILevel level)
        {
            Labels = new System.Windows.Controls.Label[level.Grid.GetLength(0), level.Grid.GetLength(1)];

            for (int i = 0; i < level.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < level.Grid.GetLength(1); j++)
                {
                    System.Windows.Controls.Label label1 = new System.Windows.Controls.Label();
                    //label1.Content = 1;
                    Labels[i, j] = label1;
                    label1.VerticalAlignment = VerticalAlignment.Center;
                    label1.HorizontalAlignment = HorizontalAlignment.Center;
                    label1.FontSize = 20;
                    Grid.SetColumn(label1, j);
                    Grid.SetRow(label1, i);
                    LevelWindow.DynamicGrid.Children.Add(label1);
                }
            }
        }

        public void DrawGrid(ILevel level)
        {
            _window.Dispatcher.Invoke(() =>
            {
                for (int x = 0; x < level.Grid.GetLength(0); x++)
                {
                    for (int y = 0; y < level.Grid.GetLength(1); y++)
                    {
                        if (level.Grid[x, y] == Level.PLAYER)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkOrange;
                        }
                        else if (level.Grid[x, y] == Level.TREASURE)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.YellowGreen;
                        }
                        else if (level.Grid[x, y] == Level.ENEMY)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkRed;
                        }
                        else if (level.Grid[x, y] == Level.POWER)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkBlue;
                        }
                        else if (level.Grid[x, y] == Level.EXIT)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkMagenta;
                        }
                        else if (level.Grid[x, y] == Level.EMPTY)
                        {
                            _window.Labels[x, y].Foreground = System.Windows.Media.Brushes.GhostWhite;
                        }
                        _window.Labels[x,y].Content = level.Grid[x, y];
                        //Console.ResetColor();

                        if (y == level.Grid.GetLength(1) - 1)
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
