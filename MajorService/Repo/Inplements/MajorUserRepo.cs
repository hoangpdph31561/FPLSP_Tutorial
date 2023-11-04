using MajorService.Data.UserMajor;
using MajorService.Pagination;
using MajorService.Repo.Interfaces;
using System.Net.Http.Json;

namespace MajorService.Repo.Inplements
{
    public class MajorUserRepo : IMajorUserRepo
    {
        private readonly HttpClient _httpClient;

        public MajorUserRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PaginationResponse<MajorUserDto>> GetListMajorUser()
        {
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorUserDto>>($"/api/MajorUsers");
            if (result == null)
            {
                return new();
            }
            return result;
        }
    }
}
