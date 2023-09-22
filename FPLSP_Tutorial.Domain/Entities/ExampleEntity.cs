using FPLSP_Tutorial.Domain.Constants;
using FPLSP_Tutorial.Domain.Entities.Base;

namespace FPLSP_Tutorial.Domain.Entities
{
    public class ExampleEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
    }
}
