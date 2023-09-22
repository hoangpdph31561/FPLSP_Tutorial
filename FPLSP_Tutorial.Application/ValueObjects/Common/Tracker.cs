namespace FPLSP_Tutorial.Application.ValueObjects.Common
{
    public class Tracker
    {
        public string RequestId { get; set; } = null!;

        public Tracker()
        {
        }

        public Tracker(string requestId)
        {
            RequestId = requestId;
        }
    }
}
