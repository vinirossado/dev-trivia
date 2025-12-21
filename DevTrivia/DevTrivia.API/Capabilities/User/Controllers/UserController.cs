using DevTrivia.API.Capabilities.Trivia.Models;
using DevTrivia.API.Capabilities.User.Models;
using DevTrivia.API.Capabilities.User.Services.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevTrivia.API.Capabilities.User.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Login with email and password
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            _logger.ValidationFailed("Login", string.Join(", ", errors));
            return BadRequest(new ApiResponse<object>(false, null, "Validation failed", errors));
        }

        try
        {
            var response = await _userService.LoginAsync(request, cancellationToken);
            return Ok(new ApiResponse<LoginResponse>(true, response, "Login successful"));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("login", ex.Message, ex);
            return StatusCode(500, new ApiResponse<object>(false, null, "An error occurred during login"));
        }
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            _logger.ValidationFailed("Register", string.Join(", ", errors));
            return BadRequest(new ApiResponse<object>(false, null, "Validation failed", errors));
        }

        try
        {
            var user = await _userService.RegisterAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, 
                new ApiResponse<UserDto>(true, user, "User registered successfully"));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("register", ex.Message, ex);
            return StatusCode(500, new ApiResponse<object>(false, null, "An error occurred during registration"));
        }
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id, cancellationToken);
            
            if (user is null)
            {
                return NotFound(new ApiResponse<object>(false, null, $"User with ID {id} not found"));
            }

            return Ok(new ApiResponse<UserDto>(true, user, "User retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("get user by id", ex.Message, ex);
            return StatusCode(500, new ApiResponse<object>(false, null, "An error occurred while retrieving user"));
        }
    }

    /// <summary>
    /// Get all users with pagination
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            var (users, totalCount) = await _userService.GetAllAsync(page, pageSize, cancellationToken);

            var result = new
            {
                Users = users,
                Pagination = new
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                }
            };

            return Ok(new ApiResponse<object>(true, result, "Users retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("get all users", ex.Message, ex);
            return StatusCode(500, new ApiResponse<object>(false, null, "An error occurred while retrieving users"));
        }
    }

    /// <summary>
    /// Update user profile
    /// </summary>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        // Check if user is updating their own profile or is admin
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim != id.ToString())
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            _logger.ValidationFailed("Update User", string.Join(", ", errors));
            return BadRequest(new ApiResponse<object>(false, null, "Validation failed", errors));
        }

        try
        {
            var user = await _userService.UpdateAsync(id, request, cancellationToken);
            return Ok(new ApiResponse<UserDto>(true, user, "User updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("update user", ex.Message, ex);
            return StatusCode(500, new ApiResponse<object>(false, null, "An error occurred while updating user"));
        }
    }

    /// <summary>
    /// Delete user
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        // Check if user is deleting their own account or is admin
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim != id.ToString())
        {
            return Forbid();
        }

        try
        {
            var deleted = await _userService.DeleteAsync(id, cancellationToken);
            
            if (!deleted)
            {
                return NotFound(new ApiResponse<object>(false, null, $"User with ID {id} not found"));
            }

            return Ok(new ApiResponse<object>(true, null, "User deleted successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("delete user", ex.Message, ex);
            return StatusCode(500, new ApiResponse<object>(false, null, "An error occurred while deleting user"));
        }
    }

    /// <summary>
    /// Change user password
    /// </summary>
    [HttpPost("{id}/change-password")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ChangePassword(long id, [FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        // Check if user is changing their own password
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim != id.ToString())
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            _logger.ValidationFailed("Change Password", string.Join(", ", errors));
            return BadRequest(new ApiResponse<object>(false, null, "Validation failed", errors));
        }

        try
        {
            await _userService.ChangePasswordAsync(id, request, cancellationToken);
            return Ok(new ApiResponse<object>(true, null, "Password changed successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("change password", ex.Message, ex);
            return StatusCode(500, new ApiResponse<object>(false, null, "An error occurred while changing password"));
        }
    }

    /// <summary>
    /// Get current authenticated user profile
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new ApiResponse<object>(false, null, "Invalid token"));
            }

            var user = await _userService.GetByIdAsync(userId, cancellationToken);
            
            if (user is null)
            {
                return NotFound(new ApiResponse<object>(false, null, "User not found"));
            }

            return Ok(new ApiResponse<UserDto>(true, user, "User profile retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("get current user", ex.Message, ex);
            return StatusCode(500, new ApiResponse<object>(false, null, "An error occurred while retrieving user profile"));
        }
    }
}
