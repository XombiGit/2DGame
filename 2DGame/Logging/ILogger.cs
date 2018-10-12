using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.Logging
{
    public interface ILogger
    {
        void LogDebug(string info, string className, string method);
        void LogInfo(string info, string className, string method);
        void LogError(string info, string className, string method);
    }
}
