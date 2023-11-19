using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IMajorRequestRepo
    {
        public Task<PaginationResponse<MajorRequestDto>> GetListMajorRequest(ViewMajorRequestSearchWithPaginationRequest request);
        public Task<bool> DeleteMajorRequest(MajorRequestDeleteRequest request);
    }
}
