// Controller/ObjectController.cs — C# with inline comments
// ------------------------------------------------------------

using System.Net.Http; // HttpClient to perform HTTP requests
using System.Text.Json; // JSON serializer for parsing API responses
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiShowcase.Wpf.Models;

namespace ApiShowcase.Wpf.Controller // Namespace groups related classes of this project
{
    public class ObjectController // Controller: encapsulates API communication logic
    {
        private readonly HttpClient client = new HttpClient(); // Reusable HTTP client instance (recommended to reuse)

        public async Task<List<ApiObject>> GetObjectsAsync() // Method is asynchronous and returns a Task
        {
            string url = "https://api.restful-api.dev/objects";
            var response = await client.GetAsync(url); // Perform an asynchronous HTTP GET request
            response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx — basic error handling
            using var stream = await response.Content.ReadAsStreamAsync(); // Read the response body as a stream for efficient deserialization
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Make property name matching case-insensitive (JSON → C#)
            var objects = await JsonSerializer.DeserializeAsync<List<ApiObject>>(stream, options); // Convert JSON stream to C# object(s)
            return objects ?? new List<ApiObject>();
        }

        public async Task<ApiObject?> GetObjectByIdAsync(string id) // Method is asynchronous and returns a Task
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            string url = $"https://api.restful-api.dev/objects/{id}";
            var response = await client.GetAsync(url); // Perform an asynchronous HTTP GET request
            if (!response.IsSuccessStatusCode) return null;
            using var stream = await response.Content.ReadAsStreamAsync(); // Read the response body as a stream for efficient deserialization
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Make property name matching case-insensitive (JSON → C#)
            var obj = await JsonSerializer.DeserializeAsync<ApiObject>(stream, options); // Convert JSON stream to C# object(s)
            return obj;
        }
    }
}