namespace FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request
{
    public class PostTagDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
