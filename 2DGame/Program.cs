using _2DGame.Enemies;
using _2DGame.Levels;
using _2DGame.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DGame.DataParsers;
using System.Windows;


using GUITest;
using System.Threading;
using System.Windows.Threading;
using _2DGame.Logging;

namespace _2DGame
{

    public class Program
    {
        //static string DataFile = @"C:\Users\UnknownUser\Desktop\LevelParameters.txt";
        [STAThread]
        static void Main(string[] args)
        {
            //CustomDataParser parse = new CustomDataParser();
            //parse.ReadFile(DataFile);
            IRenderer renderer;

            if (args.Length == 0 || string.Equals(args[0], "false", StringComparison.OrdinalIgnoreCase))
            {
                renderer = new ConsoleRenderer();
            }
            else
            {

                LevelWindow test = new LevelWindow();
                test.Show();

                //GraphicsWindow testy = new GraphicsWindow();
                //testy.Show();
                renderer = new GuiRenderer(test);
                //renderer = new ImageGUIRenderer(testy);
            }

            Thread gameThread = new Thread(() => StartGame(renderer));
            gameThread.Start();

            Dispatcher.Run();


            //Console.ReadLine();
        }

        public static void StartGame(IRenderer renderer)
        { 
            Logger logger = new Logger();
            //IGameEngine begin = new GameEngine(renderer, logger);
            IGameEngine begin = new TurnBasedGameEngine(renderer);
        }

    }
}
