using StoreApp.Helpers;
using StoreApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace StoreApp.Models
{
  public  class Customer : UserAccount,IUserMenu
    {
        private readonly IStoreService _storeService;
        private readonly ICartService _cartService;
        public Customer()
        {
            _storeService = DependencyContainer.GetStoreService();
            _cartService = DependencyContainer.GetCartService();
        }

        public override void ShowMenu()
        {
           IOrderService orderService = DependencyContainer.GetOrderService();  
            

            while (true)
            {
                int choice = UIHelper.MenuSelect(new[]
                {
                    "Категория товаров", // choice = 0
                    "Моя Корзина",
                    "Мои Заказы",
                    "Выйти"
                }, $"Добро пожаловать,{Username}");

                switch (choice)
                {
                    case 0:
                       // Console.Write("Выберите категорию по Id: ");
                        _storeService.ShowCategories(this);
                        
                        
                       // int catId = int.Parse(Console.ReadLine());
                        //_storeService.ShowProductsByCategory(catId);
                        //  Console.WriteLine("Товары пока не добавлены");
                        Console.ReadKey();
                        break;
                    case 1:
                        //Console.WriteLine("Корзина в разработке");
                        _cartService.ShowCart();

                        Console.ReadKey();
                        break;
                    case 2:
                        //Console.WriteLine("Оформление заказа (будет позже)");
                        
                        Console.ReadKey();
                        break;
                    case 3:
                        return; // выход из меню
                }
            }
        }
    }
}
