using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.WASM.Data.MajorRequest;
using FPLSP_Tutorial.WASM.Data.MajorRequest.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Data.UserMajor;
using FPLSP_Tutorial.WASM.ViewModel;

namespace FPLSP_Tutorial.WASM.Repo.Interfaces
{
    public interface IMajorRequestRepo
    {
        public Task<PaginationResponse<MajorRequestDto>> GetListMajorRequest(ViewMajorRequestSearchWithPaginationRequest request);
        public Task<bool> DeleteMajorRequest(MajorRequestDeleteRequest request);
    }
}
