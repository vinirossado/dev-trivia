using DevTrivia.API.Capabilities.Question.Extensions;
using DevTrivia.API.Capabilities.Question.Models;
using DevTrivia.API.Capabilities.Question.Services.Interfaces;
using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using DevTrivia.API.Capabilities.Question.Enums;

namespace DevTrivia.API.Capabilities.Question.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;
    private readonly ILogger<QuestionController> _logger;

    public QuestionController(IQuestionService questionService, ILogger<QuestionController> logger)
    {
        _questionService = questionService;
        _logger = logger;
    }

    /// <summary>
    /// Create a new question
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<QuestionResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<QuestionResponse>>> Create(
        [FromBody] QuestionRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var createdQuestion = await _questionService.CreateAsync(request, cancellationToken);
            var response = createdQuestion.ToResponse();

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                ApiResponse<QuestionResponse>.SuccessResponse(response, "Question created successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            // Category not found
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            // Title already exists
            return Conflict(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("creating question", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while creating the question"));
        }
    }

    /// <summary>
    /// Update an existing question
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<QuestionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] QuestionRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var updatedQuestion = await _questionService.UpdateAsync(request, id, cancellationToken);
            var response = updatedQuestion.ToResponse();

            return Ok(ApiResponse<QuestionResponse>.SuccessResponse(response, "Question updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("updating question", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while updating the question"));
        }
    }

    /// <summary>
    /// Delete a question by ID
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await _questionService.DeleteAsync(id, cancellationToken);
            return Ok(ApiResponse<bool>.SuccessResponse(deleted, "Question deleted successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("deleting question", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while deleting the question"));
        }
    }

    /// <summary>
    /// Get all questions
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuestionResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var questions = await _questionService.GetAllAsync(cancellationToken);
            var response = questions.ToResponseList();

            return Ok(ApiResponse<IEnumerable<QuestionResponse>>.SuccessResponse(
                response, "Questions retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving questions", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving questions"));
        }
    }

    /// <summary>
    /// Get a question by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<QuestionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        try
        {
            var question = await _questionService.GetByIdAsync(id, cancellationToken);
            var response = question!.ToResponse();

            return Ok(ApiResponse<QuestionResponse>.SuccessResponse(response, "Question retrieved successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving question", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,
                ApiResponse.ErrorResponse("An error occurred while retrieving the question"));
        }
    }
    
    /// <summary>
    /// Get all questions by category ID
    /// </summary>
    /// <param name="categoryId">The category ID to filter by</param>
    /// <remarks>
    /// Returns an empty list if the category exists but has no questions.
    /// Returns 404 if the category doesn't exist.
    /// </remarks>
    [HttpGet("category/{categoryId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuestionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCategory(long categoryId, CancellationToken cancellationToken)
    {
        try
        {
            var questions = await _questionService.GetByCategoryIdAsync(categoryId, cancellationToken);
            var response = questions.ToResponseList();
            
            var message = response.Any() 
                ? $"Found {response.Count()} question(s) in category" 
                : "Category found but has no questions";
            
            return Ok(ApiResponse<IEnumerable<QuestionResponse>>.SuccessResponse(response, message));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving questions by category", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, 
                ApiResponse.ErrorResponse("An error occurred while retrieving questions"));
        }
    }

    /// <summary>
    /// Get questions by category ID and difficulty level
    /// </summary>
    /// <param name="categoryId">The category ID to filter by</param>
    /// <param name="difficulty">The difficulty level (Easy=1, Medium=2, Hard=3, Super=4)</param>
    /// <remarks>
    /// Returns an empty list if the category exists but has no questions with that difficulty.
    /// Returns 404 if the category doesn't exist.
    /// </remarks>
    [HttpGet("category/{categoryId}/difficulty/{difficulty}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<QuestionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCategoryAndDifficulty(
        long categoryId, 
        DifficultyEnum difficulty, 
        CancellationToken cancellationToken)
    {
        try
        {
            var questions = await _questionService.GetByCategoryAndDifficultyAsync(categoryId, difficulty, cancellationToken);
            var response = questions.ToResponseList();
            
            var message = response.Any() 
                ? $"Found {response.Count()} {difficulty} question(s) in category" 
                : $"Category found but has no {difficulty} questions";
            
            return Ok(ApiResponse<IEnumerable<QuestionResponse>>.SuccessResponse(response, message));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("retrieving questions by category and difficulty", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, 
                ApiResponse.ErrorResponse("An error occurred while retrieving questions"));
        }
    }
}
