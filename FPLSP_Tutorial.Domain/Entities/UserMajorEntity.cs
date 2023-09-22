using FPLSPTutorial.Domain.Constants;
using FPLSPTutorial.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Domain.Entities
{
    public class UserMajorEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public Guid MajorId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }

        public MajorEntity Major { get; set; }
        public UserEntity User { get; set; }
    }
}
