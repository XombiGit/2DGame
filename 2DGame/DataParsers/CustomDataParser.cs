using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DGame.Levels;

namespace _2DGame.DataParsers
{
    public class CustomDataParser : IDataParser
    {
        public List<Level> Levels = new List<Level>();
        public List<string> SplitTest = new List<string>();
        public string[] splitter;

        public int NumLevels { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumEnemies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumTreasures { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumPowers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ParseEnemies(string[] splitArray)
        {
            throw new NotImplementedException();
        }

        public void ParseLevel(string[] splitArray)
        {
            throw new NotImplementedException();
        }

        public void ParsePowers(string[] splitArray)
        {
            throw new NotImplementedException();
        }

        public void ParseTreasure(string[] splitArray)
        {
            throw new NotImplementedException();
        }

        public void ReadFile()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(@"C:\Users\UnknownUser\Desktop\LevelParameters.txt"))
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
                    splitter = line.Split('\n');
                    for (int i = 0; i < splitter.Length; i++)
                    {
                        Console.WriteLine(splitter[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
