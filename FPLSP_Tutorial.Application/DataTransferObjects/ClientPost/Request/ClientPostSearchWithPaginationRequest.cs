using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class ClientPostSearchWithPaginationRequest : PaginationRequest
    {
        public Guid? MajorId { get; set; }
        public List<Guid>? LstTagsId { get; set; }
        public string? StringSearch { get; set; }

    }
}
