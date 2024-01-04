namespace FPLSP_Tutorial.Infrastructure.Extensions;

public static class StringHandleExtensions
{
    public static string GetStringWithEllipsis(string input, int maxLength)
    {
        if (input.Length <= maxLength)
            return input;
        return input.Substring(0, maxLength) + "...";
    }
}