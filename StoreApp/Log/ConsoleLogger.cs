using System;
using System.IO;
using StoreApp.Interface;

namespace StoreApp.Log
{
    public class ConsoleLogger : ILogger
    {
     public void Info(string message)
        {
            ConsoleLogPanel.Write($"[INFO]: {message}");
        }

        public void Error (string message)
        {
            ConsoleLogPanel.Write($"[ERROR]: {message}");
        }
    }
}