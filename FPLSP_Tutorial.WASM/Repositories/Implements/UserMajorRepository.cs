using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using System.Net.Http.Json;

namespace FPLSP_Tutorial.WASM.Repositories.Implements
{
    public class UserMajorRepository : IUserMajorRepository
    {
        private readonly HttpClient _httpClient;

        public UserMajorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<PaginationResponse<UserMajorDTO>> GetListWithPaginationAsync(UserMajorViewWithPaginationRequest request)
        {
            string url = $"/api/UserMajors?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<UserMajorDTO>>(url);
            return result;

        }

        public async Task<bool> CreateAsync(UserMajorCreateRequest request)
        {
            var resultCreate = await _httpClient.PostAsJsonAsync("/api/UserMajors", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
