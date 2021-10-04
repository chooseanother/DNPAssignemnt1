using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Data;
using Models;

namespace Assignment1.Data
{
    public class UserServicePersistence : IUserService
    {
        public IList<User> users { get; private set; }
        
        private readonly string usersFile = "users.json";

        public UserServicePersistence()
        {
            users = File.Exists(usersFile) ? ReadData<User>(usersFile) : InitUsers();
        }
        
        private IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(s))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }
        
        public void SaveChanges()
        {
            string jsonFamilies = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(usersFile, false))
            {
                outputFile.Write(jsonFamilies);
            }
        }
        
        public User ValidateUser(string userName, string password) {
            User first = users.FirstOrDefault(user => user.UserName.Equals(userName));
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
            users = new[] {
                new User {
                    Password = "123456",
                    SecurityLevel = 5,
                    UserName = "Admin"
                },
                new User {
                    Password = "123456",
                    SecurityLevel = 3,
                    UserName = "Troels"
                },
                new User {
                    Password = "123456",
                    SecurityLevel = 1,
                    UserName = "Kim"
                }
            }.ToList();
            SaveChanges();
            return users;
        }
    }
}