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
        private List<Category> _categories;
        private List<Product> _products;
        public List<Product> GetAllProducts() => _products;
        public List<Product> Products { get; set; } = new List<Product>();

        public StoreService(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _products = dataManager.LoadProducts();
            _categories = dataManager.LoadCategories();
        }

        public void ShowCategories()
        {


            Console.WriteLine("Категория товаров:");
            foreach (var p in _categories)
                Console.WriteLine($"{p.Id}:{p.Name}");
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
            _dataManager.SaveProducts(_products);
        }

        public void ShowProductsByCategory(int categoryId)
        {
            var products = _products.Where(p => p.CategoryId == categoryId).ToList();
                if(!products.Any())
                {
                Console.WriteLine("Нет товаров в этой категории");
                return;
                }
                Console.WriteLine($"Товары категории: {_categories.First(c => c.Id == categoryId).Name}");

            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id}:{p.Name} - {p.Price}руб (Остаток {p.Quantity})");
            }
               

        }
    }

}
