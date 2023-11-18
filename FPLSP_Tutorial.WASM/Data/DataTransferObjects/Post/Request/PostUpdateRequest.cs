using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request
{
    public class PostUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public string PostType { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        
        public EntityStatus Status { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
