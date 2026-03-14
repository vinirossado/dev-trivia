using DevTrivia.API.Capabilities.User.Enums;

namespace DevTrivia.API.Capabilities.User.Models;

public record ChangeRoleRequest
{
    public required RoleEnum Role { get; init; }
}
