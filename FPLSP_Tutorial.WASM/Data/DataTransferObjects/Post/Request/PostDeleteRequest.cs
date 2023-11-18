namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request
{
    public class PostDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid DeletedBy { get; set; }
    }
}
