using StoreApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Log
{
    public class CompositeLogger : ILogger
    {

        private readonly List<ILogger> _loggers = new();


        public CompositeLogger(params ILogger[] loggers)
        {
            _loggers.AddRange(loggers);
        }

        public void Info (string msg)
        {
            foreach (var l in _loggers) 
                l.Info (msg);
        }

        public void Error(string msg)
        {
            foreach(var l in _loggers)
                l.Error (msg);
        }
    }
}
