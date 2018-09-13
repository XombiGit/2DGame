using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DGame.Levels;
using _2DGame.Enemies;
using _2DGame.StationaryItems;

namespace _2DGame.DataParsers
{
    public class CustomDataParser : IDataParser
    {
        public List<Level> Levels = new List<Level>();
        public List<string> SplitTest = new List<string>();
        public string[] splitter;
        //public List<Enemy> adversaries = new List<Enemy>();
        public List<List<Enemy>> Antagonists = new List<List<Enemy>>();
        public List<List<Treasure>> Prizes = new List<List<Treasure>>();
        public List<List<PowerUp>> Serums = new List<List<PowerUp>>();
        public List<char[,]> Dimensions = new List<char[,]>();

        public List<List<Enemy>> ParseEnemies(string[] splitArray)
        {
            string Enemies = splitArray[3];
            string[] EnemiesSplitOne = Enemies.Split('\n');

            for (int j = 1; j <= Int32.Parse(splitArray[0]); j++)
            {
                string EnemyPoints = EnemiesSplitOne[j];
                string[] EnemySplitTwo = EnemyPoints.Split(';');
                //int j = 0;
                List<Enemy> adversaries = new List<Enemy>();

                for (int i = 1; i <= Int32.Parse(EnemySplitTwo[0]); i++)
                {
                    string EnemyNum = EnemySplitTwo[i];
                    string[] EnemySplitThree = EnemyNum.Replace("\r", string.Empty).Split(',');
                    int XPos = Int32.Parse(EnemySplitThree[0]);
                    int YPos = Int32.Parse(EnemySplitThree[1]);
                    EnemyType enemyType = (EnemyType)Int32.Parse(EnemySplitThree[2]);

                    Enemy villain = new Enemy(XPos, YPos, enemyType);
                    adversaries.Add(villain);
                    
                    //Console.WriteLine(Level.enemies[j].currX);
                    //Console.WriteLine(Level.enemies[j].currY);
                    //Console.WriteLine(Level.enemies[j].nemesis);
                    //j++;
                }

                Antagonists.Add(adversaries);
            }
            return Antagonists;
            //return adversaries;
        }

        public List<char[,]> ParseGridSize(string[] splitArray)
        {
            string GridDimensions = splitArray[2];
            string[] GridSplitOne = GridDimensions.Split('\n');

            for (int i = 1; i <= Int32.Parse(splitArray[0]); i++)
            {
                string GridPoints = GridSplitOne[i];
                //string[] GridSplitTwo = GridPoints.Split(';');
                //int j = 0;
                //List<char[,]> dimensions  = new List<char[,]>();

            
                //string GridNum = GridSplitTwo[i];
                string[] GridSplitTwo = GridPoints.Split(',');
                int XPos = Int32.Parse(GridSplitTwo[0]);
                int YPos = Int32.Parse(GridSplitTwo[1]);
                //EnemyType enemyType = (EnemyType)Int32.Parse(EnemySplitThree[2]);
                char[,] axis = new char[XPos, YPos];
                //Enemy villain = new Enemy(XPos, YPos, enemyType);
                Dimensions.Add(axis);
            }
            
            return Dimensions;
        }

        public int ParseLevel(string[] splitArray)
        {
            int LevelNum = Int32.Parse(splitArray[0]);
            return LevelNum;
        }

        public List<List<PowerUp>> ParsePower(string[] splitArray)
        {
            string Powers = splitArray[5];
            string[] PowersSplitOne = Powers.Split('\n');

            for (int j = 1; j <= Int32.Parse(splitArray[0]); j++)
            {
                string PowerPoints = PowersSplitOne[j];
                string[] PowersSplitTwo = PowerPoints.Split(';');
                List<PowerUp> potentials = new List<PowerUp>();
                //int j = 0;
                for (int i = 1; i <= Int32.Parse(PowersSplitTwo[0]); i++)
                {
                    string PowerNum = PowersSplitTwo[i];
                    string[] PowersSplitThree = PowerNum.Split(',');
                    PowerUp energy = new PowerUp(Int32.Parse(PowersSplitThree[0]), Int32.Parse(PowersSplitThree[1]));
                    potentials.Add(energy);
                }

                Serums.Add(potentials);
            }
            return Serums;
        }

        public List<List<Treasure>> ParseTreasure(string[] splitArray)
        {
            string Treasures = splitArray[4];
            string[] TreasuresSplitOne = Treasures.Split('\n');

            for (int j = 1; j <= Int32.Parse(splitArray[0]); j++)
            {
                string TreasurePoints = TreasuresSplitOne[1];
                string[] TreasuresSplitTwo = TreasurePoints.Split(';');
                List<Treasure> fortunes = new List<Treasure>();
           
                for (int i = 1; i <= Int32.Parse(TreasuresSplitTwo[0]); i++)
                {
                    string TreasureNum = TreasuresSplitTwo[i];
                    string[] TreasuresSplitThree = TreasureNum.Split(',');
                    Treasure precious = new Treasure(Int32.Parse(TreasuresSplitThree[0]), Int32.Parse(TreasuresSplitThree[1]));
                    fortunes.Add(precious);
                }

                Prizes.Add(fortunes);
            }
            return Prizes;
        }

        public string[] ReadFile(string filename)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filename))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line;

                   /* while((line = sr.ReadLine()) != null)
                    {
                        SplitTest.Add(line);
                    }

                    foreach(string word in SplitTest)
                    {
                        Console.WriteLine(word);
                    }*/

                    line = sr.ReadToEnd();
                    splitter = line.Split('#');

                    /*for (int i = 0; i < splitter.Length; i++)
                    {
                        Console.WriteLine(splitter[i]);
                    }*/
                    //ParseLevel(splitter);
                    //ParseGridSize(splitter);
                    //ParseEnemies(splitter);
                    //ParseTreasure(splitter);
                    //ParsePower(splitter);

                    return splitter;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
