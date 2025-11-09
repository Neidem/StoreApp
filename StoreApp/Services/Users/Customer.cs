using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace StoreApp.Services.Users
{
  public  class Customer : UserAccount,IUserMenu
    {
        private readonly IStoreService _storeService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public Customer()
        {
            _storeService = DependencyContainer.GetStoreService();
            _cartService = DependencyContainer.GetCartService();
            _orderService = DependencyContainer.GetOrderService();  
        }

        public override void ShowMenu()
        {
                      

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
                        _cartService.ShowCart(this);

                        Console.ReadKey();
                        break;
                    case 2:
                        //Console.WriteLine("Оформление заказа (будет позже)");
                        _orderService.ViewOrders(this);
                        Console.ReadKey();
                        break;
                    case 3:
                        ConsoleHelper.ClearUIArea(3);

                        return; // выход из меню
                }
            }
        }
    }
}
