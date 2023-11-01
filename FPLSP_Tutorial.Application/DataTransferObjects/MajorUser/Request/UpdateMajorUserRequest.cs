using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request
{
    public class UpdateMajorUserRequest
    {
        public Guid Id { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
