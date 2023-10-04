using FPLSP_Tutorial.Application.DataTransferObjects.Example.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IExampleReadWriteRepository
    {
        Task<RequestResult<Guid>> AddExampleAsync(ExampleEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateExampleAsync(ExampleEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteExampleAsync(ExampleDeleteRequest request, CancellationToken cancellationToken);
    }
}