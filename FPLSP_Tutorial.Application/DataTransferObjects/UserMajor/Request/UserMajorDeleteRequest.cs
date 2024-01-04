namespace FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;

public class UserMajorDeleteRequest
{
    public Guid MajorId { get; set; }
    public Guid UserId { get; set; }
    public Guid? DeletedBy { get; set; }
}