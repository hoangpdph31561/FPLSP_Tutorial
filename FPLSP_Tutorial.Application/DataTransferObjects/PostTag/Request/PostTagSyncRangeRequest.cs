using FPLSP_Tutorial.Application.DataTransferObjects.Tag;

namespace FPLSP_Tutorial.Application.DataTransferObjects.PostTag.Request;

public class PostTagSyncRangeRequest
{
    public Guid PostId { get; set; }
    public List<TagDTO> ListTag { get; set; } = new();
}