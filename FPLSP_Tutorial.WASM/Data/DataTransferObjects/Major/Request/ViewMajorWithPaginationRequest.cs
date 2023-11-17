using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request
{
    public class ViewMajorWithPaginationRequest : PaginationRequest
    {
        public string? Name { get; set; } 
    }
}
