using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Interface
{
    public interface IAdminUserViewer
    {
        bool ShowOrdersUser(UserAccount user);
        bool ShowCartUser(UserAccount user);

    }
}
