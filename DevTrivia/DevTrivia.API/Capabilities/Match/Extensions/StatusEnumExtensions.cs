using DevTrivia.API.Capabilities.Match.Enums;

namespace DevTrivia.API.Capabilities.Match.Extensions;

public static class StatusEnumExtensions
{
    public static bool IsValid(this StatusEnum status)
    {
        return Enum.IsDefined(typeof(StatusEnum), status);
    }
}
