using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request
{
    public class ViewMajorUserBySearchRequest : PaginationRequest
    {
        public string Email { get; set; }
        public Guid? MajorId { get; set; }

    }
}
