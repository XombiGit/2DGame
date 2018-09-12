using _2DGame.Enemies;
using _2DGame.Levels;
using _2DGame.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DGame.DataParsers;

namespace _2DGame
{
    public class Program
    {
        //static string DataFile = @"C:\Users\UnknownUser\Desktop\LevelParameters.txt";
        static void Main(string[] args)
        {
            //CustomDataParser parse = new CustomDataParser();
            //parse.ReadFile(DataFile);
            //IGameEngine begin = new GameEngine();
            IGameEngine begin = new TurnBasedGameEngine();
            //Console.ReadLine();
        }
    } 
}
