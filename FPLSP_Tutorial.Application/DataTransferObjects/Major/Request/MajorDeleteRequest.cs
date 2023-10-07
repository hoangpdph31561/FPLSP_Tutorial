namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class MajorDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
