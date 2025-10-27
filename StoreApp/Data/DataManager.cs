using StoreApp;
using StoreApp.Services;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace StoreApp.Data
{

    class DataManager
    {
        private const string UsersFile = "Data/users.json";

        public static List<UserAccount> LoadUsers()
        {
            if (!File.Exists(UsersFile))
            {
                return new List<UserAccount>();
            }

            try
            {
                string json = File.ReadAllText(UsersFile);
                var users = JsonSerializer.Deserialize<List<UserAccount>>(json);

                Logger.Info($"✅ Загрузка пользователей завершена. Найдено: {users?.Count ?? 0+1} записей.");
                return users ?? new List<UserAccount>();
            }
            catch (Exception ex)
            {
                return new List<UserAccount>();
            }
        }

        public static void SaveUsers(List<UserAccount> users)
        {
            try
            {
                string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(UsersFile, json);
                Logger.Info($"💾 Данные сохранены ({users.Count} пользователей).");
            }
            catch (Exception ex)
            {
            }
        }
    }
}