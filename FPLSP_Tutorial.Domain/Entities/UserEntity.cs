using FPLSP_Tutorial.Domain.Entities.Base;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Domain.Entities
{
    public class UserEntity : ICreatedBase
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<string> RoleCodes { get; set; } = new List<string>();
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public List<UserMajorEntity> UserMajors { get; set; }
    }
}
