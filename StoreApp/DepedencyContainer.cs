using StoreApp.Data;
using StoreApp.Interface;
using StoreApp.Log;
using StoreApp.Services;

namespace StoreApp
{
    public static class DependencyContainer
    {
        public static IDataManager GetDataManager() =>
            new JsonDataManager();
        public static ILogger GetLogger() =>
            new CompositeLogger(
                new ConsoleLogger(),
                new FileLogger()
                );
        public static IApp GetApplication() =>
            new Application (GetAuthService(),
                GetRegistrationService(),
                GetLogger());
        public static IAuthService GetAuthService() =>
            new AuthService(GetDataManager());
        public static IUserRegistrationService GetRegistrationService() =>
            new UserRegistrationService(GetDataManager());
       public static ICartService GetCartService() =>
            new CartService(GetDataManager(),
                GetOrderService(),
                GetLogger()); 
        public static IOrderService GetOrderService() =>
            new OrderService(GetDataManager(),GetLogger());
        public static IStoreService GetStoreService() =>
            new StoreService(GetDataManager(),
                GetCartService(),
                GetOrderService());
        public static IAdminUserMenu GetAdminUserMenu() =>
            new AdminUserMenu(GetDataManager(),
                GetAdminUserService(),
                GetAdminUserViewer());
        public static IAdminStoreMenu GetAdminStoreMenu() =>
             new AdminStoreMenu(GetAdminStoreService());
        public static IAdminStoreService GetAdminStoreService()=> 
            new AdminStoreService(GetDataManager(),
                GetLogger());
        public static IAdminUserService GetAdminUserService() =>
            new AdminUserService(GetDataManager());
         public static IAdminUserViewer GetAdminUserViewer() =>
            new AdminUserViewer(GetDataManager(),
                GetOrderService(),
                GetCartService());
       

    }

}
