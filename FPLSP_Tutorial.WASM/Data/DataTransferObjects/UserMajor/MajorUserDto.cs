using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor
{
    public class MajorUserDto
    {
        public Guid Id { get; set; }
        public Guid MajorId { get; set; }
        public string TenChuyenNganh { get; set; }
        public string email { get; set; }
        public List<string> RoleCodes { get; set; } = new List<string>();
        public Guid UserId { get; set; }
        public bool IsManager { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
