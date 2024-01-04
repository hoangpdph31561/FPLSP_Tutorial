namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag.TagRequest;

public class TagDeleteRequest
{
    public Guid Id { get; set; }

    public Guid? DeletedBy { get; set; }
}