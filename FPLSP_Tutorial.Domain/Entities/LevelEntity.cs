﻿using FPLSPTutorial.Domain.Constants;
using FPLSPTutorial.Domain.Entities.Base;

namespace FPLSPTutorial.Domain.Entities
{
    public class LevelEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }

        public ICollection<PostEntity> PostEntities { get; set; }
    }
}