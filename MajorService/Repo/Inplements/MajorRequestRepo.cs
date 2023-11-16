using MajorService.Data.MajorRequest;
using MajorService.Data.MajorRequest.Request;
using MajorService.Data.Pagination;
using MajorService.Data.UserMajor;
using MajorService.Repo.Interfaces;
using MajorService.ViewModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace MajorService.Repo.Inplements
{
    public class MajorRequestRepo : IMajorRequestRepo
    {
        private readonly HttpClient _httpClient;

        public MajorRequestRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PaginationResponse<MajorRequestDto>> GetListMajorRequest()
        {
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorRequestDto>>($"/api/MajorRequests/majorRequestNotDeleted");
            if (result == null)
            {
                return new();
            }
            return result;
        }
        public async Task<bool> DeleteMajorRequest(MajorRequestDeleteRequest request)
        {
            var resultDelete = await _httpClient.DeleteAsync($"/api/MajorRequests?id={request.Id}");
            if (resultDelete.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
