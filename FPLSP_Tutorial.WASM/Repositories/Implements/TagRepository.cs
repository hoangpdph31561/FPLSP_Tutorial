﻿using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag.TagRequest;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using System.Net.Http.Json;

namespace FPLSP_Tutorial.WASM.Repositories.Implements
{
    public class TagRepository : ITagRepository
    {
        private readonly HttpClient _httpClient;
        public TagRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TagDTO>> GetListAsync(TagViewRequest request)
        {
            string url = $"/api/Tags/GetListAsync?";

            if (request.MajorId != null) { url += $"&MajorId={request.MajorId}"; }

            var result = await _httpClient.GetFromJsonAsync<List<TagDTO>>(url);
            if (result == null)
            {
                return new();
            }
            return result;
        }

        public async Task<PaginationResponse<TagDTO>> GetListWithPaginationAsync(TagViewWithPaginationRequest request)
        {
            string url = $"/api/Tags/GetListWithPaginationAsync?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

            if(request.MajorId != null) { url += $"&MajorId={request.MajorId}"; }

            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<TagDTO>>(url);
            if (result == null)
            {
                return new();
            }
            return result;
        }

        public async Task<TagDTO> GetByIdAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<TagDTO>($"api/Tags/{id}");
            return result;
        }

        public async Task<bool> AddAsync(TagCreateRequest request)
        {
            var resultCreate = await _httpClient.PostAsJsonAsync($"/api/Tags", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(TagUpdateRequest request)
        {
            var resultCreate = await _httpClient.PutAsJsonAsync($"/api/Tags", request);
            if (resultCreate.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(TagDeleteRequest request)
        {
            string url = $"api/Tags?Id={request.Id}";

            if (request.DeletedBy != null)
            {
                url += $"&DeletedBy={request.DeletedBy}";
            }

            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }
    }
}
