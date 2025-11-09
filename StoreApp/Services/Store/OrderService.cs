using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Log;
using StoreApp.Models;

namespace StoreApp.Services.Store
{
    public class OrderService : IOrderService
    {
        private readonly IDataManager _dataManager;
        private readonly ILogger _logger;

        public OrderService(IDataManager dataManager, ILogger logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }


        public async Task PlaceOrder(UserAccount user, int productId, int quantity)
        {
            var products = _dataManager.LoadProducts();
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                Console.WriteLine("❌ Товар не найден.");
                return;
            }

            if (product.Quantity < quantity)
            {
                Console.WriteLine("Нет товара!");
                return;
            }

            await Task.Run(() =>
                {
                    // загружаем заказы пользователя
                    var orders = _dataManager.LoadOrders(user.Username);
                    // создаём заказ
                    var order = new Order
                    {
                        Id = Guid.NewGuid().GetHashCode(),
                        Username = user.Username,
                        ProductId = productId,
                        Quantity = quantity,
                        Date = DateTime.Now,
                        TotalPrice = product.Price * quantity
                    };


                    // сохраняем заказ в user (локально)


                    orders.Add(order);
                    _dataManager.SaveOrders(user.Username, orders);
                    // уменьшаем количество товара на складе
                    product.Quantity -= quantity;
                    _dataManager.SaveProducts(products);

                    var carts = _dataManager.LoadCarts();
                    if (carts.ContainsKey(user.Username))
                    {
                        var userCart = carts[user.Username];
                        var itemToRemove = userCart.FirstOrDefault(c => c.ProductId == productId);
                        if (itemToRemove != null)
                        {
                            userCart.Remove(itemToRemove);

                        }
                    }
                    _logger.Info(LogEvents.OrderCreated(user.Username, product.Name));
                    _dataManager.SaveCarts(carts);

                });


        }

        public List<Order> GetOrderByUsername(string username)
        {
            var orders = LoadOrders(username);
            return orders.Where(o => o.Username.Equals(username, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool ViewOrders(UserAccount user)
        {
            ConsoleHelper.ClearUIArea(3);
            var orders = LoadOrders(user.Username);



            if (!orders.Any())
            {
                Console.WriteLine(" У вас нет заказов.");
                return true;
            }
            while (true)
            {
                ConsoleHelper.ClearUIArea(3);
                Console.WriteLine($"\n Ваши заказы: {user.Username}");


                var products = _dataManager.LoadProducts();
                var menuItems = new List<string>();
                decimal totalAllOrders = 0;

                foreach (var order in orders)
                {
                    var product = products.FirstOrDefault(p => p.Id == order.ProductId);
                    if (product == null) continue;

                    decimal subtotal = product.Price + product.Quantity;
                    menuItems.Add($"{product.Name} - {order.Quantity}шт {product.Price} руб = {subtotal} руб");

                    //  Console.WriteLine($"{product?.Name ?? "Товар удалён"} — {order.Quantity} шт. — {order.TotalPrice:C} — {order.Date:g}");
                    totalAllOrders += subtotal;

                }
                menuItems.Add("Back");

                Console.WriteLine($"Общая сумма всех заказов: {totalAllOrders} руб \n");

                int choice = UIHelper.MenuSelect(menuItems.ToArray(), "Выберите действие");

                if (choice == menuItems.Count - 1)
                    return false;

                var selectedOrder = orders[choice];
                var selectedProduct = products.FirstOrDefault(p => p.Id == selectedOrder.ProductId);
                if (selectedProduct == null)
                    continue;

                if (!ShowOrderActions(user, selectedOrder, selectedProduct))
                    return false;
            }

        }



        public List<Order> LoadOrders(string username)
        {
            return _dataManager.LoadOrders(username);

        }

        public bool ShowOrderActions(UserAccount user, Order order, Product product)
        {
            while (true)
            {
                ConsoleHelper.ClearUIArea(3);
                Console.WriteLine($" Заказ: {product.Name}");
                Console.WriteLine($"Цена: {product.Price} руб × {order.Quantity} = {product.Price * order.Quantity} руб\n");

                int choice = UIHelper.MenuSelect(new[]
                {
                    "Отменить заказ",
                    "Назад"
                });

                switch (choice)
                {
                    case 0:
                        CancelOrder(user, order);
                        Console.ReadKey();
                        ViewOrders(user);
                        ConsoleHelper.ClearUIArea(3);
                        break;
                    case 1:
                        ConsoleHelper.ClearUIArea(3);
                        ViewOrders(user);
                        return false;
                }

            }


        }

        public void CancelOrder(UserAccount user, Order order)
        {
            var orders = _dataManager.LoadOrders(user.Username);

            var existing = orders.FirstOrDefault(o => o.Id == order.Id);
            if (existing != null)
            {
                orders.Remove(existing);
                _dataManager.SaveOrders(user.Username, orders);
                Console.WriteLine("заказ отменен");
            }

            else
            {
                Console.WriteLine("Заказ не найден");

            }
        }

    }

}
