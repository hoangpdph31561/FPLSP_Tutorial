using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace FPLSP_Tutorial.Application.DataTransferObjects.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<string> RoleCodes { get; set; } = new List<string>();
        public int Status { get; set; } = 1;
    }
}
