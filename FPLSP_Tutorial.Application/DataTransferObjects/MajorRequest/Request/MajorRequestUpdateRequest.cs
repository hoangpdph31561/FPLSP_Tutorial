﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request
{
    public class MajorRequestUpdateRequest
    {
        public Guid Id { get; set; }
        public int Status { get; set; } = 1;
    }
}
