using FPLSP_Tutorial.WASM.Data.DataTransferObjects.PostTag.Request;

namespace FPLSP_Tutorial.WASM.Repositories.Interfaces;

public interface IPostTagRepository
{
    Task<bool> AddAsync(PostTagCreateRequest request);
    Task<bool> AddRangeAsync(List<PostTagCreateRequest> request);
    Task<bool> SyncRangeAsync(PostTagSyncRangeRequest request);
}