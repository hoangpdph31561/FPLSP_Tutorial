using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class PostCreateRequest
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? ParentPost { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
