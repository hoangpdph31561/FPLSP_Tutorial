using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request
{
    public class ViewPostWithPaginationRequest : PaginationRequest
    {
        public Guid? PostId { get; set; }
    }
}
