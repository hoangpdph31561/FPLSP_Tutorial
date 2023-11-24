using FPLSP_Tutorial.WASM.Enum;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<string> RoleCodes { get; set; } = new List<string>();
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }

        public List<MajorDTO> ListJoinedMajors { get; set; } = new();
    }
}
