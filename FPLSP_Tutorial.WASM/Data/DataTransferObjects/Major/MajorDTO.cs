using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major
{
    public class MajorDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int NumberOfLecturer { get; set; }
        public int NumberOfRequest { get; set; }
        public string Name { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
    }
}
