using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StoreApp.Models;

namespace StoreApp.Data
{
    class DataManager
    {
        private const string UsersFile = "Data/users.json";

        public static List<UserAccount> LoadUsers()
        {
            if (!File.Exists(UsersFile)) return new List<UserAccount>();
            string json = File.ReadAllText(UsersFile);
            return JsonSerializer.Deserialize<List<UserAccount>>(json);
        }
    }
}
