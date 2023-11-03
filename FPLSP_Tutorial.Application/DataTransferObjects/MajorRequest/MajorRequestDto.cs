using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest
{
    public class MajorRequestDto
    {
        public Guid MajorId { get; set; }
        public string tenChuyenNganh { get; set; }
        public string email { get; set; }
        public bool IsManager { get; set; }
        public int Status { get; set; } = 1;
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

    }
}
