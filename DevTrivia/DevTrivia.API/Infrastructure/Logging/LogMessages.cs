using Microsoft.Extensions.Logging;

namespace DevTrivia.API.Infrastructure.Logging;

public static partial class LogMessages
{
    // User Authentication Logs
    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Information,
        Message = "User login attempt for email: {Email}")]
    public static partial void UserLoginAttempt(this ILogger logger, string email);

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Information,
        Message = "User login successful for email: {Email}, UserId: {UserId}")]
    public static partial void UserLoginSuccessful(this ILogger logger, string email, long userId);

    [LoggerMessage(
        EventId = 1003,
        Level = LogLevel.Warning,
        Message = "User login failed for email: {Email} - Invalid credentials")]
    public static partial void UserLoginFailed(this ILogger logger, string email);

    [LoggerMessage(
        EventId = 1004,
        Level = LogLevel.Information,
        Message = "User registered successfully with email: {Email}, UserId: {UserId}")]
    public static partial void UserRegistered(this ILogger logger, string email, long userId);

    [LoggerMessage(
        EventId = 1005,
        Level = LogLevel.Warning,
        Message = "User registration failed for email: {Email} - Email already exists")]
    public static partial void UserRegistrationEmailExists(this ILogger logger, string email);

    // User CRUD Logs
    [LoggerMessage(
        EventId = 2001,
        Level = LogLevel.Information,
        Message = "Fetching user by ID: {UserId}")]
    public static partial void FetchingUserById(this ILogger logger, long userId);

    [LoggerMessage(
        EventId = 2002,
        Level = LogLevel.Warning,
        Message = "User not found with ID: {UserId}")]
    public static partial void UserNotFound(this ILogger logger, long userId);

    [LoggerMessage(
        EventId = 2003,
        Level = LogLevel.Information,
        Message = "Updating user with ID: {UserId}")]
    public static partial void UpdatingUser(this ILogger logger, long userId);

    [LoggerMessage(
        EventId = 2004,
        Level = LogLevel.Information,
        Message = "User updated successfully with ID: {UserId}")]
    public static partial void UserUpdated(this ILogger logger, long userId);

    [LoggerMessage(
        EventId = 2005,
        Level = LogLevel.Information,
        Message = "Deleting user with ID: {UserId}")]
    public static partial void DeletingUser(this ILogger logger, long userId);

    [LoggerMessage(
        EventId = 2006,
        Level = LogLevel.Information,
        Message = "User deleted successfully with ID: {UserId}")]
    public static partial void UserDeleted(this ILogger logger, long userId);

    [LoggerMessage(
        EventId = 2007,
        Level = LogLevel.Information,
        Message = "Fetching all users with pagination - Page: {Page}, PageSize: {PageSize}")]
    public static partial void FetchingAllUsers(this ILogger logger, int page, int pageSize);

    // JWT Token Logs
    [LoggerMessage(
        EventId = 3001,
        Level = LogLevel.Information,
        Message = "Generating JWT token for UserId: {UserId}")]
    public static partial void GeneratingJwtToken(this ILogger logger, long userId);

    [LoggerMessage(
        EventId = 3002,
        Level = LogLevel.Error,
        Message = "Failed to generate JWT token for UserId: {UserId} - {ErrorMessage}")]
    public static partial void JwtTokenGenerationFailed(this ILogger logger, long userId, string errorMessage, Exception ex);

    // Database Logs
    [LoggerMessage(
        EventId = 4001,
        Level = LogLevel.Error,
        Message = "Database error occurred while {Operation} - {ErrorMessage}")]
    public static partial void DatabaseError(this ILogger logger, string operation, string errorMessage, Exception ex);

    [LoggerMessage(
        EventId = 4002,
        Level = LogLevel.Information,
        Message = "Database operation completed successfully: {Operation}")]
    public static partial void DatabaseOperationSuccess(this ILogger logger, string operation);

    // Validation Logs
    [LoggerMessage(
        EventId = 5001,
        Level = LogLevel.Warning,
        Message = "Validation failed for {Operation} - {ValidationErrors}")]
    public static partial void ValidationFailed(this ILogger logger, string operation, string validationErrors);

    [LoggerMessage(
        EventId = 5002,
        Level = LogLevel.Warning,
        Message = "Invalid password format for email: {Email}")]
    public static partial void InvalidPasswordFormat(this ILogger logger, string email);
}
