using System.Text.Json.Serialization;

namespace MajorService.Data.Value_Objects.Common
{
    public class SortModel
    {
        [JsonPropertyName("col_name")]
        public string ColName { get; set; } = string.Empty;
        [JsonPropertyName("sort_direction")]
        public string SortDirection { get; set; } = string.Empty; // desc, asc

        public string PairAsSqlExpression => $"{ColName} {SortDirection}";
    }
}
