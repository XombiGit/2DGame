using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame
{
    public class Countdown
    {
        public int Minute { get; set; }
        public int Second { get; set; }
        public bool End { get; set; }

        public Countdown(int minute, int second, bool end)
        {
            Minute = minute;
            Second = second;
            End = end;
            //tickTock(this.minute, this.second);
            //Is there away to have a something run on one line while other lines do something else on the console ?
        }

        public bool tickTock(int minute, int second)
        {
            for(int x = minute; x >= 0; x--)
            {
                Minute = x;

                for(int y = second; y >= 0; y--)
                {
                    Second = y;
                    //Console.SetCursorPosition(0, 11);
                    //Console.Write("\r{0}:{1}", minute, second);
                    System.Threading.Thread.Sleep(1000);
                }
                    
            }
            return End = true;
        }

        
    }
}
