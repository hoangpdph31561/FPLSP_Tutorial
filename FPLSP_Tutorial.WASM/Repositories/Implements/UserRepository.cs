using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using System.Net.Http.Json;

namespace FPLSP_Tutorial.WASM.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<UserDTO>> GetListWithPaginationAsync(UserViewWithPaginationRequest request)
        {
            string url = $"/api/Users/GetListWithPagination?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<UserDTO>>(url);
            if (result == null)
            {
                return new();
            }
            return result;
        }

        public async Task<UserDTO?> GetByEmailAsync(string email)
        {
            try
            {
                var url = $"/api/Users/GetByEmailAsync?email={email}";
                var response = await _httpClient.GetFromJsonAsync<UserDTO?>(url);
                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(UserCreateRequest request)
        {
            try
            {
                var url = $"/api/Users";
                var response = await _httpClient.PostAsJsonAsync<UserCreateRequest>(url, request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UserUpdateRequest request)
        {
            var resultCreate = await _httpClient.PutAsJsonAsync($"/api/Users", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
