using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGame.Logging
{
    class Logger : ILogger
    {
        public void LogDebug(string info, string className, string method)
        {
            throw new NotImplementedException();
        }

        public void LogError(string info, string className, string method)
        {
            //Append to File
            throw new NotImplementedException();
        }

        public void LogInfo(string info, string className, string method)
        {
            Debug.WriteLine($"Class Name: {className}, Method: {method}, Info: {info}");
        }
    }
}
