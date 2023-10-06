using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class PostCreateRequest
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? ParentPost { get; set; }
        public int Status { get; set; }
    }
}
