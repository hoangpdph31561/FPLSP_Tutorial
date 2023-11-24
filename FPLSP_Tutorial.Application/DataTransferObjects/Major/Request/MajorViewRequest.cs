namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class MajorViewRequest
    {
        public Guid? UserId { get; set; } = null;
        public bool NotJoined { get; set; }
    }
}
