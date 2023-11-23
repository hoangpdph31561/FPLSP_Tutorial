using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IClientPostReadWriteRespository
    {
        Task<RequestResult<Guid>> AddMajorRequest(MajorRequestEntity entity, CancellationToken cancellationToken);
    }
}
