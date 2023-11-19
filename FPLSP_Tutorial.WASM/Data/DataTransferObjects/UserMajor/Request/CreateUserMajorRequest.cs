using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request
{
    public class CreateUserMajorRequest
    {
        public Guid MajorId { get; set; }
        public Guid? UserId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
