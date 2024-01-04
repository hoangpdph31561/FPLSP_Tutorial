namespace FPLSP_Tutorial.Application.DataTransferObjects.Major.Request;

public class MajorViewRequest
{
    public Guid? UserId { get; set; } = null;
    public bool NotJoined { get; set; }
    public bool ContainPostOnly { get; set; }

    public string? SortingProperty { get; set; } = null;
    public string SortingDirection { get; set; } = "desc";

    //For MajorId,MajorName
    public string? SearchString { get; set; } = null;
}