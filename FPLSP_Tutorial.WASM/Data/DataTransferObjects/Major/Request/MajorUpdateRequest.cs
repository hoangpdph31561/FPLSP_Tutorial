using System.ComponentModel.DataAnnotations;
using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request;

public class MajorUpdateRequest
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Trường này không được bỏ trống")]
    public string Code { get; set; }

    [Required(ErrorMessage = "Trường này không được bỏ trống")]
    public string Name { get; set; } = string.Empty;

    public EntityStatus Status { get; set; } = EntityStatus.Active;

    public Guid? ModifiedBy { get; set; }
}