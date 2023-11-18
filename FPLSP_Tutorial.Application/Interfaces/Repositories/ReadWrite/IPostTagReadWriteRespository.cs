using FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IPostTagReadWriteRespository
    {
        Task<RequestResult<int>> AddPostTagAsync(List<PostTagEntity> entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdatePostTagAsync(PostTagEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeletePostTagAsync(PostTagDeleteRequest request, CancellationToken cancellationToken);
    }
}
