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
        // Chỉnh sửa int Status sang string 
        public string Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        // Thêm Delete 
        public bool Deleted { get; set; }
        public object DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public List<UserMajorEntity> UserMajors { get; set; }
        //Thêm Modified
        public object ModifiedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
    }
}
