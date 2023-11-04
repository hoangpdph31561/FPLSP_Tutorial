using MajorService.Data.UserMajor;
using MajorService.Pagination;

namespace MajorService.Repo.Interfaces
{
    public interface IMajorUserRepo
    {
        public Task<PaginationResponse<MajorUserDto>> GetListMajorUser();
    }
}
