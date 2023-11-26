using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class PostViewWithPaginationRequest : PaginationRequest
    {
        public Guid? UserId { get; set; } = null;
        public Guid? PostId { get; set; } = null;
        public Guid? MajorId { get; set; } = null;
        public bool IsGetSystemPost { get; set; }
        public bool IsGetTopLevel { get; set; }
    }
}
