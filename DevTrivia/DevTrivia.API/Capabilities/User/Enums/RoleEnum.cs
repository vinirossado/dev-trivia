namespace DevTrivia.API.Capabilities.User.Enums;

public enum RoleEnum
{
    Admin = 1,
    Player = 2
}

public static class Roles
{
    public const string Admin = nameof(RoleEnum.Admin);
    public const string Player = nameof(RoleEnum.Player);
}