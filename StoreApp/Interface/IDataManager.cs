using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Models;
using StoreApp.Services;


namespace StoreApp.Interface
{
    public interface IDataManager
    {
        List<UserAccount> LoadUsers();
        void SaveUsers(List<UserAccount> users);

        List<Product> LoadProducts();
        void SaveProducts(List<Product> products);
        List<Category> LoadCategories();

        List<Order> LoadOrders();
        void SaveOrders(List<Order> orders);



    }
}
