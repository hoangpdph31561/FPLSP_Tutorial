namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class PostViewRequest
    {
        public Guid? UserId { get; set; } = null;
        public Guid? PostId { get; set; } = null;
        public Guid? MajorId { get; set; } = null;
    }
}
