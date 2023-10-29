using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public JsonArray RoleCodes { get; set; } = new JsonArray();
        public int Status { get; set; } = 1;
    }
}
