using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest
{
    public class MajorRequestDto
    {
        public Guid Id { get; set; }

        public Guid MajorId { get; set; }
        public string MajorName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsManager { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public bool Deleted { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }


    }
}
