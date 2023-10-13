using FPLSP_Tutorial.Domain.Constants;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Status { get; set; } = EntityStatus.Active;
    }
}
