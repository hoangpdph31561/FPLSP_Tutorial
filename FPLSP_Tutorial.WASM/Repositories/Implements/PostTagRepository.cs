using System.Net.Http.Json;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.PostTag.Request;
using FPLSP_Tutorial.WASM.Repositories.Interfaces;

namespace FPLSP_Tutorial.WASM.Repositories.Implements;

public class PostTagRepository : IPostTagRepository
{
    private readonly HttpClient _httpClient;

    public PostTagRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AddAsync(PostTagCreateRequest request)
    {
        var resultDelete = await _httpClient.PostAsJsonAsync("/api/PostTags", request);
        if (resultDelete.IsSuccessStatusCode) return true;
        return false;
    }

    public async Task<bool> AddRangeAsync(List<PostTagCreateRequest> request)
    {
        var resultDelete = await _httpClient.PostAsJsonAsync("/api/PostTags/AddRangeAsync", request);
        if (resultDelete.IsSuccessStatusCode) return true;
        return false;
    }

    public async Task<bool> SyncRangeAsync(PostTagSyncRangeRequest request)
    {
        var url = "api/PostTags/SyncRangeAsync";

        var result = await _httpClient.PutAsJsonAsync(url, request);


        if (result.IsSuccessStatusCode)
            //var contentAsString = await result.Content.ReadAsStringAsync();
            //var resultDTO = JsonSerializer.Deserialize<Lis>(contentAsString, new JsonSerializerOptions()
            //{
            //    PropertyNameCaseInsensitive = true
            //});
            return true;
        return false;
    }
}