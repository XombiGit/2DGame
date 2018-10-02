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

        private bool cancel = false;

        public Countdown(int minute, int second, bool end)
        {
            Minute = minute;
            Second = second;
            End = end;
            //tickTock(this.minute, this.second);
            //Is there away to have a something run on one line while other lines do something else on the console ?
        }

        public void Stop()
        {
            cancel = true;
        }

        public bool tickTock(int minute, int second)
        {
            for(int x = minute; x >= 0; x--)
            {
                Minute = x;

                for (int y = second; y >= 0; y--)
                {
                    Second = y;
                    System.Threading.Thread.Sleep(1000);

                    if (cancel)
                    {
                        return false;
                    }
                }
                    
            }
            return End = true;
        }

        
    }
}
