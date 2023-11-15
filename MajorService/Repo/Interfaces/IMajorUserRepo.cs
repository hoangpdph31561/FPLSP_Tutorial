using MajorService.Data.DataTransferObjects.UserMajor.Request;
using MajorService.Data.Pagination;
using MajorService.Data.UserMajor;
using MajorService.Data.UserMajor.Request;
using MajorService.ViewModel;

namespace MajorService.Repo.Interfaces
{
    public interface IMajorUserRepo
    {
        public Task<PaginationResponse<MajorUserDto>> GetListMajorUser();
        public Task<PaginationResponse<MajorUserDto>> GetListMajorUserBySearch(ViewMajorUserBySearchRequest request);
        public Task<bool> CreateMajorUser(CreateUserMajorRequest request);

    }
}
