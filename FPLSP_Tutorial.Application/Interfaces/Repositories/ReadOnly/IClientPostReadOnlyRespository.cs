using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost;
using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Request;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IClientPostReadOnlyRespository
    {
        #region API old
        Task<RequestResult<PaginationResponse<MajorBaseDTO>>> GetAllMajorAsync(ClientPostMajorRequestWithPagination request, CancellationToken cancellationToken);
        #endregion
        #region API new
        Task<RequestResult<PostDetailDTO>> GetPostDetailByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<PostMainDTO>>> GetAllPostByMajorAsync(ClientPostListRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PostMainDTO?>> GetParentPostByPostIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<PostMainDTO?>>> GetChildPostsByPostIdAsync(PostIdRequestWithPagination request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<TagBaseDTO>>> GetPostTagsByPostIdAsync(PostIdRequestWithPagination request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<MajorBaseDTO>>> GetMajorByUserIdAsync(GetMajorByUserIdWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<List<PostMainDTO>>> GetAllPostByMajorId(Guid? id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<PostMainDTO>>> GetAllPostsBySearchAsyn(ClientPostSearchWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<MajorBaseDTO?>> GetMajorByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<List<TagBaseDTO>>> GetAllTagList(CancellationToken cancellationToken);
        #endregion
    }
}
