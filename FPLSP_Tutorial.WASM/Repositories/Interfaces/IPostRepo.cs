using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IPostRepo
    {
        Task<PaginationResponse<PostDto>> GetListWithPaginationAsync(ViewPostWithPaginationRequest request);
        Task<PostDto> GetPostById(Guid id);
        Task<bool> CreatePostAsync(PostCreateRequest request);
        Task<bool> UpdatePostAsync(PostUpdateRequest request);
        Task<bool> DeletePostAsync(PostDeleteRequest request);

    }
}
