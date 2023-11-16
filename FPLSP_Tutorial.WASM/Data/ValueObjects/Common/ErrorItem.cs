using System.Diagnostics.CodeAnalysis;

namespace FPLSP_Tutorial.WASM.Data.ValueObjects.Common;

[ExcludeFromCodeCoverage]
public class ErrorItem
{
    public string FieldName { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
}