using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request
{
    public class MajorCreateRequest
    {
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public Guid? CreatedBy { get; set; }
    }
}
