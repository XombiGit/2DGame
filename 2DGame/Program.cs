﻿using _2DGame.Enemies;
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
    class Program
    {
        static void Main(string[] args)
        {
            //IGameEngine begin = new GameEngine();
            //IGameEngine begin = new TurnBasedGameEngine();
            CustomDataParser parse = new CustomDataParser();
            parse.ReadFile();
            Console.ReadLine();
        }
    } 
}
