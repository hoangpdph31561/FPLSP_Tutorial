using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserDeleteRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public object DeletedBy { get; set; }
    }
}
