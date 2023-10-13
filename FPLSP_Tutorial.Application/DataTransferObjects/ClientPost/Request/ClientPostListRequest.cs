using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class ClientPostListRequest : PaginationRequest
    {
        //Kiểm tra MajorID => ra Hệ thống hoặc chuyên ngành
        public Guid? MajorId { get; set; }
    }
}
