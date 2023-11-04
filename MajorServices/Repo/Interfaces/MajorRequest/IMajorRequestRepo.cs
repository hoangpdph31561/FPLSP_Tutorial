using MajorServices.Data.MajorRequest;
using MajorServices.Data.MajorRequest.Request;
using MajorServices.Pagination;
using System.Threading.Tasks;

namespace MajorServices.Repo.Interfaces.MajorRequest
{
    public interface IMajorRequestRepo
    {
        public Task<PaginationResponse<MajorRequestDto>> GetListMajorRequest();
        public Task<bool> DeleteMajorRequest(MajorRequestDeleteRequest request);
    }
}
