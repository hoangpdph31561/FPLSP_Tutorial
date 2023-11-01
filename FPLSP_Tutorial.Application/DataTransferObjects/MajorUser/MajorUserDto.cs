using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorUser
{
    public class MajorUserDto
    {
        public Guid MajorId { get; set; }
        public Guid UserId { get; set; }
        public bool IsManager { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }

    }
}
