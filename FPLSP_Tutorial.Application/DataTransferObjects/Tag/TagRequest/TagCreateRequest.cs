using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;

public class TagCreateRequest
{
    public Guid? MajorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public Guid? CreatedBy { get; set; }
}