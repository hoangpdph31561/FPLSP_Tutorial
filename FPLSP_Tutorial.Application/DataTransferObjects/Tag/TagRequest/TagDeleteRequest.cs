namespace FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest
{
    public class TagDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
