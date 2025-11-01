using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Interface
{
    public interface ICartService
    {
        void AddToCart(UserAccount user, int productId, int quantity);
        void ShowCart(UserAccount user);

    }
}
