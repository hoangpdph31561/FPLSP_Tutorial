﻿using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Post
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public string PostType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }

        public List<TagDTO> ListTag { get; set; } = new List<TagDTO>();
        public int NumberOfChildPost { get; set; } = 0;
        public string CreatedByName { get; set; } = string.Empty;
        public string CreatedByEmail { get; set; } = string.Empty;
        public Guid? MajorId { get; set; } = null;
        public string? MajorCode { get; set; } = null;
        public string? MajorName { get; set; } = null;
    }
}
