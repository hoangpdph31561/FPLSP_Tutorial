﻿using System.Text.Json.Serialization;
using ClientPost.Data.ValueObject.Common;

namespace ClientPost.Data.ValueObject.Response;

public class APIResponse
{
    [JsonPropertyName("success")] public bool Success { get; set; }

    [JsonPropertyName("error_items")] public IEnumerable<ErrorItem>? ErrorItems { get; set; }

    [JsonPropertyName("is_valid")] public bool IsValid { get; set; } = true;

    [JsonPropertyName("message")] public string? Message { get; set; }

    [JsonPropertyName("data")] public object Data { get; set; } = new();
}