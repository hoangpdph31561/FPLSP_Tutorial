using FPLSP_Tutorial.Application.ValueObjects.Pagination;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest;

public class TagViewWithPaginationRequest : PaginationRequest
{
    public string? Name { get; set; } = null;
    public Guid? MajorId { get; set; } = null;
    public bool IgnoreMajorId { get; set; } = false;
}