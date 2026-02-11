// Controller/CountriesController.cs — C# with inline comments
// ------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Net.Http; // HttpClient to perform HTTP requests
using System.Text.Json; // JSON serializer for parsing API responses
using System.Threading.Tasks;
using ApiShowcase.Wpf.Models;

namespace ApiShowcase.Wpf.Controller // Namespace groups related classes of this project
{
    public class CountriesController // Controller: encapsulates API communication logic
    {
        private readonly HttpClient client = new HttpClient(); // Reusable HTTP client instance (recommended to reuse)

        public async Task<List<CountryItem>> SearchByNameAsync(string name) // Method is asynchronous and returns a Task
        {
            if (string.IsNullOrWhiteSpace(name)) return new List<CountryItem>();
            string url = $"https://restcountries.com/v3.1/name/{name}";
            var response = await client.GetAsync(url); // Perform an asynchronous HTTP GET request
            if (!response.IsSuccessStatusCode) return new List<CountryItem>();
            using var stream = await response.Content.ReadAsStreamAsync(); // Read the response body as a stream for efficient deserialization
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Make property name matching case-insensitive (JSON → C#)
            var raw = await JsonSerializer.DeserializeAsync<List<CountryRaw>>(stream, options) ?? new List<CountryRaw>(); // Convert JSON stream to C# object(s)
            return raw.Select(c => new CountryItem
            {
                Name = c.name?.common ?? string.Empty,
                Code = c.cca2 ?? string.Empty,
                Capital = (c.capital != null && c.capital.Count > 0) ? c.capital[0] : string.Empty,
                Population = c.population
            }).ToList();
        }
    }
}