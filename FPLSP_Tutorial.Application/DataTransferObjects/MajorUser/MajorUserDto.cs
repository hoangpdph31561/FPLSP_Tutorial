using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorUser
{
    public class MajorUserDto
    {
        public Guid MajorId { get; set; }
        public string TenChuyenNganh { get; set; }
        public string email { get; set; }
        public List<string> RoleCodes { get; set; } = new List<string>();
        public Guid UserId { get; set; }
        public bool IsManager { get; set; }
        public int Status { get; set; } = 1;
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }

    }
}
