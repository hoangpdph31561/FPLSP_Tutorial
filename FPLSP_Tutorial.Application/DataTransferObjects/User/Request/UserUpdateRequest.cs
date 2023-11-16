using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public List<string> RoleCodes { get; set; } = new List<string>();
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
