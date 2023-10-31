using FPLSP_Tutorial.Domain.Constants;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class MajorDeleteRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Status { get; set; } = EntityStatus.Active;
        public Guid? DeletedBy { get; set; }
    }
}
