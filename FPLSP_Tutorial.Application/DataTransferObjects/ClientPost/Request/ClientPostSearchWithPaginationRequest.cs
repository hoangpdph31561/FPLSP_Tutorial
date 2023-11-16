using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class ClientPostSearchWithPaginationRequest : PaginationRequest
    {
        public List<Guid>? LstTags { get; set; }
        public string? StringSearch { get; set; }

    }
}
