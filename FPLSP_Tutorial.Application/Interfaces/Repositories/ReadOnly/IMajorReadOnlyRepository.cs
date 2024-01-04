using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;

public interface IMajorReadOnlyRepository
{
    Task<RequestResult<List<MajorDTO>>> GetMajorAsync(MajorViewRequest request, CancellationToken cancellationToken);

    Task<RequestResult<PaginationResponse<MajorDTO>>> GetMajorWithPaginationAsync(
        MajorViewWithPaginationRequest request, CancellationToken cancellationToken);

    Task<RequestResult<MajorDTO?>> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken);
}