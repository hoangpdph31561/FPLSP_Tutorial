﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request
{
    public class MajorRequestDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }

    }
}
