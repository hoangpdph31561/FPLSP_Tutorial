using FPLSP_Tutorial.WASM.Enums;
using System.ComponentModel.DataAnnotations;

namespace FPLSP_Tutorial.WASM.Data.DTO.Major.Request
{
    public class MajorCreateRequest
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Code { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }
}
