using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Interface
{
   public interface IAdminStoreService : IStoreService
    {
        void AddProduct(string name, decimal price, int quantity, int categoryId);
        bool ShowProductMenu(UserAccount user);
        
       

    }
}
