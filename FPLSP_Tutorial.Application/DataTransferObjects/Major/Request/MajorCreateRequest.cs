using FPLSP_Tutorial.Domain.Constants;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class MajorCreateRequest
    {
        public string Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Status { get; set; } = EntityStatus.Active;
    }
}
