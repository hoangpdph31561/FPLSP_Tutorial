using FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IMajorReadWriteRepository
    {
        Task<RequestResult<Guid>> AddMajorAsync(MajorEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateMajorAsync(MajorEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteMajorAsync(MajorDeleteRequest request, CancellationToken cancellationToken);
    }
}
