using StoreApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Log
{
    public class FileLogger : ILogger
    {
        private static readonly string logPath =
          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
                

        private void Write (string message)
        {
            File.AppendAllText(logPath, $"{DateTime.Now:HH:mm:ss} [INFO] {message} \n");

        }

        public void Error(string message)
        {
            Write($"ERROR: {message}");
        }

        public void Info(string message)
        {
            Write($"INFO: {message}");
        }
        
    }
}
