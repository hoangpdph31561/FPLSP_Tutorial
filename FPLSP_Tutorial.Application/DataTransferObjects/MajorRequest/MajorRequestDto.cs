using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;

public class MajorRequestDTO
{
    public Guid Id { get; set; }
    public Guid MajorId { get; set; }
    public bool IsManager { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public DateTimeOffset CreatedTime { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset ModifiedTime { get; set; }
    public Guid? ModifiedBy { get; set; }
    public bool Deleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTimeOffset DeletedTime { get; set; }

    public string MajorCode { get; set; } = string.Empty;
    public string MajorName { get; set; } = string.Empty;
    public string CreatedByEmail { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
}