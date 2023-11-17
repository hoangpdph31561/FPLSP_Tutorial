using ClientPost.Data.DataTransferObject;
using ClientPost.Data.DataTransferObject.Request;
using ClientPost.Data.ValueObject.Pagination;

namespace ClientPost.Service.Interfaces
{
    public interface IClientPostService
    {
        Task<PaginationResponse<MajorBaseDTO>> GetMajorList(ClientPostMajorRequestWithPagination request);
        Task<PaginationResponse<PostMainDTO>> GetPostsByMajorId(ClientPostGetByMajorIdWithPaginationRequest request);
        Task<PostDetailDTO?> GetPostDetailById(string id);
        Task<PaginationResponse<TagBaseDTO>> GetTagsByPostId(ClientPostGetTagsByPostIdWithPaginationRequest request);
        Task<PostBaseDTO?> GetParentPostById(string id);
        Task<PaginationResponse<PostBaseDTO>?> GetChildByPostId(ClientPostGetChildWithPaginationRequest request);
        Task<MajorBaseDTO> GetMajorsById(string id);
        Task<List<TagBaseDTO>> GetAllListTags();
    }
}
