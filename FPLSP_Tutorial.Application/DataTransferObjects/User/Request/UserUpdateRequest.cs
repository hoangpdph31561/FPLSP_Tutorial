using FPLSP_Tutorial.Domain.Constants;
using System.Text.Json.Nodes;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User.Request
{
    public class UserUpdateRequest
    {
        public JsonArray RoleCodes { get; set; } = new JsonArray();
        public int Status { get; set; } = EntityStatus.Active;
    }
}
