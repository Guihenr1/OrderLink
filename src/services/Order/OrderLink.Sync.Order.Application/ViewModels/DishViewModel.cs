using System.Text.Json.Serialization;

namespace OrderLink.Sync.Order.Application.ViewModels
{
    public class DishViewModelData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    public class DishViewModel
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public List<DishViewModelData> Data { get; set; }
    }
}
