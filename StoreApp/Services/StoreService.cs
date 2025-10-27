using StoreApp.Data;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    class StoreService : IStoreService
    {
        private readonly IDataManager _dataManager;
        private List<Product> _products;
        public List<Product>GetAllProducts() => _products;
        public List<Product> Products { get; set; } = new List<Product>();

        public StoreService(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _products = dataManager.LoadProducts();
        }

        public void ShowProducts()
        {
            Console.WriteLine("Список товаров:");
            foreach (var p in _products)
                Console.WriteLine($"{p.Id}: {p.Name} - {p.Price} руб, Остаток: {p.Quantity}");
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
            _dataManager.SaveProducts(_products);
        }
    }

}
