// Controller/PokeController.cs — C# with inline comments
// ------------------------------------------------------------

using System.Net.Http; // HttpClient to perform HTTP requests
using System.Text.Json; // JSON serializer for parsing API responses
using System.Threading.Tasks;
using ApiShowcase.Wpf.Models;

namespace ApiShowcase.Wpf.Controller // Namespace groups related classes of this project
{
    public class PokeController // Controller: encapsulates API communication logic
    {
        private readonly HttpClient client = new HttpClient(); // Reusable HTTP client instance (recommended to reuse)

        public async Task<Pokemon?> GetPokemonAsync(string nameOrId) // Method is asynchronous and returns a Task
        {
            if (string.IsNullOrWhiteSpace(nameOrId)) return null;
            string url = $"https://pokeapi.co/api/v2/pokemon/{nameOrId}";
            var response = await client.GetAsync(url); // Perform an asynchronous HTTP GET request
            if (!response.IsSuccessStatusCode) return null;
            using var stream = await response.Content.ReadAsStreamAsync(); // Read the response body as a stream for efficient deserialization
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Make property name matching case-insensitive (JSON → C#)
            var poke = await JsonSerializer.DeserializeAsync<Pokemon>(stream, options); // Convert JSON stream to C# object(s)
            return poke;
        }
    }
}