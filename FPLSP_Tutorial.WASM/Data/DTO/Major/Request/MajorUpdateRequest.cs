using FPLSP_Tutorial.WASM.Enums;

namespace FPLSP_Tutorial.WASM.Data.DTO.Major.Request
{
    public class MajorUpdateRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
