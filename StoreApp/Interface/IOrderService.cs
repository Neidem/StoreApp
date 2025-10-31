using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Interface
{
    public interface IOrderService
    {
        //void AddToCart(UserAccount user, int productId, int quantity);
        //void RemoveFromCart(UserAccount user, int productId);
        //void ViewCart(UserAccount user);
         Task PlaceOrder(UserAccount user, int productId, int quantity);

    }
}
