using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    internal class Admin : UserAccount
    {

        public override void ShowMenu()
        {
            Console.WriteLine("\nМеню администратора:");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Просмотреть пользователей");
            Console.WriteLine("3. Выйти");

        }


    }
}
