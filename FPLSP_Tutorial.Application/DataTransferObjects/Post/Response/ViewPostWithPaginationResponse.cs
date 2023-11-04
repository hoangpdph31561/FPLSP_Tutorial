using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Response
{
    public class ViewPostWithPaginationResponse
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        //Mã bài viết mẹ 
        public Guid? ParentId { get; set; }

    }
}
