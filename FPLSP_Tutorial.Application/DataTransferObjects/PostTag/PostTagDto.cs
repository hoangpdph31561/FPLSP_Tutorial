using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.PostTag
{
    public class PostTagDto
    {
        public Guid TagId { get; set; }
        public Guid PostId { get; set; }
    }
}
