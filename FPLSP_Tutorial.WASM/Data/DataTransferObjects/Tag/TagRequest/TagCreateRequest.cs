using System.ComponentModel.DataAnnotations;
using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag.TagRequest;

public class TagCreateRequest
{
    public Guid? MajorId { get; set; }

    [Required(ErrorMessage = "Chưa nhập tên nhãn")]
    [MaxLength(10, ErrorMessage = "Không nhập quá 10 ký tự")]
    public string Name { get; set; } = string.Empty;

    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public Guid? CreatedBy { get; set; }
}