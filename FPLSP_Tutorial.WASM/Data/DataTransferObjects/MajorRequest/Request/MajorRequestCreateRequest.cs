using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request;

public class MajorRequestCreateRequest
{
    public Guid MajorId { get; set; }
    public bool IsManager { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public Guid? CreatedBy { get; set; }
}