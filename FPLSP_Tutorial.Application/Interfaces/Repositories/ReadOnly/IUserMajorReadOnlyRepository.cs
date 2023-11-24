using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser;
using FPLSP_Tutorial.Application.DataTransferObjects.MajorUser.Request;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IUserMajorReadOnlyRepository
    {
        Task<RequestResult<List<UserMajorDTO>>> GetMajorUserAsync(UserMajorViewRequest request, CancellationToken cancellationToken);
    }
}
