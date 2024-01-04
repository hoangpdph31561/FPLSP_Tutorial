using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;

public interface IPostTagReadWriteRepository
{
    Task<RequestResult<int>> AddRangeAsync(List<PostTagEntity> entity, CancellationToken cancellationToken);
    Task<RequestResult<int>> AddAsync(PostTagEntity entity, CancellationToken cancellationToken);
    Task<RequestResult<int>> UpdateAsync(PostTagEntity entity, CancellationToken cancellationToken);
    Task<RequestResult<int>> DeleteAsync(PostTagDeleteRequest request, CancellationToken cancellationToken);
    Task<RequestResult<int>> SyncRangeAsync(Guid PostId, List<TagEntity> request, CancellationToken cancellationToken);
}