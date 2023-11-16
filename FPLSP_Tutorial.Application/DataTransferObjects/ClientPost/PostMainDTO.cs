using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost
{
    public class PostMainDTO : PostBaseDTO
    {
        public string PostType { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        //Tên người tạo
        public string CreatedName { get; set; } = string.Empty;
        //Lấy ra 300 ký tự đầu tiên của bài viết
        public string ContentShortened { get; set; } = string.Empty;
    }
}
