namespace ClientPost.Data
{
    public class PostDetailDTO : PostBaseDTO
    {
        public string Content { get; set; } = string.Empty;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        //Người viết bài
        public string UserCreatedName { get; set; } = string.Empty;
    }
}
