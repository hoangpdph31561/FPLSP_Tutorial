using FPLSP_Tutorial.Domain.Constants;
using System.Text.Json.Nodes;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserCreateRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public JsonArray RoleCodes { get; set; } = new JsonArray();
        public int Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
