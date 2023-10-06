using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Response
{
    public class ViewPostWithPaginationResponse
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public Guid CreatedBy { get; set; }
        //Mã bài viết mẹ 
        public Guid? ParentId { get; set; }
        
    }
}
