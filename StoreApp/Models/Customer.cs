using StoreApp.Helpers;
using StoreApp.Services;
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
        public Customer()
        {
            _storeService = DependencyContainer.GetStoreService();
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
                       // _storeService.ShowCategories();
                        Console.Write("Выберите категорию по Id: ");
                        
                        int catId = int.Parse(Console.ReadLine());
                        _storeService.ShowProductsByCategory(catId);
                        //  Console.WriteLine("Товары пока не добавлены");
                        Console.ReadKey();
                        break;
                    case 1:
                        Console.WriteLine("Корзина в разработке");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Оформление заказа (будет позже)");
                        Console.ReadKey();
                        break;
                    case 3:
                        return; // выход из меню
                }
            }
        }
    }
}
