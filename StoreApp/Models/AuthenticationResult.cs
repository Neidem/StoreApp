using StoreApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Models
{
    public class AuthenticationResult
    {
        public bool Succes { get; set; }
        public bool IsBanned { get; set; }
        public UserAccount User { get; set; }
        public IUserMenu UserMenu { get; set; }
    }
}
