using System.Net.Http.Json;
using System.Text.Json;
using FPLSP_Tutorial.WASM.Data;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FPLSP_Tutorial.WASM.Repositories.Implements;

public class MajorRepository : IMajorRepository
{
    private readonly HttpClient _httpClient;

    public MajorRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MajorDTO>> GetListAsync(MajorViewRequest request)
    {
        var url = "/api/Majors/GetListAsync?";

        if (request.UserId != null) url += $"&UserId={request.UserId}";
        if (request.NotJoined) url += $"&NotJoined={request.NotJoined}";
        if (request.ContainPostOnly) url += $"&ContainPostOnly={request.ContainPostOnly}";
        if (!request.SearchString.IsNullOrEmpty()) url += $"&SearchString={request.SearchString}";

        var result = await _httpClient.GetFromJsonAsync<List<MajorDTO>>(url);
        if (result == null) return new List<MajorDTO>();
        return result;
    }


    public async Task<PaginationResponse<MajorDTO>> GetListWithPaginationAsync(MajorViewWithPaginationRequest request)
    {
        var url = $"/api/Majors/GetListWithPaginationAsync?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

        if (request.UserId != null) url += $"&UserId={request.UserId}";
        if (request.NotJoined) url += $"&NotJoined={request.NotJoined}";
        if (request.ContainPostOnly) url += $"&ContainPostOnly={request.ContainPostOnly}";

        if (request.SortingProperty != null) url += $"&SortingProperty={request.SortingProperty}";
        if (request.SortingProperty != null) url += $"&SortingDirection={request.SortingDirection}";

        if (!request.SearchString.IsNullOrEmpty()) url += $"&SearchString={request.SearchString}";

        var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorDTO>>(url);
        if (result == null) return new PaginationResponse<MajorDTO>();
        return result;
    }

    public async Task<MajorDTO> GetByIdAsync(Guid id)
    {
        var result = await _httpClient.GetFromJsonAsync<MajorDTO>($"api/Majors/{id}");
        return result;
    }

    public async Task<APIResponse> AddAsync(MajorCreateRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("/api/Majors", request);
        if (result.IsSuccessStatusCode)
        {
            var contentAsString = await result.Content.ReadAsStringAsync();
            var resultDTO = JsonSerializer.Deserialize<APIResponse>(contentAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return resultDTO;
        }

        return new APIResponse
        {
            Success = false,
            Message = "Yêu cầu thất bại"
        };
    }

    public async Task<APIResponse> UpdateAsync(MajorUpdateRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync("/api/Majors", request);
        if (result.IsSuccessStatusCode)
        {
            var contentAsString = await result.Content.ReadAsStringAsync();
            var resultDTO = JsonSerializer.Deserialize<APIResponse>(contentAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return resultDTO;
        }

        return new APIResponse
        {
            Success = false,
            Message = "Yêu cầu thất bại"
        };
    }

    public async Task<APIResponse> DeleteAsync(MajorDeleteRequest request)
    {
        var url = $"api/Majors?Id={request.Id}";

        if (request.DeletedBy != null) url += $"&DeletedBy={request.DeletedBy}";

        var result = await _httpClient.DeleteAsync(url);
        if (result.IsSuccessStatusCode)
        {
            var contentAsString = await result.Content.ReadAsStringAsync();
            var resultDTO = JsonSerializer.Deserialize<APIResponse>(contentAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return resultDTO;
        }

        return new APIResponse
        {
            Success = false,
            Message = "Yêu cầu thất bại"
        };
    }
}