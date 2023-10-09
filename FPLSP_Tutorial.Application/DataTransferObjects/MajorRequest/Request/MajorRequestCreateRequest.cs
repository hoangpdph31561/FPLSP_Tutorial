using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request
{
    public class MajorRequestCreateRequest
    {
        public Guid MajorId { get; set; }
        public bool IsManager { get; set; }
        public int Status { get; set; } = 1;

    }
}
