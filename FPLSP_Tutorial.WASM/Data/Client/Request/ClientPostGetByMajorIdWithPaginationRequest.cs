using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.Client.Request
{
    public class ClientPostGetByMajorIdWithPaginationRequest : PaginationRequest
    {
        public string? MajorId { get; set; }
        public List<Guid> LstTagsId { get; set; } = new();
        public string? StringSearch { get; set; }
    }
}
