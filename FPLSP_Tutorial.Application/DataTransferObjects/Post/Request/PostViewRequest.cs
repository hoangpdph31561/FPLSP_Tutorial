namespace FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;

public class PostViewRequest
{
    public Guid? UserId { get; set; } = null;
    public Guid? PostId { get; set; } = null;

    public Guid? MajorId { get; set; } = null;

    //0: All, 1: Sys, 2: Maj
    public int PostType { get; set; } = 0;
    public bool IsGetTopLevel { get; set; }

    public string? SortingProperty { get; set; } = null;
    public string SortingDirection { get; set; } = "desc";

    public string? SearchString { get; set; } = null;

    public List<Guid> ListTagId { get; set; } = new();
}