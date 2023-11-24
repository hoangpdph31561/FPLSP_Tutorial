using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Major.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using System.Net.Http.Json;

namespace FPLSP_Tutorial.WASM.Repositories.Implements
{
    public class MajorRepository : IMajorRepository
    {
        private readonly HttpClient _httpClient;
        public MajorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<MajorDTO>> GetListWithPaginationAsync(MajorViewWithPaginationRequest request)
        {
            string url = $"/api/Majors/GetListWithPagination?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

            
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorDTO>>(url);
            if (result == null)
            {
                return new();
            }
            return result;
        }

        public async Task<MajorDTO> GetByIdAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<MajorDTO>($"api/Majors/{id}");
            return result;
        }

        public async Task<bool> AddAsync(MajorCreateRequest request)
        {
            var resultCreate = await _httpClient.PostAsJsonAsync($"/api/Majors", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(MajorUpdateRequest request)
        {
            var resultCreate = await _httpClient.PutAsJsonAsync($"/api/Majors", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(MajorDeleteRequest request)
        {
            string url = $"api/Majors?Id={request.Id}";

            if (request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }

            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }
    }
}
