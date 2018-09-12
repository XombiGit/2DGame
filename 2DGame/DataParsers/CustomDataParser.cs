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
        public List<PowerUp> potentials = new List<PowerUp>();
        public List<Treasure> fortunes = new List<Treasure>();

        public int NumLevels { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumEnemies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumTreasures { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumPowers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public char[,] ParseGridSize(string[] splitArray)
        {
            string GridDimensions = splitArray[2];
            string[] GridSplit = GridDimensions.Split('\n');

            string Testing = GridSplit[1];
            string[] TestingGrid = Testing.Split(',');
            char[,] Size = new char[Int32.Parse(TestingGrid[0]), Int32.Parse(TestingGrid[1])];
            return Size;
        }

        public void ParseLevel(string[] splitArray)
        {
            string LevelIDs = splitArray[1];
            string[] LevelSplit = LevelIDs.Split('\n');

            //if (LevelSplit[0].Equals("levelID"))
            //{
                for (int i = 1; i < LevelSplit.Length; i++)
                {
                    Console.WriteLine(LevelSplit[i]);
                }
            //}
        }

        public List<PowerUp> ParsePower(string[] splitArray)
        {
            string Powers = splitArray[5];
            string[] PowersSplitOne = Powers.Split('\n');

            string PowerPoints = PowersSplitOne[1];
            string[] PowersSplitTwo = PowerPoints.Split(';');
            //int j = 0;
            for (int i = 1; i <= Int32.Parse(PowersSplitTwo[0]); i++)
            {
                string PowerNum = PowersSplitTwo[i];
                string[] PowersSplitThree = PowerNum.Split(',');
                PowerUp energy = new PowerUp(Int32.Parse(PowersSplitThree[0]), Int32.Parse(PowersSplitThree[1]));
                potentials.Add(energy);
            }

            return potentials;
        }

        public List<Treasure> ParseTreasure(string[] splitArray)
        {
            string Treasures = splitArray[4];
            string[] TreasuresSplitOne = Treasures.Split('\n');

            string TreasurePoints = TreasuresSplitOne[1];
            string[] TreasuresSplitTwo = TreasurePoints.Split(';');
            //int j = 0;
            for (int i = 1; i <= Int32.Parse(TreasuresSplitTwo[0]); i++)
            {
                string TreasureNum = TreasuresSplitTwo[i];
                string[] TreasuresSplitThree = TreasureNum.Split(',');
                Treasure precious = new Treasure(Int32.Parse(TreasuresSplitThree[0]), Int32.Parse(TreasuresSplitThree[1]));
                fortunes.Add(precious);
            }

            return fortunes;
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
