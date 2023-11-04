using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.Domain.Entities;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite
{
    public interface IMajorUserReadWriteResponsitory
    {
        Task<RequestResult<Guid>> AddMajorUserAsync(UserMajorEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteMajorUserAsync(DeleteMajorUserRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateMajorUserAsync(UserMajorEntity entity, CancellationToken cancellationToken);


    }
}
