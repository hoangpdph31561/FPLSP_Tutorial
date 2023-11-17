using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserDeleteRequest
    {
        public Guid Id { get; set; }
        public object DeletedBy { get; set; }
    }
}
