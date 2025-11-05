using StoreApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Interface
{
    public interface IAuthService
    {
       AuthenticationResult AuthenticateUser(string username, string password);

    }
}
