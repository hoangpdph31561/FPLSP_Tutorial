using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<string> RoleCodes { get; set; } = new List<string>();
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public Guid? CreatedBy { get; set; }
    }
}
