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
                    "Просмотреть товары", // choice = 0
                    "Добавить в корзину",
                    "Оформить заказ",
                    "Выйти"
                }, "Меню покупателя");

                switch (choice)
                {
                    case 0:
                        _storeService.ShowProducts();
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
