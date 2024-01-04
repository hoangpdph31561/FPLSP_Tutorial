using FPLSP_Tutorial.Application.DataTransferObjects.Post;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;

public interface IPostReadOnlyRespository
{
    Task<RequestResult<List<PostDTO>>> GetPostAsync(
        PostViewRequest request, CancellationToken cancellationToken);

    Task<RequestResult<PaginationResponse<PostDTO>>> GetPostWithPaginationAsync(
        PostViewWithPaginationRequest request, CancellationToken cancellationToken);

    Task<RequestResult<PostDTO?>> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken);
}