using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.Application.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;

public interface IUserMajorReadOnlyRepository
{
    Task<RequestResult<List<UserMajorDTO>>> GetMajorUserAsync(UserMajorViewRequest request,
        CancellationToken cancellationToken);
}