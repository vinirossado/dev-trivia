using System.ComponentModel.DataAnnotations;

namespace DevTrivia.API.Capabilities.User.Models;

public sealed record ChangePasswordRequest
{
    [Required(ErrorMessage = "Current password is required")]
    public required string CurrentPassword { get; init; }

    [Required(ErrorMessage = "New password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
    public required string NewPassword { get; init; }
}