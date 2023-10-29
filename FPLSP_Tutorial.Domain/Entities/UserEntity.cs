using FPLSP_Tutorial.Domain.Constants;
using FPLSP_Tutorial.Domain.Entities.Base;
using System.Text.Json.Nodes;

namespace FPLSP_Tutorial.Domain.Entities
{
    public class UserEntity : ICreatedBase
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public JsonArray RoleCodes { get; set; } = new JsonArray();
        public int Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public List<UserMajorEntity> UserMajors { get; set; }
    }
}
