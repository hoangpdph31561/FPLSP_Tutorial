using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserCreateRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<string> RoleCodes { get; set; } = new List<string>();

        public Guid? CreatedBy { get; set; }
    }
}
