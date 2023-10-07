namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class MajorUpdateRequest
    {
        public Guid MajorId { get; set; }
        public Guid UserId { get; set; }
        public bool IsManager { get; set; }
        public int Status { get; set; } = 1;
        
        public string Username { get; set; } = string.Empty;
    }
}
