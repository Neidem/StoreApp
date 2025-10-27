using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public interface IStoreService
    {
        List<Product> GetAllProducts();
        void ShowProducts();
        void AddProduct(Product product);

    }
}
