namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag.TagRequest;

public class TagViewRequest
{
    public string? Name { get; set; } = null;
    public Guid? MajorId { get; set; } = null;
    public bool IgnoreMajorId { get; set; } = false;
}