using MajorService.Data.MajorRequest;
using MajorService.Data.MajorRequest.Request;
using MajorService.Data.UserMajor;
using MajorService.Pagination;

namespace MajorService.Repo.Interfaces
{
    public interface IMajorRequestRepo
    {
        public Task<PaginationResponse<MajorRequestDto>> GetListMajorRequest();
        public Task<bool> DeleteMajorRequest(MajorRequestDeleteRequest request);
     

    }
}
