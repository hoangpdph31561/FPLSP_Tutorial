using FPLSP_Tutorial.Domain.Constants;
using FPLSP_Tutorial.Domain.Entities.Base;

namespace FPLSP_Tutorial.Domain.Entities
{
    public class UserMajorEntity : ICreatedBase,IDeletedBase
    {
        public Guid Id { get; set; }
        public Guid MajorId { get; set; }
        public Guid UserId { get; set; }
        public bool IsManager { get; set; }
        public string Status { get; set; } 
        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }

        public MajorEntity Major { get; set; }
        public UserEntity User { get; set; }
    }
}
