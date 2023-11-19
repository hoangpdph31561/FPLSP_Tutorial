using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.UserMajor.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using System.Net.Http.Json;

namespace FPLSP_Tutorial.WASM.Repositories.Implements
{
    public class MajorUserRepo : IMajorUserRepo
    {
        private readonly HttpClient _httpClient;

        public MajorUserRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateMajorUser(CreateUserMajorRequest request)
        {
            var resultCreate = await _httpClient.PostAsJsonAsync("/api/MajorUsers", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<PaginationResponse<MajorUserDto>> GetListMajorUser(ViewMajorUserBySearchRequest request)
        {
            string url = $"/api/MajorUsers?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

            if (!string.IsNullOrEmpty(request.Email))
            {
                url = $"/api/MajorUsers/?Email={request.Email}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }
            if (request.MajorId != null)
            {
                url = $"/api/MajorUsers/?MajorId={request.MajorId}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            }

            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorUserDto>>(url);
            if (result == null)
            {
                return new();
            }
            return result;

        }
    }
}
