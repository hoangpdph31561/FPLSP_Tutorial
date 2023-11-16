﻿using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request
{
    public class ViewMajorRequestSearchWithPaginationRequest : PaginationRequest
    {
       
        public Guid? MajorId  { get; set; }
    }
}
