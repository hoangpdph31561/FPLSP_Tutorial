using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor;

public class UserMajorDTO
{
    public Guid Id { get; set; }
    public Guid MajorId { get; set; }
    public Guid UserId { get; set; }
    public bool IsManager { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public DateTimeOffset CreatedTime { get; set; }
    public Guid? CreatedBy { get; set; }
    public bool Deleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTimeOffset DeletedTime { get; set; }


    public string MajorCode { get; set; } = string.Empty;
    public string MajorName { get; set; } = string.Empty;
}