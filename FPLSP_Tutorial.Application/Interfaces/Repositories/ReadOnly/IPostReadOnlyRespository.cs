
using FPLSP_Tutorial.Application.DataTransferObjects.Post;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Response;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IPostReadOnlyRespository
    {
        Task<RequestResult<PostDto?>> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<PostDto>>> GetPostWithPaginationAsync(
            ViewPostWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
