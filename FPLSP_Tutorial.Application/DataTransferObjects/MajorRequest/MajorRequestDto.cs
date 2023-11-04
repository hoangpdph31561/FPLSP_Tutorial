using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest
{
    public class MajorRequestDto
    {
        public Guid MajorId { get; set; }
        public string tenChuyenNganh { get; set; }
        public string email { get; set; }
        public bool IsManager { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

    }
}
