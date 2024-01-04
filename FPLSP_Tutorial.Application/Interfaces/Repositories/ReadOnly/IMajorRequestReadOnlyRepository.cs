using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;

public interface IMajorRequestReadOnlyRepository
{
    Task<RequestResult<List<MajorRequestDTO>>> GetMajorRequestAsync(
        MajorRequestViewRequest request, CancellationToken cancellationToken);

    Task<RequestResult<PaginationResponse<MajorRequestDTO>>> GetMajorRequestWithPaginationAsync(
        MajorRequestViewWithPaginationRequest request, CancellationToken cancellationToken);

    Task<RequestResult<MajorRequestDTO?>> GetMajorRequestByIdAsync(Guid idMajorRequest,
        CancellationToken cancellationToken);
}