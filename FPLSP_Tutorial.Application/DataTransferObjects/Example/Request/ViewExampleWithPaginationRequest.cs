using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Example.Request
{
    public class ViewExampleWithPaginationRequest : PaginationRequest
    {
        // SearchFields
        public string? Name { get; set; }

        // SortFields
        public EntityStatus? Status { get; set; }
    }
}
