using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest
{
    public class MajorRequestDto
    {
        public Guid Id { get; set; }

        public Guid MajorId { get; set; }
        public string majorName { get; set; }
        public string email { get; set; }
        public bool IsManager { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public bool Deleted { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}
