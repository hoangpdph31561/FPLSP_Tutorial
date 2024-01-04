using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public List<string> RoleCodes { get; set; } = new();
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public DateTimeOffset CreatedTime { get; set; }
    public Guid? CreatedBy { get; set; }

    public List<UserMajorDTO> ListJoinedMajors { get; set; } = new();
}