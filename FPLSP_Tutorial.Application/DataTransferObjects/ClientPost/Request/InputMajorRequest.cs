﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request
{
    public class InputMajorRequest
    {
        public Guid UserId { get; set; }
        public Guid MajorId { get; set; }
    }
}
