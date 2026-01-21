using System.Text.Json.Serialization;

namespace ApiRest.Models
{
    public class RecipeModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        [JsonPropertyName("imageType")]
        public string ImageType { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[{Id}] {Title}";
        }
    }
}