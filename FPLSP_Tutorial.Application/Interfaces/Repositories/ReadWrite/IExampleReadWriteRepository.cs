using FPLSPTutorial.Application.DataTransferObjects.Example.Request;
using FPLSPTutorial.Application.ValueObjects.Respone;
using FPLSPTutorial.Domain.Entities;

namespace FPLSPTutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IExampleReadWriteRepository
    {
        Task<RequestResult<Guid>> AddExampleAsync(ExampleEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateExampleAsync(ExampleEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteExampleAsync(ExampleDeleteRequest request, CancellationToken cancellationToken);
    }
}