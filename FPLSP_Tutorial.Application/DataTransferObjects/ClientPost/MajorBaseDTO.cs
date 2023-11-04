namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost
{
    public class MajorBaseDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
