using System.Net;
using DevTrivia.API.Capabilities.AnswerOptions.Extensions;
using DevTrivia.API.Capabilities.AnswerOptions.Models;
using DevTrivia.API.Capabilities.AnswerOptions.Services.Interfaces;
using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevTrivia.API.Capabilities.AnswerOptions.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswerOptionController : ControllerBase
{
    private readonly IAnswerOptionService _answerOptionService;
    private readonly ILogger<AnswerOptionController> _logger;

    public AnswerOptionController(IAnswerOptionService answerOptionService, ILogger<AnswerOptionController> logger)
    {
        _answerOptionService = answerOptionService;
        _logger = logger;
    }

    /// <summary>
    /// Get all answer options
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AnswerOptionResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var options = await _answerOptionService.GetAllAsync(cancellationToken);
            return Ok(ApiResponse<IEnumerable<AnswerOptionResponse>>.SuccessResponse(
                options.ToResponseList(), "Answer options retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving answer options", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving answer options"));
        }
    }

    /// <summary>
    /// Get an answer option by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AnswerOptionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        try
        {
            var option = await _answerOptionService.GetByIdAsync(id, cancellationToken);
            return Ok(ApiResponse<AnswerOptionResponse>.SuccessResponse(
                option!.ToResponse(), "Answer option retrieved successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving answer option", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving the answer option"));
        }
    }

    /// <summary>
    /// Get all answer options for a question
    /// </summary>
    [HttpGet("question/{questionId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AnswerOptionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByQuestionId(long questionId, CancellationToken cancellationToken)
    {
        try
        {
            var options = await _answerOptionService.GetAnswerOptionsByQuestionId(questionId, cancellationToken);
            var response = options.ToResponseList();

            var message = response.Any()
                ? $"Found {response.Count()} answer option(s) for question"
                : "Question found but has no answer options";

            return Ok(ApiResponse<IEnumerable<AnswerOptionResponse>>.SuccessResponse(response, message));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving answer options by question", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving answer options"));
        }
    }

    /// <summary>
    /// Create a new answer option
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<AnswerOptionResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<AnswerOptionResponse>>> Create(
        [FromBody] AnswerOptionRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var created = await _answerOptionService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                ApiResponse<AnswerOptionResponse>.SuccessResponse(created.ToResponse(), "Answer option created successfully"));
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
            _logger.DatabaseError("creating answer option", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while creating the answer option"));
        }
    }

    /// <summary>
    /// Update an answer option
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<AnswerOptionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(long id, [FromBody] AnswerOptionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _answerOptionService.UpdateAsync(request, id, cancellationToken);
            return Ok(ApiResponse<AnswerOptionResponse>.SuccessResponse(
                updated.ToResponse(), "Answer option updated successfully"));
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
            _logger.DatabaseError("updating answer option", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while updating the answer option"));
        }
    }

    /// <summary>
    /// Delete an answer option
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await _answerOptionService.DeleteAsync(id, cancellationToken);
            return Ok(ApiResponse<bool>.SuccessResponse(deleted, "Answer option deleted successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("deleting answer option", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while deleting the answer option"));
        }
    }
}
