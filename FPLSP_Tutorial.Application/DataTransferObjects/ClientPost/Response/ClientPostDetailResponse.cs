using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response
{
    public class ClientPostDetailResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        //Người viết bài
        public string UserCreatedName { get; set; }

    }
}
