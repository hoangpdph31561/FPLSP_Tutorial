﻿using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request
{
    public class MajorViewWithPaginationRequest : PaginationRequest
    {
        public Guid? UserId { get; set; } = null;
        public bool NotJoined { get; set; }
        public bool ContainPostOnly { get; set; }
    }
}
