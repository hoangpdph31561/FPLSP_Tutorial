using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<PaginationResponse<PostDTO>> GetListWithPaginationAsync(PostViewWithPaginationRequest request);
        Task<PostDTO> GetByIdAsync(Guid id);
        Task<bool> AddAsync(PostCreateRequest request);
        Task<bool> UpdateAsync(PostUpdateRequest request);
        Task<bool> DeleteAsync(PostDeleteRequest request);

    }
}
