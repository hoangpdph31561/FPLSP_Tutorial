using System.Text.Json.Serialization;

namespace FPLSP_Tutorial.WASM.Data.Client
{
    public class MajorBaseDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        public bool ShowDetail { get; set; } = false;
        public int NumberOfPosts { get; set; }
    }
}
