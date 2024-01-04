using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;

public class MajorUpdateRequest
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public Guid? ModifiedBy { get; set; }
}