using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;
using FPLSP_Tutorial.WASM.Repo.Interfaces;
using System.Net.Http.Json;

namespace FPLSP_Tutorial.WASM.Repo.Inplements
{
    public class PostRepo : IPostRepo
    {
        private readonly HttpClient _httpClient;
        public PostRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginationResponse<PostDto>> GetListWithPaginationAsync(ViewPostWithPaginationRequest request)
        {
            string url = $"/api/Posts/GetListWithPaginationAsync?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            if (request.PostId != null)
            {
                url += $"?PostId={request.PostId}";
            }
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<PostDto>>(url);
            if (result == null)
            {
                return new();
            }
            return result;
        }

        public async Task<PostDto> GetPostById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<PostDto>($"api/Posts/{id}");
            return result;
        }

        //public async Task<bool> AddPostAsync(PostCreateRequest request)
        //{
        //    var resultCreate = await _httpClient.PostAsJsonAsync($"/api/Posts", request);
        //    if (resultCreate.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<bool> DeletePostAsync(PostDeleteRequest request)
        //{
        //    string url = $"api/Posts?Id={request.Id}";
        //    if (request.DeletedBy != null)
        //    {
        //        url += $"&DeletedBy={request.DeletedBy}";
        //    }
        //    var result = await _httpClient.DeleteAsync(url);
        //    return result.IsSuccessStatusCode;
        //}

        

        

        //public async Task<bool> UpdatePostAsync(PostUpdateRequest request)
        //{
        //    var resultCreate = await _httpClient.PutAsJsonAsync($"/api/Posts", request);
        //    if (resultCreate.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
