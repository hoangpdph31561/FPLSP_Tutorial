using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IMajorRepo
    {
        Task<PaginationResponse<MajorDTO>> GetListMajor(ViewMajorWithPaginationRequest request);
        Task<bool> AddMajorAsync(MajorCreateRequest request);
        Task<bool> UpdateMajorAsync(MajorUpdateRequest request);
        Task<bool> DeleteMajorAsync(MajorDeleteRequest request);
        Task<MajorDTO> GetMajorById(Guid id);
    }
}
