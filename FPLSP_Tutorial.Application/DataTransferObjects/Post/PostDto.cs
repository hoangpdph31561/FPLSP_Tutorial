using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public string PostType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<TagDto> ListTag { get; set; } = new List<TagDto>();
        public int CountPost { get; set; } = 0;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
