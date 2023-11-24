using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IMajorRepository
    {
        Task<PaginationResponse<MajorDTO>> GetListWithPagination(MajorViewWithPaginationRequest request);
        Task<MajorDTO> GetById(Guid id);
        Task<bool> AddAsync(MajorCreateRequest request);
        Task<bool> UpdateAsync(MajorUpdateRequest request);
        Task<bool> DeleteAsync(MajorDeleteRequest request);
    }
}
