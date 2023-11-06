using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request
{
    public class MajorRequestCreateRequest
    {
        public Guid MajorId { get; set; }
        public bool IsManager { get; set; }
        public bool Deleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;

    }
}
