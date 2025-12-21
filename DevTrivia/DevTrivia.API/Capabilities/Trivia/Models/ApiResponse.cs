namespace DevTrivia.API.Capabilities.Trivia.Models;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    string? Message,
    IEnumerable<string>? Errors = null
);