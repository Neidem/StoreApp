using StoreApp.Helpers;
using StoreApp.Interface;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Services
{
    public class AdminStoreMenu : IAdminStoreMenu
    {
        private readonly IAdminStoreService _adminStoreService;

        public AdminStoreMenu(IAdminStoreService adminStoreService)
        {
            _adminStoreService = adminStoreService;
        }
        public bool ShowProductMenu(UserAccount user)
        {
            while (true)
            {
                int choice = UIHelper.MenuSelect(new[]
                {
                "Добавить товар",
                "Редактировать товар",
                "Удалить товар",
                "Назад"

            }, "Управление товарами ");

                switch (choice)
                {
                    case 0:
                        _adminStoreService.CreateProductInteractive();
                        break;
                    case 1:
                        _adminStoreService.EditProductInteractive();
                        break;
                    case 2:
                        _adminStoreService.IdRemove();
                        break;
                    case 3:
                        return true;

                }
            }
        }
    }
}
