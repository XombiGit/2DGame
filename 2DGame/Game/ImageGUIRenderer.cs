using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using _2DGame.Levels;
using GUITest;

namespace _2DGame.Game
{
    class ImageGUIRenderer : IRenderer
    {
       
        public Countdown Counter { get; set; }

        GraphicsWindow _window;

        public ImageGUIRenderer(GraphicsWindow window)
        {
            _window = window;
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
                            _window.Images[x,y].Source = new BitmapImage(new Uri(@"Images\sunfire.png", UriKind.Relative));
                            //_window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkOrange;
                        }
                        else if (level.Grid[x, y] == Level.TREASURE)
                        {
                            _window.Images[x, y].Source = new BitmapImage(new Uri(@"Images\crystal.png", UriKind.Relative));
                            //_window.Labels[x, y].Foreground = System.Windows.Media.Brushes.YellowGreen;
                        }
                        else if (level.Grid[x, y] == Level.ENEMY)
                        {
                            _window.Images[x, y].Source = new BitmapImage(new Uri(@"Images\sentinel.png", UriKind.Relative));
                            //_window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkRed;
                        }
                        else if (level.Grid[x, y] == Level.POWER)
                        {
                            _window.Images[x, y].Source = new BitmapImage(new Uri(@"Images\phoenix.png", UriKind.Relative));
                            //_window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkBlue;
                        }
                        else if (level.Grid[x, y] == Level.EXIT)
                        {
                            _window.Images[x, y].Source = new BitmapImage(new Uri(@"Images\blink.png", UriKind.Relative));
                            //_window.Labels[x, y].Foreground = System.Windows.Media.Brushes.DarkMagenta;
                        }
                        else if (level.Grid[x, y] == Level.EMPTY)
                        {
                            _window.Images[x, y].Source = new BitmapImage(new Uri(@"Images\logo.png", UriKind.Relative));
                            //_window.Labels[x, y].Foreground = System.Windows.Media.Brushes.GhostWhite;
                        }
                        //_window.Labels[x, y].Content = level.Grid[x, y];
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
