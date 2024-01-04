using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag;
using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major;

public class MajorDTO
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public DateTimeOffset CreatedTime { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset ModifiedTime { get; set; }
    public Guid? ModifiedBy { get; set; }
    public bool Deleted { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTimeOffset DeletedTime { get; set; }

    public int NumberOfLecturer { get; set; }
    public int NumberOfLecturerRequest { get; set; }
    public List<TagDTO> ListTag { get; set; } = new();

    public bool HasSentRequest { get; set; } = new();
    public int NumberOfPost { get; set; }
    public int NumberOfPostByUser { get; set; }
    public bool IsManagerUser { get; set; }
}