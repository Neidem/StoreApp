using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Log
{
    public static class LogEvents
    {
        public static  string UserLogin(string username) =>
        $"Пользователь: {username} Вход в систему. Время:{DateTime.Now:HH:mm:ss}";
        public static string UserLoginFailed(string username) =>
        $"Неудачная попытка входа: {username} Время:{DateTime.Now:HH:mm:ss}";
        public static string UserBanned(string username) =>
        $"Пользователь: {username} заблокирован. Время: {DateTime.Now:HH:mm:ss}";
        public static string AddToCart(string username, string product) =>
        $"Пользователь: {username} добавил {product} в корзину. Время:{DateTime.Now:HH:mm:ss}";
        public static string OrderCreated(string username, string product) =>
        $"Пользователь: {username} оформил заказ {product}. Время:{DateTime.Now:HH:mm:ss}";
        public static string DeleteProduct(string name) =>
        $"Товар удалён администратором: {name}";

    }
}
