using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class ViewPostWithPaginationRequest : PaginationRequest
    {
        public Guid? PostId { get; set; }
    }
}
