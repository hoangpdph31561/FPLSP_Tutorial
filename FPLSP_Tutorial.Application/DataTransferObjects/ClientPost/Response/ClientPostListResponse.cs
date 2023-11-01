using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response
{
    //Xem danh sách bài viết
    public class ClientPostListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        //Tên người viết
        public string UserCreatedName { get; set; }
    }
}
