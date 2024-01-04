namespace FPLSP_Tutorial.WASM.Data;

public class APIResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
}