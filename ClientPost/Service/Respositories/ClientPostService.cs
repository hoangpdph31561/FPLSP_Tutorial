using ClientPost.Data.DataTransferObject;
using ClientPost.Data.DataTransferObject.Request;
using ClientPost.Data.ValueObject.Pagination;
using ClientPost.Data.ValueObject.Response;
using ClientPost.Service.Interfaces;

namespace ClientPost.Service.Respositories
{
    public class ClientPostService : IClientPostService
    {
        private readonly HttpClient _httpClient;
        public ClientPostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MajorBaseDTO>> GetAllMajorsList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<MajorBaseDTO>>("/api/ClientPosts/getAllMajorsList");
            return result;
        }

        public async Task<PaginationResponse<PostBaseDTO>?> GetChildByPostId(ClientPostGetChildWithPaginationRequest request)
        {
            string url = $"/api/ClientPosts/getChildPost?Id={request.Id}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<PostBaseDTO>>(url);
            return result;
        }

        public async Task<PaginationResponse<MajorBaseDTO>> GetMajorList(ClientPostMajorRequestWithPagination request)
        {
            string url = $"/api/ClientPosts/getMajor?PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<MajorBaseDTO>>(url);
            return result;
        }

        public async Task<MajorBaseDTO> GetMajorsById(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<MajorBaseDTO>($"/api/ClientPosts/getMajor/{id}");
            return result!;
        }

        public async Task<PostBaseDTO?> GetParentPostById(string id)
        {
            var url = $"/api/ClientPosts/getParentPost/{id}";
            var result = await _httpClient.GetFromJsonAsync<PostBaseDTO>(url);
            return result;
        }

        public async Task<PostDetailDTO?> GetPostDetailById(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<PostDetailDTO>($"/api/ClientPosts/getPostDetail/{id}");
            return result;
        }

        public async Task<PaginationResponse<PostMainDTO>> GetPostsByMajorId(ClientPostGetByMajorIdWithPaginationRequest request)
        {
            string url = $"api/ClientPosts/getPostByMajorId?MajorId={request.MajorId}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<PostMainDTO>>(url);
            return result;
        }

        public async Task<PaginationResponse<TagBaseDTO>> GetTagsByPostId(ClientPostGetTagsByPostIdWithPaginationRequest request)
        {
            string url = $"/api/ClientPosts/getPostTags?Id={request.Id}&PageNumber={request.PageNumber}&PageSize={request.PageSize}";
            var result = await _httpClient.GetFromJsonAsync<PaginationResponse<TagBaseDTO>>(url);
            return result!;

        }
    }
}
