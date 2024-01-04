using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces;

public interface IMajorRequestRepository
{
    public Task<PaginationResponse<MajorRequestDTO>> GetListWithPaginationAsync(
        MajorRequestViewWithPaginationRequest request);

    public Task<bool> AddAsync(MajorRequestCreateRequest request);
    public Task<bool> DeleteAsync(MajorRequestDeleteRequest request);
}