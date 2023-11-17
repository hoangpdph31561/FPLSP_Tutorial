using FPLSP_Tutorial.Application.DataTransferObjects.Major;
using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IMajorReadOnlyRepository
    {
        Task<RequestResult<MajorDTO?>> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<MajorDTO>>> GetMajorWithPaginationByAdminAsync(
            ViewMajorWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
