﻿using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request
{
    public class MajorViewWithPaginationRequest : PaginationRequest
    {
        public Guid? UserId { get; set; } = null;
    }
}
