using FPLSP_Tutorial.Domain.Constants;
using System.Text.Json.Nodes;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public List<string> RoleCodes { get; set; } = new List<string>();
        public int Status { get; set; } = EntityStatus.Active;
    }
}
