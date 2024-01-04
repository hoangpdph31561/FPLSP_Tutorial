using System.Net.Http.Json;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.User.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FPLSP_Tutorial.WASM.Repositories.Implements;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;

    public UserRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginationResponse<UserDTO>> GetListWithPaginationAsync(UserViewWithPaginationRequest request)
    {
        var url = $"/api/Users/GetListWithPagination?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

        if (!request.SearchString.IsNullOrEmpty()) url += $"&SearchString={request.SearchString}";

        var result = await _httpClient.GetFromJsonAsync<PaginationResponse<UserDTO>>(url);
        if (result == null) return new PaginationResponse<UserDTO>();
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
            var url = "/api/Users";
            var response = await _httpClient.PostAsJsonAsync(url, request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(UserUpdateRequest request)
    {
        var resultCreate = await _httpClient.PutAsJsonAsync("/api/Users", request);
        if (resultCreate.IsSuccessStatusCode) return true;
        return false;
    }
}