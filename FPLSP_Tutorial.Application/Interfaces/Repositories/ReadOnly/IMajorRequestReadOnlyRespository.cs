using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Enums;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IMajorRequestReadOnlyRespository
    {
        Task<RequestResult<MajorRequestDto?>> GetMajorRequestByIdAsync(Guid idMajorRequest, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<MajorRequestDto>>> GetMajorRequestWithPaginationByAdminAsync(
            ViewMajorRequestWithPaginationRequest request, CancellationToken cancellationToken); 
        Task<RequestResult<PaginationResponse<MajorRequestDto>>> GetMajorRequestWithPaginationByADeletedAsync(
            ViewMajorRequestWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
