using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Interface
{
    public interface IStoreService
    {
        List<Product> GetAllProducts();
        bool ShowCategories(UserAccount user);
       bool ShowProductsByCategory(int catId, UserAccount user);
        void AddProduct(Product product);

    }
}
