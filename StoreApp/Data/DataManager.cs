using StoreApp;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StoreApp.Log;


namespace StoreApp.Data
{

  abstract class DataManager
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
            }
            catch (Exception ex)
            {
            }
        }
    }
}