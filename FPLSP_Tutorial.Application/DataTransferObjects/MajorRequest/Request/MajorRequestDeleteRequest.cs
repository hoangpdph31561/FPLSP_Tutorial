﻿namespace FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request
{
    public class MajorRequestDeleteRequest
    {
        public Guid Id { get; set; }
        public Guid? DeletedBy { get; set; }

    }
}
