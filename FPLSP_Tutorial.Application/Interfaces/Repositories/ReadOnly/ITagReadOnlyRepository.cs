using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;

public interface ITagReadOnlyRepository
{
    Task<RequestResult<List<TagDTO>>> GetTagAsync(TagViewRequest request, CancellationToken cancellationToken);

    Task<RequestResult<PaginationResponse<TagDTO>>> GetTagWithPaginationAsync(TagViewWithPaginationRequest request,
        CancellationToken cancellationToken);

    Task<RequestResult<TagDTO?>> GetTagByIdAsync(Guid idTag, CancellationToken cancellationToken);
}