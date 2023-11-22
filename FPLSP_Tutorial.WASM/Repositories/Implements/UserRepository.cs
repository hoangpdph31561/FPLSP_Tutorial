using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User.Request;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using static FPLSP_Tutorial.Application.ValueObjects.Common.LocalizationString;

namespace FPLSP_Tutorial.WASM.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            try
            {
                var url = $"/api/Users/GetUserByEmailAsync?email={email}";
                var response = await _httpClient.GetFromJsonAsync<UserDTO?>(url);
                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateUserAsync(UserCreateRequest request)
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
    }
}
