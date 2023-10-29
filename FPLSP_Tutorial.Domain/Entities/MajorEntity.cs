using FPLSP_Tutorial.Domain.Constants;
using FPLSP_Tutorial.Domain.Entities.Base;

namespace FPLSP_Tutorial.Domain.Entities
{
    public class MajorEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        // chuyển từ kiểu int sang string của Status
        public int Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }

        public List<UserMajorEntity> UserMajors { get; set; }
        public List<TagEntity> Tags { get; set; }
        public List<MajorRequestEntity> MajorRequests { get; set; }
    }
}
