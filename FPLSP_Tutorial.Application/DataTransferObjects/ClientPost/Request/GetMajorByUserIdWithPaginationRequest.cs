using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class GetMajorByUserIdWithPaginationRequest : PaginationRequest
    {
        public Guid Id { get; set; }
    }
}
