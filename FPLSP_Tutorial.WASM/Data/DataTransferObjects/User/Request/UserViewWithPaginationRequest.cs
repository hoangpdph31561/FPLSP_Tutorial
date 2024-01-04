using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.User.Request;

public class UserViewWithPaginationRequest : PaginationRequest
{
    public string? SearchString { get; set; }
}