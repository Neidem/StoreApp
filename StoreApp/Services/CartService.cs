using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public class CartService
    {
        private readonly List<Product> _cartItems = new();

        public void AddToCart(Product product)
        {
            _cartItems.Add(product);
            Console.WriteLine($"{product.Name} добавлен в корзину");
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
