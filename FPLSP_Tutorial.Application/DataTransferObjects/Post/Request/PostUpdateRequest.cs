using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class PostUpdateRequest
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid? ParentId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public Guid ModifiedBy { get; set; }
    }
}
