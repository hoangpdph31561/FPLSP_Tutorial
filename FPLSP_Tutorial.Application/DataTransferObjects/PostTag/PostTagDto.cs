using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.PostTag;

public class PostTagDTO
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid TagId { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public DateTimeOffset CreatedTime { get; set; }
    public Guid? CreatedBy { get; set; }
    public bool Deleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTimeOffset DeletedTime { get; set; }
}