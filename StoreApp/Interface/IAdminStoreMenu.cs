using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Interface
{
    public interface IAdminStoreMenu
    {
        bool ShowProductMenu(UserAccount user);
    }
}
