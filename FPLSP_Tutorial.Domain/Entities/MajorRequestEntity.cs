using FPLSP_Tutorial.Domain.Constants;
using FPLSP_Tutorial.Domain.Entities.Base;
namespace FPLSP_Tutorial.Domain.Entities
{
    public class MajorRequestEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public Guid MajorId { get; set; }
        public bool IsManager { get; set; }
        public int Status { get; set; } = 1;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }

        public MajorEntity Major { get; set; }
    }
}
