using FPLSP_Tutorial.Application.DataTransferObjects.User;
using FPLSP_Tutorial.Application.DataTransferObjects.User.Request;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;

namespace FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly
{
    public interface IUserReadOnlyRepository
    {
        Task<RequestResult<UserDTO?>> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<RequestResult<UserDTO?>> GetUserByIdAsync(Guid idUser, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<UserDTO>>> GetUserWithPaginationByAdminAsync(
            ViewUserWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
