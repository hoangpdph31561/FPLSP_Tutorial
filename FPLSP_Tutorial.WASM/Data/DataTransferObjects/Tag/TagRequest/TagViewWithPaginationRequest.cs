using FPLSP_Tutorial.WASM.Data.Pagination;

namespace FPLSP_Tutorial.WASM.Data.DataTransferObjects.Tag.TagRequest;

public class TagViewWithPaginationRequest : PaginationRequest
{
    public string? Name { get; set; } = null;
    public Guid? MajorId { get; set; } = null;
    public bool IgnoreMajorId { get; set; } = false;
}