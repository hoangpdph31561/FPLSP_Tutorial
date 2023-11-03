using System;
using MajorServices.Data.MajorRequest;
using MajorServices.Data.MajorRequest.Request;
using MajorServices.Pagination;
using MajorServices.Repo.Interfaces.MajorRequest;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MajorServices.Repo.Implements.MajorRequest
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
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorRequestDto>>($"/api/MajorRequests");
            if(result == null)
            {
                return new();
            }
            return result;
        }
        public async Task<bool> DeleteMajorRequest(MajorRequestDeleteRequest request)
        {
            var uri = $"/api/MajorRequests/{request.Id}";
            var resultDelete = await _httpClient.DeleteAsync(uri);
            if (resultDelete.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}
