namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserDeleteRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int Status { get; set; } = 1;
        public object DeletedBy { get; set; }
    }
}
