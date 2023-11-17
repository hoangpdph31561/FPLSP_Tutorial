using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost
{
    public class PostMainDTO : PostBaseDTO
    {
        public string PostType { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        //Tên người tạo
        public string CreatedName { get; set; } = string.Empty;
        //List Tag của bài viết đó
        public List<TagBaseDTO>? LstTags { get; set; }
    }
}
