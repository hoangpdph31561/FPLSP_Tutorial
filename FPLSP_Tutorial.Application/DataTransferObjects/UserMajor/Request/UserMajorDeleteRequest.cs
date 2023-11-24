namespace FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request
{
    public class UserMajorDeleteRequest
    {
        public Guid Id { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
