using FPLSP_Tutorial.WASM.Data.DTO.Major;
using FPLSP_Tutorial.WASM.Data.DTO.Major.Request;
using FPLSP_Tutorial.WASM.ValueObjects.Pagination;

namespace FPLSP_Tutorial.WASM.Service.Interfaces
{
    public interface IMajorRepository
    {
        Task<PaginationResponse<MajorDTO>> GetListMajor(ViewMajorWithPaginationRequest request);
        Task<bool> AddMajorAsync(MajorCreateRequest request);
        Task<bool> UpdateMajorAsync(MajorUpdateRequest request);
        Task<bool> DeleteMajorAsync(MajorDeleteRequest request);
        Task<MajorDTO> GetMajorById(Guid id);
    }
}
