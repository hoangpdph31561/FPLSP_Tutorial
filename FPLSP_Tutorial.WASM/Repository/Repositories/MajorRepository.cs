using FPLSP_Tutorial.WASM.Data.DTO.Major;
using FPLSP_Tutorial.WASM.Data.DTO.Major.Request;
using FPLSP_Tutorial.WASM.Service.Interfaces;
using FPLSP_Tutorial.WASM.ValueObjects.Pagination;
using System.Net.Http.Json;


namespace FPLSP_Tutorial.WASM.Service.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly HttpClient _httpClient;
        public MajorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddMajorAsync(MajorCreateRequest request)
        {
            var resultCreate = await _httpClient.PostAsJsonAsync($"/api/Majors", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMajorAsync(MajorDeleteRequest request)
        {
            string url = $"api/Majors?Id={request.Id}";
            if (request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PaginationResponse<MajorDTO?>> GetListMajor(ViewMajorWithPaginationRequest request)
        {
            string url = $"/api/Majors/get?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorDTO>>(url);
            if (result == null)
            {
                return new();
            }
            return result;
        }

        public async Task<MajorDTO> GetMajorById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<MajorDTO>($"api/Majors/{id}");
            return result;
        }

        public async Task<bool> UpdateMajorAsync(MajorUpdateRequest request)
        {
            var resultCreate = await _httpClient.PutAsJsonAsync($"/api/Majors", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
