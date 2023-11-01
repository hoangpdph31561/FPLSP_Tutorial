using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class ClientPostListRequest : PaginationRequest
    {
        //Kiểm tra MajorID => ra Hệ thống hoặc chuyên ngành
        public Guid? MajorId { get; set; }
    }
}
