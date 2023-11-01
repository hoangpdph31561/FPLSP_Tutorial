using FPLSP_Tutorial.Domain.Constants;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Major
{
    public class MajorDTOs
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
