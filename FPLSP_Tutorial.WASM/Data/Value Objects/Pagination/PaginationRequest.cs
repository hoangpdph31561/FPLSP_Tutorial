using FPLSP_Tutorial.WASM.Common;
using FPLSP_Tutorial.WASM.Data.Value_Objects.Common;

namespace FPLSP_Tutorial.WASM.Data.Pagination
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public ICollection<SearchModel>? SearchByFields { get; set; }
        public ICollection<SortModel>? SortByFields { get; set; }
    }
}
