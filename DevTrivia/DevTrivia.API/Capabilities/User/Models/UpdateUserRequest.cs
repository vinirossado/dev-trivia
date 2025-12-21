using System.ComponentModel.DataAnnotations;

namespace DevTrivia.API.Capabilities.User.Models;

public sealed record UpdateUserRequest
{
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string? Name { get; init; }

    [MaxLength(500, ErrorMessage = "Bio cannot exceed 500 characters")]
    public string? Bio { get; init; }

    [MaxLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
    public string? Location { get; init; }

    public DateTime? DateOfBirth { get; init; }

    [Url(ErrorMessage = "Invalid URL format")]
    public string? ProfileImageUrl { get; init; }

    [MaxLength(10, ErrorMessage = "Preferred language cannot exceed 10 characters")]
    public string? PreferredLanguage { get; init; }
}
