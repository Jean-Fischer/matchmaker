using Business.Services.RandomUserGeneration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Services.RandomUserGeneration
{
    public class RandomUserGeneration : IRandomUserGeneration
    {

        private const string UriString = "https://randomuser.me/api/";


        public async Task<GeneratedPlayerResults> GenerateRandomUsers(int numberOfUsers, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient() { BaseAddress = new Uri(UriString) };
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var response = await httpClient.GetFromJsonAsync<GeneratedPlayerResults>($"?results={numberOfUsers}", serializeOptions, cancellationToken);
            
            return response;
        }

        
    }
}
