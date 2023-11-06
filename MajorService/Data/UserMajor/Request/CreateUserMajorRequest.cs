using MajorService.Enum;

namespace MajorService.Data.UserMajor.Request
{
    public class CreateUserMajorRequest
    {
        public Guid MajorId { get; set; }
        public Guid? UserId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
