using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request
{
    public class ViewMajorRequestSearchWithPaginationRequest : PaginationRequest
    {
        public string? Email { get; set; }

    }
}
