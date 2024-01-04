using System.Net.Http.Json;
using System.Text.Json;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FPLSP_Tutorial.WASM.Repositories.Implements;

public class PostRepository : IPostRepository
{
    private readonly HttpClient _httpClient;

    public PostRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PostDTO>> GetListAsync(PostViewRequest request)
    {
        var url = "/api/Posts/GetListAsync?";

        if (request.PostId != null) url += $"&PostId={request.PostId}";
        if (request.MajorId != null) url += $"&MajorId={request.MajorId}";
        if (request.UserId != null) url += $"&UserId={request.UserId}";
        if (request.PostType != null) url += $"&PostType={request.PostType}";
        if (request.IsGetTopLevel) url += $"&IsGetTopLevel={request.IsGetTopLevel}";
        if (!request.SearchString.IsNullOrEmpty()) url += $"&SearchString={request.SearchString}";
        if (request.ListTagId.Count() > 0)
            foreach (var i in request.ListTagId)
                url += $"&ListTagId={i}";

        var result = await _httpClient.GetFromJsonAsync<List<PostDTO>>(url);
        if (result == null) return new List<PostDTO>();
        return result;
    }

    public async Task<PaginationResponse<PostDTO>> GetListWithPaginationAsync(PostViewWithPaginationRequest request)
    {
        var url = $"/api/Posts/GetListWithPaginationAsync?PageNumber={request.PageNumber}&PageSize={request.PageSize}";

        if (request.PostId != null) url += $"&PostId={request.PostId}";
        if (request.MajorId != null) url += $"&MajorId={request.MajorId}";
        if (request.UserId != null) url += $"&UserId={request.UserId}";
        if (request.PostType != null) url += $"&PostType={request.PostType}";
        if (request.IsGetTopLevel) url += $"&IsGetTopLevel={request.IsGetTopLevel}";
        if (!request.SearchString.IsNullOrEmpty()) url += $"&SearchString={request.SearchString}";
        if (request.ListTagId.Count() > 0)
            foreach (var i in request.ListTagId)
                url += $"&ListTagId={i}";


        var result = await _httpClient.GetFromJsonAsync<PaginationResponse<PostDTO>>(url);
        if (result == null) return new PaginationResponse<PostDTO>();
        return result;
    }

    public async Task<PostDTO> GetByIdAsync(Guid id)
    {
        var result = await _httpClient.GetFromJsonAsync<PostDTO>($"api/Posts/{id}");
        return result;
    }

    public async Task<PostDTO?> AddAsync(PostCreateRequest request)
    {
        var resultCreate = await _httpClient.PostAsJsonAsync("/api/Posts", request);
        if (resultCreate.IsSuccessStatusCode)
        {
            var contentAsString = await resultCreate.Content.ReadAsStringAsync();
            var resultDTO = JsonSerializer.Deserialize<PostDTO>(contentAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return resultDTO;
        }

        return null;
    }

    public async Task<bool> UpdateAsync(PostUpdateRequest request)
    {
        var resultCreate = await _httpClient.PutAsJsonAsync("/api/Posts", request);
        if (resultCreate.IsSuccessStatusCode) return true;
        return false;
    }

    public async Task<bool> DeleteAsync(PostDeleteRequest request)
    {
        var result = await _httpClient.DeleteAsync($"/api/Posts?Id={request.Id}&DeletedBy={request.DeletedBy}");
        return result.IsSuccessStatusCode;
    }
}