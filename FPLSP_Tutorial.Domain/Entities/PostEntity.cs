using FPLSP_Tutorial.Domain.Entities.Base;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Domain.Entities
{
    public class PostEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public string PostType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }

        public List<PostTagEntity> PostTags { get; set; }

        public PostEntity Post { get; set; }
        public List<PostEntity> Posts { get; set; }
    }
}
