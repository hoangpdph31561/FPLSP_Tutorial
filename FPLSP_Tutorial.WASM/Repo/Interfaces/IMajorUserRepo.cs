using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Data.UserMajor;
using FPLSP_Tutorial.WASM.Data.UserMajor.Request;
using FPLSP_Tutorial.WASM.ViewModel;

namespace FPLSP_Tutorial.WASM.Repo.Interfaces
{
    public interface IMajorUserRepo
    {
        public Task<PaginationResponse<MajorUserDto>> GetListMajorUser(ViewMajorUserBySearchRequest request);
        public Task<bool> CreateMajorUser(CreateUserMajorRequest request);

    }
}
