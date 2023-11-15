using System.Text.Json.Serialization;
using static FPLSP_Tutorial.WASM.Common.Constant;

namespace FPLSP_Tutorial.WASM.Common
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
