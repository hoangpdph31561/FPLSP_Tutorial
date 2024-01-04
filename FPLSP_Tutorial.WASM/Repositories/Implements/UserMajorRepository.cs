using System.Net.Http.Json;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;

namespace FPLSP_Tutorial.WASM.Repositories.Implements;

public class UserMajorRepository : IUserMajorRepository
{
    private readonly HttpClient _httpClient;

    public UserMajorRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<PaginationResponse<UserMajorDTO>> GetListWithPaginationAsync(
        UserMajorViewWithPaginationRequest request)
    {
        var url = $"/api/UserMajors?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

        var result = await _httpClient.GetFromJsonAsync<PaginationResponse<UserMajorDTO>>(url);
        return result;
    }

    public async Task<bool> CreateAsync(UserMajorCreateRequest request)
    {
        var resultCreate = await _httpClient.PostAsJsonAsync("/api/UserMajors", request);
        if (resultCreate.IsSuccessStatusCode) return true;
        return false;
    }

    public async Task<bool> UpdateAsync(UserMajorUpdateRequest request)
    {
        var resultCreate = await _httpClient.PutAsJsonAsync("/api/UserMajors", request);
        if (resultCreate.IsSuccessStatusCode) return true;
        return false;
    }

    public async Task<bool> DeleteAsync(UserMajorDeleteRequest request)
    {
        var url = $"/api/UserMajors?MajorId={request.MajorId}&UserId={request.UserId}";
        if (request.DeletedBy != null) url += $"&DeletedBy={request.DeletedBy}";

        var result = await _httpClient.DeleteAsync(url);
        if (result.IsSuccessStatusCode) return true;
        return false;
    }
}