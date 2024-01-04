namespace FPLSP_Tutorial.Application.ValueObjects.Common;

public class Tracker
{
    public Tracker()
    {
    }

    public Tracker(string requestId)
    {
        RequestId = requestId;
    }

    public string RequestId { get; set; } = null!;
}