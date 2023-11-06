using MajorService.Data.UserMajor;
using MajorService.Data.UserMajor.Request;
using MajorService.Pagination;

namespace MajorService.Repo.Interfaces
{
    public interface IMajorUserRepo
    {
        public Task<PaginationResponse<MajorUserDto>> GetListMajorUser();
        public Task<bool> CreateMajorUser(CreateUserMajorRequest request);

    }
}
