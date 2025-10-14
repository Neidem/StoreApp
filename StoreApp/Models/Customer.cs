using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    internal class Customer : UserAccount
    {

        public override void ShowMenu()
        {
            Console.WriteLine("\nМеню покупателя:");
            Console.WriteLine("1. Купить товар");
            Console.WriteLine("2. Вернуть товар");
            Console.WriteLine("3. Выйти");
        }

    }
}
