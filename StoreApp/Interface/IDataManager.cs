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
        // Пользователи

        List<UserAccount> LoadUsers();
        void SaveUsers(List<UserAccount> users);
        //
        // Продукция
        //
        List<Product> LoadProducts();
        void SaveProducts(List<Product> products);
        List<Category> LoadCategories();
       //
       // Корзина
       //
        Dictionary<string, List<CartItem>> LoadCarts();
        void SaveCarts(Dictionary<string, List<CartItem>> carts);
        //
        // Заказы
        //
        List<Order> LoadOrders(string username);
        void SaveOrders(string username,List<Order> orders);



    }
}
