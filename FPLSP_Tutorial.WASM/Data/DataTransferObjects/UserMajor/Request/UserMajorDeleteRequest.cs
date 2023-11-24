namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request
{
    public class UserMajorDeleteRequest
    {
        public Guid Id { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
