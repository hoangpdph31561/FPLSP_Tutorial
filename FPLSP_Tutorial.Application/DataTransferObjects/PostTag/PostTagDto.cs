namespace FPLSP_Tutorial.Application.DataTransferObjects.PostTag
{
    public class PostTagDto
    {
        public Guid TagId { get; set; }
        public Guid PostId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
