using MajorService.Data.Pagination;

namespace MajorService.Data.DataTransferObjects.UserMajor.Request
{
    public class ViewMajorUserBySearchRequest : PaginationRequest
    {
        public string Email { get; set; }
        public Guid? MajorId { get; set; }

    }
}
