namespace FPLSP_Tutorial.WASM.Data.MajorRequest.Request
{
    public class MajorRequestDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}
