using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request
{
    public class ViewMajorRequestSearchWithPaginationRequest : PaginationRequest
    {

        public Guid? MajorId { get; set; }
    }
}
