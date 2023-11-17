using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request
{
    public class ViewMajorWithPaginationRequest : PaginationRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
