using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Data;
using Models;

namespace Data
{
    public class FamilyWebService : IFamilyDataService
    {

        public async Task<IList<Family>> GetFamiliesAsync()
        {
            using HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            using HttpClient client = new HttpClient(clientHandler);
            
            HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:5003/Families");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            string result = await responseMessage.Content.ReadAsStringAsync();

            List<Family> todos = JsonSerializer.Deserialize<List<Family>>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return todos;
        }


        public async Task<Family> GetAsync(string streetName, int houseNumber)
        {
            using HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            using HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:5003/Families/Family?streetName={streetName}&houseNumber={houseNumber}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            string result = await responseMessage.Content.ReadAsStringAsync();

            Family family = JsonSerializer.Deserialize<Family>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return family;
        }

        public async Task AddAdultAsync(string streetName, int houseNumber, Adult adult)
        {
            using HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            using HttpClient client = new HttpClient(clientHandler);

            string adultAsJson = JsonSerializer.Serialize(adult);

            StringContent content = new StringContent(adultAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await client.PostAsync($"https://localhost:5003/Families/adult?streetName={streetName}&houseNumber={houseNumber}",content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            using HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            using HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:5003/Families/adult/{adultId}");
            if(!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }
    }
}