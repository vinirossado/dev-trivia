using DevTrivia.API.Capabilities.Match.Extensions;
using DevTrivia.API.Capabilities.Match.Models;
using DevTrivia.API.Capabilities.Match.Services.Interfaces;
using DevTrivia.API.Capabilities.Question.Models;
using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevTrivia.API.Capabilities.Match.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;
    private readonly ILogger<MatchController> _logger;

    public MatchController(IMatchService matchService, ILogger<MatchController> logger)
    {
        _matchService = matchService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new match
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<MatchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<MatchResponse>>> Create(
        [FromBody] MatchRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var match = request.ToEntity();

            var createdMatch = await _matchService.CreateAsync(match, cancellationToken);
            var response = createdMatch.ToResponse();

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                ApiResponse<MatchResponse>.SuccessResponse(response, "Match created successfully"));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("creating match", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while creating the match"));
        }
    }

    /// <summary>
    /// Update an existing match
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<MatchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] MatchRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var updatedMatch = await _matchService.UpdateAsync(request, id, cancellationToken);
            var response = updatedMatch.ToResponse();

            return Ok(ApiResponse<MatchResponse>.SuccessResponse(response, "Match updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("updating match", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while updating the match"));
        }
    }

    /// <summary>
    /// Delete a match by ID
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await _matchService.DeleteAsync(id, cancellationToken);
            return Ok(ApiResponse<bool>.SuccessResponse(deleted, "Match deleted successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("deleting match", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while deleting the match"));
        }
    }

    /// <summary>
    /// Get all match
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<MatchResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var matchs = await _matchService.GetAllAsync(cancellationToken);
            var response = matchs.ToResponseList();

            return Ok(ApiResponse<IEnumerable<MatchResponse>>.SuccessResponse(
                response, "Matches retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving matches", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving matches"));
        }
    }

    /// <summary>
    /// Get a match by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<MatchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        try
        {
            var match = await _matchService.GetByIdAsync(id, cancellationToken);
            var response = match!.ToResponse();

            return Ok(ApiResponse<MatchResponse>.SuccessResponse(response, "Match retrieved successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving match", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving the match"));
        }
    }
}