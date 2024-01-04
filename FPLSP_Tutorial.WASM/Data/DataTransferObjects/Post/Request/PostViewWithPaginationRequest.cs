using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Post.Request;

public class PostViewWithPaginationRequest : PaginationRequest
{
    public Guid? UserId { get; set; } = null;
    public Guid? PostId { get; set; } = null;
    public Guid? MajorId { get; set; } = null;
    public int PostType { get; set; } = 0;
    public bool IsGetTopLevel { get; set; }

    public string? SortingProperty { get; set; } = null;
    public string SortingDirection { get; set; } = "desc";

    public string? SearchString { get; set; } = null;

    public List<Guid> ListTagId { get; set; } = new();
}