using System.Text.Json.Serialization;
using static MajorService.Common.Constant;

namespace MajorService.Common
{
    public class SearchModel
    {
        [JsonPropertyName("search_field_name")]
        public string SearchFieldName { get; set; } = string.Empty;
        [JsonPropertyName("search_value")]
        public string? SearchValue { get; set; } = string.Empty;
        [JsonPropertyName("match_type")]
        public bool MatchType { get; set; } = MatchTypes.Contain;
    }
}
