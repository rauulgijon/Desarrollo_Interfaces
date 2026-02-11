using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApiUrlTemplate.Models;

namespace ApiUrlTemplate.Controllers
{
    public class ApiController
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<List<ApiObject>> GetObjectsAsync()
        {
            string url = "https://api.restful-api.dev/objects";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // VERSIÓN C# 7.3: Usamos llaves para el using
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var objects = await JsonSerializer.DeserializeAsync<List<ApiObject>>(stream, options);

                return objects ?? new List<ApiObject>();
            }
        }
    }
}