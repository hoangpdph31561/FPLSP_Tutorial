using FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.PostTag.Request;

public class PostTagSyncRangeRequest
{
    public Guid PostId { get; set; }
    public List<TagDTO> ListTag { get; set; } = new();
}