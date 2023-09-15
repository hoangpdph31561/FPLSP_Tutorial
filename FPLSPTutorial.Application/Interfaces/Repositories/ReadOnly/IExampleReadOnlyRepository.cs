using FPLSPTutorial.Application.DataTransferObjects.Example;
using FPLSPTutorial.Application.DataTransferObjects.Example.Request;
using FPLSPTutorial.Application.ValueObjects.Pagination;
using FPLSPTutorial.Application.ValueObjects.Respone;

namespace FPLSPTutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IExampleReadOnlyRepository
    {
        Task<RequestResult<ExampleDto?>> GetExampleByIdAsync(Guid idExample, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ExampleDto>>> GetExampleWithPaginationByAdminAsync(
            ViewExampleWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
