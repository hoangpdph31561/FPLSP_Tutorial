using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.Client.Request
{
    public class ClientPostGetTagsByPostIdWithPaginationRequest : PaginationRequest
    {
        public string Id { get; set; }
    }
}
