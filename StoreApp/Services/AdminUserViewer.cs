using StoreApp.Interface;
using StoreApp.Models;
using StoreApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public class AdminUserViewer: IAdminUserViewer
    {
        private readonly IDataManager _dataManager;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public AdminUserViewer(IDataManager dataManager, IOrderService orderService, ICartService cartService)
        {
            _dataManager = dataManager;
            _orderService = orderService;
            _cartService = cartService;
        }

        public bool ShowOrdersUser(UserAccount user)
        {
            ConsoleHelper.ClearUIArea(3);
            Console.WriteLine($" Заказы пользователя [{user.Username}]:\n");

            var orders = _orderService.GetOrderByUsername(user.Username);

            if (orders.Count == 0)
            {
                Console.WriteLine("У пользователя нет заказов");

            }
            else
            {
                foreach (var order in orders)
                {
                    Console.WriteLine($"ID: {order.Id}");
                    Console.WriteLine($"Товар: {order.ProductId}");
                    Console.WriteLine($"Количество: {order.Quantity}");
                    Console.WriteLine($"Сумма: {order.TotalPrice} руб.");
                    Console.WriteLine($"Дата: {order.Date}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
            return true;
        }

        public bool ShowCartUser(UserAccount user)
        {

            var carts = _cartService.ShowCart(user);

            return true;
           
        }


    }
}
