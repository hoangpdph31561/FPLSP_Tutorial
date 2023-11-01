using FPLSP_Tutorial.Domain.Constants;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class MajorUpdateRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
