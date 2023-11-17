using ClientPost.Data.ValueObject.Pagination;

namespace ClientPost.Data.DataTransferObject.Request
{
    public class ClientPostGetByMajorIdWithPaginationRequest : PaginationRequest
    {
        public string? MajorId { get; set; }
        public List<Guid> LstTagsId { get; set; } = new();
        public string? StringSearch { get; set; }
    }
}
