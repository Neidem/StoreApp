using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Models;
using System.IO.Pipes;
using System.Net.WebSockets;

namespace StoreApp.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IDataManager _dataManager;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        public AdminUserService(IDataManager dataManager, IOrderService orderService, ICartService cartService)
        {
            _dataManager = dataManager;
            _orderService = orderService;
            _cartService = cartService;
        }

        public bool ViewAllUsers(UserAccount Admin)
        {
            Console.Clear();
            Console.WriteLine($"\n Администратор: {Admin.Username}");
            Console.WriteLine("Загрузка списка пользователей... ");

            var users = _dataManager.LoadUsers();
            if (!users.Any())
            {
                Console.WriteLine("Пользвоателей нет");
                Console.ReadKey();
                return false;
            }

            while (true)
            {
                Console.Clear();
                string[] options = users
                    .Select(u => $"{u.Username}")
                    .Append("Назад")
                    .ToArray();


                int choice = UIHelper.MenuSelect(options, "Выберите пользователя:");

                if (choice == options.Length - 1)
                {
                    return false;
                }

                var selectedUser = users[choice];
                if (!ShowUserActions(selectedUser))
                {
                    return false;
                }


            }
        }

        private bool ShowUserActions(UserAccount user)
        {
            while (true)
            {
                int choice = UIHelper.MenuSelect(new[]
                {
                    "Забанить/Разбанить",
                    "Удалить пользователя",
                    "Заказы пользователя",
                    "Корзина пользователя",
                    "Назад"

               }, $"Пользователь: {user.Username}");

                switch (choice)
                {
                    case 0:
                        BanUser(user.Username);
                        break;
                    case 1:
                        DeleteUser(user);
                        break;
                    case 2:
                        ShowOrdersUser(user);
                        break;
                    case 3:
                        ShowCartUser(user);
                        break;
                    case 4:
                        return true;

                }

            }

        }

        private void BanUser(string username)
        {
            var users = _dataManager.LoadUsers();
            var user = users.FirstOrDefault(u=>u.Username==username);

            if(user == null)
            {
                Console.WriteLine($"Пользователь {username} не найден! ");
                return;
            }

            if(user.Role =="admin")
            {
                Console.WriteLine("Нельзя блокировать администратора");
                return;
            }

            user.IsBanned = true;
            _dataManager.SaveUsers(users);
            Console.WriteLine(user.IsBanned
                ? $"Пользователь {user.Username} был забанен!"
                : $"Пользователь {user.Username} разбанен!");
            Console.ReadKey();
          
        }
      

        //public void UpdateUser(UserAccount user)
        //{
        //    var ban = true;
        //    var users = _dataManager.LoadUsers();
        //    var existing = users.FirstOrDefault(u => u.Username == u.Username);
        //    if (existing == null)
        //    {
        //        Console.WriteLine($"Пользователь {user.Username} не найден");
        //        return;
        //    }

        //    existing.PasswordHash = user.PasswordHash;
        //    existing.Role = user.Role;
        //    ban = user.IsBanned;

        //   _dataManager.SaveUsers(users);


        //}

        private void DeleteUser(UserAccount user)
        {
            var users = _dataManager.LoadUsers();
            var existing = users.FirstOrDefault(u => u.Username == user.Username);

            if (existing == null)
            {
                Console.WriteLine($"Пользователь {user.Username} не найден ");
                return;
            }

            if (user.Role == "admin")
            {
                Console.WriteLine("Нельзя удалять администратора");
                Console.ReadKey();
                return;
            }

            if (UIHelper.Confirm($"Удалить пользователя {user.Username} ?"))
            {
                DeleteUser(user);
                Console.WriteLine($"Пользователь {user.Username} удалён.");
                Console.ReadKey();
            }

            users.Remove(existing);
            _dataManager.SaveUsers(users);

            Console.WriteLine($"Пользователь {user.Username} успешно удалён");

        }

        private bool ShowOrdersUser(UserAccount user)
        {
            Console.Clear();
            Console.WriteLine($"📦 Заказы пользователя [{user.Username}]:\n");

            var orders = _orderService.GetOrderByUsername(user.Username);
            
            if(orders.Count==0)
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

        private bool ShowCartUser(UserAccount user)
        {

            var carts = _cartService.ShowCart(user);

            return true;
            //Console.Clear();
            //Console.WriteLine($"{user.Username}");

            //var carts = _dataManager.LoadCarts();
            //if(!carts.ContainsKey(user.Username) || carts[user.Username].Count == 0)
            //{
            //    Console.WriteLine("❌ Корзина пуста.");
            //    Console.ReadKey();
            //    return false;
            //}

            //var products = _dataManager.LoadProducts();
            //var carItems = carts[user.Username];

            //decimal total = 0;

            //foreach( var item in carItems )
            //{
            //    var product = products.FirstOrDefault(p=>p.Id==item.ProductId);
            //    if (product == null)
            //        continue;

            //    decimal subtotal = product.Price * item.Quantity;
            //    total += subtotal;
            //    Console.WriteLine($"{product.Name} — {item.Quantity} шт × {product.Price} руб = {subtotal} руб");

            //}
            //Console.WriteLine($"\nИтого: {total} руб");
            //Console.WriteLine("\nНажмите любую клавишу для возврата...");
            //Console.ReadKey();
            //return true;
        }
        

    }
}
