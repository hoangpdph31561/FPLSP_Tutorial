using FPLSP_Tutorial.WASM.Enum;
using System.ComponentModel.DataAnnotations;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request
{
    public class MajorCreateRequest
    {
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string Code { get; set; }
        public EntityStatus Status { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
