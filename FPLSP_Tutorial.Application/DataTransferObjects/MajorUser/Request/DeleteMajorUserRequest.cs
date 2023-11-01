namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request
{
    public class DeleteMajorUserRequest
    {
        public Guid Id { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
