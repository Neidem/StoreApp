using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Models;
using System.Text.Json;
using System.Data;


namespace StoreApp.Data
{
    public class JsonDataManager : IDataManager
    {
        private readonly string productsFile;
        private readonly string _usersFile;
        private readonly string ordersFile;
        public JsonDataManager()
        {
            // Константный путь относительно BaseDirectory
            _usersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");
            productsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "products.json");

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

        public void SaveUsers(List<UserAccount> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_usersFile, json);
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

        public List<Order> LoadOrders()
        {
            if(!File.Exists(ordersFile)) return new List<Order>();
            string json = File.ReadAllText(ordersFile);
            return JsonSerializer.Deserialize<List<Order>>(json) ?? new List<Order>();  
        
        }

        public void SaveOrders(List<Order> orders)
        {
            string json = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ordersFile, json);
        }

       

    }
}
