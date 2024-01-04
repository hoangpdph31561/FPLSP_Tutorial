using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces;

public interface IUserRepository
{
    Task<PaginationResponse<UserDTO>> GetListWithPaginationAsync(UserViewWithPaginationRequest request);
    Task<UserDTO> GetByEmailAsync(string email);
    Task<bool> AddAsync(UserCreateRequest request);
    Task<bool> UpdateAsync(UserUpdateRequest request);
}