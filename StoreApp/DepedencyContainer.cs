using StoreApp.Data;
using StoreApp.Interface;
using StoreApp.Services;

namespace StoreApp
{
    public static class DependencyContainer
    {
        public static IDataManager GetDataManager() =>
            new JsonDataManager();
        
        public static IAuthService GetAuthService() =>
            new AuthService(GetDataManager());
        public static IUserRegistrationService GetRegistrationService() =>
            new UserRegistrationService(GetDataManager());
       public static ICartService GetCartService() =>
            new CartService(GetDataManager()); 
        public static IOrderService GetOrderService() =>
            new OrderService(GetDataManager());
        public static IStoreService GetStoreService() =>
            new StoreService(GetDataManager(), GetCartService());
        
        

    }

}
