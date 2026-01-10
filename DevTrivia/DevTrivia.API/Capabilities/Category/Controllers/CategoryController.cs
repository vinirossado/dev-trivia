using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Category.Models;
using DevTrivia.API.Capabilities.Category.Services.Interfaces;
using DevTrivia.API.Capabilities.Trivia.Models;
using DevTrivia.API.Capabilities.User.Models;
using DevTrivia.API.Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace DevTrivia.API.Capabilities.Category.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponse<CategoryResponse>>> Create([FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var category = new Database.Entities.Category
            {
                Name = request.Name.ToLower(),
                Description = request.Description
            };
            var categoryDb = await _categoryService.CreateAsync(category, cancellationToken);
            return CreatedAtAction(nameof(Create), new ApiResponse<CategoryResponse>(true, new CategoryResponse
            {
                Id = categoryDb.Id,
                Name = categoryDb.Name,
                Description = categoryDb.Description
            }, "Category created successfully"));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("Update Category", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>(false, null, ex.Message));
        }
    }
    [HttpPut("{id}")]
    //[Authorize]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(long id,[FromBody] CategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var categoryUpdate = await _categoryService.UpdateAsync(request, id, cancellationToken);
            return Ok(new ApiResponse<CategoryResponse>(true, new CategoryResponse
            {
                Id = categoryUpdate.Id,
                Name = categoryUpdate.Name,
                Description = categoryUpdate.Description
            }, "Category updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("Update Category", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError,new ApiResponse<object>(false, null, ex.Message));
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
            var categoryDelete = await _categoryService.DeleteAsync(id, cancellationToken);
            return Ok(new ApiResponse<bool>(true, categoryDelete, "Category deleted successfully"));
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
    public async Task<IActionResult> Searches( CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _categoryService.GetAll(cancellationToken);
            return Ok(new ApiResponse<IEnumerable<Database.Entities.Category>>(true, categories, "Categories retrieved successfully"));
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
        try{
            var category = await _categoryService.GetByIdAsync(id, cancellationToken);
            return Ok(new ApiResponse<Database.Entities.Category>(true, category, "Category retrieved successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse<object>(false, null, ex.Message));
        }
        catch(Exception ex)
        {
            _logger.DatabaseError("Searche Category", ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>(false, null, ex.Message));
        }
    }
}
