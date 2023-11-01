using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Response
{
    public class ViewPostByIdResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        //Bài viết mẹ
        public Guid? ParentId { get; set; }
        //Danh sách bài viết con
        public List<Guid>? ChildPostsId { get; set; }

    }
}
