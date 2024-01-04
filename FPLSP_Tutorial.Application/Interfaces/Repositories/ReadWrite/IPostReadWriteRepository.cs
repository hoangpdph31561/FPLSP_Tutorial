using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;

public interface IPostReadWriteRepository
{
    Task<RequestResult<Guid>> AddAsync(PostEntity entity, CancellationToken cancellationToken);
    Task<RequestResult<int>> UpdateAsync(PostEntity entity, CancellationToken cancellationToken);
    Task<RequestResult<int>> DeleteAsync(PostDeleteRequest request, CancellationToken cancellationToken);
}