using StoreApp.Data;
using StoreApp.Services;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public static class DependencyContainer
    {
        public static IDataManager GetDataManager() => new JsonDataManager();
        public static IAuthService GetAuthService() => new AuthService(GetDataManager());
        public static IUserRegistrationService GetRegistrationService() => new UserRegistrationService(GetDataManager());
        public static IStoreService GetStoreService() => new StoreService(GetDataManager());
    
    
    }

}
