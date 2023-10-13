using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response
{
    //Xem danh sách bài viết
    public class ClientPostListResponse 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        //Tên người viết
        public string UserCreatedName { get; set; }
    }
}
