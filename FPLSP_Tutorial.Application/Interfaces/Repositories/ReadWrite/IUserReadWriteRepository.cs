using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IUserReadWriteRepository
    {
        Task<RequestResult<Guid>> AddUserAsync(UserEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateUserAsync(UserEntity entity, CancellationToken cancellationToken);
    }
}
