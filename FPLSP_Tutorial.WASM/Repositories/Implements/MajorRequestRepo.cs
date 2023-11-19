using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using System.Net.Http.Json;

namespace FPLSP_Tutorial.WASM.Repositories.Implements
{
    public class MajorRequestRepo : IMajorRequestRepo
    {
        private readonly HttpClient _httpClient;

        public MajorRequestRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PaginationResponse<MajorRequestDto>> GetListMajorRequest(ViewMajorRequestSearchWithPaginationRequest request)
        {
            string url = $"/api/MajorRequests/majorRequestNotDeleted?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

            if (!string.IsNullOrEmpty(request.Email))
            {
                url = $"/api/MajorRequests/majorRequestNotDeleted/?Email={request.Email}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorRequestDto>>(url);
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
