using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IUserMajorRepository
    {
        public Task<PaginationResponse<UserMajorDTO>> GetListWithPaginationAsync(UserMajorViewWithPaginationRequest request);
        public Task<bool> CreateAsync(UserMajorCreateRequest request);
        public Task<bool> DeleteAsync(UserMajorDeleteRequest request);

    }
}
