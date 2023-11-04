namespace ClientPost.Data
{
    public class PostListDTO : PostBaseDTO
    {
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        //Tên người viết
        public string UserCreatedName { get; set; } = string.Empty;
    }
}
