using FPLSPTutorial.Domain.Constants;
using FPLSPTutorial.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSPTutorial.Domain.Entities
{
    public class CategoryEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public long? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
    }
}
