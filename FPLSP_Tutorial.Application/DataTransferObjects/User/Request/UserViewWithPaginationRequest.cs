using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request;

public class UserViewWithPaginationRequest : PaginationRequest
{
    public string? SearchString { get; set; }
}