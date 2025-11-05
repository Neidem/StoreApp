using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Models;
using System.Text.Json;
using System.Data;
using StoreApp.Services;
using StoreApp.Interface;


namespace StoreApp.Data
{
    public class JsonDataManager : IDataManager
    {
        private readonly string productsFile;
        private readonly string _usersFile;
        private readonly string ordersFile;
        private readonly string _categoriesFile;
        private readonly string _cartFile;

        public JsonDataManager()
        {
            // Константный путь относительно BaseDirectory
            _usersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");
            productsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "products.json");
            _categoriesFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "categories.json");
            ordersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "orders.json");
            _cartFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "carts.json");
            // Убедимся, что папка Data существует
            Directory.CreateDirectory(Path.GetDirectoryName(_usersFile)!);
        }

        public List<UserAccount> LoadUsers()
        {
            if (!File.Exists(_usersFile))
                return new List<UserAccount>();

            string json = File.ReadAllText(_usersFile);
            return JsonSerializer.Deserialize<List<UserAccount>>(json) ?? new List<UserAccount>();
        }



        public Dictionary<string, List<CartItem>> LoadCarts()
        {
            if (!File.Exists(_cartFile))
                return new Dictionary<string, List<CartItem>>();
            string json = File.ReadAllText(_cartFile);
            return JsonSerializer.Deserialize<Dictionary<string, List<CartItem>>>(json)
                ?? new Dictionary<string, List<CartItem>>();

        }

        public void SaveCarts(Dictionary<string, List<CartItem>> carts)
        {
            string json = JsonSerializer.Serialize(carts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_cartFile, json); 
        }

        public void SaveUsers(List<UserAccount> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_usersFile, json);
        }

        public List<Category> LoadCategories()
        {
            if (!File.Exists(_categoriesFile)) 
                return new List<Category>();
            string json = File.ReadAllText(_categoriesFile);
            return JsonSerializer.Deserialize<List<Category>>(json);


        }

        public List<Product> LoadProducts()
        {
            if(!File.Exists(productsFile)) 
                return new List<Product>();

            string json = File.ReadAllText(productsFile);
            return JsonSerializer.Deserialize<List<Product>>(json) 
                ?? new List<Product>();
        
        }

        public void SaveProducts(List<Product> products)
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(productsFile, json);

        }

        public List<Order> LoadOrders(string username)
        {
            if (!File.Exists(ordersFile))
                return new List<Order>();

            string json = File.ReadAllText(ordersFile);

            if(string.IsNullOrEmpty(json)) return new List<Order>();    
            var allOrders = JsonSerializer.Deserialize<Dictionary<string, List<Order>>>(json)
                            ?? new Dictionary<string, List<Order>>();

            return allOrders.ContainsKey(username) ? allOrders[username] : new List<Order>();
        }


        public void SaveOrders(string username, List<Order> orders)
        {
            Dictionary<string, List<Order>> allOrders;
            if (File.Exists(ordersFile))
            {
                string json = File.ReadAllText(ordersFile);

                allOrders = JsonSerializer.Deserialize<Dictionary<string, List<Order>>>(json)
                             ?? new Dictionary<string, List<Order>>();
            }
            else
            {
                allOrders = new Dictionary<string, List<Order>>();
            }

            allOrders[username] = orders;
            string newJson = JsonSerializer.Serialize(allOrders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ordersFile, newJson);
        }



    }
}
