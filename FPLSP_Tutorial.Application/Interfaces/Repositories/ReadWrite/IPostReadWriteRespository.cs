using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IPostReadWriteRespository
    {
        Task<RequestResult<Guid>> AddPostAsync(PostEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdatePostAsync(PostEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeletePostAsync(PostDeleteRequest request, CancellationToken cancellationToken);
    }
}
