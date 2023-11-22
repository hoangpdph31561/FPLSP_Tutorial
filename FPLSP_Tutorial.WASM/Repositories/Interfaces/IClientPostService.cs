using FPLSP_Tutorial.WASM.Data.Client;
using FPLSP_Tutorial.WASM.Data.Client.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces
{
    public interface IClientPostService
    {
        Task<PaginationResponse<MajorBaseDTO>> GetMajorList(ClientPostMajorRequestWithPagination request);
        Task<PaginationResponse<PostMainDTO>> GetPostsByMajorId(ClientPostGetByMajorIdWithPaginationRequest request);
        Task<PostDetailDTO?> GetPostDetailById(string id);
        Task<PaginationResponse<TagBaseDTO>> GetTagsByPostId(ClientPostGetTagsByPostIdWithPaginationRequest request);
        Task<PostMainDTO?> GetParentPostById(string id);
        Task<PaginationResponse<PostMainDTO>?> GetChildByPostId(ClientPostGetChildWithPaginationRequest request);
        Task<MajorBaseDTO> GetMajorsById(string id);
        Task<List<TagBaseDTO>> GetAllListTags();
    }
}
