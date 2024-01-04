using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces;

public interface IUserMajorRepository
{
    Task<PaginationResponse<UserMajorDTO>> GetListWithPaginationAsync(UserMajorViewWithPaginationRequest request);
    Task<bool> CreateAsync(UserMajorCreateRequest request);
    Task<bool> UpdateAsync(UserMajorUpdateRequest request);
    Task<bool> DeleteAsync(UserMajorDeleteRequest request);
}