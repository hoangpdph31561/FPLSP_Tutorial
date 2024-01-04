using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request;

public class UserMajorUpdateRequest
{
    public Guid Id { get; set; }

    public bool IsManager { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public Guid? ModifiedBy { get; set; }
}