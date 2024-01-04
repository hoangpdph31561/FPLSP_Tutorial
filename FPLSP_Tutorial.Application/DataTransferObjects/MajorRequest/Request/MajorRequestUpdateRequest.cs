using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;

public class MajorRequestUpdateRequest
{
    public Guid Id { get; set; }

    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public Guid? ModifiedBy { get; set; }
}