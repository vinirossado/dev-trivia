namespace DevTrivia.API.Capabilities.Shared.Models;

public record ApiResponse<T>(bool Success, T? Data, string? Message = null, Dictionary<string, string[]>? Errors = null)
{
    public static ApiResponse<T> SuccessResponse(T data, string? message = null) =>
        new(true, data, message);

    public static ApiResponse<T> ErrorResponse(string message, Dictionary<string, string[]>? errors = null) =>
        new(false, default, message, errors);
}

public record ApiResponse(bool Success, string? Message = null, Dictionary<string, string[]>? Errors = null)
{
    public static ApiResponse SuccessResponse(string? message = null) =>
        new(true, message);

    public static ApiResponse ErrorResponse(string message, Dictionary<string, string[]>? errors = null) =>
        new(false, message, errors);
}