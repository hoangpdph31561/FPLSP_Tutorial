namespace FPLSP_Tutorial.WASM.Data.DTO.Major.Request
{
    public class MajorDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
