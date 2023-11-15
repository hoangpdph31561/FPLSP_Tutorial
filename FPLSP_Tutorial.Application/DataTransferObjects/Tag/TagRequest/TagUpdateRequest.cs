namespace FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest
{
    public class TagUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid? MajorId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
