using StoreApp.Models;
using StoreApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Services.Auth;

namespace StoreApp.Tests.Data
{
    public class FakeDataManager : IDataManager
    {
        public List<UserAccount> Users { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> FakeProducts { get; set; }
        


        public FakeDataManager ()
        {
           

            Users = new List<UserAccount>
            {
                new UserAccount
                {
                    Username = "test",
                    PasswordHash = AuthService.GetMd5Hash("1234"),
                    Role = "customer",
                    IsBanned = false
                }
            };

            //Categories = new List<Category>
            //{
            //    new Category
            //    {
            //        Id = 6,
            //        Name = "Mouse",
            //        Products = FakeProducts
            //    }
            //};
 //FakeProducts = new List<Product>
            //{
            //    new Product
            //    {
            //        Id = 1,
            //        Name = "A4Tech X7",
            //        Price = 690,
            //        Quantity = 9,
            //        CategoryId = 6

            //    }
            //};
            
        }

        public List<UserAccount> LoadUsers()
            => Users;
        public List<Category> LoadCategories()
            => Categories;

        // не использую для теста

        public void SaveProducts(List<Product> products)
        => throw new NotImplementedException();
        public List<Product> LoadProducts()
        => throw new NotImplementedException();

        public Dictionary<string, List<CartItem>> LoadCarts()
        => throw new NotImplementedException();

        public void SaveCarts(Dictionary<string, List<CartItem>> carts)
            => throw new NotImplementedException();

        public List<Order> LoadOrders(string username)
            => throw new NotImplementedException();

        public void SaveOrders(string username, List<Order> orders)
            => throw new NotImplementedException();
        public void SaveUsers(List<UserAccount> users)
        => throw new NotImplementedException();

    }
}
