using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Models;
using DevTrivia.API.Capabilities.Question.Services.Interfaces;
using DevTrivia.API.Capabilities.Trivia.Models;
using DevTrivia.API.Capabilities.User.Models;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

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
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<QuestionResponse>>> Create([FromBody] QuestionRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var question = new Database.Entities.Question
            {
                Title = request.Title.ToLower(),
                Description = request.Description,
                Difficulty = request.Difficulty
            };
            var questionDb = await _questionService.CreateAsync(question, cancellationToken);
            return CreatedAtAction(nameof(Create), new ApiResponse<QuestionResponse>(true, new QuestionResponse
            {
                Id = questionDb.Id,
                Title = questionDb.Title,
                Description = questionDb.Description,
                Difficulty = questionDb.Difficulty,
                //CategoryId = questionDb.CategoryId
            }, "Category created successfully"));
    }
        catch (InvalidOperationException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("Update Category", ex.Message, ex);
            return StatusCode((int) HttpStatusCode.InternalServerError, new ApiResponse<object>(false, null, ex.Message));
        }

    }

    [HttpPut("{id}")]
    //[Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(long id, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var questionUpdate = await _questionService.UpdateAsync(request, id, cancellationToken);
            return Ok(new ApiResponse<QuestionResponse>(true, new QuestionResponse
            {
                Id = questionUpdate.Id,
                Title = questionUpdate.Title,
                Description = questionUpdate.Description
            }, "Category updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("Update Category", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>(false, null, ex.Message));
        }
    }
    [HttpDelete]
    //[Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        try
        {
            var questionDelete = await _questionService.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResponse<bool>(true, questionDelete, "Category deleted successfully"));
            //return Ok("Category retrieved successfully");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("Delete Category", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>(false, null, ex.Message));
        }
    }
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Searches(CancellationToken cancellationToken)
    {
        try
        {
            var questions = await _questionService.GetAll(cancellationToken);
            return Ok(new ApiResponse<IEnumerable<Database.Entities.Question>>(true, questions, "Categories retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("Searche Category", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>(false, null, ex.Message));
        }
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SearchesId(long id, CancellationToken cancellationToken)
    {
        try
        {
            var question = await _questionService.GetByIdAsync(id, cancellationToken);
            return Ok(new ApiResponse<Database.Entities.Question>(true, question, "Category retrieved successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("Searche Category", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>(false, null, ex.Message));
        }
    }
}
