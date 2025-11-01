using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Models;

namespace StoreApp.Services
{
    public class CartService : ICartService
    {
        private readonly IDataManager _dataManager;
        private readonly IOrderService _orderService;

        //private readonly IStoreService _storeService;
        private readonly List<Product> _cartItems = new();

        public CartService(IDataManager dataManager, IOrderService orderService)
        {
            _orderService = orderService;
            _dataManager = dataManager;
        }

        public void AddToCart(UserAccount user, int productId, int quantity)
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
                Console.WriteLine("Нет товара на складе!");
                Console.ReadKey();
                return;
            }

            var carts = _dataManager.LoadCarts();
            if (!carts.ContainsKey(user.Username))
                carts[user.Username] = new List<CartItem>();


            var existing = carts[user.Username].FirstOrDefault(c => c.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
                Console.WriteLine($" Количество {product.Name} обновлено: теперь {existing.Quantity} шт.");
            }
            else
            {
                carts[user.Username].Add(new CartItem { ProductId = productId, Quantity = quantity });


                Console.WriteLine($" {product.Name} добавлен в корзину ({quantity} шт).");


            }
            _dataManager.SaveCarts(carts);
            Console.ReadKey(true);
        }


        public void ShowCart(UserAccount user)
        {
            Console.Clear();
            var carts = _dataManager.LoadCarts();
            if (!carts.ContainsKey(user.Username) || carts[user.Username].Count == 0)
            {
                Console.WriteLine("Корзина пуста");
                return;
            }

            while (true)
            {
                Console.Clear();    
                Console.WriteLine("\n Товары в корзине:");
                
                var products = _dataManager.LoadProducts();
                var cartItems = carts[user.Username];
                decimal total = 0;

                var menuItems = new List<string>();
                foreach (var item in cartItems)
                {
                    var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                    if (product == null) continue;

                    decimal subtotal = product.Price * item.Quantity;
                    menuItems.Add($"{product.Name} - {item.Quantity} шт {product.Price} рую = {subtotal} руб");
                    total += subtotal;
                }

                menuItems.Add("Back");

                Console.WriteLine($"\n Итого: {total}");

                int choice = UIHelper.MenuSelect(menuItems.ToArray(),"Выберите товар для действия");


                if (choice == menuItems.Count - 1)
                    break;

                var selectedItem = cartItems[choice];
                var selectedProduct = products.FirstOrDefault(p=>p.Id == selectedItem.ProductId);
                
                if(selectedItem == null)
                {
                    Console.WriteLine("Ошибка:товар не найден в корзине");
                    continue;
                }

                ShowCartItemActions(user, selectedProduct);

            }
        }
        public void ClearCart() => _cartItems.Clear();

        public bool ShowCartItemActions(UserAccount user, Product product)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($" {product.Name}");
                Console.WriteLine($"Цена: {product.Price} руб");
               

                int choice = UIHelper.MenuSelect(new[]
                {
                    "Оформить заказ",
                    "Удалить из корзины",
                    "Назад"
                });

                switch (choice)
                {
                    case 0:
                        _orderService.PlaceOrder(user, product.Id, 1);
                        Console.WriteLine(" Заказ оформлен!");
                        Console.ReadKey(true);
                        break;
                    case 1:
                        RemoveFromCart(user, product.Id,product.Name);
                        Console.Clear();
                        ShowCart(user);
                        return true;
                    case 2:
                        return false;

                }

            }

        }
        public void RemoveFromCart(UserAccount user, int productId, string name )
        {
            var carts = _dataManager.LoadCarts();
            if (!carts.ContainsKey(user.Username)) 
                return;

            var userCart = carts[user.Username];
            var item = userCart.FirstOrDefault(c => c.ProductId == productId);
            
            if(item==null)
            {
                Console.WriteLine("Товар не найден в корзине");
                return;

            }

            if(item.Quantity>1)
            {
                item.Quantity--;
                Console.WriteLine($"Убрали 1шт. Теперь {item.Quantity} шт");
            }

            else
            {
                userCart.Remove(item);
                Console.WriteLine($" {name} удалён из корзины полностью.");
            }

            _dataManager.SaveCarts(carts);
        }

    }
}

