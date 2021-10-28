using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Data;
using Models;

namespace Data
{
    public class UserWebService : IUserService
    {
        
        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            using HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            using HttpClient client = new HttpClient(clientHandler);
            
            HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:5003/User?userName={userName}&password={password}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"{await responseMessage.Content.ReadAsStringAsync()}");
            }
            
            string result = await responseMessage.Content.ReadAsStringAsync();
            
            User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            return user;
        }
    }
}