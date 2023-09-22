using FPLSP_Tutorial.Domain.Constants;
using FPLSP_Tutorial.Domain.Entities.Base;

namespace FPLSP_Tutorial.Domain.Entities
{
    public class PostTagEntity : ICreatedBase, IDeletedBase
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public Guid TagId { get; set; }
        public string Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }

        public PostEntity Post { get; set; }
        public TagEntity Tag { get; set; }
    }
}
