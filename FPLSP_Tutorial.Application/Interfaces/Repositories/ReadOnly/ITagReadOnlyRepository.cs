using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface ITagReadOnlyRepository
    {
        Task<RequestResult<TagDto?>> GetTagByIdAsync(Guid idTag, CancellationToken cancellationToken);
        Task<RequestResult<List<TagDto>>> GetTagByIdMajorAsync(Guid? idMajor, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<TagDto>>> GetTagWithPaginationByAdminAsync(
            ViewTagWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
