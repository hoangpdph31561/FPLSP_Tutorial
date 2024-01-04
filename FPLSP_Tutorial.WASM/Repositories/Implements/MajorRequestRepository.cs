using System.Net.Http.Json;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;

namespace FPLSP_Tutorial.WASM.Repositories.Implements;

public class MajorRequestRepository : IMajorRequestRepository
{
    private readonly HttpClient _httpClient;

    public MajorRequestRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginationResponse<MajorRequestDTO>> GetListWithPaginationAsync(
        MajorRequestViewWithPaginationRequest request)
    {
        var url =
            $"/api/MajorRequests/GetListWithPagination?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

        var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorRequestDTO>>(url);
        if (result == null) return new PaginationResponse<MajorRequestDTO>();
        return result;
    }

    public async Task<bool> AddAsync(MajorRequestCreateRequest request)
    {
        var resultDelete = await _httpClient.PostAsJsonAsync("/api/MajorRequests", request);
        if (resultDelete.IsSuccessStatusCode) return true;
        return false;
    }

    public async Task<bool> DeleteAsync(MajorRequestDeleteRequest request)
    {
        var resultDelete = await _httpClient.DeleteAsync($"/api/MajorRequests?id={request.Id}");
        if (resultDelete.IsSuccessStatusCode) return true;
        return false;
    }
}