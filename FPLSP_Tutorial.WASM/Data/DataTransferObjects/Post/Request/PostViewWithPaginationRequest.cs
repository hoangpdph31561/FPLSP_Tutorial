using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request
{
    public class PostViewWithPaginationRequest : PaginationRequest
    {
        public Guid? MajorId { get; set; } = null;
    }
}
