using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;

public interface IUserMajorReadWriteRepository
{
    Task<RequestResult<Guid>> AddUserMajorAsync(UserMajorEntity entity, CancellationToken cancellationToken);
    Task<RequestResult<int>> DeleteUserMajorAsync(UserMajorDeleteRequest request, CancellationToken cancellationToken);
    Task<RequestResult<int>> UpdateUserMajorAsync(UserMajorEntity entity, CancellationToken cancellationToken);
}