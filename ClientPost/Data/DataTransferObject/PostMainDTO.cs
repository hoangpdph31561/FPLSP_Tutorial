using ClientPost.Data.ValueObject.Enums;

namespace ClientPost.Data.DataTransferObject
{
    public class PostMainDTO : PostBaseDTO
    {
        public string PostType { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool ShowDetail { get; set; } = false;
        //Tên người tạo
        public string CreatedName { get; set; } = string.Empty;
    }
}
