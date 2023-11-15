using ClientPost.Data.ValueObject.Pagination;

namespace ClientPost.Data.DataTransferObject.Request
{
    public class ClientPostGetTagsByPostIdWithPaginationRequest : PaginationRequest
    {
        public string Id { get; set; }
    }
}
