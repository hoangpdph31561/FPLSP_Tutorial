using FPLSP_Tutorial.WASM.Data;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces;

public interface IMajorRepository
{
    Task<List<MajorDTO>> GetListAsync(MajorViewRequest request);
    Task<PaginationResponse<MajorDTO>> GetListWithPaginationAsync(MajorViewWithPaginationRequest request);
    Task<MajorDTO> GetByIdAsync(Guid id);
    Task<APIResponse> AddAsync(MajorCreateRequest request);
    Task<APIResponse> UpdateAsync(MajorUpdateRequest request);
    Task<APIResponse> DeleteAsync(MajorDeleteRequest request);
}