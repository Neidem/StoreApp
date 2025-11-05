using StoreApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
   public  class UserAccount : IUserMenu
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }  // не сам пароль, а его хэш
        public string Role { get; set; }          // "admin" или "customer"
        public bool IsBanned { get; set; }
        public List<CartItem> Cart { get; set; } = new List<CartItem>();

        public List<Order> Orders { get; set; } = new();   

        public virtual void ShowMenu()
        {
            Console.WriteLine("Базовое меню (по умолчанию)");
        }


    }
}

