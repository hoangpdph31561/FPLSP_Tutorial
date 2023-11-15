using ClientPost.Data.ValueObject.Pagination;

namespace ClientPost.Data.DataTransferObject.Request
{
    public class ClientPostGetChildWithPaginationRequest : PaginationRequest
    {
        public string Id { get; set; } = string.Empty;
    }
}
