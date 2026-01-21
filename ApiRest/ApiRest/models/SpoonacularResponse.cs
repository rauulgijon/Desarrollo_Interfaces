using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiRest.Models 
{
    public class SpoonacularResponse
    {
        [JsonPropertyName("results")]
        public List<RecipeModel> Results { get; set; } = new List<RecipeModel>();

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }
    }
}