using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request
{
    public class PostUpdateRequest
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid? ParentId { get; set; }
        public int Status { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
