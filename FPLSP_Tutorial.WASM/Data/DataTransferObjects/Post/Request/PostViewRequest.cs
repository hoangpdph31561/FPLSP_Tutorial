namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request
{
    public class PostViewRequest
    {
        public Guid? UserId { get; set; } = null;
        public Guid? PostId { get; set; } = null;
        public Guid? MajorId { get; set; } = null;
        public bool IsGetSystemPost { get; set; }
        public bool IsGetTopLevel { get; set; }
    }
}
