using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces;

public interface ITagRepository
{
    Task<List<TagDTO>> GetListAsync(TagViewRequest request);
    Task<PaginationResponse<TagDTO>> GetListWithPaginationAsync(TagViewWithPaginationRequest request);
    Task<TagDTO> GetByIdAsync(Guid id);
    Task<bool> AddAsync(TagCreateRequest request);
    Task<bool> UpdateAsync(TagUpdateRequest request);
    Task<bool> DeleteAsync(TagDeleteRequest request);
}