using StoreApp.Data;
using StoreApp.Interface;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public class CartService : ICartService
    {
        private readonly IDataManager _dataManager;
        //private readonly IStoreService _storeService;
        private readonly List<Product> _cartItems = new();

        public CartService(IDataManager dataManager)
        {

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

            var existing = user.Cart.FirstOrDefault(c=> c.ProductId == productId);  
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                user.Cart.Add(new CartItem { ProductId = productId, Quantity = quantity });

                Console.WriteLine($" {product.Name} добавлен в корзину ({quantity} шт).");
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey(true);
            }
        }


        public void ShowCart()
        {
            if (_cartItems.Count == 0)
            {
                Console.WriteLine("Корзина пуста");
                return;
            }

            Console.WriteLine("\n Товары в корзине");
            foreach (var item in _cartItems) 
                Console.WriteLine($"{item.Name} - {item.Price} руб");

            Console.WriteLine($"\n Итого: {_cartItems.Sum(p => p.Price)}");

        }
        public void ClearCart() => _cartItems.Clear();
    }
}
