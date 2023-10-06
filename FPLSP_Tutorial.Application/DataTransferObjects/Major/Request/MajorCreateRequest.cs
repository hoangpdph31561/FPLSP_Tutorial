namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class MajorCreateRequest
    {
        public Guid MajorId { get; set; }
        public string Username { get; set; } = string.Empty;
        public bool IsManager { get; set; }
        public int Status { get; set; } = 1;

        public Guid UserId { get; set; }
    }
}
