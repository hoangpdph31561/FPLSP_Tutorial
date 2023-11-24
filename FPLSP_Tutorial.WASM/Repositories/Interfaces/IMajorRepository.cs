using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IMajorRepository
    {
        Task<PaginationResponse<MajorDTO>> GetListWithPaginationAsync(MajorViewWithPaginationRequest request);
        Task<MajorDTO> GetByIdAsync(Guid id);
        Task<bool> AddAsync(MajorCreateRequest request);
        Task<bool> UpdateAsync(MajorUpdateRequest request);
        Task<bool> DeleteAsync(MajorDeleteRequest request);
    }
}
