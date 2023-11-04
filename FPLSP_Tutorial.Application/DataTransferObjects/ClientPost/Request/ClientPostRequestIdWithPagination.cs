using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class ClientPostRequestIdWithPagination : PaginationRequest
    {
        //Id post 
        public Guid Id { get; set; }
    }
}
