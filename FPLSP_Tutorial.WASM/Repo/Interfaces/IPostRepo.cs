using FPLSP_Tutorial.WASM.Data.DataTransferObjects.MajorRequest.Request;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post;
using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Repo.Interfaces
{
    public interface IPostRepo
    {
        Task<PaginationResponse<PostDto>> GetListWithPaginationAsync(ViewPostWithPaginationRequest request);
        //Task<bool> AddMajorAsync(MajorCreateRequest request);
        //Task<bool> UpdateMajorAsync(MajorUpdateRequest request);
        //Task<bool> DeleteMajorAsync(MajorDeleteRequest request);
        //Task<MajorDTO> GetMajorById(Guid id);
    }
}
