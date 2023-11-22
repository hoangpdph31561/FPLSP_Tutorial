
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User.Request;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(UserCreateRequest request);

    }
}
