namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.PostTag.Request;

public class PostTagDeleteRequest
{
    public Guid Id { get; set; }

    public Guid? DeletedBy { get; set; }
}