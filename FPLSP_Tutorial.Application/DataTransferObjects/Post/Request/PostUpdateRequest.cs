using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class PostUpdateRequest
    {
        public Guid Id { get; set; }
        public string PostType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid? PostId { get; set; }
        public EntityStatus Status { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
