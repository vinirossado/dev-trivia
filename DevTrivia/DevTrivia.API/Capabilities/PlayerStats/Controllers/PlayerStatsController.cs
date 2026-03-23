using DevTrivia.API.Capabilities.Match.Extensions;
using DevTrivia.API.Capabilities.PlayerStats.Extensions;
using DevTrivia.API.Capabilities.PlayerStats.Models;
using DevTrivia.API.Capabilities.PlayerStats.Services.Interfaces;
using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Capabilities.User.Enums;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevTrivia.API.Capabilities.PlayerStats.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerStatsController : ControllerBase
{
    private readonly IPlayerStatsService _playerStatsService;
    private readonly ILogger<PlayerStatsController> _logger;

    public PlayerStatsController(IPlayerStatsService playerStatsService, ILogger<PlayerStatsController> logger)
    {
        _playerStatsService = playerStatsService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new playerstats
    /// </summary>
    //[Authorize(Roles = Roles.Admin)]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PlayerStatsResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<PlayerStatsResponse>>> Create(
        [FromBody] PlayerStatsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var createdPlayerStats = await _playerStatsService.CreateAsync(request, cancellationToken);
            var response = createdPlayerStats.ToResponse();

            return Ok(ApiResponse<PlayerStatsResponse>.SuccessResponse(response, "Player stats created successfully"));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("creating player stats", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while creating the player stats"));
        }
    }

    /// <summary>
    /// Update an existing playerstats
    /// </summary>
    [Authorize(Roles = Roles.Admin)]
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<PlayerStatsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromBody] PlayerStatsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _playerStatsService.UpdateAsync(request, cancellationToken);
            var response = updated.ToResponse();

            return Ok(ApiResponse<PlayerStatsResponse>.SuccessResponse(response, "Player stats updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("updating player stats", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while updating the player stats"));
        }
    }

    /// <summary>
    /// Delete a playerstats by UserID
    /// </summary>
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await _playerStatsService.DeleteAsync(id, cancellationToken);

            return Ok(ApiResponse<bool>.SuccessResponse(deleted, "Player stats deleted successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("deleting player stats", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while deleting the player stats"));
        }
    }

    /// <summary>
    /// Get all playerstats
    /// </summary>
    [Authorize (Roles = Roles.Admin)]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PlayerStatsResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var playerStats = await _playerStatsService.GetAllAsync(cancellationToken);
            var response = playerStats.ToResponseList();

            return Ok(ApiResponse<IEnumerable<PlayerStatsResponse>>.SuccessResponse(
                response, "Player stats retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving player stats", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving player stats"));
        }
    }

    /// <summary>
    /// Get a playerstats by UserID
    /// </summary>
    [Authorize]
    [HttpGet("user/{id}")]
    [ProducesResponseType(typeof(ApiResponse<PlayerStatsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var playerStats = await _playerStatsService.GetByIdAsync(id, cancellationToken);
            var response = playerStats.ToResponse();

            return Ok(ApiResponse<PlayerStatsResponse>.SuccessResponse(response, "Player stats retrieved successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving player stats", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving the player stats"));
        }
    }
}