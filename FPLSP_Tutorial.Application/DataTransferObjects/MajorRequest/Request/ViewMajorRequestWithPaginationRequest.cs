using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request
{
    public class ViewMajorRequestWithPaginationRequest : PaginationRequest
    {
        public string? Email { get; set; }
    }
}
