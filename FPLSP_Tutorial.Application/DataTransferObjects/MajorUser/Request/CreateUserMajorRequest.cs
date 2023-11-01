using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request
{
    public class CreateUserMajorRequest
    {
        public Guid MajorId { get; set; }
        public Guid UserId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
