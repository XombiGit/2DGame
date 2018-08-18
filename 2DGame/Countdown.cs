using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame
{
    class Countdown
    {
        public int minute { get; set; }
        public int second { get; set; }
        public bool end { get; set; }

        public Countdown(int minute, int second, bool end)
        {
            this.minute = minute;
            this.second = second;
            this.end = end;
            //tickTock(this.minute, this.second);
            //Is there away to have a something run on one line while other lines do something else on the console ?
        }

        public void tickTock(int minute, int second)
        {
            for(int x = minute; x >= 0; x--)
            {
                minute = x;

                for(int y = second; y >= 0; y--)
                {
                    second = y;
                    //Console.Write("\r{0}:{1}", minute, second);
                    System.Threading.Thread.Sleep(1000);
                }
                    
            }

            end = true;
        }

        
    }
}
