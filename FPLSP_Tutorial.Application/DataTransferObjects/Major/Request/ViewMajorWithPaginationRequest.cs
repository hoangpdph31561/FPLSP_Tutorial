using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class ViewMajorWithPaginationRequest : PaginationRequest
    {
        public string? Name { get; set; }
    }
}
