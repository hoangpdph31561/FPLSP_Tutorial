﻿using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag;
using FPLSP_Tutorial.WASM.Enum;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request
{
    public class PostCreateRequest
    {
        public Guid? PostId { get; set; }
        public string PostType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public Guid? CreatedBy { get; set; }
    }
}
