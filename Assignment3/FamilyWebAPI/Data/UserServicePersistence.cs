using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FamilyWebAPI.Models;

namespace FamilyWebAPI.Data
{
    public class UserServicePersistence : IUserService
    {
        public IList<User> Users { get; set; }
        
        private readonly string usersFile = "users.json";

        public UserServicePersistence()
        {
            Users = File.Exists(usersFile) ? ReadData<User>(usersFile) : InitUsers();
        }
        
        private IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(s))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }
        
        private void SaveChanges()
        {
            string jsonFamilies = JsonSerializer.Serialize(Users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(usersFile, false))
            {
                outputFile.Write(jsonFamilies);
            }
        }
        
        public async Task<User> ValidateUserAsync(string userName, string password) {
            User first = Users.FirstOrDefault(user => user.UserName.Equals(userName));
            if (first == null) {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password)) {
                throw new Exception("Incorrect password");
            }

            return first;
        }

        private IList<User> InitUsers()
        {
            Users = new[] {
                new User {
                    Password = "123456",
                    UserName = "Admin"
                },
                new User {
                    Password = "123456",
                    UserName = "Troels"
                },
                new User {
                    Password = "123456",
                    UserName = "Kim"
                }
            }.ToList();
            SaveChanges();
            return Users;
        }
    }
}