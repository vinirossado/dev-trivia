using DevTrivia.API.Capabilities.Match.Extensions;
using DevTrivia.API.Capabilities.PlayerStats.Extensions;
using DevTrivia.API.Capabilities.PlayerStats.Models;
using DevTrivia.API.Capabilities.PlayerStats.Services.Interfaces;
using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevTrivia.API.Capabilities.PlayerStats.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerStatsController : ControllerBase
{
    private readonly IPlayerStatsService _playerstatsService;
    private readonly ILogger<PlayerStatsController> _logger;

    public PlayerStatsController(IPlayerStatsService playerstatsService, ILogger<PlayerStatsController> logger)
    {
        _playerstatsService = playerstatsService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new playerstats
    /// </summary>
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
            var playerstats = request.ToEntity();

            var createdPlayerstats = await _playerstatsService.CreateAsync(playerstats, cancellationToken);
            var response = createdPlayerstats.ToResponse();

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
            var updated = await _playerstatsService.UpdateAsync(request, request.UserId, cancellationToken);
            var response = updated.ToResponse();

            return Ok(ApiResponse<PlayerStatsResponse>.SuccessResponse(response, "Player stats updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
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
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await _playerstatsService.DeleteAsync(id, cancellationToken);
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
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<PlayerStatsResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var playerstats = await _playerstatsService.GetAllAsync(cancellationToken);
            var response = playerstats.ToResponseList();

            return Ok(ApiResponse<IEnumerable<PlayerStatsResponse>>.SuccessResponse(
                response, "Player stats retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving player stats", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving palyer stats"));
        }
    }

    /// <summary>
    /// Get a playerstats by UserID
    /// </summary>
    [HttpGet("user/{id}")]
    [ProducesResponseType(typeof(ApiResponse<PlayerStatsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var playerstats = await _playerstatsService.GetByIdAsync(id, cancellationToken);
            var response = playerstats!.ToResponse();

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