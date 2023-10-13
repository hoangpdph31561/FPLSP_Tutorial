using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
