using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Major
{
    public class MajorDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int NumberOfLecturer { get; set; }
        public int NumberOfRequest { get; set; }
        public EntityStatus Status { get; set; }
    }
}
