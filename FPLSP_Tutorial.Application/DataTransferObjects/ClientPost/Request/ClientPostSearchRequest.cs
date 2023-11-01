using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class ClientPostSearchRequest : PaginationRequest
    {
        //Lọc theo major người dùng
        public Guid? MajorId { get; set; }
        //Tên bài viết cần tìm
        public string? NamePost { get; set; }
        //Danh sách Tag (theo guid)
        public List<Guid>? LstTags { get; set; }
    }
}
