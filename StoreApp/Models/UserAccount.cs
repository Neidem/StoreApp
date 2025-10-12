using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    class UserAccount
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }  // не сам пароль, а его хэш
        public string Role { get; set; }          // "admin" или "customer"
    }
}

