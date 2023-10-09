using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request
{
    public class CreateUserMajorRequest
    {
        public Guid MajorId { get; set; }
        public Guid UserId { get; set; }
        public int Status { get; set; } = 1;
    }
}
