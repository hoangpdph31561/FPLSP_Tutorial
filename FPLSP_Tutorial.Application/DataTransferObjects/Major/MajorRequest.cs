namespace FPLSP_Tutorial.Application.DataTransferObjects.Major
{
    public class MajorRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int Status { get; set; } = 1;
        public DateTimeOffset CreatedTime { get; set; }
        public bool IsManager { get; set; }
         
        public string MajorName { get; set; } = string.Empty;
    }
}
