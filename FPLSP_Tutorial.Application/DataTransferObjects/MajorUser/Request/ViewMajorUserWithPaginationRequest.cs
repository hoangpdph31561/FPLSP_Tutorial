using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request
{
    public class ViewMajorUserWithPaginationRequest : PaginationRequest
    {
        public string? Email { get; set; }
        public Guid? MajorId { get; set; }
    }
}
