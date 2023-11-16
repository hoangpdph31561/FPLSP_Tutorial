using MajorService.Data.MajorRequest;
using MajorService.Data.MajorRequest.Request;
using MajorService.Data.Pagination;
using MajorService.Data.UserMajor;
using MajorService.ViewModel;

namespace MajorService.Repo.Interfaces
{
    public interface IMajorRequestRepo
    {
        public Task<PaginationResponse<MajorRequestDto>> GetListMajorRequest(ViewMajorRequestSearchWithPaginationRequest request);
        public Task<bool> DeleteMajorRequest(MajorRequestDeleteRequest request);
    }
}
