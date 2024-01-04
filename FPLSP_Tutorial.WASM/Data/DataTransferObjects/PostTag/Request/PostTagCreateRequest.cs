using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.PostTag.Request;

public class PostTagCreateRequest
{
    public Guid PostId { get; set; }
    public Guid TagId { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public Guid? CreatedBy { get; set; }
}