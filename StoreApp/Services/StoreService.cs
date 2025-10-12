using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    class StoreService
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public void ShowProducts()
        {
            Console.WriteLine("Список товаров:");
            foreach (var p in Products)
                Console.WriteLine($"{p.Id}: {p.Name} - {p.Price} руб, Остаток: {p.Stock}");
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }

}
