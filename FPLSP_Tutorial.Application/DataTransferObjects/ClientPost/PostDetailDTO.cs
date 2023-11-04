using FPLSP_Tutorial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost
{
    public class PostDetailDTO : PostMainDTO
    {
        public string Content { get; set; } = string.Empty;
    }
}

