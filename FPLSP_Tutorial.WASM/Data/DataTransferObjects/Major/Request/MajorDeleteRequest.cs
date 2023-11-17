namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request
{
    public class MajorDeleteRequest
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; } = true;
        public Guid? DeletedBy { get; set; }
    }
}
