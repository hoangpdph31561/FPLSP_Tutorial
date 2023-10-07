using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IMajorReadOnlyRepository
    {
        Task<RequestResult<MajorDTOs?>> GetMajorByIdAsync(Guid idExample, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<MajorDTOs>>> GetMajorWithPaginationByAdminAsync(
            ViewMajorWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
