using FPLSP_Tutorial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost
{
    public class PostMainDTO : PostBaseDTO
    {
        public string PostType { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        //Tên người tạo
        public string CreatedName { get; set; } = string.Empty;
    }
}
