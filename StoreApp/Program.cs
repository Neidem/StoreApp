using StoreApp.Enums;
using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Log;
using StoreApp.Models;


namespace StoreApp
{
    sealed class Program
    {
        private static void Main(string[] args)
        {
            DependencyContainer.GetApplication().Run();
        }
    }
}