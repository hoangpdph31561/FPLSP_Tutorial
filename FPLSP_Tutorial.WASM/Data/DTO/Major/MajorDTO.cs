using FPLSP_Tutorial.WASM.Enums;

namespace FPLSP_Tutorial.WASM.Data.DTO.Major
{
    public class MajorDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int NumberOfLecturer { get; set; }
        public int NumberOfRequest { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
