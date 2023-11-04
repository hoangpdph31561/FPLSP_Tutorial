namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class PostDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid DeletedBy { get; set; }

    }
}
