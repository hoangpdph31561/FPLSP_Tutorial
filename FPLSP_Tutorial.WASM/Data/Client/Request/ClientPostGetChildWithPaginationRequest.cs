using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.Client.Request
{
    public class ClientPostGetChildWithPaginationRequest : PaginationRequest
    {
        public string Id { get; set; } = string.Empty;
    }
}
