namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request
{
    public class MajorViewRequest
    {
        public Guid? UserId { get; set; } = null;
        public bool NotJoined { get; set; }
    }
}
