using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IMajorUserRepo
    {
        public Task<PaginationResponse<MajorUserDto>> GetListMajorUser(ViewMajorUserBySearchRequest request);
        public Task<bool> CreateMajorUser(CreateUserMajorRequest request);

    }
}
